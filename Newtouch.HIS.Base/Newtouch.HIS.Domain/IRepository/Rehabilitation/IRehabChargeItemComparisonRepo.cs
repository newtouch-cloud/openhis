using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRehabChargeItemComparisonRepo : IRepositoryBase<RehabChargeItemComparisonEntity>
    {
        /// <summary>
        /// 保存
        /// </summary>
        void SubmitForm(RehabChargeItemComparisonEntity entity, string sfxmdzId);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        void DeleteForm(string sfxmdzId,string OrganizeId);
    }
}
