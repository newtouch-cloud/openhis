using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.DTO.PharmacyDrugStorage;
using Newtouch.HIS.Domain.Entity.Purchase;
using Newtouch.HIS.Domain.IRepository.Purchase;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Repository.Purchase
{
    public class PurchaseBillRepo : RepositoryBase<PurchaseBillEntity>, IPurchaseBillRepo
    {
        public PurchaseBillRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public int InsertItem(PurchaseMainYY019 entity, string orgId)
        {
            PurchaseBillEntity dbEntity = new PurchaseBillEntity();
            dbEntity.Id = Guid.NewGuid().ToString();
            dbEntity.OrganizeId = orgId;
            dbEntity.fph = entity.FPH;
            dbEntity.yqbm = entity.YQBM;
            dbEntity.fphszje = entity.FPHSZJE;
            dbEntity.fpzt = "0"; //0.未入库 1.已入库
            dbEntity.Create(true);
            return this.Insert(dbEntity);
        }

        //分页获取发票入库列表
        public IList<PurchaseBillEntity> GetBillStoreGridJson(Pagination pagination, DateTime kssj, DateTime jssj, string fph, string OrganizeId, string fpzt)
        {
            var sql = @"select * from xt_yp_fp
where zt=1 and organizeId=@OrganizeId 
and createtime BETWEEN @kssj  AND  @jssj+' 23:59:59' ";

            if (fpzt != "9") //0 未入库 1已入库 9全部
            {
                sql += " and fpzt=@fpzt";
            }
            if (!string.IsNullOrEmpty(fph)) //0 未入库 1已入库 9全部
            {
                sql += " and fph like @fph";
            }
            var parms = new List<SqlParameter>
            {
                new SqlParameter("@OrganizeId", OrganizeId),
                new SqlParameter("@kssj", kssj),
                new SqlParameter("@jssj", jssj),
                new SqlParameter("@fpzt", fpzt),
                new SqlParameter("@fph", "'%"+fph+"%'"),
            };

            return QueryWithPage<PurchaseBillEntity>(sql, pagination, parms.ToArray(), false);
        }

    }
}
