using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage
{
    public interface ISysMedicineAntibioticTypeDmnService
    {
        SysMedicineAntibioticTypeVO GetById(string orgId, string Id);
        /// <summary>
        /// 根据组织机构,级别,获取抗生素分类列表
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <param name="Level"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        IList<SysMedicineAntibioticTypeVO> GetValidListByOrg(string OrganizeId, string Level, string parentId);
        /// <summary>
        /// 根据组织机构,上级Id,获取抗生素分类列表
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        IList<SysMedicineAntibioticTypeVO> GetListByParentId(string OrganizeId, string parentId);
        /// <summary>
        /// 提交抗生素分类信息
        /// </summary>
        /// <param name="entity"></param>
        void SubmitForm(SysMedicineAntibioticTypeVO entity);
    }
}
