using System;
using System.Collections.Generic;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.DTO.OutOrInStoredOperate;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;
using Newtouch.PDS.Requset;

namespace Newtouch.HIS.Application.Implementation.Process
{
    /// <summary>
    /// 内部申领
    /// </summary>
    public class ApplyProcess : ProcessorFun<DjInfoDTO>
    {
        private SysMedicineRequisitionEntity sysMedicineRequisitionEntity;
        private List<SysMedicineRequisitionDetailEntity> sysMedicineRequisitionDetailEntities;
        private readonly IApplyDmnService applyDmnService;

        public ApplyProcess(DjInfoDTO request) : base(request)
        {
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        protected override ActResult Validata()
        {
            if (Request == null)
            {
                throw new FailedException("单据信息不能为空");
            }
            if (Request.mx == null || Request.mx.Count <= 0)
            {
                throw new FailedException("单据明细不能为空");
            }
            return new ActResult();
        }

        /// <summary>
        /// 预处理
        /// </summary>
        protected override void BeforeAction(ActResult actResult)
        {
            sysMedicineRequisitionEntity = new SysMedicineRequisitionEntity
            {
                sldId = Guid.NewGuid().ToString(),
                Ckbm = Request.ckbm,
                OrganizeId = OrganizeId,
                Sldh = Request.djh,
                Slbm = Request.rkbm,
                ffzt = (int)EnumSLDDeliveryStatus.None,
                zt = "1"
            };
            sysMedicineRequisitionEntity.Create(true, Guid.NewGuid().ToString());
            sysMedicineRequisitionDetailEntities = new List<SysMedicineRequisitionDetailEntity>();
            Request.mx.ForEach(p =>
            {
                var item = new SysMedicineRequisitionDetailEntity
                {
                    sldId = sysMedicineRequisitionEntity.sldId,
                    ypCode = p.ypdm,
                    slsl = p.sl * (int)p.rkzhyz,    //转换成最小单位，保存之
                    Zhyz = 1,
                    yfsl = 0,   //已发数量
                    zt = "1"
                };
                item.Create(true, Guid.NewGuid().ToString());
                sysMedicineRequisitionDetailEntities.Add(item);
            });
        }

        /// <summary>
        /// 主处理
        /// </summary>
        /// <returns></returns>
        protected override void Action(ActResult actResult)
        {
            var result = applyDmnService.SubmitApply(sysMedicineRequisitionEntity, sysMedicineRequisitionDetailEntities);
            if (string.IsNullOrWhiteSpace(result)) return;
            actResult.IsSucceed = false;
            actResult.ResultMsg = result;
        }
    }
}
