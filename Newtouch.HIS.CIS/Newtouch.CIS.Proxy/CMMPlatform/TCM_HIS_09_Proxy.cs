using System;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_09;
using Newtouch.Infrastructure;
using Newtouch.Infrastructure.Log;

namespace Newtouch.CIS.Proxy.CMMPlatform
{
    /// <summary>
    /// 提取诊断信息
    /// </summary>
    public class TcmHis09Proxy : GuiAnTelemedicinePlatformProxy<ReceiveRequestEntity, PullDiagInfoRequestEntity>
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        public override PullDiagInfoRequestEntity Execute(ReceiveRequestEntity requestDto)
        {
            var requestXml = "";
            try
            {
                requestXml = requestDto.XmlSerialize();
                var responseXml = Monitoring.RequestMonitor(_ => new TcmReceiveServiceReference.ReceiveWebServiceInterfaceClient().TCM_HIS_09(requestXml), requestDto, "TcmHis08Proxy-Execute");
                return responseXml.XmlDeSerialize<PullDiagInfoRequestEntity>() as PullDiagInfoRequestEntity;
            }
            catch (Exception e)
            {
                LogCore.Error("TcmHis08Proxy Execute error", e, string.Format("requestXml:{0}", requestXml));
                throw;
            }
        }
    }
}