using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.CIS.APIRequest
{
    public class LisRequest
    {
        [DllImport(@"../bin/Debug/Lis_HisReport.dll", EntryPoint = "GetMzReport", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void GetMzReport(
     [MarshalAs(UnmanagedType.BStr)]
            string Ado_lis,
     [MarshalAs(UnmanagedType.BStr)]
            string brxx_id);

        [DllImport(@"../bin/Debug/Lis_HisReport.dll", EntryPoint = "GetZyReport", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void GetZyReport([MarshalAs(UnmanagedType.BStr)] string Ado_lis, [MarshalAs(UnmanagedType.BStr)] string brxx_id);//lis住院报告
        [DllImport(@"../bin/Debug/Lis_HisReport.dll", EntryPoint = "CreateJpg_C", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void CreateJpg_C([MarshalAs(UnmanagedType.BStr)] string Ado_lis, [MarshalAs(UnmanagedType.BStr)] string brxx_tmh,
          [MarshalAs(UnmanagedType.BStr)] string zydj_id, [MarshalAs(UnmanagedType.BStr)] string brxx_id);

        [DllImport(@"../bin/Debug/Lis_HisReport.dll", EntryPoint = "CreatePdf_C", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void CreatePdf_C([MarshalAs(UnmanagedType.BStr)] string Ado_lis, [MarshalAs(UnmanagedType.BStr)] string brxx_tmh,
         [MarshalAs(UnmanagedType.BStr)] string zydj_id, [MarshalAs(UnmanagedType.BStr)] string brxx_id);
    }
    
}
