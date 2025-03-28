using Newtouch.Herp.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// ÷§’’Œ¨ª§
    /// </summary>
    public interface ILicLicenceRepo : IRepositoryBase<LicLicenceEntity>
    {
        /// <summary>
        /// submit
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyWord"></param>
        int SubmitForm(LicLicenceEntity entity, string keyWord);
    }
}