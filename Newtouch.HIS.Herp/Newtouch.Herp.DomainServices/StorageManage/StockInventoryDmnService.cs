using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.Entity.VEntity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure.Enum;
using Newtouch.Herp.Infrastructure.TSql;

namespace Newtouch.Herp.DomainServices.StorageManage
{
    /// <summary>
    /// 库存盘点
    /// </summary>
    public class StockInventoryDmnService : DmnServiceBase, IStockInventoryDmnService
    {
        private readonly IKfKcxxDmnService _kfKcxxDmnService;
        private readonly IKcSyxxRepo _kcSyxxRepo;

        public StockInventoryDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取盘点时间
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<VInventoryDateDropDownEntity> GetPdSj(string warehouseId, string organizeId)
        {
            const string sql = @"
SELECT Id pdId, pdsj FROM (
SELECT pdxx.Id,Convert(Varchar(20),pdxx.kssj,120) + '=>' + Isnull(Convert(Varchar(20),pdxx.jssj,120),' ') pdsj, pdxx.CreateTime
FROM kc_pdxx(NOLOCK) pdxx
INNER JOIN dbo.kc_pdxxmx(NOLOCK) pdmx ON pdmx.pdId=pdxx.Id
WHERE pdxx.warehouseId =@warehouseId 
and pdxx.OrganizeId=@OrganizeId
GROUP BY pdxx.Id, pdxx.Kssj, pdxx.Jssj, pdxx.CreateTime
) a
ORDER BY a.CreateTime DESC
";
            var param = new DbParameter[]
            {
                new SqlParameter("@warehouseId", warehouseId),
                new SqlParameter("@OrganizeId", organizeId)
            };
            return FindList<VInventoryDateDropDownEntity>(sql, param);
        }

        /// <summary>
        /// 获取未完成的盘点时间
        /// </summary>
        /// <returns></returns>
        public List<VInventoryDateDropDownEntity> GetHangUpPdDates(string warehouseId, string organizeId)
        {
            var strSql = new StringBuilder(@"
SELECT Id pdId, pdsj FROM (
SELECT pdxx.Id,Convert(Varchar(20),pdxx.kssj,120) + '=>' + Isnull(Convert(Varchar(20),pdxx.jssj,120),' ') pdsj, pdxx.CreateTime
FROM kc_pdxx(NOLOCK) pdxx
INNER JOIN dbo.kc_pdxxmx(NOLOCK) pdmx ON pdmx.pdId=pdxx.Id
WHERE pdxx.warehouseId =@warehouseId 
AND pdxx.OrganizeId=@OrganizeId
AND pdxx.Jssj IS NULL 
GROUP BY pdxx.Id, pdxx.Kssj, pdxx.Jssj, pdxx.CreateTime
) a
");
            var param = new DbParameter[]
            {
                new SqlParameter("@warehouseId",warehouseId),
                new SqlParameter("@OrganizeId",organizeId)
            };
            return FindList<VInventoryDateDropDownEntity>(strSql.ToString(), param).ToList();
        }

        /// <summary>
        /// 生成盘点信息
        /// </summary>
        public string GenerateInventoryInfo(string warehouseId, string organizeId)
        {
            var paraList = new List<DbParameter>
            {
                new SqlParameter("@warehouseId",warehouseId),
                new SqlParameter("@OrganizeId",organizeId),
                new SqlParameter("@CreatorCode", OperatorProvider.GetCurrent().UserCode)
            };
            return FindList<string>(StockInventory.generate_inventory_info, paraList.ToArray()).FirstOrDefault();
        }

        /// <summary>
        /// 获取盘点信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IList<VInventoryInfoEntity> QueryInventoryInfoList(Pagination pagination, InventorySearchDTO param, string warehouseId, string organizeId)
        {
            var sql = new StringBuilder(@"
SELECT mx.productId, wz.name wzmc, wz.gg, dbo.[f_getComplexWzSlandDw](mx.llsl,pu.zhyz,dw.name,zxdw.name) llsl
,Floor(mx.sjsl / mx.zhyz) deptSjsl, dw.name deptdw,(mx.sjsl % mx.zhyz) minSjsl, zxdw.name zxdw
,mx.zhyz, mx.ph, mx.pc, mx.yxq, mx.lsj
,CONVERT(NUMERIC(11,2),mx.lsj*mx.llsl/mx.Zhyz) lllsje
,CONVERT(NUMERIC(11,2),mx.lsj*mx.sjsl/mx.Zhyz) sjlsje
,ISNULL(wz.py,'') py, mx.Id pdmxId, pdxx.CreateTime
FROM dbo.kc_pdxx(NOLOCK) pdxx
INNER JOIN dbo.kc_pdxxmx(NOLOCK) mx ON mx.pdId=pdxx.Id
LEFT JOIN dbo.wz_product(NOLOCK) wz ON wz.Id=mx.productId AND wz.OrganizeId=pdxx.OrganizeId
LEFT JOIN dbo.rel_productWarehouse(NOLOCK) pw ON pw.warehouseId=pdxx.warehouseId AND pw.productId=mx.productId AND pw.OrganizeId=pdxx.OrganizeId
LEFT JOIN dbo.rel_productUnit(NOLOCK) pu ON pu.productId=pw.productId AND pu.unitId=pw.unitId
LEFT JOIN NewtouchHIS_Base.dbo.wz_unit(NOLOCK) dw ON dw.Id=pu.unitId 
LEFT JOIN NewtouchHIS_Base.dbo.wz_unit(NOLOCK) zxdw ON zxdw.Id=wz.minUnit
WHERE pdxx.OrganizeId=@OrganizeId
AND pdxx.warehouseId=@warehouseId 
AND pdxx.Id=@pdId
");
            var p = new List<DbParameter>
            {
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@warehouseId", warehouseId),
                new SqlParameter("@pdId", param.pdId),
            };
            if (!string.IsNullOrWhiteSpace(param.keyWord))
            {
                sql.AppendLine("AND (wz.name LIKE '%'+@keyWord+'%' OR wz.py LIKE '%'+@keyWord+'%') ");
                p.Add(new SqlParameter("@keyWord", param.keyWord));
            }
            if (!string.IsNullOrWhiteSpace(param.lb))
            {
                sql.AppendLine("AND wz.typeId=@lbId ");
                p.Add(new SqlParameter("@lbId", param.lb));
            }
            if (!string.IsNullOrWhiteSpace(param.wzzt))
            {
                sql.AppendLine("AND wz.zt=@wzzt ");
                p.Add(new SqlParameter("@wzzt", param.wzzt));
            }
            switch (param.kcxs)
            {
                case (int)EnumKCXS.Xslkc://显示零库存
                    sql.AppendLine("AND (mx.Llsl = 0 OR mx.Sjsl = 0) ");
                    break;
                case (int)EnumKCXS.Bxsllslwl://不显示理论数量为0
                    sql.AppendLine("AND mx.Llsl <> 0 ");
                    break;
                case (int)EnumKCXS.Bxssjslwl://不显示实际数量为0
                    sql.AppendLine("AND mx.Llsl Sjsl <> 0 ");
                    break;
                case (int)EnumKCXS.Bxslzdwl://不显示两者都为0
                    sql.AppendLine("AND (mx.Llsl <> 0 OR mx.Sjsl <> 0) ");
                    break;
            }
            return QueryWithPage<VInventoryInfoEntity>(sql.ToString(), pagination, p.ToArray());
        }

        /// <summary>
        /// 获取盘点信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IList<VInventoryInfoEntity> QueryInventoryInfoListNoPc(Pagination pagination, InventorySearchDTO param, string warehouseId, string organizeId)
        {
            var sql = new StringBuilder(@"
SELECT s.productId,s.wzmc,s.gg,dbo.[f_getComplexWzSlandDw](s.lsl,s.zhyz,s.deptdw,s.zxdw) llsl,s.deptdw,s.zxdw,s.zhyz,s.lsj
,CONVERT(NUMERIC(11,2),s.lsj/s.zhyz*s.lsl) lllsje,CONVERT(NUMERIC(11,2),s.lsj/s.zhyz*s.sjsl) sjlsje,s.py,s.CreateTime 
,Floor(s.sjsl / s.zhyz) deptSjsl
,(s.sjsl % s.zhyz) minSjsl
FROM (
	SELECT mx.productId, wz.name wzmc, wz.gg, SUM(mx.llsl) lsl,SUM(mx.sjsl) sjsl, dw.name deptdw, zxdw.name zxdw ,mx.zhyz, mx.lsj
	,ISNULL(wz.py,'') py, pdxx.CreateTime
	FROM dbo.kc_pdxx(NOLOCK) pdxx
	INNER JOIN dbo.kc_pdxxmx(NOLOCK) mx ON mx.pdId=pdxx.Id
	LEFT JOIN dbo.wz_product(NOLOCK) wz ON wz.Id=mx.productId AND wz.OrganizeId=pdxx.OrganizeId
	LEFT JOIN dbo.rel_productWarehouse(NOLOCK) pw ON pw.warehouseId=pdxx.warehouseId AND pw.productId=mx.productId AND pw.OrganizeId=pdxx.OrganizeId
	LEFT JOIN dbo.rel_productUnit(NOLOCK) pu ON pu.productId=pw.productId AND pu.unitId=pw.unitId
	LEFT JOIN NewtouchHIS_Base.dbo.wz_unit(NOLOCK) dw ON dw.Id=pu.unitId 
	LEFT JOIN NewtouchHIS_Base.dbo.wz_unit(NOLOCK) zxdw ON zxdw.Id=wz.minUnit
	WHERE pdxx.OrganizeId=@OrganizeId
	AND pdxx.warehouseId=@warehouseId 
	AND pdxx.Id=@pdId 
");
            var p = new List<DbParameter>
            {
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@warehouseId", warehouseId),
                new SqlParameter("@pdId", param.pdId),
            };
            if (!string.IsNullOrWhiteSpace(param.keyWord))
            {
                sql.AppendLine("	AND (wz.name LIKE '%'+@keyWord+'%' OR wz.py LIKE '%'+@keyWord+'%') ");
                p.Add(new SqlParameter("@keyWord", param.keyWord));
            }
            if (!string.IsNullOrWhiteSpace(param.lb))
            {
                sql.AppendLine("	AND wz.typeId=@lbId ");
                p.Add(new SqlParameter("@lbId", param.lb));
            }
            if (!string.IsNullOrWhiteSpace(param.wzzt))
            {
                sql.AppendLine("	AND wz.zt=@wzzt ");
                p.Add(new SqlParameter("@wzzt", param.wzzt));
            }
            switch (param.kcxs)
            {
                case (int)EnumKCXS.Xslkc://显示零库存
                    sql.AppendLine("	AND (mx.Llsl = 0 OR mx.Sjsl = 0) ");
                    break;
                case (int)EnumKCXS.Bxsllslwl://不显示理论数量为0
                    sql.AppendLine("	AND mx.Llsl <> 0 ");
                    break;
                case (int)EnumKCXS.Bxssjslwl://不显示实际数量为0
                    sql.AppendLine("	AND mx.Llsl Sjsl <> 0 ");
                    break;
                case (int)EnumKCXS.Bxslzdwl://不显示两者都为0
                    sql.AppendLine("	AND (mx.Llsl <> 0 OR mx.Sjsl <> 0) ");
                    break;
            }

            sql.AppendLine("	GROUP BY mx.productId,wz.name,wz.gg,dw.name,zxdw.name,mx.zhyz,mx.lsj,wz.py,pdxx.CreateTime");
            sql.AppendLine(") s ");
            return QueryWithPage<VInventoryInfoEntity>(sql.ToString(), pagination, p.ToArray());
        }

        /// <summary>
        /// 取消盘点 
        /// 删除 盘点信息（xt_yp_pdxx）、盘点信息明细（xt_yp_pdxxmx）
        /// </summary>
        public void CancelInventory(long pdId)
        {
            var sql = new StringBuilder(@"
DELETE FROM dbo.kc_pdxx WHERE Id=@pdId_param;
IF @@ERROR <>0
BEGIN
	SELECT 0 AS Jgxx
END
ELSE
BEGIN
	DECLARE @tmpTab TABLE(pdmxId BIGINT);
	INSERT INTO @tmpTab SELECT TOP 200 Id FROM dbo.kc_pdxxmx(NOLOCK) WHERE pdId=@pdId_param;

	WHILE EXISTS(SELECT 1 FROM @tmpTab)
	BEGIN
		DELETE FROM dbo.kc_pdxxmx WHERE Id IN (SELECT pdmxId FROM @tmpTab);
		DELETE FROM @tmpTab;
		INSERT INTO @tmpTab SELECT TOP 200 Id FROM dbo.kc_pdxxmx(NOLOCK) WHERE pdId=@pdId_param;
	END
	SELECT 1 AS Jgxx
END
");
            var result = FindList<int>(sql.ToString(), new DbParameter[]
            {
                new SqlParameter("@pdId_param", pdId)
            }).FirstOrDefault();
            if (result == 0)
            {
                throw new Exception("取消失败！");
            }
        }

        /// <summary>
        /// 结束盘点
        /// </summary>
        /// <returns></returns>
        public string EndInventory(long pdId, string organizeId, string creatorCode)
        {
            var paraList = new List<DbParameter>
            {
                new SqlParameter("@pdId", pdId),
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@CreatorCode", creatorCode)
            };
            return FindList<string>(StockInventory.end_Inventory, paraList.ToArray()).FirstOrDefault();
        }

        /// <summary>
        /// 批量保存损益信息
        /// </summary>
        /// <param name="syList"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public int InsertSyxxBatch(List<KcSyxxEntity> syList, string warehouseId, string organizeId)
        {
            var ret = 0;
            if (syList == null || syList.Count <= 0) return ret;
            using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
            {
                syList.ForEach(p =>
                {
                    _kcSyxxRepo.Insert(p);
                    if (_kfKcxxDmnService.UpdateKcsl(p.productId, p.pc, p.Ph, warehouseId, p.Sysl, organizeId, p.CreatorCode) <= 0)
                    {
                        throw new Exception("修改库存数量失败");
                    }
                    ++ret;
                });
                db.Commit();
            }
            return ret;
        }

        /// <summary>
        /// 保存变更后的盘点明细
        /// </summary>
        /// <param name="saveInventoryDto"></param>
        /// <param name="pdId"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public string SaveInventoryDetailNoPc(List<SaveInventoryDTO> saveInventoryDto, long pdId, string warehouseId, string organizeId)
        {
            var result = "";
            using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
            {
                foreach (var p in saveInventoryDto)
                {
                    var param = new DbParameter[]
                    {
                        new SqlParameter("@proId",p.productId),
                        new SqlParameter("@pdId",pdId),
                        new SqlParameter("@OrganizeId",organizeId),
                        new SqlParameter("@warehouseId",warehouseId),
                        new SqlParameter("@sl",p.sjsl),
                    };
                    result = FindList<string>(StockInventory.save_inventory_info, param).FirstOrDefault();
                    if (!string.IsNullOrWhiteSpace(result)) break;
                }

                if (string.IsNullOrWhiteSpace(result))
                {
                    db.Commit();
                }
            }
            return result;
        }
    }
}
