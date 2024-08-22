/*******************************************************************************
 * Copyright © 2016 Newtouch 版权所有
 * Author: Newtouch
 * Description: 

*********************************************************************************/

using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 收费项目
    /// </summary>
    public interface ISysChargeItemRepo : IRepositoryBase<SysChargeItemVEntity>
    {
        /// <summary>
        /// 根据sfxm Code获取Entity
        /// </summary>
        /// <param name="sfxm"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        SysChargeItemVEntity SelectSysChargeItemBysfxm(string sfxm, string orgId);

        /// <summary>
        /// 根据编码获取dl
        /// </summary>
        /// <param name="sfxm"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetdlCodeBysfxm(string sfxm, string orgId);
    }
}
