namespace Newtouch.HIS.Domain.BusinessObjects.HospitalizationManage
{
    /// <summary>
    /// 住院计费项目和药品列表
    /// </summary>
    public class GridZyXmYpjfb
    {
        public string ZYH { get; set; }
        //public string XSEBH { get; set; }

        public string SFXM { get; set; }
        public string IS_SFXM { get; set; }
        public string CreateTime { get; set; }
        public string SFXMMC { get; set; }
        public string JFDW { get; set; }
        public string SL { get; set; }
        public string DJ { get; set; }
        


    }

    /// <summary>
    /// 住院计费项目和药品明细
    /// </summary>
    public class gridZyXmYpItemsjfb
    {
        public string JFBBH { get; set; }
        public string CXZYJFBBH { get; set; }
        public string CreateTime { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string SFXMCode { get; set; }
        /// <summary>
        /// 项目或药品名称
        /// </summary>
        public string SFXMMC { get; set; }
        public string JFDW { get; set; }
        public string SL { get; set; }
        public string RETURN_SL { get; set; }



    }
}
