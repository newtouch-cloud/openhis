using System.Collections.Generic;
using Newtouch.Infrastructure.EF;
using FrameworkBase.Domain.Entity;

namespace FrameworkBase.Domain.IRepository
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-20 13:03
    /// 描 述：字典分类
    /// </summary>
    public interface ISysItemsTypeRepo : IRepositoryBase<SysItemsTypeEntity>
    {
        /// <summary>
        /// 获取分类列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        List<SysItemsTypeEntity> GetList(string keyword = null);

        /// <summary>
        /// 获取有效分类列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        List<SysItemsTypeEntity> GetValidList(string keyword = null);

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        void SubmitForm(SysItemsTypeEntity entity, string keyValue);

    }
}