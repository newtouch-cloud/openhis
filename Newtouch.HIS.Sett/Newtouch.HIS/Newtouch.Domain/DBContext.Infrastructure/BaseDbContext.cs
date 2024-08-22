using Newtouch.HIS.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

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

            //modelBuilder.Entity<V_S_Sys_Organize>().ToTable("V_S_Sys_Organize").HasKey(p => p.Id);

            base.OnModelCreating(modelBuilder);
        }

    }
}
