using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity.PharmacyDrugStorage;
using Newtouch.HIS.Domain.IRepository.PharmacyDrugStorage;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Repository.PharmacyDrugStorage
{

    public class PurchaseRepo : RepositoryBase<PurchaseEntity>, IPurchaseRepo
    {
        public PurchaseRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public IList<PurchaseEntity> GetPurchaseGridJson(Pagination pagination, DateTime kssj, DateTime jssj, string OrganizeId,int ddzt)
        {
            var sql = @"select * from xt_yp_cg
where zt=1 and organizeId=@OrganizeId
and createtime BETWEEN @kssj  AND  @jssj+' 23:59:59' ";

            if (ddzt != 0)
            {
                sql += " and ddzt=@ddzt";//已传报
            }
            var parms = new List<SqlParameter>
            {
                new SqlParameter("@OrganizeId", OrganizeId),
                new SqlParameter("@kssj", kssj),
                new SqlParameter("@jssj", jssj),
                new SqlParameter("@ddzt", ddzt),
            };

            return QueryWithPage<PurchaseEntity>(sql, pagination, parms.ToArray(), false);
        }

        public void PurchaseDelete(string cgId, string orgId )
        {
                var dbEntity = this.FindEntity(cgId);
                //properties
                dbEntity.zt = "0";
                dbEntity.Modify(cgId);
                this.Update(dbEntity);
           
        }
        public void PurchaseStateUpdate(string cgId,int ddzt, string orgId)
        {
            var dbEntity = this.FindEntity(cgId);
            //properties
            dbEntity.ddzt = ddzt;
            dbEntity.Modify(cgId);
            this.Update(dbEntity);

        }

        /// <summary>
        /// 更新采购单的订单编号
        /// </summary>
        /// <param name="cgId"></param>
        /// <param name="ddbh"></param>
        /// <param name="orgId"></param>

        public void PurchaseDdbhUpdate(string cgId, string ddbh, string orgId)
        {
            var dbEntity = this.FindEntity(cgId);
            //properties
            dbEntity.ddbh = ddbh;
            dbEntity.Modify(cgId);
            this.Update(dbEntity);

        }

        public string SubmitForm(PurchaseEntity entity,string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                //properties
                dbEntity.cgId = entity.cgId;
                dbEntity.czlx = entity.czlx;
                dbEntity.jls = entity.jls;
                dbEntity.Modify(keyValue);
                this.Update(dbEntity);
                return keyValue;
            }
            else
            {
                entity.cgId = Guid.NewGuid().ToString();
                entity.ddsj = DateTime.Now.ToString("yyyy-MM-dd HH:ss:mm");
                //entity.ddbh = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("xt_cg_ddbh", entity.OrganizeId, "{0:D4}", true);
                entity.yyjhdh = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("xt_cg_ddbh", entity.OrganizeId, "{0:D4}", true);
                entity.ddzt = 1; //1已保存
                //entity.zt = "1";
                //entity.CreatorCode = user.rygh;
                //entity.CreateTime = DateTime.Now;
                //cgEntity.LastModifierCode = "";
                //cgEntity.LastModifyTime = null;
                entity.Create(true);
                this.Insert(entity);
                return entity.cgId;
            }
        }

    }
}
