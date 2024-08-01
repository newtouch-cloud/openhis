using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;

namespace FrameworkBase.MultiOrg.Domain.IRepository
{
    /// <summary>
    /// 系统科室
    /// </summary>
    public interface ISysDepartmentRepo : IRepositoryBase<SysDepartmentVEntity>
    {
        /// <summary>
        /// 获取科室列表
        /// </summary>
        /// <param name="orgId">医疗机构</param>
        /// <param name="zt">有效标志。0无效1有效null、Empty所有</param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysDepartmentVEntity> GetList(string orgId, string zt = null, string keyword = null);

        /// <summary>
        /// 根据Code获取科室名称
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetNameByCode(string code, string orgId);

        /// <summary>
        /// 根据Code获取科室实体
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        SysDepartmentVEntity GetEntityByCode(string code, string orgId);

        /// <summary>
        /// 根据Name获取Code
        /// </summary>
        /// <param name="name"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetCodeByNameFirstOrDefault(string name, string orgId);

    }
}
