using System;
using System.Runtime.InteropServices;

namespace NewtouchActiveX
{
    [Guid("F48D2AD3-23FF-4882-8A9A-FB540AA718F5")]
    public class CISActiveX : ActiveXControl
    {
        //public string jianyandiaoyue()
        //{
        //    Process myprocess = new Process();
        //    ProcessStartInfo startInfo = new ProcessStartInfo(@"E:\发布\CIS\lisresult.exe", "门诊号" + "@" + DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") + "@" + DateTime.Now.ToString("yyyy-MM-dd") + "@");
        //    myprocess.StartInfo = startInfo;
        //    myprocess.StartInfo.UseShellExecute = true;
        //    myprocess.Start();
        //    return "123";
        //}

        [DllImport("Lis_HisInterface.dll", EntryPoint = "GetMzReport", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void GetMzReport(
[MarshalAs(UnmanagedType.BStr)]
            string Ado_lis,
[MarshalAs(UnmanagedType.BStr)]
            string brxx_id);

        public string jianyandiaoyue(string mzh)
        {
            var LISConn = "Provider=SQLOLEDB.1;Password=wqsj2000;Persist Security Info=true;User ID=wqsj;Initial Catalog=lisdata;Data Source=59.80.30.169,34443";
            var brbgid = mzh;

            GetMzReport(LISConn, brbgid);

            return null;
        }

        public string AAA()
        {
            return "111";
        }
        public string BBB(string str)
        {
            return str;
        }
    }
}
