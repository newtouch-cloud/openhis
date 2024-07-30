using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.SystemManage;
using Newtouch.Infrastructure.EF;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Newtouch.HIS.Domain.DBContext.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class NewtouchDbContext : DbContextBase
    {
        /// <summary>
        /// 
        /// </summary>
        public NewtouchDbContext()
            : base("NewtouchDbContext")
        {
            this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.ValidateOnSaveEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure Code First to ignore PluralizingTableName convention 
            // If you keep this convention then the generated tables will have pluralized names. 
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<DbBackupEntity>().ToTable("Sys_DbBackup").HasKey(p => p.Id);
            modelBuilder.Entity<SysModuleButtonEntity>().ToTable("Sys_ModuleButton").HasKey(p => p.Id);
            modelBuilder.Entity<SysModuleEntity>().ToTable("Sys_Module").HasKey(p => p.Id);
            modelBuilder.Entity<SysRoleAuthorizeEntity>().ToTable("Sys_RoleAuthorize").HasKey(p => p.Id);
            modelBuilder.Entity<SysRoleEntity>().ToTable("Sys_Role").HasKey(p => p.Id);
            modelBuilder.Entity<FilterIPEntity>().ToTable("Sys_FilterIP").HasKey(p => p.Id);
            modelBuilder.Entity<SysLogEntity>().ToTable("Sys_Log").HasKey(p => p.Id);

            modelBuilder.Entity<SysPatientBasicInfoEntity>().RegisterTable().HasKey(p => p.patid);
            modelBuilder.Entity<HospPatientBasicInfoEntity>().RegisterTable().HasKey(p => p.syxh);
            modelBuilder.Entity<SysPatientAccountEntity>().RegisterTable().HasKey(p => p.zhbh);
            modelBuilder.Entity<SysPatientAccountRevenueAndExpenseEntity>().RegisterTable().HasKey(p => p.zhszjlbh);

            modelBuilder.Entity<SysPatientChargeAdditionalEntity>().RegisterTable().HasKey(p => p.brsffjbh);
            modelBuilder.Entity<SysPatientChargeWaiverEntity>().RegisterTable().HasKey(p => p.brsfjmbh);
            modelBuilder.Entity<SysChargeAdditionalCategoryEntity>().RegisterTable().HasKey(p => p.dlbh);
            modelBuilder.Entity<SysChargeItemPriceAdjustEntity>().RegisterTable().HasKey(p => p.tjbh);
            modelBuilder.Entity<SysPatientChargeRangeEntity>().RegisterTable().HasKey(p => p.brsffwbh);

            modelBuilder.Entity<SysPatientChargeAlgorithmEntity>().RegisterTable().HasKey(p => p.brsfsfbh);
            modelBuilder.Entity<SysPatientNatureEntity>().RegisterTable().HasKey(p => p.brxzbh);
            modelBuilder.Entity<SysCashPaymentModelEntity>().RegisterTable().HasKey(p => p.xjzffsbh);
            modelBuilder.Entity<SysMedicalTechItemMappEntity>().RegisterTable().HasKey(p => p.yjxmdzbh);
            modelBuilder.Entity<SysCardEntity>().RegisterTable().HasKey(p => p.CardId);

            modelBuilder.Entity<ComprehensiveMaterialChargeItemMappEntity>().RegisterTable().HasKey(p => p.clzhsfxm);
            modelBuilder.Entity<HospSettlementPaymentModelEntity>().RegisterTable().HasKey(p => p.zyjszffsbh);
            modelBuilder.Entity<HospExecuteMedicalOrderReduceBurdenItemEntity>().RegisterTable().HasKey(p => p.jfxmbh);
            modelBuilder.Entity<SysChargeMaterialItemSynthesisEntity>().RegisterTable().HasKey(p => p.clzhxmbh);
            modelBuilder.Entity<SysPatientComprehensiveNatureEntity>().RegisterTable().HasKey(p => p.brxzzhbh);
            modelBuilder.Entity<HospItemBillingEntity>().RegisterTable().HasKey(p => p.jfbbh);
            modelBuilder.Entity<HospDrugBillingEntity>().RegisterTable().HasKey(p => p.jfbbh);
            modelBuilder.Entity<HospMultiDiagnosisEntity>().RegisterTable().HasKey(p => p.rydzdId);
            modelBuilder.Entity<HospSettlementEntity>().RegisterTable().HasKey(p => p.jsnm);//住院结算


            modelBuilder.Entity<HospTransactionSettlementDetailEntity>().RegisterTable().HasKey(p => p.jsmxbh);
            modelBuilder.Entity<HospTransactionSettlementEntity>().RegisterTable().HasKey(p => p.jsnm);
            modelBuilder.Entity<HospSettlementDetailEntity>().RegisterTable().HasKey(p => p.jsmxbh);
            modelBuilder.Entity<HospSettlementCategoryEntity>().RegisterTable().HasKey(p => p.jsdlId);


            modelBuilder.Entity<OutpatientRegistEntity>().RegisterTable().HasKey(p => p.ghnm);//门诊挂号
            modelBuilder.Entity<OutpatientSettlementCategoryEntity>().RegisterTable().HasKey(p => p.jsdlId);//门诊结算大类
            modelBuilder.Entity<OutpatientSettlementEntity>().RegisterTable().HasKey(p => p.jsnm);//门诊结算
            modelBuilder.Entity<OutpatientRegistItemEntity>().RegisterTable().HasKey(p => p.xmnm);//门诊挂号项目
            modelBuilder.Entity<OutpatientRegistNonAttendanceEntity>().RegisterTable().HasKey(p => p.thnm);//门诊挂号退号
            modelBuilder.Entity<OutpatientSettlementPaymentModelEntity>().RegisterTable().HasKey(p => p.mzjszffsbh);//门诊结算支付方式
            modelBuilder.Entity<OutpatientSettlementDetailEntity>().RegisterTable().HasKey(p => p.jsmxnm);//门诊结算明细

            modelBuilder.Entity<OutpatientPrescriptionEntity>().RegisterTable().HasKey(p => p.cfnm);//门诊处方
            modelBuilder.Entity<OutpatientPrescriptionDetailEntity>().RegisterTable().HasKey(p => p.cfmxId);//门诊处方明细 
            modelBuilder.Entity<OutpatientItemEntity>().RegisterTable().HasKey(p => p.xmnm);//门诊项目
            modelBuilder.Entity<OupatientInvoicePrintEntity>().RegisterTable().HasKey(p => p.dybh);//门诊发票打印


            modelBuilder.Entity<FinanceReceiptEntity>().RegisterTable().HasKey(p => p.cwsjId); //财务收据 
            modelBuilder.Entity<FinancialInvoiceEntity>().RegisterTable().HasKey(p => p.fpdm); //财务发票
            modelBuilder.Entity<SysDrugInventoryEntity>().RegisterTable().HasKey(p => p.Kcxh);//药品库存

            modelBuilder.Entity<SysChargeTemplateEntity>().RegisterTable().HasKey(p => p.sfmbbh); //系统收费模版
            modelBuilder.Entity<SysChargeTemplateItemMappEntity>().RegisterTable().HasKey(p => p.sfmbxmId); //收费模版项目
            modelBuilder.Entity<SysFailedCodeMessageMappEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysUserRoleEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysOrganizeAuthorizeEntity>().RegisterTable().HasKey(p => p.Id);

            modelBuilder.Entity<SysConfigEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysChargeItemSpecialMarkEntity>().RegisterTable().HasKey(p => p.sfxmtsbzbh);
            modelBuilder.Entity<HospMedicinalOrderEntity>().RegisterTable().HasKey(p => p.yzId);
            modelBuilder.Entity<HospMedicinalOrderExecuteEntity>().RegisterTable().HasKey(p => p.yzzxId);

            modelBuilder.Entity<SysRegistSpecialDiseaseEntity>().RegisterTable().HasKey(p => p.ghzbbh);
            modelBuilder.Entity<OutpatientRegistScheduleEntity>().RegisterTable().HasKey(p => p.ghpbId);

            modelBuilder.Entity<HospAccountingPlanEntity>().RegisterTable().HasKey(p => p.jzjhId);
            modelBuilder.Entity<HospAccountingPlanDetailEntity>().RegisterTable().HasKey(p => p.jzjhmxId);

            modelBuilder.Entity<NonTreatmentItemBillingEntity>().RegisterTable().HasKey(p => p.jfbId);
            modelBuilder.Entity<SysMedicalInsuranceFilingEntity>().RegisterTable().HasKey(p => p.ybbabId);

            modelBuilder.Entity<OutpatientAccountEntity>().RegisterTable().HasKey(p => p.jzjhId);//mz_jzjh 门诊记账计划
            modelBuilder.Entity<OutpatientAccountDetailEntity>().RegisterTable().HasKey(p => p.jzjhmxId);//mz_jzjhmx 门诊记账计划明细
            modelBuilder.Entity<CommercialInsuranceEntity>().RegisterTable().HasKey(p => p.Id);//xt_sybx 商业保险
            modelBuilder.Entity<SysCommercialInsuranceFilingEntity>().RegisterTable().HasKey(p => p.sbbabId);//xt_sbbab 商保备案表
            modelBuilder.Entity<SysCommercialInsuranceChargeItemEntity>().RegisterTable().HasKey(p => p.sbkbxmId);//xt_sbkbxm 商保可报项目 
            modelBuilder.Entity<DoctorWorkingDaysPlanEntity>().RegisterTable().HasKey(p => p.Id);//jz_yspb 治疗师排班
            modelBuilder.Entity<AdjustWorkingHoursEntity>().RegisterTable().HasKey(p => p.Id);//jz_sctz 时长调整

            modelBuilder.Entity<RptMonthReportDetailEntity>().RegisterTable().HasKey(p => p.Id);

            //同步治疗记录
            modelBuilder.Entity<SyncTreatmentServiceRecordEntity>().RegisterTable().HasKey(p => p.Id);

            //金额上限提醒
            modelBuilder.Entity<MoneyUpperLimitReminderEntity>().RegisterTable().HasKey(p => p.sxtxId);
            modelBuilder.Entity<SysShortcutMenuEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysRoleShortcutMenuEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<MzghbookEntity>().RegisterTable().HasKey(p => p.BookId);
            base.OnModelCreating(modelBuilder);
        }

    }

}