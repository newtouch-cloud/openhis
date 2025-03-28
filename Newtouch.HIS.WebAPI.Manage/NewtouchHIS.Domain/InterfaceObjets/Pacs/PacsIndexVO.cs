namespace NewtouchHIS.Domain.InterfaceObjets
{
    public class PacsIndexVO
    {
        /// <summary>
        /// 门诊：门诊号  住院：住院号
        /// </summary>
        public string? ywlsh { get; set; }
        public string? sqdh { get; set; }
        /// <summary>
        /// Enummzzybz 1 门诊 2 住院
        /// </summary>
        public int? mzzybz { get; set; }

    }
}
