using System;

namespace Newtouch.HIS.Domain.VO
{
    /// <summary>
    /// 处方发药记录
    /// </summary>
    public class CffyjlVO
    {
        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 处方内码
        /// </summary>
        public long cfnm { get; set; }

        /// <summary>
        /// 发药标志 0：未排；1：已排；2：已发；3：已退
        /// </summary>
        public string fybz { get; set; }

        /// <summary>
        /// 发药时间
        /// </summary>
        public DateTime? fysj { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        public string Fph { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int? nl { get; set; }

        /// <summary>
        /// 病人性质描述
        /// </summary>
        public string brxzmc { get; set; }

        /// <summary>
        /// 发药人员
        /// </summary>
        public string fyry { get; set; }
    }
}