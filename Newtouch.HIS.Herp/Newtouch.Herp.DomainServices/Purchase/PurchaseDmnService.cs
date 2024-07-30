using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common.Operator;
using Newtouch.Herp.Domain.Entity.Purchase;
using Newtouch.Herp.Domain.IDomainServices.Purchase;
using Newtouch.Herp.Domain.ValueObjects.Purchase;
using Newtouch.Herp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Herp.DomainServices.Purchase
{
    public class PurchaseDmnService : DmnServiceBase, IPurchaseDmnService
    {
        public PurchaseDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }


        public List<PurchaseEntity> QueryPurchasebyId(string cgId, string orgId)
        {

            var strSql = new StringBuilder(@"
select * from xt_wz_cg a
  where a.zt = '1'
  and a.OrganizeId=@orgId
  and a.cgId=@cgId
");
            var param = new List<SqlParameter>
            {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@cgId", cgId),
            };

            return this.FindList<PurchaseEntity>(strSql.ToString(), param.ToArray());
        }
        public List<PurchaseDetailVO> QueryPurchaseDetailbyId(string cgId, string orgId)
        {

            var strSql = new StringBuilder(@"

select cgmx.*,wz.name productmc ,gys.name qymc ,
case cglx when 1 then '医保范围招标' when 2 then '医保范围未招标' end cglxmc,
case dcpsbs when 0 then '不允许' when 1 then '允许' end dcpsbsmc,
case sfjj when 0 then '无需加急' when 1 then '需要加急' end sfjjmc,
case psyq when 1 then '按单配送' when 2 then '按需配送' end psyqmc
from xt_wz_cgmx cgmx
left join [dbo].[wz_product] wz on cgmx.productCode =wz.productCode and cgmx.OrganizeId=wz.OrganizeId and cgmx.zt=wz.zt
left join gys_supplier gys on cgmx.qybm=gys.py and cgmx.OrganizeId=gys.OrganizeId and cgmx.zt=gys.zt

  where cgmx.zt = '1'
  and cgmx.OrganizeId=@orgId
  and cgmx.cgId=@cgId
");


            var param = new List<SqlParameter>
            {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@cgId", cgId),
            };

            return this.FindList<PurchaseDetailVO>(strSql.ToString(), param.ToArray());
        }

        public string PurchaseSubmit(PurchaseEntity cgEntity, List<PurchaseDetailEntity> cgmxList, string orgId, OperatorModel user)
        {
            try
            {
                //更新
                if (cgEntity.cgId != null)
                {
                    //更新采购列表
                    //string sql = "delete from zy_ksbyth where Id=@Id and OrganizeId=@orgId and zt='1' ";
                    //SqlParameter[] para = {
                    //        new SqlParameter("@Id", Djnr.isdeldj),
                    //        new SqlParameter("@orgId", orgId) };
                    //ExecuteSqlCommand(sql, para);

                    //删除采购明细
                    string sql2 = "delete from xt_wz_cgmx where cgId=@cgId and OrganizeId=@orgId and zt='1' ";
                    SqlParameter[] para2 = {
                            new SqlParameter("@cgId", cgEntity.cgId),
                            new SqlParameter("@orgId", orgId) };
                    ExecuteSqlCommand(sql2, para2);
                    //再添加采购明细
                    return "采购单据修改成功!";
                }

                //if (Djnr.issavesubmit == "1")
                //{
                //    shzt = "2";//提交
                //    tjsj = DateTime.Now.Date;
                //}
                //var request = new
                //{
                //    OrganizeId = orgId,
                //    yhgh = user.rygh,
                //    yplist = Djnr
                //};

                //新增
                else
                {
                    using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
                    {
                        //cgEntity.cgId = Guid.NewGuid().ToString();
                        //cgEntity.OrganizeId = orgId;
                        //cgEntity.ddsj = DateTime.Now.ToString("yyyy-MM-dd HH:ss:mm");
                        //cgEntity.ddbh = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("xt_cg_ddbh", orgId, "{0:D4}", true);
                        //cgEntity.ddzt = 1; //1已保存
                        //cgEntity.zt = "1";
                        //cgEntity.CreatorCode = user.rygh;
                        //cgEntity.CreateTime = DateTime.Now;
                        //cgEntity.LastModifierCode = "";
                        //cgEntity.LastModifyTime = null;
                        //db.Insert(cgEntity);
                        var cg = new PurchaseEntity
                        {
                            cgId = Guid.NewGuid().ToString(),
                            OrganizeId = orgId,
                            ddsj = DateTime.Now.ToString("yyyy-MM-dd HH:ss:mm"),
                            ddbh = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("xt_cg_ddbh", orgId, "{0:D4}", true),
                            ddzt = 1,//1已保存
                            zt = "1",
                            CreatorCode = user.rygh,
                            CreateTime = DateTime.Now,
                            LastModifierCode = "",
                            LastModifyTime = null,
                            czlx = cgEntity.czlx,
                            ddlx = cgEntity.ddlx,
                            psdbm = cgEntity.psdbm,
                            yybm = cgEntity.yybm,
                            yyjhdh = cgEntity.yyjhdh,
                            zwdhrq = cgEntity.zwdhrq,
                        };
                        db.Insert(cg);
                        foreach (var cgmxEntity in cgmxList)
                        {
                            cgmxEntity.cgmxId = Guid.NewGuid().ToString();
                            cgmxEntity.cgId = cgEntity.cgId;
                            cgmxEntity.OrganizeId = orgId;
                            cgEntity.zt = "1";
                            cgEntity.CreatorCode = user.rygh;
                            cgEntity.CreateTime = DateTime.Now;
                            cgEntity.LastModifierCode = "";
                            cgEntity.LastModifyTime = null;
                            // db.Insert(cgmxEntity);
                        };
                        db.Commit();
                    }
                    return "采购单据保存成功!";
                }
                //if (Djnr.issavesubmit == "1")
                //{
                //    var apires = SiteYfykAPIHelper.Request<APIRequestHelper.DefaultResponse>("/api/Stock/PrepareMedicineReturn", request, autoAppendToken: false);
                //    if (apires.code == APIRequestHelper.ResponseResultCode.SUCCESS && apires.msg == "")
                //    {
                //        return "保存并提交成功！";
                //    }
                //    else
                //    {
                //        return "调用药房接口失败，请联系开发人员！";
                //    }
                //}
                //return "科室备药退回保存成功！";
            }
            catch (Exception ex)
            {
                return "" + ex.InnerException.ToString();
            }
        }

        #region 采购退货

        public List<ReturnedEntity> QueryPurchaseReturnbyId(string thId, string orgId)
        {

            var strSql = new StringBuilder(@"
select * from xt_wz_th a
  where a.zt = '1'
  and a.OrganizeId=@orgId
  and a.thId=@thId
");
            var param = new List<SqlParameter>
            {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@thId", thId),
            };

            return this.FindList<ReturnedEntity>(strSql.ToString(), param.ToArray());
        }

        public List<PurchaseReturnDetailVO> QueryPurchaseReturnDetailbyId(string thId, string orgId)
        {

            var strSql = new StringBuilder(@"

select thmx.*,wz.name productmc ,gys.name qymc ,
case cglx when 1 then '招标采购' when 2 then '带量采购' when 3 then '挂网采购' when 9 then '其他'end cglxmc,
case thlx when 1 then '正常退货' when 2 then '补差价退货' end thlxmc  ,
case psmxtmlx when '01' then 'GS1 条码' when '02' then 'HIBC 条码' when '99' then '其他' end psmxtmlxmc
from xt_wz_thmx thmx
left join [dbo].[wz_product] wz on thmx.YYBDDM =wz.productCode and thmx.OrganizeId=wz.OrganizeId and thmx.zt=wz.zt
left join gys_supplier gys on thmx.qybm=gys.py and thmx.OrganizeId=gys.OrganizeId and thmx.zt=gys.zt

  where thmx.zt = '1'
  and thmx.OrganizeId=@orgId
  and thmx.thId=@thId
");


            var param = new List<SqlParameter>
            {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@thId", thId),
            };

            return this.FindList<PurchaseReturnDetailVO>(strSql.ToString(), param.ToArray());
        }

        #endregion

        #region 外部入库 导入采购单
        public List<PurchaseStoreVO> QueryPurchaseStorebyId(string cgId, string orgId, string warehouseId)
        {
            var strSql = new StringBuilder(@"

select * from 
(
select cgmx.*,wz.name productmc ,gys.name qymc ,wz.Id productId,
case cglx when 1 then '招标采购' when 2 then '带量采购' when 3 then '挂网采购' when 9 then '其他' end  cglxmc,
case psyq when 1 then '按单配送' when 2 then '按需配送' end psyqmc
from xt_wz_cgmx cgmx
left join wz_product wz on cgmx.productCode =wz.productCode and cgmx.OrganizeId=wz.OrganizeId and cgmx.zt=wz.zt
left join gys_supplier gys on cgmx.qybm=gys.py and cgmx.OrganizeId=gys.OrganizeId and cgmx.zt=gys.zt

  where cgmx.zt = '1'
  and cgmx.OrganizeId=@orgId
  and cgmx.cgId=@cgId
  ) a
  left join (
SELECT dbo.f_getComplexWzSlandDw(kykcsl,zhyz, bmdw.name, zxdw.name) slstr
, zxdw.name mindwmc,bmdw.name bmdwmc, a.* 
FROM (
    SELECT p.Id,p.OrganizeId,p.name,p.minUnit zxdwId, rpw.unitId bmdwId 
    ,ISNULL(SUM(kcxx.kcsl),0) kcsl, ISNULL(SUM(kcxx.kcsl-kcxx.djsl),0) kykcsl
    ,p.lsj minlsj, ISNULL(CONVERT(NUMERIC(11,4),p.lsj*rpu.zhyz),0) bmlsj
    ,p.py,p.gg,p.supplierId,s.name supplierName,wt.name lbmc,p.typeId lbId,ISNULL(rpu.zhyz,1) zhyz,ISNULL(rpu.zhyz,1) bmdwzhyz,p.productCode,p.hcgjybdm,p.hcgjybdm gjybdm
    FROM dbo.wz_product(NOLOCK) p 
    INNER JOIN dbo.gys_supplier(NOLOCK) s ON p.supplierId = s.Id AND p.OrganizeId = s.OrganizeId AND s.zt = '1'
    INNER JOIN dbo.rel_productWarehouse(NOLOCK) rpw ON rpw.OrganizeId = p.OrganizeId AND rpw.productId = p.Id 
    LEFT JOIN dbo.kf_kcxx(NOLOCK) kcxx ON kcxx.productId=p.Id AND kcxx.warehouseId=rpw.warehouseId AND kcxx.zt='1'
    LEFT JOIN dbo.kf_crkmx(NOLOCK) mx ON mx.Id=kcxx.crkmxId AND mx.pc=kcxx.pc AND mx.ph=kcxx.ph AND mx.zt='1' 
    LEFT JOIN dbo.kf_crkdj(NOLOCK) dj ON dj.Id=mx.crkId AND dj.OrganizeId=p.OrganizeId AND dj.zt='1'
    LEFT JOIN dbo.rel_productUnit(NOLOCK) rpu on rpu.productId=rpw.productId AND rpu.unitId=rpw.unitId AND rpu.OrganizeId=p.OrganizeId AND rpu.zt='1'
    LEFT JOIN NewtouchHIS_Base.dbo.wz_type(NOLOCK) wt ON wt.id = p.typeId AND wt.zt='1'
    WHERE p.OrganizeId = @orgId 
    AND rpw.warehouseId=@warehouseId 
    AND rpw.zt='1'
    AND p.zt = 1
	
    GROUP BY p.Id, p.OrganizeId, p.name, p.py, p.supplierId, s.name, p.minUnit, p.gg, wt.name,p.typeId,p.lsj,rpu.zhyz, p.minUnit, rpw.unitId,p.productCode,p.hcgjybdm) a
LEFT JOIN NewtouchHIS_Base.dbo.wz_unit(NOLOCK) bmdw ON bmdw.Id=a.bmdwId AND bmdw.zt='1'  
LEFT JOIN NewtouchHIS_Base.dbo.wz_unit(NOLOCK) zxdw ON zxdw.Id=a.zxdwId AND zxdw.zt='1' 
) b on a.productCode=b.productCode
");


            var param = new List<SqlParameter>
            {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@cgId", cgId),
                new SqlParameter("@warehouseId", warehouseId),
            };

            return this.FindList<PurchaseStoreVO>(strSql.ToString(), param.ToArray());
        }
        #endregion

    }
}
