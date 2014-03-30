using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace AM.Interop.WinApi
{
    public class ole32
    {
        [DllImport("ole32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern HRESULT OleLoadFromStream(
            IStream pStm,
            [In] ref Guid riid,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppvObj);

        [DllImport("ole32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern HRESULT CreateBindCtx(
            uint reserved,
            [Out, MarshalAs(UnmanagedType.Interface)] out IBindCtx pctx);
    }
}
