using System;
using System.Collections.Generic;
using Newtouch.CIS.Proxy.CMMPlatform;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.AERequest;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.EMRRequest;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.HEALRequest;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.KRequest;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_01;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_02;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_07;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_08;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_09;
using Newtouch.Infrastructure;
using Newtouch.Infrastructure.Log;
using ReceiveRequestEntity = Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_07.ReceiveRequestEntity;

namespace Newtouch.CIS.Proxy
{
    /// <summary>
    /// 中医馆接口调用中心
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TV"></typeparam>
    public class CmmProxyCenter<T, TV> where T : class
    {

        private static readonly IDictionary<object, object> ProxyMappingDic;

        static CmmProxyCenter()
        {
            ProxyMappingDic = new Dictionary<object, object>
            {
                { typeof(GuiAnTelemedicinePlatformProxy<PushPatientRequestEntity, PushPatientResponseEntity>), new TcmHis01Proxy()},
                { typeof(GuiAnTelemedicinePlatformProxy<ReceiveRequestEntity, PullRecordRequestEntity>), new  TcmHis07Proxy()},
                { typeof(GuiAnTelemedicinePlatformProxy<CMMPlatform.DTO.TCM_HIS_08.ReceiveRequestEntity, PullPrescriptionRequestEntity>), new  TcmHis08Proxy()},
                { typeof(GuiAnTelemedicinePlatformProxy<CMMPlatform.DTO.TCM_HIS_09.ReceiveRequestEntity, PullDiagInfoRequestEntity>), new TcmHis09Proxy()},
                { typeof(GuiAnTelemedicinePlatformProxy<EMRRequestEntity, string>), new EMRRequestProxy()},
                { typeof(GuiAnTelemedicinePlatformProxy<AERequestEntity, string>), new AERequestProxy()},
                { typeof(GuiAnTelemedicinePlatformProxy<PushMedicineInfoRequestEntity, PushMedicineInfoResponseEntity>), new TcmHis02Proxy()},
                { typeof(GuiAnTelemedicinePlatformProxy<KRequestEntity, string>), new KRequestProxy()},
                { typeof(GuiAnTelemedicinePlatformProxy<HEALRequestEntity, string>), new HEALRequestProxy()}
                //
            };

        }

        /// <summary>
        /// 执行proxy
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        public static TV Execute(T requestDto)
        {
            try
            {
                var proxy = (GuiAnTelemedicinePlatformProxy<T, TV>)ProxyMappingDic[typeof(GuiAnTelemedicinePlatformProxy<T, TV>)];
                var logMsg = "Execute-" + typeof(T).FullName;
                return Monitoring.Running(() => proxy.Execute(requestDto), logMsg, logMsg);
            }
            catch (Exception e)
            {
                LogCore.Error("CmmProxyCenter Execute error", e, "The Proxy is not registered");
                throw;
            }
        }
    }
}