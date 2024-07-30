using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysItemsDetailRepo : IRepositoryBase<SysItemsDetailEntity>
    {
        /// <summary>
        /// 获取 组织机构 字典项List
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysItemsDetailEntity> GetListByTopOrg(string topOrganizeId, string itemId = null, string keyword = null);


    }
}
