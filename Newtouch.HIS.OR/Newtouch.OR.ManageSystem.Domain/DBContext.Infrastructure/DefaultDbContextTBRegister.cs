using Newtouch.Infrastructure.EF;
using Newtouch.OR.ManageSystem.Domain.Entity;
using System.Data.Entity;

namespace Newtouch.OR.ManageSystem.Domain.DBContext.Infrastructure
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

            //
            modelBuilder.Entity<ORApplyInfoEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<OROperationEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<ORRoomEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<ORAnesthesiaEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<ORStaffEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<ORNotchGradeEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<ORArrangementEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<OROpStaffRecordEntity>().RegisterTable().HasKey(p => p.Id);
            modelBuilder.Entity<ORRegistrationEntity>().RegisterTable().HasKey(p => p.Id);
			modelBuilder.Entity<ORApplyInfoExpandEntity>().RegisterTable().HasKey(p => p.Id);
		}

    }
}