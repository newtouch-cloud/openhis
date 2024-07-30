using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.MR.ManageSystem.Domain.IRepository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2019-11-20 11:05
    /// 描 述：病案首页手术记录表
    /// </summary>
    public interface IMrbasyssRepo : IRepositoryBase<MrbasyssEntity>
    {
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        int SubmitForm(MrbasyssEntity entity, string keyValue);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        int DeleteForm(string keyValue);

    }
}