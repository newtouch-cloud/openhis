using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 农保收费大类
    /// </summary>
    public interface ISysAgriInsuranceChargeCategoryRepo : IRepositoryBase<SysAgriInsuranceChargeCategoryEntity>
    {
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IList<SysAgriInsuranceChargeCategoryEntity> GetList(string orgId, string keyword = null);

        /// <summary>
        /// 获取有效
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IList<SysAgriInsuranceChargeCategoryEntity> GetValidList(string orgId);

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysAgriInsuranceChargeCategoryEntity GetForm(int keyValue);

        /// <summary>
        /// 提交保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysAgriInsuranceChargeCategoryEntity entity, int? keyValue);


    }
}
