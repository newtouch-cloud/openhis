using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Domain.Entity.PACS;
using NewtouchHIS.Lib.Base;

namespace NewtouchHIS.Domain.IDomainService.PACS
{
    public interface IPacsDmnService : IScopedDependency
    {
        /// <summary>
        /// 收费项目
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<List<PacsFeeitemVEntity>> PacsFeeitemQuery(string orgId, string? keyword);
    }
}
