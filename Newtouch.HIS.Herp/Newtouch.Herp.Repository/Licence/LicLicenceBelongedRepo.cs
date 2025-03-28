using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// 证照所属维护
    /// </summary>
    public class LicLicenceBelongedRepo: RepositoryBase<LicLicenceBelongedEntity>, ILicLicenceBelongedRepo
    {
        public LicLicenceBelongedRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// submit
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyWord"></param>
        public int SubmitForm(LicLicenceBelongedEntity entity, string keyWord)
        {
            if (string.IsNullOrWhiteSpace(keyWord))
            {
                entity.Create(true);
                return Insert(entity);
            }
            var dbOld = FindEntity(p => p.Id == keyWord);
            if (dbOld == null) return 0;
            dbOld.belonged = entity.belonged;
            dbOld.zt = entity.zt;
            dbOld.Modify();
            return Update(dbOld);
        }
    }
}
