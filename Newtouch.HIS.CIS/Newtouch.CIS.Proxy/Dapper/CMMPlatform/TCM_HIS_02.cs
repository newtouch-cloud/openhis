using System;
using Newtouch.CIS.Proxy.CMMPlatform.DTO;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_02;

namespace Newtouch.CIS.Proxy.Dapper.CMMPlatform
{
    /// <summary>
    /// 推送草药信息
    /// </summary>
    public class TcmHis02 : ProxyDapperBase<PushMedicineInfoRequestEntity, PushMedicineInfoResponseEntity, Result>
    {
        private readonly Medicine _medicine;

        public TcmHis02(Medicine medicine)
        {
            _medicine = medicine;
        }

        public override void BuildProxyRequest()
        {
            var sendTimeStr = DateTime.Now.ToString("yyyyMMddHHmmss");
            var sender = "HIS";
            Request = new PushMedicineInfoRequestEntity
            {
                Header = new Header
                {
                    sender = sender,
                    receiver = "PLAT,AE",
                    sendTime = sendTimeStr,
                    msgType = "TCM_HIS_02",
                    msgID = string.Format("{0}{1}", sender, sendTimeStr)
                },
                Medicine = _medicine
            };
        }

        public override void ExecuteProxy()
        {
            Response = CmmProxyCenter<PushMedicineInfoRequestEntity, PushMedicineInfoResponseEntity>.Execute(Request);
        }

        public override Result ExtractProxyResponse()
        {
            return Response == null ? null : Response.Result;
        }
    }
}