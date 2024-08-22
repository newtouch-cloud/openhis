namespace Newtouch.HIS.Domain.ValueObjects.SystemManage
{
    /// <summary>
    /// 收费项目VO
    /// 不包括药品
    /// </summary>
    public class SysChargeItemVO
    {
        public string sfxmId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sfxmCode { get; set; }

        /// <summary>
        /// 
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
        /// 门诊住院标志
        /// </summary>
        public string mzzybz { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string bz { get; set; }
        /// <summary>
        /// 单位计量数
        /// </summary>
        public int? dwjls { get; set; }
        public int? jjcl { get; set; }
        public string zfxz { get; set; }
    }

    public class SysChargeClassVO
    {
        public int dlId { get; set; }
        public string dlCode { get; set; }
        public string dlmc { get; set; }
        public string OrganizeId { get; set; }
    }
}
