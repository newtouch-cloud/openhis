using Newtouch.EMR.Domain.Entity;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Infrastructure.EF;

namespace Newtouch.EMR.Domain.IRepository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2018-09-20 14:48
    /// 描 述：模板权限控制表
    /// </summary>
    public interface IBlmbqxkzRepo : IRepositoryBase<BlmbqxkzEntity>
    {
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        void SubmitForm(BlmbqxkzEntity entity, string keyValue);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        void DeleteForm(string keyValue);


        /// <summary>
        /// 根据模板id 批量修改数据权限
        /// </summary>
        /// <param name="mbId">模板ID</param>
        /// <param name="entity">数据权限包装实体</param>
        void UpdateCtrlLevelByMbId(string mbId, BlmbqxkzEntity entity);

    }
}