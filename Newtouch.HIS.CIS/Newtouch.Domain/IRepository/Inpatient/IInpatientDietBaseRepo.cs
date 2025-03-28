using Newtouch.Core.Common;
using Newtouch.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;

namespace Newtouch.Domain.IRepository
{

    public interface IInpatientDietBaseRepo : IRepositoryBase<InpatientDietBaseEntity>
    {
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        void SubmitForm(InpatientDietBaseEntity entity, string keyValue);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        void DeleteForm(string keyValue);

        IList<InpatientDietBaseEntity> GetGridList(Pagination pagination, string lb, string keyword, string orgId);

    }
}