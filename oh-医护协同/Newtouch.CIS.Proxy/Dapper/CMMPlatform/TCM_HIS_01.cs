using System;
using Newtouch.CIS.Proxy.CMMPlatform.DTO;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_01;

namespace Newtouch.CIS.Proxy.Dapper.CMMPlatform
{
    /// <summary>
    /// 推送患者信息
    /// </summary>
    public class TcmHis01 : ProxyDapperBase<PushPatientRequestEntity, PushPatientResponseEntity, Result>
    {
        private Patient _patient;

        public TcmHis01(Patient patient)
        {
            _patient = patient;
        }

        public override void BuildProxyRequest()
        {
            var sendTimeStr = DateTime.Now.ToString("yyyyMMddHHmmss");
            var sender = "HIS";
            Request = new PushPatientRequestEntity
            {
                Header = new Header
                {
                    sender = sender,
                    receiver = "EMR,HEAL,PLAT",
                    sendTime = sendTimeStr,
                    msgType = "TCM_HIS_01",
                    msgID = string.Format("{0}{1}", sender, sendTimeStr)
                },
                Patient = _patient
            };
        }

        public override void ExecuteProxy()
        {
            Response = CmmProxyCenter<PushPatientRequestEntity, PushPatientResponseEntity>.Execute(Request);
        }

        public override Result ExtractProxyResponse()
        {
            return Response == null ? null : Response.Result;
        }
    }
}