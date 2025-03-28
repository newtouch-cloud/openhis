using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.OutPatientPharmacy;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.PDS.Requset;
using Newtouch.PDS.Requset.ResourcesOperate;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 处方更新
    /// </summary>
    public class UpdateRpApp : ProcessorFun<UpdateRpRequset>
    {
        private readonly IOutpatientPrescriptionDetailBatchNumberDmnService outpatientPrescriptionDetailBatchNumberDmnService;
        private readonly IOutpatientPrescriptionDetailBatchNumberRepo outpatientPrescriptionDetailBatchNumberRepo;
        private readonly IfyDmnService fyDmnService;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="request"></param>
        public UpdateRpApp(UpdateRpRequset request) : base(request)
        {
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        protected override ActResult Validata()
        {
            if (Request.Cfmx == null) throw new FailedException("处方明细不能为空");
            if (string.IsNullOrWhiteSpace(Request.Cfh)) throw new FailedException("处方号 必填");
            Request.Cfmx.ForEach(p =>
            {
                if (string.IsNullOrWhiteSpace(p.YpCode)) throw new FailedException("ItemCode(项目编号) 必填");
            });
            if (string.IsNullOrWhiteSpace(Request.OrganizeId)) throw new FailedException("OrganizeId(组织结构代码) 必填");
            return new ActResult();
        }

        /// <summary>
        /// 预处理 先原处方药品归架
        /// </summary>
        protected override void BeforeAction(ActResult actResult)
        {
            fyDmnService.ReleaseFrozenStock(outpatientPrescriptionDetailBatchNumberRepo.GetList(Request.Cfh, Request.OrganizeId));
            outpatientPrescriptionDetailBatchNumberDmnService.UpdateGjztbyCfh(Request.Cfh, Request.OrganizeId);
        }

        /// <summary>
        /// 主处理
        /// </summary>
        /// <returns></returns>
        protected override void Action(ActResult actResult)
        {
            var result = new OutpatientBookApp(AssembleBookAppRequest()).Process();
            if (result.IsSucceed) return;
            actResult.IsSucceed = false;
            actResult.ResultMsg = string.Format(@"释放原来冻结库存成功，但新冻结库存失败。 \n 失败原因：{0}", result.ResultMsg);
        }

        /// <summary>
        /// 组装预定报文
        /// </summary>
        /// <returns></returns>
        private OutpatientBookRequestDTO AssembleBookAppRequest()
        {
            var item = new System.Collections.Generic.List<ItemDetail>();
            var par = new OutpatientBookRequestDTO
            {
                OrganizeId = Request.OrganizeId,
                CreatorCode = Request.CreatorCode,
                Items = item
            };
            Request.Cfmx.ForEach(p =>
            {
                item.Add(new ItemDetail
                {
                    Cfh = Request.Cfh,
                    DlCode = p.DlCode,
                    ItemCode = p.YpCode,
                    ItemCount = p.YpCount,
                    ItemName = p.YpName,
                    Yfbm = Request.Yfbm
                });
            });
            return par;
        }
    }
}
