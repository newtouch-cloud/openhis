using System.Net;
using System.Text;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.HEALRequest;
using Newtouch.Infrastructure;

namespace Newtouch.CIS.Proxy.CMMPlatform
{
    /// <summary>
    /// 集成治未病系统
    /// </summary>
    internal class HEALRequestProxy : GuiAnTelemedicinePlatformProxy<HEALRequestEntity, string>
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        public override string Execute(HEALRequestEntity requestDto)
        {
            var requestXml = requestDto.XmlSerialize();
            var uri = new StringBuilder(IntegratedModuleUrl).Append(requestDto.resource)
                .Append("&CertificatesType=").Append(requestDto.CertificatesType)
                .Append("&CertificatesNumber=").Append(requestDto.CertificatesNumber);
            return Monitoring.RequestMonitor(_ => new WebClient().DownloadString(uri.ToString()), requestXml, "HEALRequestProxy-Execute");
        }
    }
}