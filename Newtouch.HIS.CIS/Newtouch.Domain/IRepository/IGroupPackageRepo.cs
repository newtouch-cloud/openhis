using Newtouch.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGroupPackageRepo : IRepositoryBase<GroupPackageEntity>
    {
        /// <summary>
        /// 组套列表
        /// </summary>
        /// <returns></returns>
        List<GroupPackageEntity> GetList(int type, string orgId, string keyword);
        List<GroupPackageEntity> GetList(int type, string orgId);
    }
}
