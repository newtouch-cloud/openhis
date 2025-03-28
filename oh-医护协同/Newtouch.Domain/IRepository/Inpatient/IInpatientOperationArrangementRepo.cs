using Newtouch.Domain.Entity;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// 创 建：周珏琦
    /// 日 期：2019-01-30 10:23
    /// 描 述：手术安排表
    /// </summary>
    public interface IInpatientOperationArrangementRepo : IRepositoryBase<InpatientOperationArrangementEntity>
    {
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        bool SubmitForm(InpatientOperationArrangementEntity entity, string keyValue);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        void DeleteForm(string keyValue);

    }
}