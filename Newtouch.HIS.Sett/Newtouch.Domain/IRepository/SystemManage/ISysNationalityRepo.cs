using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysNationalityRepo : IRepositoryBase<SysNationalityVEntity>
    {
        /// <summary>
        /// 获取有效国籍
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysNationalityVEntity> GetgjList();
    }
}
