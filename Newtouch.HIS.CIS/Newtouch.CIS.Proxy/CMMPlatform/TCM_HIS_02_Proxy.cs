using System;
using Newtouch.CIS.Proxy.CMMPlatform.DTO;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_02;
using Newtouch.Infrastructure;

namespace Newtouch.CIS.Proxy.CMMPlatform
{
    /// <summary>
    /// 推送草药信息
    /// </summary>
    public class TcmHis02Proxy : GuiAnTelemedicinePlatformProxy<PushMedicineInfoRequestEntity, PushMedicineInfoResponseEntity>
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        public override PushMedicineInfoResponseEntity Execute(PushMedicineInfoRequestEntity requestDto)
        {
            var requestXml = requestDto.XmlSerialize();
            requestXml = requestXml.Substring(requestXml.IndexOf('>') + 1);
            try
            {
                var responseXml = Monitoring.RequestMonitor(_ => new TcmHis02ServiceReference._Proxy70Client().acceptMessage(requestXml), requestDto, "TcmHis02Proxy-Execute");
                if (!responseXml.IsReturnSuccess()) throw new Exception("推送患者信息接口调用失败");
                return new PushMedicineInfoResponseEntity
                {
                    Result = new Result
                    {
                        code = "1",
                        desc = "成功"
                    }
                };
            }
            catch (Exception e)
            {
                return new PushMedicineInfoResponseEntity
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