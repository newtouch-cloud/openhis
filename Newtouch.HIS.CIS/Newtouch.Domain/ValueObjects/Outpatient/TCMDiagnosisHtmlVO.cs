namespace Newtouch.Domain.ValueObjects
{
    public class TCMDiagnosisHtmlVO
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
        public int zdlx { get; set; }
        /// <summary>
        /// 疑似标志
        /// </summary>
        public bool ysbz { get; set; }

        /// <summary>
        /// 症候编码
        /// </summary>
        public string zhCode { get; set; }

        /// <summary>
        /// 症候名称
        /// </summary>
        public string zhmc { get; set; }
        /// <summary>
        /// icd10
        /// </summary>
        public string icd10 { get; set; }
        /// <summary>
        /// 中医诊断备注
        /// </summary>
        public string zdbz { get; set; }
    }
}
