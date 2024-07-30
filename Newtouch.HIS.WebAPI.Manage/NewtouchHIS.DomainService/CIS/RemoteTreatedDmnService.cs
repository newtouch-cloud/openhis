using Mapster;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Base.DomainService;
using NewtouchHIS.Domain.Entity.RemoteTreated;
using NewtouchHIS.Domain.IDomainService.CIS;
using NewtouchHIS.Domain.InterfaceObjets.CIS;
using NewtouchHIS.Lib.Base.Exceptions;
using static NewtouchHIS.Lib.Common.HisEnum;

namespace NewtouchHIS.DomainService.CIS
{
    public class RemoteTreatedDmnService : BaseServices<TreatedApplyEntity>, IRemoteTreatedDmnService
    {
        private readonly ISysStaffDmnService _sysStaffDmn;
        private readonly ISysDepartmentDmnService _sysDepartmentDmn;
        public RemoteTreatedDmnService(ISysDepartmentDmnService sysDepartment, ISysStaffDmnService sysStaff)
        {
            _sysStaffDmn = sysStaff;
            _sysDepartmentDmn = sysDepartment;
        }
        #region IDU 操作
        public async Task<string> AddTreatedApply(TreatedApplyVO applyVO, string user)
        {
            applyVO.sqzt = (int)EmunRemoteTreatedStu.dqr;
            if (string.IsNullOrWhiteSpace(applyVO.OrganizeId))
            {
                throw new FailedException("机构信息不可为空");
            }
            if (string.IsNullOrWhiteSpace(applyVO.sqlsh))
            {
                throw new FailedException("远程诊疗申请唯一流水号不可为空");
            }
            if (DateTime.Compare(applyVO.sqsj ?? DateTime.Now, DateTime.Now.Date) < 0)
            {
                throw new FailedException("申请诊疗时间异常");
            }
            var lshcheck = await baseDal.GetByWhere(p => p.sqlsh == applyVO.sqlsh && p.OrganizeId == applyVO.OrganizeId && p.zt == "1");
            if (lshcheck.Count > 0)
            {
                throw new FailedException("该申请已发送，请勿重复操作");
            }
            applyVO.Id = Guid.NewGuid().ToString();
            applyVO.NewEntity(applyVO.OrganizeId, user);
            string errorMsg = string.Empty;
            if (!ValidRequired(applyVO, out errorMsg))
            {
                throw new FailedException(errorMsg);
            }
            TreatedApplyEntity entity = applyVO.Adapt<TreatedApplyEntity>();
            var result = await baseDal.Add(entity);
            if (result <= 0)
            {
                throw new FailedException("申请信息接收异常，请重试");
            }
            return entity.Id;
        }
        /// <summary>
        /// 修改申请单状态、会议号（仅就诊中&待确认时）
        /// </summary>
        /// <param name="applyEntity"></param>
        /// <returns></returns>
        /// <exception cref="FailedException"></exception>
        public async Task<bool> ModifiedApplyStu(TreatedApplyEntity applyEntity, string user)
        {
            if (string.IsNullOrWhiteSpace(applyEntity.Id) || string.IsNullOrWhiteSpace(applyEntity.sqlsh))
            {
                throw new FailedException("关键申请Id不可为空");
            }
            var entity = await baseDal.GetFirstOrDefault(p => p.Id == applyEntity.Id && p.OrganizeId == applyEntity.OrganizeId && p.sqlsh == applyEntity.sqlsh && p.zt == "1");
            if (entity == null || string.IsNullOrWhiteSpace(entity.Id))
            {
                throw new FailedException("诊疗申请信息异常，确认申请Id与申请流水号是否匹配");
            }
            switch (entity.sqzt)
            {
                case (int)EmunRemoteTreatedStu.dqr:
                    entity.sqzt = applyEntity.sqzt;
                    if (applyEntity.sqzt == (int)EmunRemoteTreatedStu.dqr || applyEntity.sqzt == (int)EmunRemoteTreatedStu.jzz)
                    {
                        entity.mzh = applyEntity.mzh;
                        entity.mettingId = string.IsNullOrWhiteSpace(applyEntity.mettingId) ? entity.mettingId : applyEntity.mettingId;
                        entity.jzsj = DateTime.Now;
                    }
                    break;
                case (int)EmunRemoteTreatedStu.jzz:
                    //更新会议号
                    if (applyEntity.sqzt == (int)EmunRemoteTreatedStu.jzz)
                    {
                        entity.mzh = applyEntity.mzh;
                        entity.mettingId = string.IsNullOrWhiteSpace(applyEntity.mettingId) ? entity.mettingId : applyEntity.mettingId;
                    }
                    //就诊结束
                    else if (applyEntity.sqzt == (int)EmunRemoteTreatedStu.yjs)
                    {
                        entity.sqzt = applyEntity.sqzt;
                    }
                    else
                    {
                        throw new FailedException($"诊疗就诊中");
                    }
                    break;
                case (int)EmunRemoteTreatedStu.yjs:
                    if (applyEntity.sqzt == (int)EmunRemoteTreatedStu.yfy)
                    {
                        entity.sqzt = applyEntity.sqzt;
                    }
                    else
                    {
                        throw new FailedException($"申请状态更新异常，本次诊疗已结束");
                    }
                    break;
                case (int)EmunRemoteTreatedStu.yth:
                    throw new FailedException($"申请状态更新异常，本次诊疗申请因驳回作废");
                case (int)EmunRemoteTreatedStu.ycx:
                    throw new FailedException($"申请状态更新异常，本次诊疗申请已撤销");
                case (int)EmunRemoteTreatedStu.yfy:
                    throw new FailedException($"申请状态更新异常，本次诊疗过程结束，药房已发药");
            }
            entity.ModifiedEntity(applyEntity.OrganizeId, user);
            return await baseDal.UpdateAsync(entity);
        }


        #endregion

        public async Task<bool> TreatedApplyStu(string applyId, string orgId)
        {
            var entity = await baseDal.FindKey(applyId);
            if (entity == null || entity.zt != "1")
            {
                throw new FailedException("诊疗申请信息异常，请重新发送申请");
            }
            if (entity.OrganizeId != orgId)
            {
                throw new FailedException("诊疗申请信息机构校验异常，请重新确认申请Id");
            }
            switch (entity.sqzt)
            {
                case (int)EmunRemoteTreatedStu.dqr:
                case (int)EmunRemoteTreatedStu.jzz:
                    return true;
            }
            return false;
        }


        public async Task<TreatedApplyVO> TreatedApplyInfo(string applyId, string orgId, string? sqlsh = null)
        {
            TreatedApplyEntity ety = new TreatedApplyEntity();
            if (!string.IsNullOrWhiteSpace(applyId))
            {
                ety = await baseDal.GetFirstOrDefault(p => p.Id == applyId && p.OrganizeId == orgId && p.zt == "1");
            }
            else if (!string.IsNullOrWhiteSpace(sqlsh))
            {
                ety = await baseDal.GetFirstOrDefault(p => p.sqlsh == sqlsh && p.OrganizeId == orgId && p.zt == "1");
            }
            if (ety == null)
            {
                throw new FailedException("诊疗申请信息异常，请重新发送申请");
            }
            return ety.Adapt<TreatedApplyVO>();
        }
        public async Task<TreatedApplyExtendVO> TreatedApplyExtendInfo(string applyId, string orgId)
        {
            var entity = await baseDal.FindKey(applyId);
            if (entity == null || entity.zt != "1")
            {
                throw new FailedException("诊疗申请信息异常，请重新发送申请");
            }
            if (entity.OrganizeId != orgId)
            {
                throw new FailedException("诊疗申请信息机构校验异常，请重新确认申请Id");
            }
            var extendVo = entity.Adapt<TreatedApplyExtendVO>();
            extendVo.ksmc = await _sysDepartmentDmn.GetNameByCode(entity.ks, entity.OrganizeId);
            extendVo.ysxm = (await _sysStaffDmn.GetStaffDeptByGh(entity.ysgh, entity.OrganizeId))?.FirstOrDefault()?.xm;
            return extendVo;
        }
    }
}
