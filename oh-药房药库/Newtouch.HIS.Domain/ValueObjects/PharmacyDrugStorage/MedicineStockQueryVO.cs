
using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 库存量查询
    /// </summary>
    public class MedicineStockQueryVO
    {
        /// <summary>
        /// 药品名称
        /// </summary>
        public string ypmc { get; set; }
        /// <summary>
        /// 药品code
        /// </summary>
        public string ypCode { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string gg { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string deptName { get; set; }
        /// <summary>
        /// 药房部门Code
        /// </summary>
        public string yfbmCode { get; set; }
        /// <summary>
        /// 账册序号
        /// </summary>
        public string zcxh { get; set; }
        /// <summary>
        /// 药厂名称
        /// </summary>
        public string ycmc { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 药库批发价
        /// </summary>
        public decimal ykpfj { get; set; }

        /// <summary>
        /// 部门零售价
        /// </summary>
        public decimal? lsj { get; set; }

        /// <summary>
        /// 部门批发价
        /// </summary>
        public decimal? pfj { get; set; }

        /// <summary>
        /// 药库零售价
        /// </summary>
        public decimal yklsj { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        //public int kcsl { get; set; }
        public string Kcslstr { get; set; }
        /// <summary>
        /// 批发总额
        /// </summary>
        public decimal pjze { get; set; }
        /// <summary>
        /// 零售总额
        /// </summary>
        public decimal ljze { get; set; }
        /// <summary>
        /// 拼音
        /// </summary>
        public string py { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
