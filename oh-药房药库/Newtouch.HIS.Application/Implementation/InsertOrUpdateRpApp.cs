using System.Collections.Generic;
using System.Linq;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.PDS.Requset;
using Newtouch.PDS.Requset.ResourcesOperate;
using Newtouch.Tools;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class InsertOrUpdateRpApp : ProcessorFun<InsertOrUpdateRpRequest>
    {
        private IOutpatientPrescriptionDetailBatchNumberRepo outpatientPrescriptionDetailBatchNumberRepo;

        public InsertOrUpdateRpApp(InsertOrUpdateRpRequest request) : base(request)
        {
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        protected override ActResult Validata()
        {
            if (Request.Rps == null) throw new FailedException("处方列表不能为空");
            return new ActResult();
        }

        /// <summary>
        /// 主处理
        /// </summary>
        /// <returns></returns>
        protected override void Action(ActResult actResult)
        {
            var failRps = new List<FailRp>();
            Request.Rps.ForEach(p =>
            {
                ActResult execResult;
                if (outpatientPrescriptionDetailBatchNumberRepo.GetList(p.Cfh, p.OrganizeId).Any())
                {
                    var tmp = p.ToJson();
                    var updateRpAppRequest = tmp.ToObject<UpdateRpRequset>();
                    execResult = new UpdateRpApp(updateRpAppRequest).Process();
                }
                else
                {
                    execResult = new OutpatientBookApp(AssembleBookAppRequest(p)).Process();
                }
                if (execResult.IsSucceed) return;
                var failRp = p as FailRp ?? new FailRp
                {
                    Cfh = p.Cfh,
                    CreatorCode = p.CreatorCode,
                    OrganizeId = p.OrganizeId,
                    Yfbm = p.Yfbm,
                    Cfmx = p.Cfmx,
                    FailMsg = execResult.ResultMsg
                };
                failRp.FailMsg = execResult.ResultMsg;
                failRps.Add(failRp);
            });
        }

        /// <summary>
        /// 组装预定报文
        /// </summary>
        /// <returns></returns>
        private OutpatientBookRequestDTO AssembleBookAppRequest(RpInfo rpInfo)
        {
            var item = new List<ItemDetail>();
            var par = new OutpatientBookRequestDTO
            {
                OrganizeId = rpInfo.OrganizeId,
                CreatorCode = rpInfo.CreatorCode,
                Items = item
            };
            rpInfo.Cfmx.ForEach(p =>
            {
                item.Add(new ItemDetail
                {
                    Cfh = rpInfo.Cfh,
                    DlCode = p.DlCode,
                    ItemCode = p.YpCode,
                    ItemCount = p.YpCount,
                    ItemName = p.YpName,
                    Yfbm = rpInfo.Yfbm
                });
            });
            return par;
        }
    }
}
