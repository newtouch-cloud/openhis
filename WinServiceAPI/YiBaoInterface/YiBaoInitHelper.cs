using System;
using System.Runtime.InteropServices;
using System.Text;
using Newtonsoft.Json;

namespace YiBaoInterface
{
    public partial class YiBaoInitHelper
    {
        [DllImport("SiInterface_hsf.dll")]
        public static extern int INIT(StringBuilder ErrorMsg);

        [DllImport("SiInterface_hsf.dll")]
        public static extern int BUSINESS_HANDLE(StringBuilder inputData, StringBuilder outputDat);

        /// <summary>
        /// 是否已初始化
        /// </summary>
        public static bool hasInit = false;

        /// <summary>
        /// 是否已签到
        /// </summary>
        public static bool hasSignIn = false;

        /// <summary>
        /// 签到返回编码 全局变量 后续交易会传
        /// </summary>
        public static string sign_no = "";

        /// <summary>
        /// 签到 IP 地址
        /// </summary>
        public static string localIp = "";

        /// <summary>
        /// 签到 MAC 地址
        /// </summary>
        public static string localMac = "";

        /// <summary>
        /// 医保初始化
        /// </summary>
        /// <returns>已初始化过或初始化成功返回空串，否则返回JSON串</returns>
        public static string DoInit()
        {
            #region 测试
            //return string.Empty;
            #endregion

            if (hasInit)
            {
                return string.Empty;
            }

            int err = -1;
            try
            {
                StringBuilder outdata = new StringBuilder(500);
                err = INIT(outdata);
                if (err != 0)
                {
                    throw new Exception("医保INIT失败：" + Convert.ToString(outdata));
                }

                hasInit = true;

                return string.Empty;
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { infcode = err, err_msg = ex.Message });
            }
        }
    }
}
