using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.HIS.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IRepository.SystemManage
{
    public interface ISysUserRepo
    {
        /// <summary>
        /// 返回一个实体
        /// </summary>
        /// <param name="topOrgId"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        SysUserVEntity GetEntity(string topOrgId, string account);
    }
}
