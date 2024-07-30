using System;
using Newtouch.CIS.Proxy.CMMPlatform.DTO;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_01;
using Newtouch.Infrastructure;

namespace Newtouch.CIS.Proxy.CMMPlatform
{
    /// <summary>
    /// 推送患者信息
    /// </summary>
    internal class TcmHis01Proxy : GuiAnTelemedicinePlatformProxy<PushPatientRequestEntity, PushPatientResponseEntity>
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        public override PushPatientResponseEntity Execute(PushPatientRequestEntity requestDto)
        {
            var requestXml = requestDto.XmlSerialize();
            requestXml = requestXml.Substring(requestXml.IndexOf('>') + 1);
            string responseXml;
            try
            {
                responseXml = Monitoring.RequestMonitor(_ => new TcmHis01ServiceReference._Proxy70Client().acceptMessage(requestXml), requestDto, "TcmHis01Proxy-Execute");
                if (!responseXml.IsReturnSuccess()) throw new Exception("推送患者信息接口调用失败");
                return new PushPatientResponseEntity
                {
                    Result = new Result
                    {
                        code = "1",
                        desc = ""
                    }
                };
            }
            catch (Exception e)
            {
                return new PushPatientResponseEntity
                {
                    Result = new Result
                    {
                        code = "0",
                        desc = e.Message + (e.InnerException == null ? "" : e.InnerException.Message)
                    }
                };
            }
        }
    }
}