using System;

namespace Newtouch.HIS.Domain.VO
{
    /// <summary>
    /// 处方信息
    /// </summary>
    public class CfxxVO
    {
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo { get; set; }
        public string mzh { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 处方内码
        /// </summary>
        public long? cfnm { get; set; }

        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        public string Fph { get; set; }
        
        /// <summary>
        /// 医生名称
        /// </summary>
        public string ysmc { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// 0：未排；1：已排；2：已发；3：已退
        /// </summary>
        public string fybz { get; set; }

        /// <summary>
        /// 收费时间
        /// </summary>
        public DateTime? sfsj { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? fysj { get; set; }
    }
}