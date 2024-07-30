using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection.Emit;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.Settlement;
using Newtouch.HIS.Domain.Entity.SystemManage;
using Newtouch.Infrastructure.EF;
using Newtouch.Infrastructure.EF.Conventions;

namespace Newtouch.HIS.Domain.DBContext.Infrastructure
{
    /// <summary>
    /// base库DB上下文
    /// </summary>
    public class BaseDbContext : DbContextBase
    {
        /// <summary>
        /// 
        /// </summary>
        public BaseDbContext()
            : base("BaseDbContext")
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

            modelBuilder.Entity<SysApplicationEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysDepartmentEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysDepartmentWardRelationEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysFailedCodeMessageMappEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysItemsDetailEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysItemsEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysModuleButtonEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysModuleEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysOrganizeAppEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysOrganizeEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysRoleAuthorizeEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysRoleEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysUserEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysUserLogOnEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysUserRoleEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysLogEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysOrganizeAuthorizeEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysStaffEntity>().RegisterTable().HasKey(p => p.Id);

            modelBuilder.Entity<SysChargeCategoryEntity>().RegisterTable().HasKey(p => p.dlId);
            modelBuilder.Entity<SysMedicalOrderFrequencyEntity>().RegisterTable().HasKey(p => p.yzpcId);
            modelBuilder.Entity<SysMedicineClassificationEntity>().RegisterTable().HasKey(p => p.ypflId);
            modelBuilder.Entity<SysMedicineEntity>().RegisterTable().HasKey(p => p.ypId);
            //药品剂量 精确到4位小数
            modelBuilder.Entity<SysMedicineEntity>().Property(x => x.jl).HasPrecision(9, 4);
            modelBuilder.Entity<SysMedicineAntibioticTypeEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysMedicineAntibioticInfoEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysMedicineFormulationEntity>().RegisterTable().HasKey(p => p.jxId);
            modelBuilder.Entity<SysMedicinePropertyEntity>().RegisterTable().HasKey(p => p.ypsxId);
            modelBuilder.Entity<SysMedicineStorageIOModeEntity>().RegisterTable().HasKey(p => p.crkfsId);
            modelBuilder.Entity<SysMedicineSupplierEntity>().RegisterTable().HasKey(p => p.gysId);
            modelBuilder.Entity<SysMedicineUnitEntity>().RegisterTable().HasKey(p => p.ypdwId);
            modelBuilder.Entity<SysMedicineUsageEntity>().RegisterTable().HasKey(p => p.yfId);
            modelBuilder.Entity<SysPharmacyDepartmentEntity>().RegisterTable().HasKey(p => p.yfbmId);
            modelBuilder.Entity<SysPharmacyDepartmentOpenMedicineEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysPharmacyWindowEntity>().RegisterTable().HasKey(p => p.yfckId);
            modelBuilder.Entity<SysWardEntity>().RegisterTable().HasKey(p => p.bqId);
            modelBuilder.Entity<SysWardRoomEntity>().RegisterTable().HasKey(p => p.bfId);
            modelBuilder.Entity<SysUserStaffEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysUserYfbmEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysMedicalRecordChargeCategoryEntity>().RegisterTable().HasKey(p => p.dlId);
            modelBuilder.Entity<SysAgriInsuranceChargeCategoryEntity>().RegisterTable().HasKey(p => p.dlId);
            modelBuilder.Entity<SysChargeItemEntity>().RegisterTable().HasKey(p => p.sfxmId);

            modelBuilder.Entity<SysDutyEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysDiagnosisEntity>().RegisterTable().HasKey(p => p.zdId);
			modelBuilder.Entity<SurgeryEntity>().RegisterTable().HasKey(p => p.id);
			modelBuilder.Entity<SysTCMSyndromeEntity>().RegisterTable().HasKey(p => p.zhId);
            modelBuilder.Entity<SysStaffDutyEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysStaffWardEntity>().RegisterTable().HasKey(p => p.Id);

            //药品权限
            modelBuilder.Entity<SysMedicineAuthorityEntity>().RegisterTable().HasKey(p => p.qxId);
            modelBuilder.Entity<SysMedicineAuthorityRelationEntity>().RegisterTable().HasKey(p => p.id);

            /*Rehabilitation*/
            modelBuilder.Entity<RehabChargeClassificationEntity>().RegisterTable().HasKey(p => p.sfflId);
            modelBuilder.Entity<RehabChargeItemComparisonEntity>().RegisterTable().HasKey(p => p.sfxmdzId);
            modelBuilder.Entity<RehabChargeItemEntity>().RegisterTable().HasKey(p => p.sfxmId);

            modelBuilder.Entity<SysWardBedEntity>().RegisterTable().HasKey(p => p.cwId);

            modelBuilder.Entity<SysNationalityEntity>().RegisterTable().HasKey(p => p.gjId);
            modelBuilder.Entity<SysNationEntity>().RegisterTable().HasKey(p => p.mzId);

            modelBuilder.Entity<SysGlobalConfigEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysChargeCategoryTypeRelationEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysMedicineDosageEntity>().RegisterTable().HasKey(p => p.Id);

            //herp
            modelBuilder.Entity<WzUnitEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<WzTypeEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<WzCrkfsEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<GzybCodeContrastEntity>().RegisterTable().HasKey(p => p.id);

            modelBuilder.Entity<SysStaffSignatureEntity>().RegisterTable().HasKey(p => p.Id);
            //报表管理
            modelBuilder.Entity<SysReportTemplateEntity>().RegisterTable().HasKey(p => p.TemplateID);
            modelBuilder.Entity<SysReportEntity>().RegisterTable().HasKey(p => p.ReportID);

            //系统诊室
            modelBuilder.Entity<SysConsultEntity>().RegisterTable().HasKey(p => p.zsId);
            modelBuilder.Entity<SysStaffConsultEntity>().RegisterTable().HasKey(p => p.Id);

            modelBuilder.Entity<Sh_YbfyxzblEntity>().RegisterTable().HasKey(p=>p.Id);

            modelBuilder.Entity<Demo>().RegisterTable().HasKey(p => p.ID);

            modelBuilder.Conventions.Add(new DecimalPrecisionAttributeConvention());//注入
            base.OnModelCreating(modelBuilder);
        }

    }

}
