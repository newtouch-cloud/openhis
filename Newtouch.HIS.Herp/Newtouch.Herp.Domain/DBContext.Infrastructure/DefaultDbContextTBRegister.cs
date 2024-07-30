using System.Data.Entity;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.Entity.Product;
using Newtouch.Herp.Domain.Entity.Purchase;
using Newtouch.Infrastructure.EF;
using Newtouch.Infrastructure.EF.Conventions;

namespace Newtouch.Herp.Domain.DBContext.Infrastructure
{
    /// <summary>
    /// DefaultDbContext TB Register
    /// </summary>
    public class DefaultDbContextTBRegister
    {
        /// <summary>
        /// 注册表（底层已经注册了一部分）
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void Registe(DbModelBuilder modelBuilder)
        {
            //注册新表
            modelBuilder.Entity<RelWarehouseDeptEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<RelWarehouseUserEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<KfWarehouseEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<GysSupplierEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<GysContactsEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<RelProductUnitEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<WzProductEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<RelProductWarehouseEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<KfCrkdjEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<KfCrkmxEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<KcPdxxEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<KcPdxxmxEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<KcSyyyEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<KcSyxxEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<KfKcxxEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<WzPriceAdjustmentEntity>().RegisterTable().HasKey(p => p.wztjId);
            modelBuilder.Entity<WzPriceAdjustmentProfitAndLossEntity>().RegisterTable().HasKey(p => p.TjsyId);
            modelBuilder.Entity<KcKcjzEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<LicLicenceEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<LicLicenceTypeEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<LicLicenceBelongedEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<RelProductAndsfxmEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<KfApplyOrderEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<KfApplyOrderDetailEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<CgPurchaseOrderEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<CgPurchaseOrderDetailEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<CgOrderEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<CgOrderDetailEntity>().RegisterTable().HasKey(p => p.Id);
            //采购
            modelBuilder.Entity<PurchaseEntity>().RegisterTable().HasKey(p => p.cgId);
            modelBuilder.Entity<PurchaseDetailEntity>().RegisterTable().HasKey(p => p.cgmxId);
            modelBuilder.Entity<PurchaseLogEntity>().RegisterTable().HasKey(p => p.Id);
            //退货
            modelBuilder.Entity<ReturnedEntity>().RegisterTable().HasKey(p => p.thId);
            modelBuilder.Entity<ReturnedDetailEntity>().RegisterTable().HasKey(p => p.thmxId);
            //配送点
            modelBuilder.Entity<PurchaseLocationEntity>().RegisterTable().HasKey(p => p.Id);
            //发票
            modelBuilder.Entity<PurchaseBillEntity>().RegisterTable().HasKey(p => p.Id);

            modelBuilder.Conventions.Add(new DecimalPrecisionAttributeConvention());
        }

    }
}