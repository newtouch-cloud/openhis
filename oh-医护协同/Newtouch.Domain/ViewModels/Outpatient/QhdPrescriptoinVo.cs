using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.Domain.ViewModels.Outpatient
{
    [XmlRoot("YBJKDATA")]
    public class QhdPrescriptoinVo
    {
        public RESPONSEDATA RESPONSEDATA { get; set; }
    }
    public class RESPONSEDATA
    {
        public string RETURNNUM { get; set; }
        public string ERRORMSG { get; set; }
        public string REFMSGID { get; set; }
        //public string OUTPUT { get; set; }
        public List<OUTROW> OUTPUT { get; set; }
    }
    //public class OUTPUT
    //{
        
    //}
    public class OUTROW
    {
        /// <summary>
        /// 处方号
        /// </summary>
        public string AKC220 { get; set; }
        /// <summary>
        /// 药品/项目编码
        /// </summary>
        public string AKE001 { get; set; }
        /// <summary>
        /// 医保药品/项目名称
        /// </summary>
        public string AKE002 { get; set; }
        /// <summary>
        /// 违反规则名称
        /// </summary>
        public string BZE868 { get; set; }
        /// <summary>
        /// 违规说明
        /// </summary>
        public string AAE013 { get; set; }
        /// <summary>
        /// 违规级别
        /// </summary>
        public string BAZ969 { get; set; }
        /// <summary>
        /// 限制级别
        /// </summary>
        public string BAZ970 { get; set; }
    }
}
