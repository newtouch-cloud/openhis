using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common;
using Newtouch.Herp.Domain.Entity.Purchase;
using Newtouch.Herp.Domain.IRepository.Purchase;
using Newtouch.Herp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Herp.Repository.Purchase
{
    public class ReturnedRepo : RepositoryBase<ReturnedEntity>, IReturnedRepo
    {
        public ReturnedRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public string SubmitForm(ReturnedEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                //properties
                dbEntity.thId = entity.thId;
                dbEntity.CZLX = entity.CZLX;
                dbEntity.YYBM = entity.YYBM;
                dbEntity.PSDBM = entity.PSDBM;
                dbEntity.SJTHRQ = entity.SJTHRQ;
                dbEntity.THDBH = entity.THDBH == null ? "" : entity.THDBH;
                dbEntity.CGFS = entity.CGFS;
                dbEntity.XTBM = entity.XTBM;
                dbEntity.SFHBSFW = entity.SFHBSFW;
                dbEntity.DDBH = entity.DDBH == null ? "" : entity.DDBH;
                dbEntity.JLS = entity.JLS;
                dbEntity.Modify(keyValue);
                this.Update(dbEntity);
                return keyValue;
            }
            else
            {
                entity.thId = Guid.NewGuid().ToString();
                entity.CZLX = entity.CZLX;
                entity.YYBM = entity.YYBM;
                entity.PSDBM = entity.PSDBM;
                entity.SJTHRQ = entity.SJTHRQ;
                entity.THDBH = entity.THDBH == null ? "" : entity.THDBH;
                entity.CGFS = entity.CGFS;
                entity.XTBM = entity.XTBM;
                entity.SFHBSFW = entity.SFHBSFW;
                entity.DDBH = entity.DDBH == null ? "" : entity.DDBH;
                entity.JLS = entity.JLS;
                entity.Create(true);
                this.Insert(entity);
                return entity.thId;
            }
        }
        public IList<ReturnedEntity> GetReturnedGridJson(Pagination pagination, DateTime kssj, DateTime jssj, string OrganizeId)
        {
            var sql = @"select * from xt_wz_th
where zt=1 and organizeId=@OrganizeId
and createtime BETWEEN @kssj  AND  @jssj+' 23:59:59' ";

            var parms = new List<SqlParameter>
            {
                new SqlParameter("@OrganizeId", OrganizeId),
                new SqlParameter("@kssj", kssj),
                new SqlParameter("@jssj", jssj),
            };

            return QueryWithPage<ReturnedEntity>(sql, pagination, parms.ToArray(), false);
        }

        public void PurchaseReturnDelete(string thId, string orgId)
        {
            var dbEntity = this.FindEntity(thId);
            //properties
            dbEntity.zt = "0";
            dbEntity.Modify(thId);
            this.Update(dbEntity);

        }
        public void PurchaseReturnStateUpdate(string thId, int thdzt, string orgId)
        {
            var dbEntity = this.FindEntity(thId);
            //properties
            dbEntity.thdzt = thdzt;
            dbEntity.Modify(thId);
            this.Update(dbEntity);

        }
        public void PurchaseDdbhUpdate(string thId, string ddbh, string orgId)
        {
            var dbEntity = this.FindEntity(thId);
            //properties
            dbEntity.THDBH = ddbh;
            dbEntity.Modify(thId);
            this.Update(dbEntity);

        }

    }
}
