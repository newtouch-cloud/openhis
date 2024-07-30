using Newtouch.EMR.Domain.Entity;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Infrastructure.EF;

namespace Newtouch.EMR.Domain.IRepository
{
    /// <summary>
    /// 创 建：hyj
    /// 日 期：2018-10-12 17:43
    /// 描 述：元素表
    /// </summary>
    public interface IBlysRepo : IRepositoryBase<BlysEntity>
    {
        List<BlysEntity> GetYsTree(string orgId, string yssjid);
        List<BlysEntity> GetYsTreeV2(string orgId, string keyword);
        List<BlysMXEntity> GetYsMX(string orgId, string YsId);
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        void SubmitForm(BlysEntity entity, string keyValue);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        void DeleteForm(string keyValue);

    }
}