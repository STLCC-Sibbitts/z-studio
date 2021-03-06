﻿#region Using directives
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using System.Configuration;
//using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
#endregion


namespace ZGUI
{
	public partial class ZExcelViewer : UserControl
	{
		private Excel.Application m_oExcelApp;
		private string m_filename;
		private List<string> m_worksheetNames;
		// Declare the event using EventHandler<T>
		public event EventHandler<WebBrowserDocumentCompletedEventArgs> WorkbookLoaded;
		public bool SelectRange(string sheetName, string rangeText)
		{
			bool rangeSelected = false;
			try
			{
				 m_oExcelApp.ActiveWorkbook.Sheets[sheetName].Activate();
				Excel.Worksheet ws = m_oExcelApp.ActiveWorkbook.Sheets[sheetName];
				ws.Range[rangeText].Select();
				rangeSelected = true;
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Trying to select[" + rangeText + "] - " + ex.ToString());
			}
			return rangeSelected;
		}
		public bool DeductionComment(string sheetName, string rangeText, string deductionComment)
		{
			bool commentApplied = false;
			if (SelectRange(sheetName, rangeText))
			{
				// TODO: 
				Excel.Range cell = m_oExcelApp.Selection;
				if ( cell.Comment == null )
					cell.AddComment(deductionComment);
				else
					cell.Comment.Text( deductionComment);
				commentApplied = true;
			}

			return commentApplied;
		}

		public bool SelectRange(string rangeText)
		{
			bool rangeSelected = false;
			try
			{
				Excel.Worksheet ws = m_oExcelApp.ActiveSheet;
				ws = m_oExcelApp.ActiveWorkbook.ActiveSheet;
				ws.Range[rangeText].Select();
				rangeSelected = true;
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Trying to select[" + rangeText + "] - " + ex.ToString());
			}
			return rangeSelected;
		}
		public Excel.Range Find(string text)
		{
			Excel.Worksheet ws = m_oExcelApp.ActiveSheet;
			return Find(ws.UsedRange, text);
		}
		public Excel.Range Find(Excel.Range searchRange, string text)
		{
			Excel.Range range = null;
			try
			{
				// Range Find(Object What,Object After,Object LookIn,	Object LookAt,Object SearchOrder,
					// XlSearchDirection SearchDirection, Object MatchCase,Object MatchByte,Object SearchFormat)
				// try most restrictive case to match the contents, if that doesn't work, try partial
				range = searchRange.Find(text,				// Object What
						System.Type.Missing,				// Object After
						Excel.XlFindLookIn.xlValues,		// Object LookIn
						Excel.XlLookAt.xlWhole,				// Object LookAt
						Excel.XlSearchOrder.xlByColumns,	// Object SearchOrder
						Excel.XlSearchDirection.xlNext,		// searchDirection
						true,								// Object MatchCase
						false
						);
				if ( range == null )
					range = searchRange.Find(text,				// Object What
							System.Type.Missing,				// Object After
							Excel.XlFindLookIn.xlValues,		// Object LookIn
							Excel.XlLookAt.xlPart,				// Object LookAt
							Excel.XlSearchOrder.xlByColumns,	// Object SearchOrder
							Excel.XlSearchDirection.xlNext,		// searchDirection
							false,								// Object MatchCase
							false
							);

			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
			}
			return range;
		}
		public ZExcelViewer()
		{
			InitializeComponent();
			m_oExcelApp = null;
			m_filename = "";
			m_worksheetNames = new List<string>();
		}
		public string fileName
		{
			get { return m_filename; }
			set
			{
				m_filename = value;
				// make sure we have a filename
				if ( value.Length > 0 )
					excelBrowswer.Navigate(m_filename);
			}
		}
		public Excel.Application oExcelApp
		{
			get { return m_oExcelApp; }
		}
		private void excelBrowswer_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			try
			{
				// Get reference to Excel.Application from the ROT. 
				m_oExcelApp = (Excel.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Excel.Application");
				string wkbkName = "";
				// Display the name of the object. 
				if (m_oExcelApp != null)
				{
					if (m_oExcelApp.ActiveWorkbook != null)
					{
						wkbkName = m_oExcelApp.ActiveWorkbook.Name;
						m_filename = m_oExcelApp.ActiveWorkbook.FullName;
						m_oExcelApp.EnableEvents = true;
						foreach (Excel.Worksheet ws in m_oExcelApp.ActiveWorkbook.Worksheets)
						{
							m_worksheetNames.Add(ws.Name);
						}
						// let the explorer know something happened.
						EventHandler<WebBrowserDocumentCompletedEventArgs> handler = WorkbookLoaded;
						// Event will be null if there are no subscribers
						if (handler != null)
						{
							// Use the () operator to raise the event.
							handler(this, e);
						}
//						m_oExcelApp.ExecuteExcel4Macro("SHOW.TOOLBAR(\"Ribbon\",True)");
						
						//object ribbonHeight = m_oExcelApp.CommandBars["Ribbon"].Height;
						//m_oExcelApp.CommandBars.ExecuteMso("MinimizeRibbon");
						////m_oExcelApp.CommandBars["Ribbon"].Height = 500;
						//ribbonHeight = m_oExcelApp.CommandBars["Ribbon"].Height;
						
						//ribbonHeight = m_oExcelApp.CommandBars["Ribbon"];

					}
				}
			}
			catch (Exception ex)
			{
				Debug.Print("Ooops, problem getting excel app:" + ex.Message);
			}
		}

		private void excelBrowswer_Navigated(object sender, WebBrowserNavigatedEventArgs e)
		{

		}

	}
}
