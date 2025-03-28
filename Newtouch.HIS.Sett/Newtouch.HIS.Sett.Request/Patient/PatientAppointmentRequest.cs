using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Sett.Request
{
    /// <summary>
    /// 签到
    /// </summary>
    public class PatientAppointmentRequest : RequestBase
    {
        public string mzh { get; set; }
        public string orgId { get; set; }

    }
}