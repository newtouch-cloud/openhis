using FrameworkBase.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace FrameworkBase.Domain.DBContext.Infrastructure
{
    /// <summary>
    /// DB上下文
    /// </summary>
    public sealed class DefaultDbContext : DbContextBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DefaultDbContext()
            : base("DefaultDbContext", true)
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
            var defaultSchemaName = Newtouch.Core.Common.Utils.ConfigurationHelper.GetAppConfigValue("DefaultDbContext_SchemaName");
            if (!string.IsNullOrWhiteSpace(defaultSchemaName))
            {
                modelBuilder.HasDefaultSchema(defaultSchemaName);
            }
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Conventions.Remove<DatabaseGeneratedAttributeConvention>();  //orcal?

            modelBuilder.Entity<SysDepartmentEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysDutyEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysLogEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysModuleEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysRoleEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysRoleAuthorizeEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysStaffEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysStaffDutyEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysUserEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysUserLogOnEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysUserRoleEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysUserStaffEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysItemsTypeEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysItemsDataEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<SysConfigEntity>().RegisterTable().HasKey(p => p.Id);

            //
            if (_ac != null)
            {
                _ac(modelBuilder);
            }

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// 
        /// </summary>
        private static Action<DbModelBuilder> _ac;

        /// <summary>
        /// 注册一个委托
        /// 此委托：向上下文中注册表
        /// （底层已经注册过了一部分表）
        /// </summary>
        /// <param name="ac"></param>
        public static void PartialTBRegister(Action<DbModelBuilder> ac)
        {
            _ac = ac;
        }

    }

}