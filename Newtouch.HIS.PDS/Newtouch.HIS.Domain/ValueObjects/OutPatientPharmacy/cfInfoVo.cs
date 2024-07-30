using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 处方信息
    /// </summary>
    public class cfInfoVo
    {
        /// <summary>
        /// 收费时间（展示用）
        /// </summary>
        public string ShowSfsj { get; set; }

        /// <summary>
        /// 主键ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 处方内码
        /// </summary>
        public long cfnm { get; set; }

        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 完整处方号
        /// </summary>
        public string cfhComplete { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        public decimal? Zje { get; set; }

        /// <summary>
        /// 结算内码
        /// </summary>
        public long jsnm { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        public string Fph { get; set; }

        /// <summary>
        /// 完整发票号
        /// </summary>
        public string FphComplete { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int? nl { get; set; }

        /// <summary>
        /// 病人性质
        /// </summary>
        public string brxzmc { get; set; }

        /// <summary>
        /// 医生
        /// </summary>
        public string ysmc { get; set; }

        /// <summary>
        /// 科室
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// 处方金额
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// 领药药房
        /// </summary>
        public string lyyf { get; set; }

        /// <summary>
        /// 0：未排；1：已排；2：已发；3：已退
        /// </summary>
        public string fybz { get; set; }

        /// <summary>
        /// 组织机构Id
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 发药时间
        /// </summary>
        public DateTime? fysj { get; set; }

    }
}
