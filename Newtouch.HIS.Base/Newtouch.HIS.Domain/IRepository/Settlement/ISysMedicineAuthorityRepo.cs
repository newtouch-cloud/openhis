using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.Settlement;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository.Settlement
{
    public interface ISysMedicineAuthorityRepo : IRepositoryBase<SysMedicineAuthorityEntity>
    {

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysMedicineAuthorityEntity GetForm(string keyValue);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IList<SysMedicineAuthorityEntity> GetList(string orgId, string keyword = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysMedicineAuthorityEntity entity, string keyValue);
        /// <summary>
        /// 获取有效权限
        /// </summary>
        /// <returns></returns>
        IList<SysMedicineAuthorityEntity> GetValidList(string orgId, string keyword);
    }
}
