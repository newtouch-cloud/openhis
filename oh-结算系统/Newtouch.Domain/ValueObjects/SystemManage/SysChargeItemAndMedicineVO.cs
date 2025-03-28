namespace Newtouch.HIS.Domain.ValueObjects.SystemManage
{
    /// <summary>
    /// 收费项目、药品 VO
    /// </summary>
    public class SysChargeItemAndMedicineVO
    {
        /// <summary>
        /// 
        /// 当为药品时ypCode
        /// </summary>
        public string sfxmCode { get; set; }

        /// <summary>
        /// 
        /// 当为药品时ypmc
        /// </summary>
        public string sfxmmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfdlCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfdlmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? duration { get; set; }

        /// <summary>
        /// 1 药品   2 项目
        /// </summary>
        public string yzlx { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string bz { get; set; }
        /// <summary>
        /// 单位计量数
        /// </summary>
        public int? dwjls { get; set; }
        public int? jjcl { get; set; }
        /// <summary>
        /// 剂量
        /// </summary>
        public decimal? jl { get; set; }
        /// <summary>
        /// 剂量单位
        /// </summary>
        public string jzdw { get; set; }

    }
}
