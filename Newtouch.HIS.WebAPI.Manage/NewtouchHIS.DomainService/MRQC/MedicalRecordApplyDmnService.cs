using Mapster;
using NewtouchHIS.Base.DomainService;
using NewtouchHIS.Domain.Entity.MRQC;
using NewtouchHIS.Domain.IDomainService.MRQC;
using NewtouchHIS.Domain.InterfaceObjets.MRQC;
using NewtouchHIS.Lib.Base.EnumExtend;
using NewtouchHIS.Lib.Base.Exceptions;
using static NewtouchHIS.Domain.Enum.MrqcEnum;

namespace NewtouchHIS.DomainService.MRQC
{
    public class MedicalRecordApplyDmnService : BaseDmnService<MRQCScoreVO>, IMedicalRecordApplyDmnService
    {
        /// <summary>
        /// 质控病历申请
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        /// <exception cref="FailedException"></exception>
        public async Task<MedicalBlApplyResponse> AddMedicalApplyRecord(MedicalBlApplyVo vo, DBEnum db = DBEnum.MrQcDb)
        {
            string errorMsg = string.Empty;
            if (!ValidRequired(vo, out errorMsg))
            {
                throw new FailedException(errorMsg);
            }
            var exists = await GetFirstOrDefaultWithAttr<MrApplyEntity>(p => p.Zyh == vo.Zyh && p.OrganizeId == vo.OrganizeId && p.zt == "1"
               && p.ApplyDept == vo.ApplyDept && p.ApplyDoctor == vo.ApplyDoctor && p.MedicalName == vo.MedicalName && p.ApplyType == vo.ApplyType
               && p.ApplyStatus == (int)ApplyStatusEnum.wpz);
            if (exists != null)
            {
                throw new FailedException("质控病历文书已申请,不可重复申请");
            }
            var entity = vo.Adapt<MrApplyEntity>();
            entity.NewEntity(vo.OrganizeId, vo.CreatorCode);
            entity.ApplyStatus = (int)ApplyStatusEnum.wpz;
            entity.Id = Guid.NewGuid().ToString();

            var sysconfig = await GetFirstOrDefaultWithAttr<SysConfigEntity>(p => p.OrganizeId == vo.OrganizeId && p.zt == "1"
            && p.Code == "BlApplyApproveSwitch");
            if (sysconfig != null)
            {
                if (sysconfig.Value == "ON")
                {
                    entity.ApplyStatus = (int)ApplyStatusEnum.ysp;
                    entity.Approver = "admin";
                    entity.ApprovalDate = DateTime.Now;
                    entity.ApproverDept = "质控办";
                }
                else
                {
                    entity.ApplyStatus = (int)ApplyStatusEnum.wpz;
                }
            }
            var result = await AddWithAttr(entity);
            MedicalBlApplyResponse resp = new MedicalBlApplyResponse();
            if (result > 0)
            {
                resp.Id = entity.Id;
                resp.ApplyStatus = entity.ApplyStatus;
                resp.ApproveDate = DateTime.Now;
                resp.Approver = entity.Approver;
                resp.ApproverDept = entity.ApplyDept;
            }
            return resp;
        }
    }
}
