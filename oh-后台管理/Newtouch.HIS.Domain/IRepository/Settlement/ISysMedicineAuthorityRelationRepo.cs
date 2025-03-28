using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.Settlement;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository.Settlement
{
    public interface ISysMedicineAuthorityRelationRepo : IRepositoryBase<SysMedicineAuthorityRelationEntity>
    {
        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysMedicineAuthorityRelationEntity GetForm(string keyValue);



        IList<SysMedicineAuthorityRelationEntity> GetList(string orgId, string keyword = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysMedicineAuthorityRelationEntity entity, string keyValue);
        void UpdateAuthority(string gh, string organizeId,  string[] AuthorityList);
    }
}
