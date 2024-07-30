namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 本部门药品信息显示信息
    /// </summary>
    public class MedicineInfoVO
    {
        public int ypId { get; set; }
        public string ypCode { get; set; }

        public string ypmc { get; set; }

        public string ycmc { get; set; }

        public string ypxzmc { get; set; }

        public string jxmc { get; set; }
        public string ypgg { get; set; }

        public int kcsl { get; set; }
        public string Zcxh { get; set; }
        public string syzt { get; set; }

        /// <summary>
        /// 药库库存
        /// </summary>
        public decimal? YkKcsl { get; set; }

        /// <summary>
        /// 药库库位
        /// </summary>
        public string YkDw { get; set; }

        /// <summary>
        /// 部门单位
        /// </summaYkDwry>
        public string deptdw { get; set; }

        /// <summary>
        /// 零售价
        /// </summary>
        public decimal? lsj { get; set; }

        /// <summary>
        /// 批发价
        /// </summary>
        public decimal? pfj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Ypkw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Pxfs1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Pxfs2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Kcsx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Kcxx { get; set; }
        public int Jhd { get; set; }
        public int Jhl { get; set; }
        public string yplb { get; set; }
        public string ypzt { get; set; }
        public int? Sysx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string klsl { get; set; }

        /// <summary>
        /// 本部门药品ID XT_YP_BMYPXX 主键
        /// </summary>
        public string bmypId { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>
        public System.DateTime? yxq { get; set; }
    }
}
