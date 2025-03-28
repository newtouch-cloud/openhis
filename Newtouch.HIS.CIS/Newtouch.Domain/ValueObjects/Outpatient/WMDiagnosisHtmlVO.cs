namespace Newtouch.Domain.ValueObjects
{
    public class WMDiagnosisHtmlVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string zdCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zdmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int zdlx{ get; set; }
        /// <summary>
        /// 疑似标志
        /// </summary>
        public bool ysbz { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string icd10 { get; set; }
        /// <summary>
        /// 西医诊断备注
        /// </summary>
        public string zdbz { get; set; }
        /// <summary>
        /// 牙位图类型
        /// </summary>
        public string ywlx { get; set; }
        /// <summary>
        /// 牙位图位置
        /// </summary>
        public string ywstr { get; set; }
        /// <summary>
        /// 牙位图显示
        /// </summary>
        public string ywxs { get; set; }
    }
}
