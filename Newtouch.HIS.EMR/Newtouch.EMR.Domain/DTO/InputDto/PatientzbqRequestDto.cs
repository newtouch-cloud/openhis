using Newtouch.Core.Common;
using System;


namespace Newtouch.EMR.Domain.DTO
{
    /// <summary>
    /// 请求在病区病人信息参数
    /// </summary>
    public class PatientzbqRequestDto
    {
        public Pagination pagination { get; set; }
        public string bqcode { get; set; }

        public string ysgh { get; set; }

        public string cw { get; set; }
    }

    public class PatientycqRequestDto : PatientzbqRequestDto
    {
        public new Pagination pagination { get; set; }
        public DateTime? cqksrq { get; set; }
        public DateTime? cqjsrq { get; set; }
        public string zyh { get; set; }
    }

    public class PatientmyRequestDto : PatientzbqRequestDto
    {
        public new Pagination pagination { get; set; }
        public DateTime? ksrq { get; set; }
        public DateTime? jsrq { get; set; }
        public string zyh { get; set; }
    }
}
