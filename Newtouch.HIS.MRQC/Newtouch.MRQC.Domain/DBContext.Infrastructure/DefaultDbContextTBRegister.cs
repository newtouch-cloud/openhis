using Newtouch.Infrastructure.EF;
using Newtouch.MRQC.Domain.Entity.QcItemManage;
using System.Data.Entity;

namespace Newtouch.MRQC.Domain.DBContext.Infrastructure
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

            //质控项
            modelBuilder.Entity<QcItemDataEntity>().RegisterTable().HasKey(p => p.Id);
            //病历申请
            modelBuilder.Entity<MrApplyEntity>().RegisterTable().HasKey(p => p.Id);
            //病历评分
            modelBuilder.Entity<MrQcScoreEntity>().RegisterTable().HasKey(p => p.Id);
        }

    }
}