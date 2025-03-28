using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRehabChargeItemRepo : IRepositoryBase<RehabChargeItemEntity>
    {
        /// <summary>
        /// 保存
        /// </summary>
        void SubmitForm(RehabChargeItemEntity entity, string sfflId);

        /// <summary>
        /// 删除
        /// </summary>
        void DeleteForm(string sfflId, string OrganizeId);

    }
}
