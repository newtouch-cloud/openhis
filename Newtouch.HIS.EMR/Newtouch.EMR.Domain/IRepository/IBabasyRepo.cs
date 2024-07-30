using Newtouch.EMR.Domain.Entity;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Infrastructure.EF;
using Newtouch.EMR.Domain.BusinessObjects;

namespace Newtouch.EMR.Domain.IRepository
{
    /// <summary>
    /// 创 建：hyj
    /// 日 期：2018-09-20 18:23
    /// 描 述：选项明细表
    /// </summary>
    public interface IBabasyRepo : IRepositoryBase<BabasyEntity>
    {
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        void SubmitForm(BabasyVO entity, string keyValue);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        void DeleteForm(string keyValue);

    }
}