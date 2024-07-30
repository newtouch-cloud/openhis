using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity.Purchase;
using Newtouch.HIS.Domain.IRepository.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Repository.Purchase
{
    public class PurchaseLocationRepo : RepositoryBase<PurchaseLocationEntity>, IPurchaseLocationRepo
    {
        public PurchaseLocationRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }


        //更新或新增
        public string SubmitForm(PurchaseLocationEntity entity, string orgId, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Id = keyValue;
                entity.OrganizeId = orgId;
                entity.psdzt = 1;
                entity.zt = "1";
                entity.Modify(keyValue);
                this.Update(entity);
                return keyValue;
            }
            else
            {
                entity.Id = Guid.NewGuid().ToString();
                entity.OrganizeId = orgId;
                entity.psdzt = 1;//已保存
                entity.Create(true);
                this.Insert(entity);
                return entity.Id;
            }
        }

        //更新配送点状态
        public int LocationStateUpdate(string Id, string orgId, int psdzt)
        {
            var dbEntity = this.FindEntity(Id);
            //properties
            dbEntity.psdzt = psdzt;//psdzt 1已保存 2已传报 3已作废
            dbEntity.Modify(Id);
            return this.Update(dbEntity);
        }
    }
}
