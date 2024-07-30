using Newtouch.Infrastructure.EF;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Newtouch.Infrastructure.EF.Conventions;

namespace Newtouch.HIS.Domain.DBContext.Infrastructure
{
    /// <summary>
    /// 基础库
    /// </summary>
    public class BaseDbContext : DbContextBase
    {
        public BaseDbContext() : base("BaseDbContext")
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

            modelBuilder.Entity<XtypcrkfsEntity>().RegisterTable().HasKey(p => p.crkfsId);

            modelBuilder.Conventions.Add(new DecimalPrecisionAttributeConvention());
            base.OnModelCreating(modelBuilder);
        }
    }
}