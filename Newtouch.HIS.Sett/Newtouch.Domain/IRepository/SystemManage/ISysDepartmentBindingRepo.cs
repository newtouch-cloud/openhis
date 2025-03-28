using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysDepartmentBindingRepo : IRepositoryBase<SysDepartmentBindingEntity>
    {

        /// <summary>
        /// 获取所有发票列表
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<SysDepartmentBindingEntity> GetSysDepartmentBindingList(string keyValue, string organizeId);

        /// <summary>
        /// 修改页面，根据主键获取实体
        /// </summary>
        /// <param name="bddm"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        SysDepartmentBindingEntity GetSysDepartmentBindingEntity(string bddm, string orgId);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="SysDepartmentBindingEntity"></param>
        /// <param name="bddm"></param>
        void SubmitForm(SysDepartmentBindingEntity sysDepartmentBindingEntity, string bddm);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="bddm"></param>
        /// <param name="orgId"></param>
        void DeleteForm(string bddm, string orgId);
    }
}
