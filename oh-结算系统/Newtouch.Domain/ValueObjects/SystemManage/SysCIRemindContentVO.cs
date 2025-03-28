using System;

namespace Newtouch.HIS.Domain.ValueObjects.SystemManage
{
    public class SysCIRemindContentVO
    {
        public int sfxjsnrbh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mzjsnr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zyjsnr { get; set; }

        /// <summary>
        /// 0 不提示 1 普通提示 2 警示 3 禁止使用
        /// </summary>
        public byte mzjsjb { get; set; }

        /// <summary>
        /// 0 不提示 1 普通提示 2 警示 3 禁止使用
        /// </summary>
        public byte zyjsjb { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        public string sfxm { get; set; }

        public string sfxmmc { get; set; }
    }
}
