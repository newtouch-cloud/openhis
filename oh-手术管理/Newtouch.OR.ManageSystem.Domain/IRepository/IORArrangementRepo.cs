using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;
using Newtouch.Core.Common;

namespace Newtouch.OR.ManageSystem.Domain.IRepository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2019-11-06 10:22
    /// 描 述：手术排班记录
    /// </summary>
    public interface IORArrangementRepo : IRepositoryBase<ORArrangementEntity>
    {
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        int SubmitForm(ORArrangementEntity entity, string ApplyId,string ArrangeId);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        int DeleteForm(string keyValue);

        IList<ORArrangementEntity> GetPagintionListForRegistration(Pagination pagination, string keyword, string bq,string OrganizeId);
        int UpdateSqzt(string keyValue, string value);
    }
}