using System;
using Newtouch.CIS.Proxy.CMMPlatform.DTO;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_08;
using ReceiveRequestEntity = Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_08.ReceiveRequestEntity;

namespace Newtouch.CIS.Proxy.Dapper.CMMPlatform
{
    /// <summary>
    /// 接收处方信息
    /// </summary>
    public class TcmHis08 : ProxyDapperBase<ReceiveRequestEntity, PullPrescriptionRequestEntity, Prescription>
    {
        private readonly Receive _receive;

        public TcmHis08(Receive receive)
        {
            _receive = receive;
        }

        public override void BuildProxyRequest()
        {
            var sendTimeStr = DateTime.Now.ToString("yyyyMMddHHmmss");
            var sender = "HIS";
            Request = new ReceiveRequestEntity
            {
                Header = new Header
                {
                    sender = sender,
                    receiver = "AE",
                    sendTime = sendTimeStr,
                    msgType = "TCM_HIS_08",
                    msgID = string.Format("{0}{1}", sender, sendTimeStr)
                },
                Receive = _receive
            };
        }

        public override void ExecuteProxy()
        {
            Response = CmmProxyCenter<ReceiveRequestEntity, PullPrescriptionRequestEntity>.Execute(Request);
        }

        public override Prescription ExtractProxyResponse()
        {
            return Response == null ? null : Response.Prescription;
        }
    }
}