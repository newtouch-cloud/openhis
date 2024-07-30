using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOrganizeRepo : IRepositoryBase<OrganizeEntity>
    {
        /// <summary>
        /// 获取组织下的所有组织
        /// </summary>
        /// <returns></returns>
        List<OrganizeEntity> GetListByTopOrg(string topOrganizeId);

        /// <summary>
        /// 获取组织下的所有有效组织
        /// </summary>
        /// <returns></returns>
        List<OrganizeEntity> GetValidListByTopOrg(string topOrganizeId);

    }
}
