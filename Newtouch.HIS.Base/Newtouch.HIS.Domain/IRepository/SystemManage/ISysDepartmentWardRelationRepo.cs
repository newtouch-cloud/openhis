using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysDepartmentWardRelationRepo : IRepositoryBase<SysDepartmentWardRelationEntity>
    {
        /// <summary>
        /// 查找科室病区绑定信息
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        IList<SysDepartmentWardRelationEntity> GetDeptWardList(string deptId);
    }
}
