using Newtouch.HIS.API.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request.Patient
{
    /// <summary>
    /// 住院病人信息
    /// </summary>
    public class InPatientInfoQueryRequest : BookingReqBaseDto
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string sfzh { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        //public string OrganizeId { get; set; }
    }
}
