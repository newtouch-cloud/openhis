using Newtouch.Infrastructure.EF;
using Newtouch.MR.ManageSystem.Domain.Entity;
using System.Data.Entity;

namespace Newtouch.MR.ManageSystem.Domain.DBContext.Infrastructure
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
            //注册新表
            //modelBuilder.Entity<SysUserEntity>().RegisterTable().HasKey(p => p.Id);

            modelBuilder.Entity<MrbasyEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<MrbasyssEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<MrbasyzdEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<MrdicblzdEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<MrdicdeptEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<MrbasyrelcodeEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<MrreldeptEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<bafeeEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<bafeeRelEntity>().RegisterTable().HasKey(p => p.Id);
		}

    }
}