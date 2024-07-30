//using FrameworkBase.MultiOrg.DmnService;
//using FrameworkBase.MultiOrg.Infrastructure;
//using Newtouch.Domain.DTO;
//using Newtouch.Domain.IDomainServices;
//using Newtouch.Domain.ValueObjects;
//using System;
//using System.Collections.Generic;
//using System.Data.Common;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Newtouch.DomainServices
//{
//    public class WZsldDmnService : DmnServiceBase, IWZsldDmnService
//    {
//        public WZsldDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
//        {

//        }

//        public List<VSelProductInfoVO> DepartmentStockListQuery(DepartmentStockListQueryParamDTO param)
//        {
//            var sql = new StringBuilder(@"
//SELECT dbo.f_getComplexWzSlandDw(kykcsl,zhyz, bmdw.name, zxdw.name) slstr
//, zxdw.name mindwmc,bmdw.name bmdwmc, a.* 
//FROM (
//    SELECT p.Id,p.OrganizeId,p.name,p.minUnit zxdwId, rpw.unitId bmdwId 
//    ,ISNULL(SUM(kcxx.kcsl),0) kcsl, ISNULL(SUM(kcxx.kcsl-kcxx.djsl),0) kykcsl
//    ,p.lsj minlsj, ISNULL(CONVERT(NUMERIC(11,4),p.lsj*rpu.zhyz),0) bmlsj
//    ,p.py,p.gg,p.supplierId,s.name supplierName,wt.name lbmc,p.typeId lbId,ISNULL(rpu.zhyz,1) zhyz,ISNULL(rpu.zhyz,1) bmdwzhyz
//    FROM [NewtouchHIS_HERP]..wz_product(NOLOCK) p 
//    INNER JOIN [NewtouchHIS_HERP]..gys_supplier(NOLOCK) s ON p.supplierId = s.Id AND p.OrganizeId = s.OrganizeId AND s.zt = '1'
//    INNER JOIN [NewtouchHIS_HERP]..rel_productWarehouse(NOLOCK) rpw ON rpw.OrganizeId = p.OrganizeId AND rpw.productId = p.Id 
//    LEFT JOIN [NewtouchHIS_HERP]..kf_kcxx(NOLOCK) kcxx ON kcxx.productId=p.Id AND kcxx.warehouseId=rpw.warehouseId AND kcxx.zt='1'
//    LEFT JOIN [NewtouchHIS_HERP]..kf_crkmx(NOLOCK) mx ON mx.Id=kcxx.crkmxId AND mx.pc=kcxx.pc AND mx.ph=kcxx.ph AND mx.zt='1' 
//    LEFT JOIN [NewtouchHIS_HERP]..kf_crkdj(NOLOCK) dj ON dj.Id=mx.crkId AND dj.OrganizeId=p.OrganizeId AND dj.zt='1'
//    LEFT JOIN [NewtouchHIS_HERP]..rel_productUnit(NOLOCK) rpu on rpu.productId=rpw.productId AND rpu.unitId=rpw.unitId AND rpu.OrganizeId=p.OrganizeId AND rpu.zt='1'
//    LEFT JOIN NewtouchHIS_Base.dbo.wz_type(NOLOCK) wt ON wt.id = p.typeId AND wt.zt='1'
//    WHERE p.OrganizeId = @OrganizeId 
//    AND rpw.warehouseId=@warehouseId 
//    AND rpw.zt='1'
//    AND p.zt = @zt
//    AND (p.name LIKE '%'+@keyWord+'%' OR p.py LIKE '%'+@keyWord+'%')
//");

//            sql.Append(@"
//    GROUP BY p.Id, p.OrganizeId, p.name, p.py, p.supplierId, s.name, p.minUnit, p.gg, wt.name,p.typeId,p.lsj,rpu.zhyz, p.minUnit, rpw.unitId) a
//LEFT JOIN NewtouchHIS_Base.dbo.wz_unit(NOLOCK) bmdw ON bmdw.Id=a.bmdwId AND bmdw.zt='1'  
//LEFT JOIN NewtouchHIS_Base.dbo.wz_unit(NOLOCK) zxdw ON zxdw.Id=a.zxdwId AND zxdw.zt='1' 
//");
//            var sqlParam = new DbParameter[]
//            {
//                new SqlParameter("@warehouseId", param.warehouseId??""),
//                new SqlParameter("@OrganizeId", param.organizeId),
//                new SqlParameter("@keyWord", param.key??""),
//                new SqlParameter("@zt", param.zt)
//            };
//            return FindList<VSelProductInfoVO>(sql.ToString(), sqlParam);
//        }

//        public List<RelWarehouseVO> GetDeptList(string organizeId, string keyword)
//        {
//            string sql = @"select ID,OrganizeId,name,py,isDefSyn from [NewtouchHIS_herp]..[kf_warehouse] (NOLOCK) where zt='1' and isdefsyn='1'
//and OrganizeId=@OrganizeId
//and (name like '%'+@keyword+'%' or py LIKE '%'+@keyword+'%')";
//            var param = new DbParameter[] {
//                new SqlParameter("@OrganizeId", organizeId),
//                new SqlParameter("@keyword", keyword),
//            };

//            return FindList<RelWarehouseVO>(sql, param);
//        }

//        public List<RelWarehouseVO> GetList( string organizeId, string keyword)
//        {
//             string sql = @"select ID,OrganizeId,name,py,isDefSyn from [NewtouchHIS_herp]..[kf_warehouse] (NOLOCK) where zt='1' and isdefsyn='1'
//and OrganizeId=@OrganizeId
//and (name like '%'+@keyword+'%' or py LIKE '%'+@keyword+'%')";
//            var param = new DbParameter[] {
//                new SqlParameter("@OrganizeId", organizeId),
//                new SqlParameter("@keyword", keyword),
//            };
            
//            return FindList<RelWarehouseVO>(sql, param);
//        }

//        public List<VProductBatchInfoVO> ProductBatchQuery(string productId, string warehouseId, string organizeId, string keyword = "")
//        {
//             string sql = @"
//SELECT TOP 20 s.ph,s.pc,s.yxq,s.scrq,s.fph,SUM(s.kcsl) kcsl,SUM(s.kykcsl) kykcsl
//,s.bmjj,s.minjj,SUM(s.jjzje) jjzje,CONCAT(CONVERT(NUMERIC(11,2),s.bmjj),'元/',s.bmdwmc) jjdwdj
//,dbo.f_getComplexWzSlandDw(SUM(s.kykcsl),s.zhyz, s.bmdwmc, s.zxdwmc) slstr 
//FROM (
//	SELECT kcxx.ph, kcxx.pc, kcxx.yxq, ISNULL(mx.scrq, '') scrq, ISNULL(mx.fph,'') fph
//	,ISNULL(CONVERT(NUMERIC(11,4),kcxx.jj/kcxx.zhyz*rpu.zhyz),0) bmjj,ISNULL(CONVERT(NUMERIC(11,4),kcxx.jj/kcxx.zhyz),0) minjj,ISNULL(CONVERT(NUMERIC(11,2),SUM(kcxx.jj/kcxx.zhyz*(kcxx.kcsl-kcxx.djsl))),0) jjzje
//	,ISNULL(SUM(kcxx.kcsl),0) kcsl
//	,ISNULL(SUM(kcxx.kcsl-kcxx.djsl),0) kykcsl
//	,rpu.zhyz, bmdw.name bmdwmc, zxdw.name zxdwmc
//	FROM NewtouchHIS_HERP..kf_kcxx(NOLOCK) kcxx
//	INNER JOIN NewtouchHIS_HERP..rel_productWarehouse(NOLOCK) rpw ON rpw.productId=kcxx.productId AND rpw.warehouseId=kcxx.warehouseId AND rpw.OrganizeId=kcxx.OrganizeId AND rpw.zt='1'
//	INNER JOIN NewtouchHIS_HERP..rel_productUnit(NOLOCK) rpu ON rpu.productId=rpw.productId AND rpu.unitId=rpw.unitId AND rpu.OrganizeId=kcxx.OrganizeId AND rpu.zt='1'
//	INNER JOIN NewtouchHIS_HERP..wz_product(NOLOCK) wz ON wz.Id=rpw.productId AND wz.OrganizeId=kcxx.OrganizeId AND wz.zt='1'
//	INNER JOIN NewtouchHIS_Base.dbo.wz_unit(NOLOCK) bmdw ON bmdw.Id=rpw.unitId AND bmdw.zt='1'
//	INNER JOIN NewtouchHIS_Base.dbo.wz_unit(NOLOCK) zxdw ON zxdw.Id=wz.minUnit AND zxdw.zt='1'
//	LEFT JOIN NewtouchHIS_HERP..kf_crkmx(NOLOCK) mx ON mx.Id=kcxx.crkmxId AND mx.zt='1'
//	LEFT JOIN NewtouchHIS_HERP..kf_crkdj(NOLOCK) dj ON dj.Id=mx.crkId AND dj.zt='1' AND dj.auditState='1' AND dj.djlx=1
//	WHERE kcxx.productId=@proId
//	AND kcxx.warehouseId=@warehouseId
//	AND kcxx.OrganizeId=@OrganizeId
//    AND (kcxx.kcsl-kcxx.djsl)>0
//	AND kcxx.zt='1'
//    AND (kcxx.pc LIKE '%'+@keyword+'%' OR kcxx.ph LIKE '%'+@keyword+'%')
//	GROUP BY kcxx.ph, kcxx.pc, kcxx.yxq,bmdw.name,zxdw.name,kcxx.jj,mx.scrq,mx.fph,rpu.zhyz,kcxx.zhyz
//) s
//GROUP BY s.ph,s.pc,s.yxq,s.scrq,s.fph,s.bmjj,s.minjj,s.zhyz,s.bmdwmc,s.zxdwmc
//";
//            var param = new DbParameter[]
//            {
//                new SqlParameter("@warehouseId", warehouseId??""),
//                new SqlParameter("@OrganizeId", organizeId),
//                new SqlParameter("@keyword", (keyword??"").Trim()),
//                new SqlParameter("@proId", productId??"")
//            };
//            return FindList<VProductBatchInfoVO>(sql, param);
//        }
//    }
//}
