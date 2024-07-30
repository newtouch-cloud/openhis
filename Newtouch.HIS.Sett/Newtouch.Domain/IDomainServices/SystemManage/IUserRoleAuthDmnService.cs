using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Common.Model;
using Newtouch.HIS.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 用户角色权限 公用DmnService
    /// </summary>
    public interface IUserRoleAuthDmnService
    {
        /// <summary>
        /// 获取用户已授权的角色列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orgId">医疗机构Id</param>
        /// <returns></returns>
        IList<SysRoleEntity> GetUserRoleList(string userId, string orgId);

        /// <summary>
        /// 获取角色 关联 用户（org）
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        IList<FirstSecond> GetCurUserIdListByRoleId(string roleId);


    }
}
