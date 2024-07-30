using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common;
using Newtouch.Herp.Domain.Entity.Purchase;
using Newtouch.Herp.Domain.IRepository.Purchase;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Herp.Repository.Purchase
{
    public class ReturnedDetailRepo : RepositoryBase<ReturnedDetailEntity>, IReturnedDetailRepo
    {
        public ReturnedDetailRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }


        //public void SubmitForm(ReturnedDetailEntity entity, string keyValue)
        //{
        //    if (!string.IsNullOrEmpty(keyValue))
        //    {
        //        var dbEntity = this.FindEntity(keyValue);
        //        //properties
        //        dbEntity.Modify(keyValue);
        //        this.Update(dbEntity);
        //    }
        //    else
        //    {
        //        //entity.cgmxId = Guid.NewGuid().ToString();
        //        //entity.cgId = keyValue;
        //        entity.Create(true);
        //        this.Insert(entity);
        //    }
        //}

        public int InsertItem(ReturnedDetailEntity entity, string keyValue)
        {
            entity.thmxId = Guid.NewGuid().ToString();
            entity.thId = keyValue;
            entity.OrganizeId = entity.OrganizeId;
            entity.SXH = entity.SXH;
            entity.CGLX = entity.CGLX;
            entity.THLX = entity.THLX;
            entity.HCTBDM = entity.HCTBDM;
            entity.HCXFDM = entity.HCXFDM;
            entity.YYBDDM = entity.YYBDDM;
            entity.CGGGXH = entity.CGGGXH;
            entity.SCPH = entity.SCPH;
            entity.SCRQ = entity.SCRQ;
            entity.YXRQ = entity.YXRQ;
            entity.PSMXTMLX = entity.PSMXTMLX;
            entity.PSMXTM = entity.PSMXTM == null ? "" : entity.PSMXTM;
            entity.THSL = entity.THSL;
            entity.THDJ = entity.THDJ;
            entity.QYBM = entity.QYBM;
            entity.THYY = entity.THYY;
            entity.Create(true);
            return this.Insert(entity);
        }

        public int DeleteItem(string thId, string orgId)
        {
            return ExecuteSqlCommand(@"DELETE FROM xt_wz_thmx WHERE thId=@thId and organizeId=@orgId and zt=1", new SqlParameter("@thId", thId), new SqlParameter("@orgId", orgId));
        }

    }
}
