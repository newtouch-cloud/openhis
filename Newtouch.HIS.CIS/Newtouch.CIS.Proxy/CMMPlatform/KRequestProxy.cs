using System.Net;
using System.Text;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.KRequest;
using Newtouch.Infrastructure;

namespace Newtouch.CIS.Proxy.CMMPlatform
{
    /// <summary>
    /// 集成知识库系统
    /// </summary>
    internal class KRequestProxy : GuiAnTelemedicinePlatformProxy<KRequestEntity, string>
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        public override string Execute(KRequestEntity requestDto)
        {
            var requestXml = requestDto.XmlSerialize();
            var uri = new StringBuilder(IntegratedModuleUrl).Append(requestDto.resource);
            return Monitoring.RequestMonitor(_ => new WebClient().DownloadString(uri.ToString()), requestXml, "KRequestProxy-Execute");
        }
    }
}