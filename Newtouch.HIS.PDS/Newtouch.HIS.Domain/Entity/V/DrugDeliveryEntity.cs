using System;

namespace Newtouch.HIS.Domain.Entity.V
{
    /// <summary>
    /// 门诊发药信息
    /// </summary>
    public class DrugDeliveryEntity
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int? nl { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// 发药日期
        /// </summary>
        public DateTime fyrq { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// 药厂名称
        /// </summary>
        public string ycmc { get; set; }

        /// <summary>
        /// 数量 带单位
        /// </summary>
        public string sl { get; set; }

        /// <summary>
        /// 进价 单价 部门单位
        /// </summary>
        public decimal jj { get; set; }

        /// <summary>
        /// 进价总额
        /// </summary>
        public decimal jjze { get; set; }

        /// <summary>
        /// 零售价 单价 部门单位
        /// </summary>
        public decimal lsj { get; set; }

        /// <summary>
        /// 零售总额
        /// </summary>
        public decimal lsze { get; set; }

        /// <summary>
        /// 部门单位
        /// </summary>
        public string bmdw { get; set; }
    }
}
