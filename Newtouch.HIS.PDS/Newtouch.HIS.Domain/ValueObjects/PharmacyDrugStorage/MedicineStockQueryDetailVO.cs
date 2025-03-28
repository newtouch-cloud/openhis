namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 库存量查询明细
    /// </summary>
    public class MedicineStockQueryDetailVO
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        public string deptName { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public string yxq { get; set; }

        /// <summary>
        /// 药品库位
        /// </summary>
        public string ypkw { get; set; }

        /// <summary>
        /// 控制标志
        /// </summary>
        public string kzbz { get; set; }

        /// <summary>
        /// 冻结数量
        /// </summary>
        public int djsl { get; set; }

        /// <summary>
        /// 冻结数量
        /// </summary>
        //public int kcsl { get; set; }
        public string djslstr { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        //public int kcsl { get; set; }
        public string kcslstr { get; set; }

        /// <summary>
        /// 药库批发价
        /// </summary>
        public decimal ykpfj { get; set; }

        /// <summary>
        /// 药库零售价
        /// </summary>
        public decimal yklsj { get; set; }

        /// <summary>
        /// 部门批发价
        /// </summary>
        public decimal? pfj { get; set; }

        /// <summary>
        /// 部门零售价
        /// </summary>
        public decimal? lsj { get; set; }

        /// <summary>
        /// 批价总额
        /// </summary>
        public decimal pjze { get; set; }

        /// <summary>
        /// 零售价总额
        /// </summary>
        public decimal ljze { get; set; }

        /// <summary>
        /// 进价
        /// </summary>
        public decimal jj { get; set; }

        /// <summary>
        /// 转化因子
        /// </summary>
        public int? zhyz { get; set; }
    }
}
