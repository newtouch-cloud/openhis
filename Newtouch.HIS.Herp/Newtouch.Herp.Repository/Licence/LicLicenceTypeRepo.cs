using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// 证照类型维护
    /// </summary>
    public class LicLicenceTypeRepo : RepositoryBase<LicLicenceTypeEntity>, ILicLicenceTypeRepo
    {
        public LicLicenceTypeRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// submit
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyWord"></param>
        public int SubmitForm(LicLicenceTypeEntity entity, string keyWord)
        {
            if (string.IsNullOrWhiteSpace(keyWord))
            {
                entity.Create(true);
                return Insert(entity);
            }
            var dbOld = FindEntity(p => p.Id == keyWord);
            if (dbOld == null) return 0;
            dbOld.belongedId = entity.belongedId;
            dbOld.typeName = entity.typeName;
            dbOld.zt = entity.zt;
            dbOld.Modify();
            return Update(dbOld);
        }
    }
}
