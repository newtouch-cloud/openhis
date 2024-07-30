using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity.PharmacyDrugStorage;
using Newtouch.HIS.Domain.IRepository.PharmacyDrugStorage;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Repository.PharmacyDrugStorage
{
    public class PurchaseDetailRepo : RepositoryBase<PurchaseDetailEntity>, IPurchaseDetailRepo
    {
        public PurchaseDetailRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        public void SubmitForm(PurchaseDetailEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                //properties
                dbEntity.Modify(keyValue);
                this.Update(dbEntity);
            }
            else
            {
                entity.cgmxId = Guid.NewGuid().ToString();
                entity.cgId = keyValue;
                entity.Create(true);
                this.Insert(entity);
            }
        }

        public int InsertItem(PurchaseDetailEntity entity, string keyValue) {
            entity.cgmxId = Guid.NewGuid().ToString();
            entity.cgId = keyValue;
            entity.Create(true);
            return this.Insert(entity);
        }

        public int DeleteItem(string cgId,string orgId)
        {
            return ExecuteSqlCommand(@"DELETE FROM xt_yp_cgmx WHERE cgId=@cgId and organizeId=@orgId and zt=1" , new SqlParameter("@cgId", cgId), new SqlParameter("@orgId", orgId));
        }

    }
}
