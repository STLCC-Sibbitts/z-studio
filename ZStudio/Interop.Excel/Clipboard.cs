using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;
using AM.Interop.WinApi;
using System.Diagnostics;

namespace AM.Interop.Excel
{
    public static class Clipboard
    {
        private readonly static string CF_LINKSOURCE_ID = "Link Source";

        public static Range GetRange()
        {
            return GetRange(GetIDataObjectFromClipboard());
        }

		public static Range GetRange(out string sheetName)
		{
			return GetRange(GetIDataObjectFromClipboard(), out sheetName);
		}
		private static IDataObject GetIDataObjectFromClipboard()
        {
            if (System.Windows.Forms.Clipboard.ContainsData(CF_LINKSOURCE_ID))
                return System.Windows.Forms.Clipboard.GetDataObject() as IDataObject;
            else
            {
                return null;
            }
        }

        public static Range GetRange(IDataObject dataObject)
        {
			Range range = null;
			if (dataObject == null)
				return range;
            IStream iStream = IStreamFromDataObject(dataObject);
            IMoniker compositeMoniker = IMonikerFromIStream(iStream);
            return RangeFromCompositeMoniker(compositeMoniker);
        }
		public static Range GetRange(IDataObject dataObject, out string sheetName)
		{
			sheetName = "";
			Range range = null;
			if (dataObject == null)
				return range;
			IStream iStream = IStreamFromDataObject(dataObject);
			IMoniker compositeMoniker = IMonikerFromIStream(iStream);
			return RangeFromCompositeMoniker(compositeMoniker, out sheetName);
		}

        private static IStream IStreamFromDataObject(IDataObject dataObject)
        {
            if (dataObject == null)
                throw new ArgumentNullException("dataObject", "dataObject is null.");

            STGMEDIUM medium;
            FORMATETC formatEtc = new FORMATETC();
            formatEtc.cfFormat = (short)System.Windows.Forms.DataFormats.GetFormat(CF_LINKSOURCE_ID).Id;
            formatEtc.dwAspect = DVASPECT.DVASPECT_CONTENT;
            formatEtc.lindex = -1;
            formatEtc.ptd = new IntPtr(0);
            formatEtc.tymed = TYMED.TYMED_ISTREAM;

            dataObject.GetData(ref formatEtc, out medium);
            return Marshal.GetObjectForIUnknown(medium.unionmember) as IStream;
        }

        private static IMoniker IMonikerFromIStream(IStream iStream)
        {
            if (iStream == null)
                throw new ArgumentNullException("iStream", "iStream is null.");

            iStream.Seek(0, 0, IntPtr.Zero);
            Guid guid = Marshal.GenerateGuidForType(typeof(stdole.IUnknown));
            object obj;
            if (ole32.OleLoadFromStream(iStream, ref guid, out obj))
                return obj as IMoniker;
            else
                return null;
        }
		private static Range RangeFromCompositeMoniker(IMoniker compositeMoniker)
		{
			string sheetName = "";
			return RangeFromCompositeMoniker(compositeMoniker, out sheetName);
		}
        private static Range RangeFromCompositeMoniker(IMoniker compositeMoniker, out string sheetName)
        {
			sheetName = "";
            List<IMoniker> monikers = SplitCompositeMoniker(compositeMoniker);
            if (monikers.Count != 2)
                throw new ApplicationException("Invalid moniker");

            IBindCtx bindctx;
            if (!ole32.CreateBindCtx(0, out bindctx) || bindctx == null)
                throw new ApplicationException("Can't create bindctx");

            object obj;
            Guid workbookGuid = Marshal.GenerateGuidForType(typeof(Workbook));
            monikers[0].BindToObject(bindctx, null, ref workbookGuid, out obj);
            Workbook workbook = obj as Workbook;
			
            ExcelItemMonikerHelper helper = new ExcelItemMonikerHelper(monikers[1], bindctx);
            return helper.GetRange(workbook, out sheetName);
        }

        private static List<IMoniker> SplitCompositeMoniker(IMoniker compositeMoniker)
        {
            if (compositeMoniker == null)
                throw new ArgumentNullException("compositeMoniker", "compositeMoniker is null.");
            
            List<IMoniker> monikerList = new List<IMoniker>();
            IEnumMoniker enumMoniker;
            compositeMoniker.Enum(true, out enumMoniker);
            if (enumMoniker != null)
            {
                IMoniker[] monikerArray = new IMoniker[1];
                IntPtr fetched = new IntPtr();
                HRESULT res;
                while (res = enumMoniker.Next(1, monikerArray, fetched))
                {
                    monikerList.Add(monikerArray[0]);
                }
                return monikerList;
            }
            else
                throw new ApplicationException("IMoniker is not composite");
        }

        private class ExcelItemMonikerHelper
        {
            private enum RangeType
            {
                Cells,
                Rows,
                Columns
            }

            private XlReferenceStyle _referenceStyle;
            private RangeType _rangeType;
            private readonly string _displayName;

            private string _sheetName;
            private string _bound1Name;
            private string _bound2Name;

            public ExcelItemMonikerHelper(IMoniker excelItemMoniker, IBindCtx bindCtx)
            {
                if (excelItemMoniker == null)
                    throw new ArgumentNullException("excelItemMoniker", "excelItemMoniker is null.");

                excelItemMoniker.GetDisplayName(bindCtx, null, out _displayName);
            }
			public Range GetRange(Workbook workbook, out string sheetName)
			{
				Range range = GetRange(workbook);
				sheetName = GetSheetName();
				return range;
			}
            public Range GetRange(Workbook workbook)
            {
                Parse();
                Worksheet sheet = GetSheet(workbook);
                _referenceStyle = sheet.Application.ReferenceStyle;
                Range range = null;
                switch (_rangeType)
                {
                    case RangeType.Cells:
                        range = sheet.get_Range(GetBound1Name(), GetBound2Name());
                        break;
                    case RangeType.Rows:
                        range = sheet.get_Range(sheet.Rows[GetBound1Name(), Type.Missing], sheet.Rows[GetBound2Name(), Type.Missing]);
                        break;
                    case RangeType.Columns:
                        range = sheet.get_Range(sheet.Columns[GetBound1Name(), Type.Missing], sheet.Columns[GetBound2Name(), Type.Missing]);
                        break;
                    default:
                        throw new ApplicationException("Illegal RangeType");
                }
                return range;
            }

            private void Parse()
            {
                string[] names = _displayName.Split('!');
                Debug.Assert(names.Length >= 2);
                _sheetName = names[1];

                string[] R1C1Bounds = names[2].Split(':');
                _bound1Name = R1C1Bounds[0];
                _bound2Name = R1C1Bounds.Length == 2 ? R1C1Bounds[1] : _bound1Name;

                if (_bound1Name.Contains("R") && _bound1Name.Contains("C"))
                    _rangeType = RangeType.Cells;
                else if (_bound1Name.Contains("R"))
                    _rangeType = RangeType.Rows;
                else if (_bound1Name.Contains("C"))
                    _rangeType = RangeType.Columns;
            }

            private Worksheet GetSheet(Workbook workbook)
            {
                return (Worksheet)workbook.Sheets[GetSheetName()];
            }

            public string GetSheetName()
            {
                return _sheetName;
            }

            private string GetBound1Name()
            {
                switch (_referenceStyle)
                {
                    case XlReferenceStyle.xlA1:
                        return RangeName.R1C1ToA1(_bound1Name);
                    case XlReferenceStyle.xlR1C1:
                        return _bound1Name;
                    default:
                        throw new ApplicationException("Illegal XlReferenceStyle");
                }
            }

            private object GetBound2Name()
            {
                switch (_referenceStyle)
                {
                    case XlReferenceStyle.xlA1:
                        return RangeName.R1C1ToA1(_bound2Name);
                    case XlReferenceStyle.xlR1C1:
                        return _bound2Name;
                    default:
                        throw new ApplicationException("Illegal XlReferenceStyle");
                }
            }
        }
    }
}
