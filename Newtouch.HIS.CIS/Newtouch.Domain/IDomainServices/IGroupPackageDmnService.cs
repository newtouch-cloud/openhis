using System.Collections.Generic;
using Newtouch.Domain.BusinessObjects;
using Newtouch.Domain.Entity;

namespace Newtouch.Domain.IDomainServices
{
    public interface IGroupPackageDmnService
    {
        /// <summary>
        /// 组套项目
        /// </summary>
        /// <param name="ztId"></param>
        GroupPackageDetailBO GetGroupPackageDetail(string ztId, string orgId);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="ztobj"></param>
        /// <param name="ztxmlist"></param>
        void SaveData(GroupPackageEntity ztobj, List<GroupPackageItemEntity> ztxmlist);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ztId"></param>
        void DeleteData(string ztId);
    }
}
