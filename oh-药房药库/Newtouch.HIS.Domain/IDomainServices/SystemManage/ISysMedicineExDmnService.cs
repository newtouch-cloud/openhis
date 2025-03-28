using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 药品信息
    /// </summary>
    public interface ISysMedicineExDmnService
    {
        /// <summary>
        /// 查询药品详情
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        SysMedicineComplexVEntity GetYpDetails(string orgId, string ypCode);
    }
}