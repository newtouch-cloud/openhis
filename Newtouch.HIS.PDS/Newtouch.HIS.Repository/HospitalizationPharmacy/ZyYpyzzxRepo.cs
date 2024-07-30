using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 住院药品医嘱执行
    /// </summary>
    public class ZyYpyzzxRepo : RepositoryBase<ZyYpyzxxEntity>, IZyYpyzxxRepo
    {
        public ZyYpyzzxRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取需要发药的病区（去重）
        /// </summary>
        /// <returns></returns>
        public List<string> GetFyBq()
        {
            return FindList<string>(@"SELECT DISTINCT bqmc FROM dbo.zy_ypyzzx(NOLOCK) WHERE fybz='" + (int)EnumFybz.Yp + "'");
        }

        /// <summary>
        /// 获取需要发药的病人和病区（去重）
        /// </summary>
        /// <returns></returns>
        public List<string> GetFyPatients()
        {
            return FindList<string>(@"SELECT DISTINCT patientName, bqmc FROM dbo.zy_ypyzzx(NOLOCK) WHERE fybz='" + (int)EnumFybz.Yp + "'");
        }

        /// <summary>
        /// 获取医嘱信息 no log
        /// </summary>
        /// <param name="yzId">医嘱ID</param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<ZyYpyzxxEntity> SelectDataByYzId(string yzId, string organizeId)
        {
            const string sql = @"
SELECT * FROM dbo.zy_ypyzxx(NOLOCK) 
WHERE yzId=@yzId AND OrganizeId=@OrganizeId 
";
            var param = new DbParameter[]
            {
                new SqlParameter("@yzId",yzId),
                new SqlParameter("@OrganizeId",organizeId)
            };
            return _dataContext.Database.SqlQuery<ZyYpyzxxEntity>(sql, param).ToList();
        }

        /// <summary>
        /// 获取医嘱信息 no log
        /// </summary>
        /// <param name="yzId"></param>
        /// <param name="zxId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<ZyYpyzxxEntity> SelectDataByYzId(string yzId, string zxId, string organizeId)
        {
            const string sql = @"
SELECT * FROM dbo.zy_ypyzxx(NOLOCK) 
WHERE zxId=@zxId AND OrganizeId=@OrganizeId AND yzId=@yzId
";
            var param = new DbParameter[]
            {
                new SqlParameter("@yzId",yzId),
                new SqlParameter("@zxId",zxId),
                new SqlParameter("@OrganizeId",organizeId)
            };
            return _dataContext.Database.SqlQuery<ZyYpyzxxEntity>(sql, param).ToList();
        }

        /// <summary>
        /// 获取医嘱信息 no log
        /// </summary>
        /// <param name="yzId"></param>
        /// <param name="zxId"></param>
        /// <param name="ypCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<ZyYpyzxxEntity> SelectDataByYzId(string yzId, string zxId, string ypCode, string organizeId)
        {
            const string sql = @"
SELECT * FROM dbo.zy_ypyzxx(NOLOCK) 
WHERE zxId=@zxId AND OrganizeId=@OrganizeId AND yzId=@yzId AND ypCode=@ypCode 
";
            var param = new DbParameter[]
            {
                new SqlParameter("@yzId",yzId),
                new SqlParameter("@zxId",zxId),
                new SqlParameter("@ypCode",ypCode),
                new SqlParameter("@OrganizeId",organizeId)
            };
            return _dataContext.Database.SqlQuery<ZyYpyzxxEntity>(sql, param).ToList();
        }

        /// <summary>
        /// 修改发药标志位已退药
        /// </summary>
        /// <param name="yzId"></param>
        /// <param name="zxId"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="db"></param>
        public int UpdateFybzTy(string yzId, string zxId, string userCode, string organizeId, Infrastructure.EF.EFDbTransaction db)
        {
            const string sql = @"
UPDATE NewtouchHIS_PDS.dbo.zy_ypyzxx
SET fybz = @fybz,
    LastModiFierCode = @userCode,
    LastModifyTime = GETDATE()
WHERE zxId=@zxId
AND OrganizeId=@OrganizeId
AND yzId=@yzId
";
            var param = new DbParameter[]
            {
                new SqlParameter("@yzId", yzId),
                new SqlParameter("@zxId", zxId),
                new SqlParameter("@fybz", 3),//已退
                new SqlParameter("@userCode", userCode),
                new SqlParameter("@OrganizeId", organizeId)
            };
           return db.ExecuteSqlCommand(sql, param);
        }

        /// <summary>
        /// 根据执行ID，物理删除此次执行的所有明细
        /// </summary>
        /// <param name="zxId"></param>
        /// <returns></returns>
        public int DeleteByZxId(string zxId)
        {
            return ExecuteSqlCommand("DELETE FROM dbo.zy_ypyzxx WHERE zxId=@zxId", new SqlParameter("@zxId", zxId));
        }
    }
}
