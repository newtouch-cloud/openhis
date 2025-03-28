using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 住院药品医嘱执行批号
    /// </summary>
    public class ZyYpyzzxphRepo : RepositoryBase<ZyYpyzzxphEntity>, IZyYpyzzxphRepo
    {
        public ZyYpyzzxphRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取住院有效未归架的排药信息  此方法不加日志
        /// </summary>
        /// <param name="yzId">医嘱ID</param>
        /// <param name="zxId">医嘱执行ID</param>
        /// <param name="ypCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<ZyYpyzzxphEntity> SelectZyphList(string yzId, string zxId, string ypCode, string organizeId)
        {
            const string sql = @"
SELECT * FROM dbo.zy_ypyzzxph(NOLOCK) 
WHERE ypCode=@ypCode AND zxId=@zxId AND yzId=@yzId AND OrganizeId=@OrganizeId AND zt='1' AND gjzt='0'
";
            var param = new DbParameter[]
            {
                new SqlParameter("@yzId", yzId),
                new SqlParameter("@zxId", zxId),
                new SqlParameter("@ypCode", ypCode),
                new SqlParameter("@OrganizeId",organizeId )
            };
            return _dataContext.Database.SqlQuery<ZyYpyzzxphEntity>(sql, param).ToList();
        }

        /// <summary>
        /// 获取住院有效未归架的排药信息  此方法不加日志
        /// </summary>
        /// <param name="yzId">医嘱ID</param>
        /// <param name="zxId">医嘱执行ID</param>
        /// <param name="ypCode"></param>
        /// <param name="ph"></param>
        /// <param name="organizeId"></param>
        /// <param name="pc"></param>
        /// <returns></returns>
        public List<ZyYpyzzxphEntity> SelectZyphList(string yzId, string zxId, string ypCode, string pc, string ph, string organizeId)
        {
            const string sql = @"
SELECT * FROM dbo.zy_ypyzzxph(NOLOCK) 
WHERE ypCode=@ypCode AND zxId=@zxId AND yzId=@yzId AND OrganizeId=@OrganizeId AND zt='1' AND gjzt='0' AND pc=@pc AND ph=@ph
";
            var param = new DbParameter[]
            {
                new SqlParameter("@yzId", yzId),
                new SqlParameter("@zxId", zxId),
                new SqlParameter("@ypCode", ypCode),
                new SqlParameter("@pc", pc),
                new SqlParameter("@ph", ph),
                new SqlParameter("@OrganizeId",organizeId )
            };
            return _dataContext.Database.SqlQuery<ZyYpyzzxphEntity>(sql, param).ToList();
        }

    }
}
