using System;
using Newtouch.CIS.Proxy.CMMPlatform.DTO;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_07;

namespace Newtouch.CIS.Proxy.Dapper.CMMPlatform
{
    /// <summary>
    /// 提取电子病历
    /// </summary>
    public class TcmHis07 : ProxyDapperBase<ReceiveRequestEntity, PullRecordRequestEntity, Record>
    {
        private readonly Receive _receive;

        public TcmHis07(Receive receive)
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
                    msgType = "TCM_HIS_07",
                    msgID = string.Format("{0}{1}", sender, sendTimeStr)
                },
                Receive = _receive
            };
        }

        public override void ExecuteProxy()
        {
            Response = CmmProxyCenter<ReceiveRequestEntity, PullRecordRequestEntity>.Execute(Request);
        }

        public override Record ExtractProxyResponse()
        {
            return Response == null ? null : Response.Record;
        }
    }
}