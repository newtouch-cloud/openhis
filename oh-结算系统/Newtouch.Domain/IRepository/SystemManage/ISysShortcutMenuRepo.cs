using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Infrastructure.EF;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-10-16 11:07
    /// 描 述：快捷菜单
    /// </summary>
    public interface ISysShortcutMenuRepo : IRepositoryBase<SysShortcutMenuEntity>
    {
        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="keyword">筛选关键字</param>
        IList<SysShortcutMenuEntity> GetList(string keyword);

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        void SubmitForm(SysShortcutMenuEntity entity, string keyValue);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        void DeleteForm(string keyValue);

    }
}