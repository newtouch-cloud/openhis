using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysMedicineAntibioticInfoRepo : IRepositoryBase<SysMedicineAntibioticInfoEntity>
    {
        /// <summary>
        /// 提交药品抗生素信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Id</returns>
        string SubmitForm(SysMedicineAntibioticInfoEntity entity);
        /// <summary>
        /// 通过Id获取药品抗生素信息
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        SysMedicineAntibioticInfoEntity GetKssInfo(string Id, string OrganizeId);
    }
}
