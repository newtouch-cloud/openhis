using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IDomainServices;

namespace Newtouch.Herp.DomainServices
{
    /// <summary>
    /// 申领单管理
    /// </summary>
    public class KfApplyOrderDmnService : DmnServiceBase, IKfApplyOrderDmnService
    {
        public KfApplyOrderDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取申领单主信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="sldh"></param>
        /// <param name="applyType"></param>
        /// <param name="applyProcess"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IList<VApplyBillInfoEntity> SelectApplyBillInfo(Pagination pagination, string sldh, int applyType, int applyProcess, DateTime kssj, DateTime jssj, string organizeId)
        {
            const string sql = @"
SELECT DISTINCT ao.applyType, ao.applyProcess, ao.sldh, ckbm.name ckbmmc, ao.ckbm ckbmId, rkbm.name rkbmmc,ao.rkbm rkbmId, ks.Name rkksmc, ao.CreateTime, ao.CreatorCode, ss.Name CreatorName
FROM dbo.kf_applyOrder(NOLOCK) ao
INNER JOIN dbo.kf_applyOrderDetail(NOLOCK) aod ON aod.sldId=ao.Id AND aod.zt='1'
LEFT JOIN dbo.kf_warehouse(NOLOCK) ckbm ON ckbm.Id=ao.ckbm AND ckbm.OrganizeId=ao.OrganizeId AND ckbm.zt='1'
LEFT JOIN dbo.kf_warehouse(NOLOCK) rkbm ON rkbm.Id=ao.rkbm AND rkbm.OrganizeId=ao.OrganizeId AND rkbm.zt='1' AND ao.applyType='2' 
LEFT JOIN NewtouchHIS_Base.dbo.Sys_Department(NOLOCK) ks ON ks.Id=ao.rkbm AND ks.OrganizeId=ao.OrganizeId AND ks.zt='1' AND ao.applyType='1'
LEFT JOIN NewtouchHIS_Base.dbo.Sys_User(NOLOCK) su ON su.Account=ao.CreatorCode AND su.zt='1'
LEFT JOIN NewtouchHIS_Base.dbo.Sys_UserStaff(NOLOCK) sus ON sus.UserId=su.Id AND sus.zt='1'
LEFT JOIN NewtouchHIS_Base.dbo.Sys_Staff(NOLOCK) ss ON ss.Id=sus.StaffId AND ss.OrganizeId=ao.OrganizeId AND ss.zt='1' AND ss.TopOrganizeId=su.TopOrganizeId
WHERE ao.OrganizeId=@OrganizeId
AND ao.zt='1'
AND (ao.applyType=@applyType OR 0=@applyType)
AND (ao.applyProcess=@applyProcess OR 0=@applyProcess)
AND ao.CreateTime BETWEEN @kssj AND @jssj
AND ao.sldh LIKE '%'+@sldh+'%'
";
            var param = new DbParameter[]
            {
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@applyType", applyType),
                new SqlParameter("@applyProcess", applyProcess),
                new SqlParameter("@kssj", kssj),
                new SqlParameter("@jssj", jssj),
                new SqlParameter("@sldh", sldh)
            };
            return QueryWithPage<VApplyBillInfoEntity>(sql, pagination, param);
        }

        /// <summary>
        /// 获取申领单主信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="sldh"></param>
        /// <param name="applyType"></param>
        /// <param name="applyProcess"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="organizeId"></param>
        /// <param name="warehouseId"></param>
        /// <param name="applyProcesses">可选处理状态集合</param>
        /// <returns></returns>
        public IList<VApplyBillInfoEntity> SelectApplyBillInfo(Pagination pagination, string sldh, int applyType, int applyProcess, DateTime kssj, DateTime jssj, string organizeId, string warehouseId, List<int> applyProcesses)
        {
            var sql = new StringBuilder(@"
SELECT DISTINCT ao.applyType, ao.applyProcess, ao.sldh, ckbm.name ckbmmc, ao.ckbm ckbmId, rkbm.name rkbmmc,ao.rkbm rkbmId, ks.Name rkksmc, ao.CreateTime, ao.CreatorCode, ss.Name CreatorName
FROM dbo.kf_applyOrder(NOLOCK) ao
INNER JOIN dbo.kf_applyOrderDetail(NOLOCK) aod ON aod.sldId=ao.Id AND aod.zt='1'
LEFT JOIN dbo.kf_warehouse(NOLOCK) ckbm ON ckbm.Id=ao.ckbm AND ckbm.OrganizeId=ao.OrganizeId AND ckbm.zt='1'
LEFT JOIN dbo.kf_warehouse(NOLOCK) rkbm ON rkbm.Id=ao.rkbm AND rkbm.OrganizeId=ao.OrganizeId AND rkbm.zt='1' AND ao.applyType='2' 
LEFT JOIN NewtouchHIS_Base.dbo.Sys_Department(NOLOCK) ks ON ks.Id=ao.rkbm AND ks.OrganizeId=ao.OrganizeId AND ks.zt='1' AND ao.applyType='1'
LEFT JOIN NewtouchHIS_Base.dbo.Sys_User(NOLOCK) su ON su.Account=ao.CreatorCode AND su.zt='1'
LEFT JOIN NewtouchHIS_Base.dbo.Sys_UserStaff(NOLOCK) sus ON sus.UserId=su.Id AND sus.zt='1'
LEFT JOIN NewtouchHIS_Base.dbo.Sys_Staff(NOLOCK) ss ON ss.Id=sus.StaffId AND ss.OrganizeId=ao.OrganizeId AND ss.zt='1' AND ss.TopOrganizeId=su.TopOrganizeId
WHERE ao.OrganizeId=@OrganizeId
AND ao.zt='1'
AND (ao.applyType=@applyType OR 0=@applyType)
AND (ao.applyProcess=@applyProcess OR 0=@applyProcess)
AND ao.CreateTime BETWEEN @kssj AND @jssj
AND ao.sldh LIKE '%'+@sldh+'%'
AND ao.ckbm=@warehouseId
");
            if (applyProcesses.Count == 0) throw new Exception("可选申领状态无效");
            sql.Append("AND ao.applyProcess IN (");
            applyProcesses.ForEach(p =>
            {
                sql.Append(p + ",");
            });
            var part1 = sql.ToString().Trim(',') + ")";
            var param = new DbParameter[]
            {
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@applyType", applyType),
                new SqlParameter("@applyProcess", applyProcess),
                new SqlParameter("@kssj", kssj),
                new SqlParameter("@jssj", jssj),
                new SqlParameter("@warehouseId", warehouseId),
                new SqlParameter("@sldh", sldh)
            };
            return QueryWithPage<VApplyBillInfoEntity>(part1, pagination, param);
        }

        /// <summary>
        /// 获取申领单主明细
        /// </summary>
        /// <param name="sldh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<VApplyBillDetailEntity> SelectApplyBillDetail(string sldh, string organizeId)
        {
            const string sql = @"
SELECT aod.productId, wz.name wzmc, dbo.f_getComplexWzSlandDw(ISNULL(aod.sl*aod.zhyz, 0), aod.zhyz, bmdw.name, zxdw.name) slStr
,dbo.f_getComplexWzSlandDw(ISNULL(aod.yfsl*aod.zhyz, 0), aod.zhyz, bmdw.name, zxdw.name) yfslStr
,lb.name lbmc, wz.gg, cj.name sccj, wz.brand
FROM dbo.kf_applyOrder(NOLOCK) ao
INNER JOIN dbo.kf_applyOrderDetail(NOLOCK) aod ON aod.sldId=ao.Id AND aod.zt='1'
INNER JOIN dbo.wz_product(NOLOCK) wz ON wz.Id=aod.productId AND wz.OrganizeId=ao.OrganizeId AND wz.zt='1'
LEFT JOIN dbo.rel_productUnit(NOLOCK) rpu ON rpu.zhyz=aod.zhyz AND rpu.productId=aod.productId AND rpu.zt='1' AND rpu.OrganizeId=ao.OrganizeId
LEFT JOIN NewtouchHIS_Base.dbo.wz_unit(NOLOCK) bmdw ON bmdw.Id=rpu.unitId AND bmdw.zt='1'
LEFT JOIN NewtouchHIS_Base.dbo.wz_unit(NOLOCK) zxdw ON zxdw.Id=wz.minUnit AND zxdw.zt='1'
LEFT JOIN NewtouchHIS_Base.dbo.wz_type(NOLOCK) lb ON lb.Id=wz.typeId AND lb.zt='1'
LEFT JOIN dbo.gys_supplier(NOLOCK) cj ON cj.Id=wz.supplierId AND cj.OrganizeId=ao.OrganizeId AND cj.zt='1'
WHERE ao.OrganizeId=@OrganizeId
AND ao.zt='1'
AND ao.sldh =@sldh
";
            var param = new DbParameter[]
            {
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@sldh", sldh)
            };
            return FindList<VApplyBillDetailEntity>(sql, param);
        }

        /// <summary>
        /// 获取申领单主明细
        /// </summary>
        /// <param name="sldhs"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<VApplyBillDetailEntity> SelectApplyBillDetailBySldhs(string sldhs, string organizeId)
        {
            const string sql = @"

-- 切割字符串
DECLARE @t TABLE(col VARCHAR(50));
DECLARE @s VARCHAR(5000);
DECLARE @split VARCHAR(1);
SET @s=@sldh;
SET @split=',';
while(charindex(@split,@s)<>0)   
begin   
    INSERT @t(col) VALUES (substring(@s,1,charindex(@split,@s)-1))   
    SET @s = STUFF(@s,1,charindex(@split,@s),'')   
end   
INSERT @t(col) VALUES (@s)   

SELECT *,d.bmdwsysl xfsl, dbo.f_getComplexWzSlandDw(d.kykcsl, d.zhyz,d.bmdwmc, d.zxdwmc) kyslStr, d.bmdwsysl*d.zhyz zxdwsysl
FROM (
	SELECT ao.Id sldId, ao.sldh, ao.rkbm, aod.Id sldmxId, aod.productId, wz.name wzmc, dbo.f_getComplexWzSlandDw(ISNULL(aod.sl*aod.zhyz, 0), aod.zhyz, bmdw.name, zxdw.name) slStr, aod.sl
	,dbo.f_getComplexWzSlandDw(ISNULL(aod.yfsl*aod.zhyz, 0), aod.zhyz, bmdw.name, zxdw.name) yfslStr
	,lb.name lbmc, wz.gg, cj.name sccj, wz.brand, ISNULL(SUM(kcxx.kcsl-kcxx.djsl),0) kykcsl, (aod.sl-ISNULL(aod.yfsl,0)) bmdwsysl, bmdw.Id bmdwId, bmdw.name bmdwmc, aod.zhyz, zxdw.name zxdwmc
	FROM dbo.kf_applyOrder(NOLOCK) ao
	INNER JOIN dbo.kf_applyOrderDetail(NOLOCK) aod ON aod.sldId=ao.Id AND aod.zt='1'
	INNER JOIN dbo.wz_product(NOLOCK) wz ON wz.Id=aod.productId AND wz.OrganizeId=ao.OrganizeId AND wz.zt='1'
	LEFT JOIN dbo.rel_productUnit(NOLOCK) rpu ON rpu.zhyz=aod.zhyz AND rpu.productId=aod.productId AND rpu.zt='1' AND rpu.OrganizeId=ao.OrganizeId
	LEFT JOIN NewtouchHIS_Base.dbo.wz_unit(NOLOCK) bmdw ON bmdw.Id=rpu.unitId AND bmdw.zt='1'
	LEFT JOIN NewtouchHIS_Base.dbo.wz_unit(NOLOCK) zxdw ON zxdw.Id=wz.minUnit AND zxdw.zt='1'
	LEFT JOIN NewtouchHIS_Base.dbo.wz_type(NOLOCK) lb ON lb.Id=wz.typeId AND lb.zt='1'
	LEFT JOIN dbo.gys_supplier(NOLOCK) cj ON cj.Id=wz.supplierId AND cj.OrganizeId=ao.OrganizeId AND cj.zt='1'
	LEFT JOIN dbo.kf_kcxx(NOLOCK) kcxx ON kcxx.productId=aod.productId AND kcxx.warehouseId=ao.ckbm AND kcxx.OrganizeId=ao.OrganizeId AND kcxx.zt='1'
	WHERE ao.OrganizeId=@OrganizeId
	AND ao.zt='1'
	AND ao.sldh IN (SELECT * FROM @t)
	GROUP BY ao.Id, ao.sldh, ao.rkbm, aod.Id, aod.productId, aod.sl, aod.zhyz, bmdw.Id, bmdw.name, zxdw.name, aod.yfsl, wz.name, lb.name, wz.gg, cj.name , wz.brand
) d
";
            var param = new DbParameter[]
            {
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@sldh", sldhs)
            };
            return FindList<VApplyBillDetailEntity>(sql, param);
        }
    }
}