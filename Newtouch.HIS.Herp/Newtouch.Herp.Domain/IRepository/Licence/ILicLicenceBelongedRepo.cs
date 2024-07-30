using Newtouch.Herp.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// 证照所属维护
    /// </summary>
    public interface ILicLicenceBelongedRepo : IRepositoryBase<LicLicenceBelongedEntity>
    {
        /// <summary>
        /// submit
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyWord"></param>
        int SubmitForm(LicLicenceBelongedEntity entity, string keyWord);
    }
}