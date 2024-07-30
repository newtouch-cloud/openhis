using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysStaffRepo : IRepositoryBase<SysStaffEntity>
    {
        /// <summary>
        /// 获取机构下所以系统人员（不包括子机构）
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        IList<SysStaffEntity> GetsatffListByOrg(string OrganizeId);

        /// <summary>
        /// 获取管理人员Id列表 根据UserId
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        IList<string> GetCurStaffIdListByUserId(string UserId);
        IList<SysStaffEntity> GetStaffList(string orgId, string keyword);
    }
}
