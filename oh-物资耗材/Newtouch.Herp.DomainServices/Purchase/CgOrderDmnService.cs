using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Herp.Domain.Entity.VEntity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Infrastructure.Enum;

namespace Newtouch.Herp.DomainServices
{
    /// <summary>
    /// 采购单
    /// </summary>
    public class CgOrderDmnService : DmnServiceBase, ICgOrderDmnService
    {
        public CgOrderDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 查询采购单信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orderNo"></param>
        /// <param name="orderType"></param>
        /// <param name="orderProcess"></param>
        /// <param name="organizeId"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        public IList<VCgOrderEntity> SelectCgOrder(Pagination pagination, string orderNo, int orderType, int orderProcess, string organizeId, DateTime kssj, DateTime jssj)
        {
            var sql = new StringBuilder(@"
SELECT DISTINCT co.orderType, co.orderProcess,co.orderNo, ISNULL(co.remark,'') remark, co.CreateTime, ss.Name CreatorName, co.LastModifyTime, ss1.Name LastModifierName
FROM dbo.cg_order(NOLOCK) co
INNER JOIN dbo.cg_orderDetail(NOLOCK) cod ON cod.orderId=co.Id AND cod.zt='1'
LEFT JOIN NewtouchHIS_Base.dbo.Sys_User(NOLOCK) su ON su.Account=co.CreatorCode AND su.zt='1' 
LEFT JOIN NewtouchHIS_Base.dbo.Sys_UserStaff(NOLOCK) sus ON sus.UserId=su.Id AND sus.zt='1'
LEFT JOIN NewtouchHIS_Base.dbo.Sys_Staff(NOLOCK) ss ON ss.Id=sus.StaffId AND ss.OrganizeId=co.OrganizeId AND ss.zt='1' 
LEFT JOIN NewtouchHIS_Base.dbo.Sys_User(NOLOCK) su1 ON su1.Account=co.LastModifierCode AND su1.zt='1' 
LEFT JOIN NewtouchHIS_Base.dbo.Sys_UserStaff(NOLOCK) sus1 ON sus1.UserId=su1.Id AND sus1.zt='1'
LEFT JOIN NewtouchHIS_Base.dbo.Sys_Staff(NOLOCK) ss1 ON ss1.Id=sus1.StaffId AND ss1.OrganizeId=co.OrganizeId AND ss1.zt='1' 
WHERE co.OrganizeId=@OrganizeId
AND co.zt='1'
AND co.orderNo LIKE '%'+ISNULL(@orderNo,'')+'%'
AND co.CreateTime BETWEEN @kssj AND @jssj
");
            var param = new List<DbParameter>
            {
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@orderNo", orderNo),
                new SqlParameter("@kssj", kssj),
                new SqlParameter("@jssj", jssj)
            };
            switch (orderType)
            {
                case (int)EnumOrderTypeHrp.OfficialOrder:
                case (int)EnumOrderTypeHrp.TempOrder:
                case (int)EnumOrderTypeHrp.BadOrder:
                    sql.AppendLine("AND co.orderType=@orderType ");
                    param.Add(new SqlParameter("@orderType", orderType));
                    break;
            }
            switch (orderProcess)
            {
                case (int)EnumOrderProcess.Waiting:
                case (int)EnumOrderProcess.Complete:
                case (int)EnumOrderProcess.Delivering:
                case (int)EnumOrderProcess.PreparingGoods:
                case (int)EnumOrderProcess.Refusal:
                case (int)EnumOrderProcess.RefusalSign:
                case (int)EnumOrderProcess.SignFor:
                    sql.AppendLine("AND co.orderProcess=@orderProcess  ");
                    param.Add(new SqlParameter("@orderProcess", orderProcess));
                    break;
            }

            return QueryWithPage<VCgOrderEntity>(sql.ToString(), pagination, param.ToArray());
        }

        /// <summary>
        /// 查询采购单明细
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<VCgOrderDetailEntity> SelectCgOrderDetailGroupByCgdh(string orderNo, string organizeId)
        {
            const string sql = @"
SELECT cod.subOrderNo, cpo.cgdh, dept.Name deptName, wz.name wzmc, CONCAT(cod.sl,cod.dwmc) slStr, wz.gg, ISNULL(wz.brand,'') brand, CONCAT(CONVERT(NUMERIC(11,2),cod.jj),'元/',cod.dwmc) jjStr
,ISNULL(cj.name,'') sccj, ISNULL(gys.name,'') gysmc, ISNULL(cod.remark,'') remark, cod.productId
FROM dbo.cg_order(NOLOCK) co
INNER JOIN dbo.cg_orderDetail(NOLOCK) cod ON cod.orderId=co.Id AND cod.zt='1'
INNER JOIN dbo.wz_product(NOLOCK) wz ON wz.Id=cod.productId AND wz.OrganizeId=co.OrganizeId AND wz.zt='1'
LEFT JOIN dbo.cg_purchaseOrderDetail(NOLOCK) cpod ON cpod.Id=cod.pdId AND cpod.zt='1'
LEFT JOIN dbo.cg_purchaseOrder(NOLOCK) cpo ON cpo.Id=cpod.purchaseId AND cpo.OrganizeId=co.OrganizeId AND cpo.zt='1' AND cpo.auditState='1'
LEFT JOIN NewtouchHIS_Base.dbo.Sys_Department(NOLOCK) dept ON dept.Code=cpo.rkbmCode AND dept.OrganizeId=co.OrganizeId AND dept.zt='1'
LEFT JOIN dbo.gys_supplier(NOLOCK) gys ON gys.Id=cod.supplierId AND gys.OrganizeId=co.OrganizeId AND gys.supplierType=2 AND gys.zt='1'
LEFT JOIN dbo.gys_supplier(NOLOCK) cj ON cj.Id=wz.supplierId AND cj.OrganizeId=co.OrganizeId AND cj.supplierType=1 AND gys.zt='1'
WHERE co.OrganizeId=@OrganizeId
AND co.zt='1'
AND co.orderNo=@orderNo
";
            var param = new DbParameter[]
            {
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@orderNo",orderNo )
            };
            return FindList<VCgOrderDetailEntity>(sql, param);
        }

        /// <summary>
        /// 查询采购单明细
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<VCgOrderDetailEntity> SelectCgOrderDetailNoCgdh(string orderNo, string organizeId)
        {
            const string sql = @"
SELECT d.wzmc, CONCAT(d.sl,d.dwmc) slStr, d.gg, d.brand, CONCAT(CONVERT(NUMERIC(11,2),d.jj),'元/',d.dwmc) jjStr,d.sccj, d.gysmc, d.remark, d.productId
FROM (
	SELECT wz.name wzmc, SUM(cod.sl) sl, wz.gg, ISNULL(wz.brand,'') brand, CONCAT(CONVERT(NUMERIC(11,2),cod.jj),'元/',cod.dwmc) jjStr
	,ISNULL(cj.name,'') sccj, ISNULL(gys.name,'') gysmc, ISNULL(cod.remark,'') remark, cod.productId, cod.dwmc, cod.jj
	FROM dbo.cg_order(NOLOCK) co
	INNER JOIN dbo.cg_orderDetail(NOLOCK) cod ON cod.orderId=co.Id AND cod.zt='1'
	INNER JOIN dbo.wz_product(NOLOCK) wz ON wz.Id=cod.productId AND wz.OrganizeId=co.OrganizeId AND wz.zt='1'
	LEFT JOIN dbo.cg_purchaseOrderDetail(NOLOCK) cpod ON cpod.Id=cod.pdId AND cpod.zt='1'
	LEFT JOIN dbo.cg_purchaseOrder(NOLOCK) cpo ON cpo.Id=cpod.purchaseId AND cpo.OrganizeId=co.OrganizeId AND cpo.zt='1' AND cpo.auditState='1'
	LEFT JOIN dbo.gys_supplier(NOLOCK) gys ON gys.Id=cod.supplierId AND gys.OrganizeId=co.OrganizeId AND gys.supplierType=2 AND gys.zt='1'
	LEFT JOIN dbo.gys_supplier(NOLOCK) cj ON cj.Id=wz.supplierId AND cj.OrganizeId=co.OrganizeId AND cj.supplierType=1 AND gys.zt='1'
	WHERE co.OrganizeId=@OrganizeId
	AND co.zt='1'
	AND co.orderNo=@orderNo
	GROUP BY wz.name, wz.gg, wz.brand, cod.jj,cj.name,gys.name, cod.remark,cod.productId, cod.dwmc
) d
";
            var param = new DbParameter[]
            {
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@orderNo",orderNo )
            };
            return FindList<VCgOrderDetailEntity>(sql, param);
        }
    }
}