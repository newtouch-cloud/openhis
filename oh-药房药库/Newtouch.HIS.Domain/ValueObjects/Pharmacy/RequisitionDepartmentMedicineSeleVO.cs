namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 内部申领 部门药品选择 数据源
    /// </summary>
    public class RequisitionDepartmentMedicineSeleVO
    {
        /// <summary>
        /// 药品Code
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// 药品 收费大类名称
        /// </summary>
        public string ypdlmc { get; set; }

        /// <summary>
        /// 可领数量and单位 5盒6支，仅显示用
        /// </summary>
        public string Klslanddw { get; set; }
        
        /// <summary>
        /// 库存数量（XT_YP_KCXXK.Kcsl）
        /// </summary>
        public int Kcsl { get; set; }

        /// <summary>
        /// 库存数量单位（对应 XT_YP_KCXXK.Kcsl）
        /// </summary>
        public string Kcsldw { get; set; }

        /// <summary>
        /// 药品规格
        /// </summary>
        public string ypgg { get; set; }

        /// <summary>
        /// 药厂名称
        /// </summary>
        public string ycmc { get; set; }

        /// <summary>
        /// 零售价
        /// </summary>
        public decimal lsj { get; set; }

        /// <summary>
        /// 批发价
        /// </summary>
        public decimal pfj { get; set; }

        /// <summary>
        /// 药库转换因子（计算价格用的）
        /// </summary>
        public decimal ykzhyz { get; set; }

    }
}
