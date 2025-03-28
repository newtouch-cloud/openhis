using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 系统药品
    /// </summary>
    public interface ISysMedicinRepo
    {
        /// <summary>
        /// 根据药品Code获取dlCode
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        string GetdlCodeByYpCode(string orgId, string ypCode);

        /// <summary>
        /// 获取药品实体
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        SysMedicineVEntity GetEntityByCode(string orgId, string ypCode);
    }
}
