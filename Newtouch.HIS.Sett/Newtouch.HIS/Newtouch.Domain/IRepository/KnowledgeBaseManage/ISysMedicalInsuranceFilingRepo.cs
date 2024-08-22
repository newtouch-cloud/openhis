using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysMedicalInsuranceFilingRepo : IRepositoryBase<SysMedicalInsuranceFilingEntity>
    {
        /// <summary>
        /// 保存
        /// </summary>
        void SubmitForm(SysMedicalInsuranceFilingEntity entity, string ybbabId);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        void DeleteForm(string ybbabId, string orgId);
    }
}
