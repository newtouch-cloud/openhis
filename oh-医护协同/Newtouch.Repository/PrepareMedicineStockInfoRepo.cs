using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Repository
{
    public class PrepareMedicineStockInfoRepo : RepositoryBase<PrepareMedicineStockInfoEntity>, IPrepareMedicineStockInfoRepo
    {
        public PrepareMedicineStockInfoRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 计算可用库存 当前方法无日志
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="pc"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="ph"></param>
        /// <returns></returns>
        public List<PrepareMedicineStockInfoEntity> SelectData(string ypCode, string ph, string pc, string yfbmCode, string organizeId)
        {
            const string sql = @"
		SELECT *
		FROM xt_ksby_kcxx(NOLOCK) 
		WHERE ypdm = @ypCode
		AND yfbmCode = @yfbmCode 
		AND tybz = '0' 
		AND kcsl>0
		AND djsl>=0
		AND pc=@pc
		AND ph=@ph
		AND zt='1'
		AND OrganizeId = @OrganizeId 
";
            var param = new DbParameter[]
            {
                new SqlParameter("@ypCode", ypCode),
                new SqlParameter("@yfbmCode", yfbmCode),
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@pc", pc),
                new SqlParameter("@ph", ph)
            };
            return _dataContext.Database.SqlQuery<PrepareMedicineStockInfoEntity>(sql, param).ToList();
        }


        /// <summary>
        /// 变更库存数量
        /// </summary>
        /// <param name="sl">最小单位数量 +：加库存  -：减库存</param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public int UpdateKcslWithTrans(int sl, string pc, string ph, string ypCode, string yfbmCode, string organizeId, string userCode, Infrastructure.EF.EFDbTransaction db)
        {
            const string sql = @"
UPDATE dbo.xt_ksby_kcxx SET kcsl=kcsl+@sl, LastModifyTime=GETDATE(), LastModifierCode=@userCode
WHERE pc=@pc AND ph=@ph AND ypdm=@ypCode AND OrganizeId=@OrganizeId AND yfbmCode=@yfbmCode AND zt='1' AND tybz='0'
";
            var param = new DbParameter[]
            {
                new SqlParameter("@sl", sl ),
                new SqlParameter("@userCode", userCode),
                new SqlParameter("@pc", pc),
                new SqlParameter("@ph",ph ),
                new SqlParameter("@ypCode",ypCode ),
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@yfbmCode", yfbmCode)
            };
            return db.ExecuteSqlCommand(sql, param);
        }

    }
}
