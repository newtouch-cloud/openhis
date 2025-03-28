using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.MR.ManageSystem.Domain.IRepository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2019-11-20 13:45
    /// 描 述：病案科室与his科室关系表
    /// </summary>
    public interface IMrreldeptRepo : IRepositoryBase<MrreldeptEntity>
    {
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        int SubmitForm(MrreldeptEntity entity,string keyValue);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Save(MrreldeptEntity entity);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        int DeleteForm(string keyValue);

    }
}