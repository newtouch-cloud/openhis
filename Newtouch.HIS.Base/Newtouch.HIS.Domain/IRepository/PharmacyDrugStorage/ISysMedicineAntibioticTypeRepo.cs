using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysMedicineAntibioticTypeRepo : IRepositoryBase<SysMedicineAntibioticTypeEntity>
    {
        /// <summary>
        /// 根据组织机构,级别,获取抗生素分类列表
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <param name="Level"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        IList<SysMedicineAntibioticTypeEntity> GetValidListByOrg(string OrganizeId, string Level, string parentId);
        /// <summary>
        /// 根据组织机构,上级Id,获取抗生素分类列表
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        IList<SysMedicineAntibioticTypeEntity> GetListByParentId(string OrganizeId, string parentId);
        /// <summary>
        /// 提交抗生素分类信息
        /// </summary>
        /// <param name="entity"></param>
        void SubmitForm(SysMedicineAntibioticTypeEntity entity);
    }
}
