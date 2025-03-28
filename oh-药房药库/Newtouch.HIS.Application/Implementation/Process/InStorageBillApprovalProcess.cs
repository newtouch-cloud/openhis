using System;
using System.Collections.Generic;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using Newtouch.HIS.Domain.DTO.OutOrInStoredOperate;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.PDS.Requset;

namespace Newtouch.HIS.Application.Implementation.Process
{
    /// <summary>
    /// 外部入库审核
    /// </summary>
    internal class InStorageBillApprovalProcess : ProcessorFun<OutOrInStorageBillApprovalDTO>
    {
        private readonly ISysPharmacyDepartmentMedicineRepo _sysPharmacyDepartmentMedicineRepo;
        private readonly IBaseDataDmnService baseDataDmnService;
        private readonly IInStorageDmnService inStorageDmnService;
        private readonly ISysMedicineStorageIOReceiptRepo _crkdj;

        internal InStorageBillApprovalProcess(OutOrInStorageBillApprovalDTO request) : base(request)
        {
        }

        /// <summary>
        /// 主处理
        /// </summary>
        /// <returns></returns>
        protected override void Action(ActResult actResult)
        {
            switch (Convert.ToInt32(Request.shzt))
            {
                case (int)EnumDjShzt.Approved:
                    var approvalResult = inStorageDmnService.InStorageAdopt(Request.dj, AssembleKcxx(), Request.auditor);
                    if (!string.IsNullOrWhiteSpace(approvalResult))
                    {
                        actResult.IsSucceed = false;
                        actResult.ResultMsg = approvalResult;
                    }
                    break;
                case (int)EnumDjShzt.Cancelled:
                    var cancelResult = inStorageDmnService.InStorageCancel(Request.dj, Request.djmx, Request.auditor);
                    if (!string.IsNullOrWhiteSpace(cancelResult))
                    {
                        actResult.IsSucceed = false;
                        actResult.ResultMsg = cancelResult;
                    }
                    Request.dj.shzt = ((int)EnumDjShzt.Cancelled).ToString();
                    Request.dj.Shczy = Request.auditor;
                    Request.dj.Modify();
                    if (_crkdj.Update(Request.dj) <= 0)
                    {
                        actResult.IsSucceed = false;
                        actResult.ResultMsg = "修改单据状态失败";
                    }
                    break;
                case (int)EnumDjShzt.Rejected:
                    Request.dj.Rksj = DateTime.Now;
                    Request.dj.shzt = ((int)EnumDjShzt.Rejected).ToString();
                    Request.dj.Shczy = Request.auditor;
                    Request.dj.Modify();
                    if (_crkdj.Update(Request.dj) <= 0)
                    {
                        actResult.IsSucceed = false;
                        actResult.ResultMsg = "修改单据状态失败";
                    }
                    break;
                default:
                    actResult.IsSucceed = false;
                    actResult.ResultMsg = "未找到匹配的操作";
                    break;
            }
        }

        /// <summary>
        /// 组装库存信息
        /// </summary>
        /// <returns></returns>
        private List<SysMedicineStockInfoEntity> AssembleKcxx()
        {
            var result = new List<SysMedicineStockInfoEntity>();
            Request.djmx.ForEach(p =>
            {
                var bzs = baseDataDmnService.GetYpDetails(OrganizeId, p.Ypdm).bzs;
                var item = new SysMedicineStockInfoEntity
                {
                    kcId = Guid.NewGuid().ToString(),
                    OrganizeId = OrganizeId,
                    yfbmCode = Request.dj.Rkbm,
                    ypdm = p.Ypdm,
                    ph = p.Ph,
                    pc = p.pc,
                    yxq = p.Yxq,
                    kcsl = p.Sl * p.Rkzhyz,
                    djsl = 0,
                    ypkw = _sysPharmacyDepartmentMedicineRepo.GetYpkw(p.Ypdm, Request.dj.Rkbm, OrganizeId),  //部门药品信息
                    kzbz = 0,
                    tybz = 0,
                    crkmxId = p.crkmxId,
                    jj = p.jj, //外部入库单只用药库单位，jj保存的就是药库单位进价
                    zhyz = p.Rkzhyz,
                    cd = p.cd, //产地目录
                };
                item.Create();
                result.Add(item);
            });
            return result;
        }
    }
}
