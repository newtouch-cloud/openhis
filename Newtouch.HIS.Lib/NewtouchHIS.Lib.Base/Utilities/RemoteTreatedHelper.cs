using NewtouchHIS.Lib.Base.Security;

namespace NewtouchHIS.Lib.Base.Utilities
{
    public class RemoteTreatedHelper
    {
        /// <summary>
        /// 厂商，新致公司：xinzhi
        /// </summary>
        public static string Organization = AppSettings.GetConfig("RemoteTreated:SKey") ?? "xinzhi";
        /// <summary>
        /// Secret 为厂商使用密码
        /// </summary>
        static string Secret = AppSettings.GetConfig("RemoteTreated:Organization") ?? "8a8e2a0db12343f589d7721762b94490";
        static string AES_IV = "A-16-Byte-String";

        /// <summary>
        /// 组装请求头 Header
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> HeaderItems()
        {
            Random random = new Random();
            int randomNumber = random.Next();
            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                { "Nonce", randomNumber.ToString() },
                { "Organization", Organization },
                { "Timestamp",  GetTimeStamp(DateTime.Now).ToString() },
                { "Signature", "" },
            };
            dic["Signature"] = MD5Encrypt.Md5($"{dic["Nonce"]}{dic["Timestamp"]}{Secret}", 32, false);
            return dic;
        }
        /// <summary>
        /// 获取Rtcuserid
        /// </summary>
        /// <param name="Rtcuserid"></param>
        /// <returns></returns>
        public static string GetRtcuser(string Rtcuserid)
        {
            var data = AESEncrypt.AESDecrypt(Rtcuserid, Secret, AES_IV);
            return data;
        }

        /// <summary>
        /// DateTime转时间戳
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long GetTimeStamp(DateTime dateTime)
        {
            // 开始时间
            DateTime startTime = new(1970, 1, 1, 8, 0, 0);
            // 10位的时间戳
            long timeStamp = Convert.ToInt64(dateTime.Subtract(startTime).TotalSeconds);
            // 13位的时间戳
            //long timeStamp = Convert.ToInt64(dateTime.Subtract(_dtStart).TotalMilliseconds);
            return timeStamp;
        }
        /// <summary>
        /// 时间戳转DateTime
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(long timeStamp)
        {
            // 开始时间
            DateTime startTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            long lTime = long.Parse(timeStamp + "0000000");
            return startTime.Add(new TimeSpan(lTime)).AddHours(8);
        }
    }
}
