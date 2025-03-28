using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.ValueObjects;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 用户相关DmnService
    /// </summary>
    public interface ISysUserExDmnService
    {
        /// <summary>
        /// 根据UserId获取系统用户可操作的药房部门Code List
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<string> GetYfbmCodeListByUserId(string userId, string orgId);

    }
}
