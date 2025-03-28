using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;

namespace Newtouch.OR.ManageSystem.Domain.IRepository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2019-11-06 10:22
    /// 描 述：手术人员记录
    /// </summary>
    public interface IOROpStaffRecordRepo : IRepositoryBase<OROpStaffRecordEntity>
    {
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        int SubmitForm(OROpStaffRecordEntity entity, string keyValue);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        int DeleteForm(string keyValue);

        /// <summary>
        /// 根据手术登记号获取编号列表
        /// </summary>
        /// <param name="ssxh"></param>
        /// <returns></returns>
        IList<OROpStaffRecordEntity> getIdBySsxh(string ssxh);

    }
}