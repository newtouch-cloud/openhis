using Newtouch.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysPharmacyWindowRepo : RepositoryBase<SysPharmacyWindowEntity>, ISysPharmacyWindowRepo
    {
        public SysPharmacyWindowRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public void submitForm(SysPharmacyWindowEntity entity, int? keyValue)
        {
            if (keyValue.HasValue && keyValue.Value > 0)
            {
                if (this.IQueryable().Any(p => p.OrganizeId == entity.OrganizeId 
                && p.yfckId != keyValue && p.yfckCode == entity.yfckCode
                ))
                {
                    throw new FailedException("窗口编号不能重复！");
                }

                SysPharmacyWindowEntity oldEntity = null;   //变更前Entity
                oldEntity = this.FindEntity(keyValue.Value);
                this.DetacheEntity(oldEntity);

                entity.Modify();
                entity.yfckId = keyValue.Value;
                this.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, SysPharmacyWindowEntity.GetTableName(), oldEntity.yfckId.ToString());
                }
            }
            else
            {
                if (this.IQueryable().Any(p => p.OrganizeId == entity.OrganizeId &&
                p.yfckCode == entity.yfckCode))
                {
                    throw new FailedException("窗口编号不能重复！");
                }
                entity.Create();
                this.Insert(entity);
            }
        }
    }
}
