using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 系统病人收费附加
    /// </summary>
    public class SysPatiChargeAddVo
    {

        /// <summary>
        /// 病人收费附加编号（xt_brsffj）
        /// </summary>
        public int brsffjbh { get; set; }

        /// <summary>
        /// 病人性质（xt_brxz）
        /// </summary>
        public string brxzmc { get; set; }

        public string brxz { get; set; }

        /// <summary>
        /// 大类（xt_sfdl）
        /// </summary>
        public string dlmc { get; set; }

        public string dl { get; set; }

        /// <summary>
        /// 附加显示大类名称（xt_fjsfdl）
        /// </summary>
        public string fjxsdlmc { get; set; }

        public string fjxsdl { get; set; }

        /// <summary>
        /// 收费项目（xt_fjsfdl）
        /// </summary>
        public string sfxmmc { get; set; }

        public string sfxm { get; set; }

        /// <summary>
        /// 服务费比例（xt_brsffj）
        /// </summary>
        public decimal fwfbl { get; set; }

        /// <summary>
        /// 状态（xt_brsffj）
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        /// 建档人员（xt_brsffj）
        /// </summary>
        public string CreatorCode { get; set; }
        /// <summary>
        /// 建档日期（xt_brsffj）
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
