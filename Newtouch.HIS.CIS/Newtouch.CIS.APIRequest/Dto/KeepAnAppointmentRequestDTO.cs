using Newtouch.HIS.API.Common;
using System;

namespace Newtouch.CIS.APIRequest.Dto
{
    public class KeepAnAppointmentRequestDTO : RequestBase
    {

        /// <summary>
        /// 预约挂号主键 mz_yygh.id
        /// </summary>
        public string mzyyghId { get; set; }

        /// <summary>
        /// 赴约时间
        /// </summary>
        public DateTime arrivalDate { get; set; }

        /// <summary>
        /// 现场处理挂号人员
        /// </summary>
        public string arrivalOpereater { get; set; }
    }
}
