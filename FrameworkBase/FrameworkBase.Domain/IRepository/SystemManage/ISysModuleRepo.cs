using System.Collections.Generic;
using Newtouch.Infrastructure.EF;
using FrameworkBase.Domain.Entity;

namespace FrameworkBase.Domain.IRepository
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-17 16:13
    /// 描 述：系统菜单
    /// </summary>
    public interface ISysModuleRepo : IRepositoryBase<SysModuleEntity>
    {
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        List<SysModuleEntity> GetList();

        /// <summary>
        /// 获取有效菜单列表
        /// </summary>
        /// <returns></returns>
        List<SysModuleEntity> GetValidList();

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        void SubmitForm(SysModuleEntity entity, string keyValue);

    }
}