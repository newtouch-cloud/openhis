using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 住院费用 按dl分组
    /// </summary>
    public class HospFeeChargeCategoryGroupVO
    {
        /// <summary>
        /// 收费大类编码
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// 收费大类名称
        /// </summary>
        public string dlmc { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal je { get; set; }
    }
    public class HospFeeChargeCategoryGroupDetailVO : HospFeeChargeCategoryGroupVO
    {
        public string tdrq { get; set; }
        public string sfxm { get; set; }
        public string sfxmmc { get; set; }
        public decimal dj { get; set; }
        public decimal? zje { get; set; }
        public decimal sl { get; set; }
        public string jfdw { get; set; }
        /// <summary>
        /// 自负性质
        /// </summary>
        public string zfxz { get; set; }
        public string gg { get; set; }
        public string zzfbz { get; set; }
    }
    /// <summary>
    /// 费用上传
    /// </summary>
    public class HospFeeUploadDetailVO
    {
        public string jfbbh { get; set; }
        public int cxzyjfbbh { get; set; }
        public DateTime tdrq { get; set; }
        public string yp { get; set; }
        public string ys { get; set; }
        public string ysmc { get; set; }
        public string ks { get; set; }
        public string ksmc { get; set; }
        public decimal dj { get; set; }
        public decimal sl { get; set; }
        public decimal je { get; set; }
        public string jfdw { get; set; }
        public string ypmc { get; set; }
        public string gjybdm { get; set; }
        public string ybdm { get; set; }
        /// <summary>
        /// 自费性质 4:甲 5:已 6:丙 1:自费
        /// </summary>
        public string zfxz { get; set; }
        /// <summary>
        /// 医保上传状态 1: 已上传  0:未上传
        /// </summary>
        public string ybsczt { get; set; }
        public string zzfbz { get; set; }
        /// <summary>
        /// 费用上传记录表中jfbbh
        /// </summary>
        public string feedetl_sn { get; set; }
        public string dlmc { get; set; }
        /// <summary>
        /// 入参表zyh
        /// </summary>
        public string zyh { get; set; }
    }
    public class InpatIentFeeSumVo
    {
        public decimal zje { get; set; }
    }

    public class InpatIentFeeInfo
    {
        public decimal zje { get; set; }
        public DateTime cyrq { get; set; }


    }
    public class InpatIentFeeObj
    {
        public decimal zje { get; set; }
        public decimal ybzje { get; set; }
        public DateTime cyrq { get; set; }
    }
}
