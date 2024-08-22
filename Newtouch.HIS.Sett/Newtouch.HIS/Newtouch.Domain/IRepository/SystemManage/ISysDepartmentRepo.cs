using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.Entity;

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
        IList<SysDepartmentVEntity> GetList(string orgId, string keyword = null);

        /// <summary>
        /// 根据Code获取科室
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        SysDepartmentVEntity GetByCode(string code, string orgId);

        /// <summary>
        /// 根据Name获取Code
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetCodeByName_FirstOrDefault(string name, string orgId);

        /// <summary>
        /// 根据Code获取名称
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetNameByCode(string code, string orgId);

    }
}
