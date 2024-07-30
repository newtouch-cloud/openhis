using Newtouch.EMR.Domain.Entity;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Infrastructure.EF;

namespace Newtouch.EMR.Domain.IRepository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2018-08-30 11:31
    /// 描 述：住院患者信息
    /// </summary>
    public interface IZybrjbxxRepo : IRepositoryBase<ZybrjbxxEntity>
    {

        ZybrjbxxEntity GetZybrjbxx(string zyh, string OrganizeId);
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        void SubmitForm(ZybrjbxxEntity entity, string keyValue);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        void DeleteForm(string keyValue);
        /// <summary>
        /// 提交病案
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="OrgId"></param>
        /// <param name="user"></param>
        void CommitRecord(string zyh, string OrgId, string user);
        /// <summary>
        /// 退回病案
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="OrgId"></param>
        /// <param name="user"></param>
        void BackRecord(string zyh, string OrgId, string user);

    }
}