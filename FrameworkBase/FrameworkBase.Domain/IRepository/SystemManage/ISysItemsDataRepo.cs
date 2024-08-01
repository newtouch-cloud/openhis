using System.Collections.Generic;
using Newtouch.Infrastructure.EF;
using FrameworkBase.Domain.Entity;

namespace FrameworkBase.Domain.IRepository
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-20 13:04
    /// 描 述：字典项
    /// </summary>
    public interface ISysItemsDataRepo : IRepositoryBase<SysItemsDataEntity>
    {
        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysItemsDataEntity> GetList(string itemId, string keyword);

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        void SubmitForm(SysItemsDataEntity entity, string keyValue);

    }
}