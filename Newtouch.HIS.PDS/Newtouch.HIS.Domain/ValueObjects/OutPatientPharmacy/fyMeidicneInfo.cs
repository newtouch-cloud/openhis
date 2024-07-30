using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 发药药品信息
    /// </summary>
    public class fyMeidicneInfo
    {
        /// <summary>
        /// 处方明细批号Id
        /// </summary>
        public string cfmxphId { get; set; }

        /// <summary>
        /// 处方内码
        /// </summary>
        public string cfnm { get; set; }
        
        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 药品
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal sl { get; set; }
        
        /// <summary>
        /// 单位
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 单位数量
        /// </summary>
        public string slstr { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// 生产厂商
        /// </summary>
        public string ycmc { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// 剂量
        /// </summary>
        public decimal jl { get; set; }

        /// <summary>
        /// 剂量单位
        /// </summary>
        public string jldw { get; set; }

        /// <summary>
        /// 用法
        /// </summary>
        public string yfmc { get; set; }

        /// <summary>
        /// 医生嘱托(备注)
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// 药品分组
        /// </summary>
        public string czh { get; set; }

        /// <summary>
        /// 1-发药 2-退药
        /// </summary>
        public string operateType { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }
}
