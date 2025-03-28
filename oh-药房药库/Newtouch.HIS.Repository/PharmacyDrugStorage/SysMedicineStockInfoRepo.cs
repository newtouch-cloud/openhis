using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// xt_yp_kcxx
    /// </summary>
    public class SysMedicineStockInfoRepo : RepositoryBase<SysMedicineStockInfoEntity>, ISysMedicineStockInfoRepo
    {
        public SysMedicineStockInfoRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 外部出库变更库存冻结数量
        /// </summary>
        /// <param name="kcId"></param>
        /// <param name="djsl"></param>
        public void UpdateDjsl(string kcId, int djsl)
        {
            var entity = this.FindEntity(kcId);
            if (entity.kcsl - entity.djsl < djsl)
            {
                throw new FailedException("可用数量少于冻结数量");
            }
            entity.djsl = entity.djsl + djsl;
            entity.Modify();
            this.Update(entity);

        }

        /// <summary>
        /// get xt_yp_kcxx by sql
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="sqlParameter"></param>
        /// <returns></returns>
        public object FindEntity<T>(string sql, DbParameter[] sqlParameter)
        {
            return FindList<T>(sql, sqlParameter);
        }

        /// <summary>
        /// get xt_yp_kcxx by yfbmcode,organizeId and pc
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="pc"></param>
        /// <returns></returns>
        public SysMedicineStockInfoEntity FindEntity(string yfbmCode, string organizeId, string pc)
        {
            return FindEntity(p => p.yfbmCode == yfbmCode && p.OrganizeId == organizeId && p.pc == pc);
        }

        /// <summary>
        /// 修改库存状态
        /// </summary>
        /// <param name="ypdm"></param>
        /// <param name="ph"></param>
        /// <param name="pc"></param>
        /// <param name="zt"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public int UpdateZt(string ypdm, string ph, string pc, string zt, string yfbmCode, string organizeId)
        {
            var entity = FindEntity(p => p.ph == ph && p.pc == pc && p.ypdm == ypdm && p.yfbmCode == yfbmCode && p.OrganizeId == organizeId);
            if (entity == null) return 0;
            entity.zt = zt.Trim();
            entity.Modify();
            return Update(entity);
        }

        /// <summary>
        /// 计算可用库存 当前方法无日志
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<SysMedicineStockInfoEntity> SelectData(string ypCode, string yfbmCode, string organizeId)
        {
            const string sql = @"
		SELECT *
		FROM xt_yp_kcxx(NOLOCK) 
		WHERE ypdm = @ypCode
		AND yfbmCode = @yfbmCode 
		AND tybz = '0' 
		AND kcsl>0
		AND djsl>=0
		AND zt='1'
		AND OrganizeId = @OrganizeId 
";
            var param = new DbParameter[]
            {
                new SqlParameter("@ypCode", ypCode),
                new SqlParameter("@yfbmCode", yfbmCode),
                new SqlParameter("@OrganizeId", organizeId),
            };
            return _dataContext.Database.SqlQuery<SysMedicineStockInfoEntity>(sql, param).ToList();
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
        public List<SysMedicineStockInfoEntity> SelectData(string ypCode, string ph, string pc, string yfbmCode, string organizeId)
        {
            const string sql = @"
		SELECT *
		FROM xt_yp_kcxx(NOLOCK) 
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
            return _dataContext.Database.SqlQuery<SysMedicineStockInfoEntity>(sql, param).ToList();
        }

        /// <summary>
        /// 减少冻结库存
        /// </summary>
        /// <param name="sl">最小单位数量 要减去的库存数</param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public int SubtractForzenKc(int sl, string pc, string ph, string ypCode, string yfbmCode, string organizeId, string userCode)
        {
            const string sql = @"
UPDATE dbo.xt_yp_kcxx SET djsl=djsl-@sl, LastModifyTime=GETDATE(), LastModifierCode=@userCode
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
            return _dataContext.Database.ExecuteSqlCommand(sql, param);
        }

        /// <summary>
        /// 减少冻结库存
        /// </summary>
        /// <param name="sl">最小单位数量 要减去的库存数</param>
        /// <param name="kcId"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public int SubtractForzenKc(int sl, string kcId ,string organizeId, string userCode)
        {
            const string sql = @"
UPDATE dbo.xt_yp_kcxx SET djsl=djsl-@sl, LastModifyTime=GETDATE(), LastModifierCode=@userCode
WHERE kcId=@kcId AND OrganizeId=@OrganizeId AND zt='1' AND tybz='0'
";
            var param = new DbParameter[]
            {
                new SqlParameter("@sl", sl ),
                new SqlParameter("@userCode", userCode),
                new SqlParameter("@kcId", kcId),
                new SqlParameter("@OrganizeId", organizeId)
            };
            return _dataContext.Database.ExecuteSqlCommand(sql, param);
        }

        /// <summary>
        /// 减少djsl和kcsl
        /// </summary>
        /// <param name="sl">最小单位数量 要减去的库存数</param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public int SubtractDjslAndKcsl(int sl, string pc, string ph, string ypCode, string yfbmCode, string organizeId, string userCode)
        {
            const string sql = @"
UPDATE dbo.xt_yp_kcxx SET djsl=djsl-@sl, kcsl=kcsl-@sl, LastModifyTime=GETDATE(), LastModifierCode=@userCode
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
            return _dataContext.Database.ExecuteSqlCommand(sql, param);
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
UPDATE dbo.xt_yp_kcxx SET kcsl=kcsl+@sl, LastModifyTime=GETDATE(), LastModifierCode=@userCode
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


        public int UpdateExpired(string kcId, string yxq)
        {
            var entity = this.FindEntity(kcId);
            entity.yxq = Convert.ToDateTime(yxq);
            entity.Modify();
            return this.Update(entity);

        }
    }
}
