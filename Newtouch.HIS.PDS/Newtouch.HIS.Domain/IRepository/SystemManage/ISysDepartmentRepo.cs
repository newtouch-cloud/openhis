using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysDepartmentRepo : IRepositoryBase<SysDepartmentVEntity>
    {
        /// <summary>
        /// 获取机构科室列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<SysDepartmentVEntity> GetList(string orgId);

        /// <summary>
        /// 根据Code获取名称
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetNameByCode(string code, string orgId);
    }
}
