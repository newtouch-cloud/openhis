using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Base.Domain.IDomainService
{
    /// <summary>
    /// 系统菜单 （增删改接口）
    /// </summary>
    public interface IRoleAuthorizeApp : IScopedDependency
    {
        /// <summary>
        /// 获取角色授权列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<List<SysRoleAuthorizeEntity>> GetValidList(string roleId);
    }
}
