using Newtouch.HIS.Domain.Entity;
using System.Collections;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysUserRepo: IRepositoryBase<SysUserEntity>
    {
        /// <summary>
        /// 返回一个实体
        /// </summary>
        /// <param name="topOrgId"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        SysUserEntity GetEntity(string topOrgId, string account);
    }
}
