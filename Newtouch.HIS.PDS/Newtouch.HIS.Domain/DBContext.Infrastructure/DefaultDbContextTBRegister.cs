using System.Data.Entity;
using Newtouch.HIS.Domain.Entity;
 using Newtouch.Infrastructure.EF;
 using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure.EF.Conventions;
using Newtouch.HIS.Domain.Entity.HospitalizationPharmacy;
using Newtouch.HIS.Domain.Entity.PharmacyDrugStorage;
using Newtouch.HIS.Domain.Entity.Purchase;

namespace Newtouch.HIS.Domain.DBContext.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultDbContextTBRegister
    {
        /// <summary>
        /// 注册表（底层已经注册了一部分）
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void Registe(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SysFailedCodeMessageMappEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysMedicineTemporaryInternalDispenseDetailEntity>().RegisterTable().HasKey(p => p.fymxId);
            modelBuilder.Entity<OutpatientPrescriptionDetailBatchNumberEntity>().RegisterTable().HasKey(p => p.cfmxphId);
            modelBuilder.Entity<SysMedicineInventoryDetailEntity>().RegisterTable().HasKey(p => p.pdmxId);
            modelBuilder.Entity<SysMedicineInventoryEntity>().RegisterTable().HasKey(p => p.pdId);
            modelBuilder.Entity<SysMedicineProfitLossEntity>().RegisterTable().HasKey(p => p.syId);
            modelBuilder.Entity<SysMedicineProfitLossReasonEntity>().RegisterTable().HasKey(p => p.syyyId);
            modelBuilder.Entity<SysMedicineReceiptApprovalEntity>().RegisterTable().HasKey(p => p.djshId);
            modelBuilder.Entity<SysMedicineReceiptEntity>().RegisterTable().HasKey(p => p.ypdjId);
            modelBuilder.Entity<SysMedicineStockCarryOverEntity>().RegisterTable().HasKey(p => p.kcId);
            modelBuilder.Entity<SysMedicineStockInfoEntity>().RegisterTable().HasKey(p => p.kcId);
            modelBuilder.Entity<SysMedicineStorageIOReceiptDetailEntity>().RegisterTable().HasKey(p => p.crkmxId);
            modelBuilder.Entity<SysMedicineStorageIOReceiptEntity>().RegisterTable().HasKey(p => p.crkId);
            modelBuilder.Entity<SysMedicineTemporaryProfitLossEntity>().RegisterTable().HasKey(p => p.syId);
            modelBuilder.Entity<SysPharmacyDepartmentMedicineEntity>().RegisterTable().HasKey(p => p.bmypId);
            modelBuilder.Entity<SysMedicineRequisitionEntity>().RegisterTable().HasKey(p => p.sldId);
            modelBuilder.Entity<SysMedicineRequisitionDetailEntity>().RegisterTable().HasKey(p => p.sldmxId);
            modelBuilder.Entity<SysMedicinePriceAdjustmentEntity>().RegisterTable().HasKey(p => p.yptjId);
            modelBuilder.Entity<SysDispensingaConfigEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysYpksfypzEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysMedicinePriceAdjustmentProfitLossEntity>().RegisterTable().HasKey(p => p.TjsyId);
            modelBuilder.Entity<ZyYpyzczjlEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<ZyYpyzxxEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<ZyYpyzzxphEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<MzCfEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<MzCfmxEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<MzCfypczjlEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysMedicineStockCarryDownEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<XtksbymxEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<XtksbyEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<XtksbythEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<XtksbythmxEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.ComplexType<XT_YP_LS_NBFYMXK>();
            modelBuilder.Entity<ZyReturnDrugApplyBillDetailEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<ZyReturnDrugApplyBillEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<ZyTymxEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.ComplexType<XT_YP_LS_NBFYMXK>();
            //采购

            modelBuilder.Entity<PurchaseEntity>().RegisterTable().HasKey(p => p.cgId);
            modelBuilder.Entity<PurchaseDetailEntity>().RegisterTable().HasKey(p => p.cgmxId);
            modelBuilder.Entity<PurchaseLogEntity>().RegisterTable().HasKey(p => p.Id);
            //配送点
            modelBuilder.Entity<PurchaseLocationEntity>().RegisterTable().HasKey(p => p.Id);
            //发票
            modelBuilder.Entity<PurchaseBillEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<PurchaseBillDetailEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Conventions.Add(new DecimalPrecisionAttributeConvention());
        }
    }
}
