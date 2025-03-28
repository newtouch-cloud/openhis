using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRehabChargeClassificationRepo : IRepositoryBase<RehabChargeClassificationEntity>
    {
        /// <summary>
        /// 获得所有列表
        /// </summary>
        List<RehabChargeClassificationEntity> GetRehabChargeClassificationList(string OrganizeId, string keyword = null);

        /// <summary>
        /// 修改form
        /// </summary>
        RehabChargeClassificationEntity GetRehabChargeClassificationEntity(string sfflId, string OrganizeId);

        /// <summary>
        /// 保存
        /// </summary>
        void SubmitForm(RehabChargeClassificationEntity entity, string sfflId);
        
        /// <summary>
        /// 删除
        /// </summary>
        void DeleteForm(string sfflId, string OrganizeId);
    }
}
