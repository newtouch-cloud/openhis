using Newtouch.HIS.Domain.BusinessObjects;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 一些不知道往哪放的sql查询，放在此处
    /// </summary>
    public interface ICommonDmnService
    {
        /// <summary>
        /// 获取就诊人数（门诊和住院）
        /// </summary>
        VisitNumBO GetVisitNum(bool isAdministrator, string orgId = null, string topOrgId = null);
    }
}
