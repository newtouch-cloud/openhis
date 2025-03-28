using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 病案收费大类
    /// </summary>
    public interface ISysMedicalRecordChargeCategoryRepo : IRepositoryBase<SysMedicalRecordChargeCategoryEntity>
    {
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IList<SysMedicalRecordChargeCategoryEntity> GetList(string orgId, string keyword = null);

        /// <summary>
        /// 获取有效
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IList<SysMedicalRecordChargeCategoryEntity> GetValidList(string orgId);

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysMedicalRecordChargeCategoryEntity GetForm(int keyValue);

        /// <summary>
        /// 提交保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysMedicalRecordChargeCategoryEntity entity, int? keyValue);
    }
}
