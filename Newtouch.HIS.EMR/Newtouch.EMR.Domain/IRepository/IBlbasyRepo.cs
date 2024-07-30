using Newtouch.EMR.Domain.Entity;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Infrastructure.EF;

namespace Newtouch.EMR.Domain.IRepository
{
    /// <summary>
    /// 创 建：hyj
    /// 日 期：2018-09-19 19:07
    /// 描 述：病案首页
    /// </summary>
    public interface IBlbasyRepo : IRepositoryBase<BlbasyEntity>
    {
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        void SubmitForm(BlbasyEntity entity, string keyValue);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        void DeleteForm(string keyValue);
        BlbasyEntity bl_basyGetByID(string ID);

    }
}