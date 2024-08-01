using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Infrastructure.EF;
using FrameworkBase.Domain.Entity;

namespace FrameworkBase.Domain.IRepository
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-17 16:13
    /// 描 述：系统角色
    /// </summary>
    public interface ISysRoleRepo : IRepositoryBase<SysRoleEntity>
    {
        /// <summary>
        /// 获取分页实体列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysRoleEntity> GetPaginationList(Pagination pagination, string keyword);

        /// <summary>
        /// 获取有效实体列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysRoleEntity> GetValidList(string keyword = null);

    }
}