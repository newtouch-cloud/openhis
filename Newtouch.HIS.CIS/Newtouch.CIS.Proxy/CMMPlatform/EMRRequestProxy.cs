using System.Net;
using System.Text;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.EMRRequest;
using Newtouch.Infrastructure;

namespace Newtouch.CIS.Proxy.CMMPlatform
{
    /// <summary>
    /// 集成电子病历
    /// </summary>
    internal class EMRRequestProxy : GuiAnTelemedicinePlatformProxy<EMRRequestEntity, string>
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        public override string Execute(EMRRequestEntity requestDto)
        {
            var requestXml = requestDto.XmlSerialize();
            var uri = new StringBuilder(IntegratedModuleUrl).Append(requestDto.resource).Append("&doctorId=")
                .Append(requestDto.doctorId).Append("&patientId=").Append(requestDto.patientId);
            return Monitoring.RequestMonitor(_ => new WebClient().DownloadString(uri.ToString()), requestXml, "EMRRequestProxy-Execute");
        }
    }
}