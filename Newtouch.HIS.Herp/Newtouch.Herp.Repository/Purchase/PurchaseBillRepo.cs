using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Herp.Domain.DTO.InputDto.Purchase;
using Newtouch.Herp.Domain.Entity.Purchase;
using Newtouch.Herp.Domain.IRepository.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Herp.Repository.Purchase
{
    public class PurchaseBillRepo : RepositoryBase<PurchaseBillEntity>, IPurchaseBillRepo
    {

        public PurchaseBillRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public int InsertItem(PurchaseMainYY132 entity,string orgId)
        {
            PurchaseBillEntity dbEntity = new PurchaseBillEntity();
            dbEntity.Id = Guid.NewGuid().ToString();
            dbEntity.OrganizeId = orgId;
            dbEntity.fpId = entity.FPID;
            dbEntity.fpdm = entity.FPDM;
            dbEntity.fph = entity.FPH;
            dbEntity.qybm = entity.QYBM;
            dbEntity.fpysjg = entity.FPYSJG;
            dbEntity.fphszje = entity.FPHSZJE;
            dbEntity.Create(true);
            return this.Insert(dbEntity);
        }
    }
}
