using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkBase.MultiOrg.Domain.IDomainServices.SystemManage
{
    /// <summary>
    /// 复杂查询
    /// </summary>
    public interface ISysConfigDmnService
    {
        /// <summary>
        /// 合并模板查询
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<SysConfigEntity> GetAllConfigs(string keyword, string orgId);
        /// <summary>
        /// 根据Id查询配置明细
        /// </summary>
        /// <param name="configId"></param>
        /// <returns></returns>
        IList<SysConfigEntity> GetConfigForm(string configId);
    }
}
