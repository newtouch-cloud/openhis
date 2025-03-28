using Newtouch.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Domain.IRepository
{

    public interface IInpatientDietSfxmdyRepo : IRepositoryBase<InpatientDietSfxmdyEntity>
    {
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        void SubmitForm(InpatientDietSfxmdyEntity entity, string keyValue);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        void DeleteForm(string keyValue);

    }
}