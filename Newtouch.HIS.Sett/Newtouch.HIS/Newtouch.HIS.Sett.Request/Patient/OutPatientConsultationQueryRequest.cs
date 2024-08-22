using Newtouch.Core.Common;
using Newtouch.HIS.API.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request.Patient
{
    /// <summary>
    /// 门诊就诊列表
    /// </summary>
    public class OutPatientConsultationQueryRequest : BookingReqBaseDto
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string sfzh { get; set; }
        
    }
}
