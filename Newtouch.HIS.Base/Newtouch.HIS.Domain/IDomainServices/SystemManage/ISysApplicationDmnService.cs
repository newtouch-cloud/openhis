using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysApplicationDmnService
    {
        /// <summary>
        /// 获取应用已授权的组织机构列表
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        IList<SysOrganizeEntity> GetAuthedOrgListByAppId(string appId);

        /// <summary>
        /// 将应用授权给指定机构
        /// </summary>
        /// <param name="appId"></param>
        void UpdateAuthOrganizeList(string appId, string orgList, string curUserCode);

        /// <summary>
        /// 将应用授权给所有机构
        /// </summary>
        /// <param name="appId"></param>
        void AuthAllOrganize(string appId, string curUserCode);

        /// <summary>
        /// 撤销全部授权（组织机构）
        /// </summary>
        /// <param name="appId"></param>
        void AuthCancelAllOrganize(string appId);

    }
}
