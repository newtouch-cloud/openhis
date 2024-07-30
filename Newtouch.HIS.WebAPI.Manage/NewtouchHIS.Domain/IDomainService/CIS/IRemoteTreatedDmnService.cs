using NewtouchHIS.Domain.Entity.RemoteTreated;
using NewtouchHIS.Domain.InterfaceObjets.CIS;
using NewtouchHIS.Lib.Base;

namespace NewtouchHIS.Domain.IDomainService.CIS
{
    public interface IRemoteTreatedDmnService : IScopedDependency
    {
        /// <summary>
        /// 新增会诊申请
        /// </summary>
        /// <param name="applyVO"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<string> AddTreatedApply(TreatedApplyVO applyVO, string user);
        /// <summary>
        /// 更新会诊申请状态
        /// 要求ApplyId、sqlsh不可为空
        /// </summary>
        /// <param name="applyEntity"></param>
        /// <returns></returns>
        Task<bool> ModifiedApplyStu(TreatedApplyEntity applyEntity, string user);
        /// <summary>
        /// 查询会诊申请状态
        /// </summary>
        /// <param name="applyId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<bool> TreatedApplyStu(string applyId, string orgId);
        /// <summary>
        /// 会诊申请信息查询 by ApplyId
        /// </summary>
        /// <param name="applyId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<TreatedApplyVO> TreatedApplyInfo(string applyId, string orgId, string? sqlsh = null);
        /// <summary>
        /// 会诊申请详细信息查询  by ApplyId
        /// 含冗余信息
        /// </summary>
        /// <param name="applyId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<TreatedApplyExtendVO> TreatedApplyExtendInfo(string applyId, string orgId);
    }
}
