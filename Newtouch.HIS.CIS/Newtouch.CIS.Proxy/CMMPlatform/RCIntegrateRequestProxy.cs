using System.Net;
using System.Text;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.RCIntegrateRequest;
using Newtouch.Infrastructure;

namespace Newtouch.CIS.Proxy.CMMPlatform
{
    /// <summary>
    /// 集成远程诊疗系统
    /// </summary>
    internal class RCIntegrateRequestProxy : GuiAnTelemedicinePlatformProxy<RCIntegrateRequestEntity, string>
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        public override string Execute(RCIntegrateRequestEntity requestDto)
        {
            var rCIntegrateRequest = Core.Common.Utils.ConfigurationHelper.GetAppConfigValue("RCIntegrateRequestURL");
            var requestXml = requestDto.XmlSerialize();
            var uri = new StringBuilder(rCIntegrateRequest)
                .Append("orgCode=").Append(requestDto.openid)
                .Append("&userName=").Append(requestDto.userName)
                .Append("&patientId=").Append(requestDto.patientId)
                .Append("&series=").Append(requestDto.series)
                .Append("&patientName=").Append(requestDto.patientName)
                .Append("&mobile=").Append(requestDto.mobile)
                .Append("&certid=").Append(requestDto.certid)
                .Append("&gender=").Append(requestDto.gender)
                .Append("&birthday=").Append(requestDto.birthday)
                .Append("&patientType=").Append(requestDto.patientType)
                .Append("&cardId=").Append(requestDto.cardId)
                .Append("&organCode=").Append(requestDto.organCode)
                .Append("&m=").Append(requestDto.m)
                .Append("&clz=").Append(requestDto.clz);
            return Monitoring.RequestMonitor(_ => new WebClient().DownloadString(uri.ToString()), requestXml, "RCIntegrateRequestProxy-Execute");
        }
    }
}