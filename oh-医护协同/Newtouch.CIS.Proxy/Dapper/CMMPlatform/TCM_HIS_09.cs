using System;
using Newtouch.CIS.Proxy.CMMPlatform.DTO;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_09;

namespace Newtouch.CIS.Proxy.Dapper.CMMPlatform
{
    /// <summary>
    /// 提取 诊断信息
    /// </summary>
    public class TcmHis09 : ProxyDapperBase<ReceiveRequestEntity, PullDiagInfoRequestEntity, DiagInfo>
    {
        private readonly Receive _receive;

        public TcmHis09(Receive receive)
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
                    receiver = "EMR",
                    sendTime = sendTimeStr,
                    msgType = "TCM_HIS_09",
                    msgID = string.Format("{0}{1}", sender, sendTimeStr)
                },
                Receive = _receive
            };
        }

        public override void ExecuteProxy()
        {
            Response = CmmProxyCenter<ReceiveRequestEntity, PullDiagInfoRequestEntity>.Execute(Request);
        }

        public override DiagInfo ExtractProxyResponse()
        {
            return Response == null ? null : Response.DiagInfo;
        }
    }
}