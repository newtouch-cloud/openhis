using Newtouch.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPatientVitalSignsRepo : IRepositoryBase<PatientVitalSignsEntity>
    {
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(PatientVitalSignsEntity entity, string keyValue);
    }
}
