using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.Entity.VEntity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure;
using Newtouch.Herp.Infrastructure.Enum;

namespace Newtouch.Herp.DomainServices
{
    /// <summary>
    /// 出入库单据
    /// </summary>
    public class KfCrkdjDmnService : DmnServiceBase, IKfCrkdjDmnService
    {
        private readonly IKfCrkmxRepo ikfCrkmxRepo;
        private readonly IKfCrkdjRepo iKfCrkdjRepo;
        private readonly IKfCrkmxRepo ikfKfCrkmxRepo;

        public KfCrkdjDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取出入库单据主表信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <param name="alldjlx"></param>
        /// <param name="warehouseId">当前库房</param>
        /// <param name="ope">操作  query：查询  approval：审核</param>
        /// <returns></returns>
        public IList<VCrkdjEntity> GetCrkdjMainList(Pagination pagination, CrkdjSearchParamDTO param, string[] alldjlx, string warehouseId, string ope = "")
        {
            var sql = new StringBuilder(@"
SELECT dj.auditState shzt, dj.Pdh, dj.Id crkId, dj.djlx,dj.deliveryNo
,CASE dj.djlx WHEN 1 THEN ckgys.name ELSE ckkf.name END ckbmmc
,CASE dj.djlx WHEN 1 THEN ckgys.Id ELSE ckkf.Id END ckbmId
,CASE dj.djlx WHEN 2 THEN rkgys.name WHEN 7 THEN rwd.deptName ELSE rkkf.name END rkbmmc 
,CASE dj.djlx WHEN 2 THEN rkgys.Id WHEN 7 THEN rwd.deptId ELSE rkkf.Id END rkbmId 
,dj.crkfs crkfsId,crkfs.crkfsmc, dj.CreateTime czsj, ISNULL(CONVERT(VARCHAR(16), dj.cksj, 120),'') cksj, ISNULL(CONVERT(VARCHAR(16), dj.rksj, 120),'') rksj
,ISNULL(CONVERT(NUMERIC(11,2), SUM(mx.lsj*mx.sl)), 0) ljze
,ISNULL(CONVERT(NUMERIC(11,2),SUM(mx.jj*mx.sl)), 0) jjze
,(ISNULL(CONVERT(NUMERIC(11,2), SUM(mx.lsj*mx.sl)), 0)-ISNULL(CONVERT(NUMERIC(11,2),SUM(mx.jj*mx.sl)), 0)) jxcj
FROM dbo.kf_crkdj(NOLOCK) dj
INNER JOIN dbo.kf_crkmx(NOLOCK) mx ON mx.crkId=dj.Id AND mx.zt='1'
LEFT JOIN dbo.kf_warehouse(NOLOCK) ckkf ON ckkf.Id=dj.ckbm AND ckkf.OrganizeId=dj.OrganizeId AND ckkf.zt='1'
LEFT JOIN dbo.kf_warehouse(NOLOCK) rkkf ON rkkf.Id=dj.rkbm AND rkkf.OrganizeId=dj.OrganizeId AND rkkf.zt='1'
LEFT JOIN NewtouchHIS_Base.dbo.wz_crkfs(NOLOCK) crkfs ON crkfs.Id=dj.crkfs AND crkfs.zt='1'
LEFT JOIN dbo.gys_supplier(NOLOCK) rkgys ON rkgys.Id=dj.rkbm AND rkgys.OrganizeId=dj.OrganizeId AND rkgys.zt='1'
LEFT JOIN dbo.gys_supplier(NOLOCK) ckgys ON ckgys.Id=dj.ckbm AND ckgys.OrganizeId=dj.OrganizeId AND ckgys.zt='1'
LEFT JOIN dbo.rel_warehouseDept(NOLOCK) rwd ON rwd.warehouseId=@warehouseId AND rwd.deptId=dj.rkbm AND rwd.zt='1' AND rwd.OrganizeId=dj.OrganizeId
WHERE dj.OrganizeId=@OrganizeId
AND dj.CreateTime BETWEEN @kssj AND @jssj 
");
            param.kssj = param.kssj < Constants.MinDateTime ? Constants.MinDateTime : param.kssj;
            param.jssj = param.jssj < Constants.MinDateTime ? Constants.MinDateTime : param.jssj;
            var sqlparam = new List<DbParameter>
            {
                new SqlParameter("@OrganizeId", param.organizeId),
                new SqlParameter("@shzt", param.shzt??""),
                new SqlParameter("@kssj", param.kssj),
                new SqlParameter("@jssj", param.jssj),
                new SqlParameter("@warehouseId", warehouseId??"")
            };
            switch (ope)
            {
                case "approval":
                    if (param.djlx == ((int)EnumOutOrInStorageBillType.Wbck).ToString())
                    {
                        sql.AppendLine("AND ckkf.Id=@warehouseId");
                    }
                    else if (string.IsNullOrWhiteSpace(param.djlx))
                    {
                        sql.AppendLine("AND ((ckkf.Id=@warehouseId AND dj.djlx=" + (int)EnumOutOrInStorageBillType.Wbck + ") OR rkkf.Id=@warehouseId) ");
                    }
                    else
                    {
                        sql.AppendLine("AND rkkf.Id=@warehouseId");
                    }
                    break;
                default:
                    sql.AppendLine("AND (ckkf.Id=@warehouseId OR rkkf.Id=@warehouseId) ");
                    break;
            }
            if (string.IsNullOrWhiteSpace(param.shzt))
            {
                sql.AppendLine("AND dj.auditState<>'" + (int)EnumAuditState.Temporary + "' ");
            }
            else
            {
                sql.AppendLine("AND dj.auditState=@shzt ");
            }
            if (alldjlx != null && alldjlx.Length > 0)
            {
                sql.AppendLine(string.Format("AND dj.djlx IN ({0}) ", string.Join(",", alldjlx)));
            }
            if (!string.IsNullOrWhiteSpace(param.djlx))
            {
                sql.AppendLine("AND dj.djlx=@djlx ");
                sqlparam.Add(new SqlParameter("@djlx", param.djlx));
            }
            if (!string.IsNullOrWhiteSpace(param.fph))
            {
                sql.AppendLine("AND mx.fph like '%'+@fph+'%' ");
                sqlparam.Add(new SqlParameter("@fph", param.fph.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(param.deliveryNo))
            {
                sql.AppendLine("AND dj.deliveryNo like '%'+@deliveryNo+'%'");
                sqlparam.Add(new SqlParameter("@deliveryNo", param.deliveryNo));
            }
            if (!string.IsNullOrWhiteSpace(param.pdh))
            {
                sql.AppendLine("AND dj.Pdh like '%'+@pdh+'%' ");
                sqlparam.Add(new SqlParameter("@pdh", param.pdh.Trim()));
            }
            sql.AppendLine("GROUP BY dj.auditState, dj.djlx, dj.Pdh,ckkf.Id,ckkf.name,rkkf.Id,rkkf.name,dj.crkfs,crkfs.crkfsmc, dj.CreateTime, dj.cksj, dj.rksj, dj.Id, dj.djlx, rkgys.Id,rkgys.name,ckgys.Id,ckgys.name,rwd.deptId,rwd.deptName,dj.deliveryNo ");
            return QueryWithPage<VCrkdjEntity>(sql.ToString(), pagination, sqlparam.ToArray());
        }

        /// <summary>
        /// 获取出入库明细
        /// </summary>
        /// <param name="crkId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<VCrkdjmxEntity> GetCrkdjmxList(string crkId, string organizeId)
        {
            const string sql = @"
SELECT mx.Id crkmxId, lb.name lbmc, wz.name wzmc, mx.sl, mx.unitName dw, wz.gg, mx.ph, mx.pc, gys.name sccj, mx.fph
,dbo.f_getComplexWzSlandDw(mx.sl*mx.zhyz, mx.zhyz, bmdw.name, zxdw.name) slStr
,ISNULL(CONVERT(NUMERIC(11,2),mx.lsj*mx.sl),0) ljze
,ISNULL(CONVERT(NUMERIC(11,2),mx.jj*mx.sl),0) jjze
,(ISNULL(CONVERT(NUMERIC(11,2),mx.lsj*mx.sl),0)-ISNULL(CONVERT(NUMERIC(11,2),mx.jj*mx.sl),0)) jxcj 
FROM dbo.kf_crkmx(NOLOCK) mx
INNER JOIN dbo.kf_crkdj(NOLOCK) dj ON dj.Id=mx.crkId
LEFT JOIN dbo.wz_product(NOLOCK) wz ON wz.Id=mx.productId AND wz.OrganizeId=dj.OrganizeId
LEFT JOIN NewtouchHIS_Base.dbo.wz_type(NOLOCK) lb ON lb.Id=wz.typeId
LEFT JOIN dbo.gys_supplier(NOLOCK) gys ON gys.Id=wz.supplierId AND gys.OrganizeId=dj.OrganizeId
LEFT JOIN NewtouchHIS_Base.dbo.wz_unit(NOLOCK) bmdw ON bmdw.Id=mx.unitId AND bmdw.zt='1'
LEFT JOIN NewtouchHIS_Base.dbo.wz_unit(NOLOCK) zxdw ON zxdw.Id=wz.minUnit AND zxdw.zt='1'
WHERE dj.OrganizeId=@OrganizeId
AND dj.Id=@crkId
";
            var param = new DbParameter[]
            {
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@crkId", crkId??""),
            };
            return FindList<VCrkdjmxEntity>(sql, param);
        }

        /// <summary>
        /// 获取供应商和发票信息
        /// </summary>
        /// <param name="keyWord"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<VFphAndGysEntity> GetFphAndGysInfo(string keyWord, string warehouseId, string organizeId)
        {
            const string sql = @"
SELECT DISTINCT TOP 200 mx.fph, dj.ckbm gysId, gys.name gysmc
FROM dbo.kf_crkmx(NOLOCK) mx
INNER JOIN dbo.kf_crkdj(NOLOCK) dj ON mx.crkId=dj.Id AND dj.zt='1' AND dj.auditState=@shzt
INNER JOIN dbo.gys_supplier(NOLOCK) gys ON gys.Id=dj.ckbm AND gys.zt='1' AND gys.OrganizeId=dj.OrganizeId
WHERE mx.zt='1'
AND dj.OrganizeId=@OrganizeId
AND dj.rkbm=@warehouseId
AND mx.fph LIKE '%'+@keyWord+'%'
ORDER BY fph DESC
";
            var param = new DbParameter[]
            {
                new SqlParameter("@shzt", ((int) EnumAuditState.Adopt).ToString()),
                new SqlParameter("@warehouseId", warehouseId),
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@keyWord", keyWord ?? "")
            };
            return FindList<VFphAndGysEntity>(sql, param);
        }

        /// <summary>
        /// 删除单据主表和明细表 慎用
        /// </summary>
        /// <param name="crkId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public int DeleteDjById(long crkId, string organizeId)
        {
            using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
            {
                var dj = iKfCrkdjRepo.FindEntity(p => p.Id == crkId && p.OrganizeId == organizeId);
                var mx = ikfKfCrkmxRepo.IQueryable(p => p.crkId == crkId);
                foreach (var item in mx)
                {
                    ikfKfCrkmxRepo.Delete(item);
                }

                iKfCrkdjRepo.Delete(dj);
                return db.Commit();
            }
        }

        /// <summary>
        /// 保存单据
        /// </summary>
        /// <param name="dj"></param>
        /// <param name="mxList"></param>
        /// <returns></returns>
        public string SaveDj(KfCrkdjEntity dj, List<KfCrkmxEntity> mxList)
        {
            var ret = iKfCrkdjRepo.Insert(dj); //新增kf_crkdj
            if (ret <= 0) return "保存出入库主信息失败";
            var successList = new List<KfCrkmxEntity>();
            try
            {
                foreach (var item in mxList)
                {
                    item.crkId = dj.Id;
                    if (ikfKfCrkmxRepo.Insert(item) <= 0) throw new FailedException("保存出入库明细失败");
                    successList.Add(item);
                }
            }
            catch (FailedException ex)
            {
                CancelCrkmx(dj, successList);
                return ex.Msg;
            }
            catch (Exception ex)
            {
                CancelCrkmx(dj, successList);
                return ex.Message;
            }
            return "";
        }

        /// <summary>
        /// 取消已成功插入明细
        /// </summary>
        /// <param name="dj"></param>
        /// <param name="successList"></param>
        public void CancelCrkmx(KfCrkdjEntity dj, List<KfCrkmxEntity> successList)
        {
            using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
            {
                if (successList != null && successList.Count > 0)
                {
                    successList.ForEach(p =>
                    {
                        ikfKfCrkmxRepo.Delete(p);
                    });
                }
                iKfCrkdjRepo.Delete(dj);
                db.Commit();
            }

        }

        /// <summary>
        /// 查询入库配送单号
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="warehouseId"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeid"></param>
        /// <param name="shzt"></param>
        /// <returns></returns>
        public List<VInStorageDeliveryInfoEntity> SelectInStorageDeliveryInfo(string keyword, string warehouseId, string userCode, string organizeid, string shzt = "4")
        {
            const string sql = @"
SELECT ISNULL(crkdj.deliveryNo,'') deliveryNo, crkdj.Pdh djh,crkdj.auditState shzt
,crkdj.CreateTime, crkdj.CreatorCode
,crkdj.ckbm gysId, gys.name gysmc
,crkdj.crkfs crkfsId, crkfs.crkfsmc
FROM dbo.kf_crkdj(NOLOCK) crkdj
LEFT JOIN dbo.gys_supplier(NOLOCK) gys ON gys.Id=crkdj.ckbm AND gys.zt='1' 
LEFT JOIN NewtouchHIS_Base.dbo.wz_crkfs(NOLOCK) crkfs ON crkfs.Id=crkdj.crkfs AND crkfs.zt='1' 
WHERE crkdj.zt='1' 
AND crkdj.deliveryNo LIKE '%'+ISNULL(@keyword, '')+'%'
AND crkdj.rkbm=@warehouseId
AND crkdj.djlx=@djlx
AND crkdj.OrganizeId=@Organizeid
AND crkdj.CreatorCode=@userCode
AND (crkdj.auditState=@shzt OR ''=@shzt)
";
            var param = new DbParameter[] {
                new SqlParameter("@keyword",keyword),
                new SqlParameter("@djlx",(int)EnumOutOrInStorageBillType.Wbrk),
                new SqlParameter("@warehouseId",warehouseId),
                new SqlParameter("@userCode",userCode),
                new SqlParameter("@Organizeid",organizeid),
                new SqlParameter("@shzt",shzt)
            };
            return FindList<VInStorageDeliveryInfoEntity>(sql, param);
        }

        /// <summary>
        /// 通过配送单号获取出入库明细
        /// </summary>
        /// <param name="deliveryNo"></param>
        /// <param name="djh"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeid"></param>
        /// <param name="gysId"></param>
        /// <returns></returns>
        public List<VCrkdjmxInfoEntity> SelectCrkmxByDeliveryNo(string deliveryNo, string djh, string warehouseId, string organizeid, string gysId = "")
        {
            var sql = new StringBuilder(@"
SELECT s.lbmc,s.fph,s.wzmc,s.gg,s.sl,s.lsj,s.minjj,s.jj,s.unitId,s.unitName,s.zhyz,s.ph,s.pc,s.yxq,s.zje,s.rkbmkc,s.remark,s.sccj,s.scrq,s.productId,s.mindwmc
    ,s.zxdwId,s.zxdwzhyz,s.bmdwId,s.bmdwmc,s.bmdwzhyz,s.minlsj,s.ckbm
    ,ISNULL(SUM(kcxx.kcsl-kcxx.djsl),0) kykcsl,dbo.f_getComplexWzSlandDw(ISNULL(SUM(kcxx.kcsl-kcxx.djsl),0),s.zhyz,s.unitName,s.mindwmc) slstr,s.gjybdm
FROM (
	SELECT wzlb.name lbmc,crkmx.fph,wz.name wzmc,wz.gg,crkmx.sl,crkmx.lsj,CONVERT(NUMERIC(11,4),crkmx.jj/crkmx.zhyz) minjj,crkmx.jj,crkmx.unitId,crkmx.unitName,crkmx.zhyz,crkmx.ph,crkmx.pc,crkmx.yxq
	    ,CONVERT(NUMERIC(11,2),crkmx.jj*crkmx.sl) zje,crkmx.rkbmkc,crkmx.remark
	    ,cj.name sccj,crkmx.scrq,crkmx.productId,minUnit.name mindwmc,minUnit.Id zxdwId,1 zxdwzhyz,rpw.unitId bmdwId,bmUnit.name bmdwmc,rpu.zhyz bmdwzhyz
	    ,wz.lsj minlsj,crkdj.ckbm,crkdj.LastModifyTime,crkdj.OrganizeId,rpw.warehouseId,wz.gjybdm
	    FROM dbo.kf_crkmx(NOLOCK) crkmx
	    INNER JOIN dbo.kf_crkdj(NOLOCK) crkdj ON crkdj.Id=crkmx.crkId AND crkdj.zt='1'
	    INNER JOIN dbo.wz_product(NOLOCK) wz ON wz.Id=crkmx.productId AND wz.OrganizeId=crkdj.OrganizeId AND wz.zt='1'
	    INNER JOIN dbo.rel_productWarehouse(NOLOCK) rpw ON rpw.productId=crkmx.productId AND rpw.OrganizeId=crkdj.OrganizeId AND rpw.zt='1' 
	    LEFT JOIN NewtouchHIS_Base.dbo.wz_type(NOLOCK) wzlb ON wzlb.Id=wz.typeId AND wzlb.zt='1'
	    LEFT JOIN NewtouchHIS_Base.dbo.wz_unit(NOLOCK) minUnit ON minUnit.Id=wz.minUnit AND minUnit.zt='1'
	    LEFT JOIN NewtouchHIS_Base.dbo.wz_unit(NOLOCK) bmUnit ON bmunit.Id=rpw.unitId AND bmunit.zt='1' 
	    LEFT JOIN dbo.rel_productUnit(NOLOCK) rpu ON rpu.productId=rpw.productId AND rpu.unitId=rpw.unitId AND rpu.OrganizeId=crkdj.OrganizeId AND rpu.zt='1'
	    LEFT JOIN dbo.gys_supplier(NOLOCK) cj ON cj.Id=wz.supplierId AND cj.zt='1'
    WHERE crkmx.zt='1' 
	    AND rpw.warehouseId=@warehouseId
	    AND ISNULL(crkdj.deliveryNo,'')=@deliveryNo
	    AND crkdj.OrganizeId=@Organizeid 
	    AND crkdj.Pdh=@pdh
");
            if (!string.IsNullOrWhiteSpace(gysId))
            {
                sql.AppendLine("	    AND crkdj.ckbm=@ckbm ");
            }
            var param = new DbParameter[] {
                new SqlParameter("@deliveryNo",deliveryNo),
                new SqlParameter("@warehouseId",warehouseId),
                new SqlParameter("@organizeid",organizeid),
                new SqlParameter("@pdh", djh??""),
                new SqlParameter("@ckbm", gysId??"")
            };
            sql.AppendLine(@") s
    LEFT JOIN dbo.kf_kcxx(NOLOCK) kcxx ON kcxx.pc = s.pc AND kcxx.ph = s.ph AND kcxx.productId = s.productId AND kcxx.OrganizeId = s.OrganizeId AND kcxx.warehouseId = s.warehouseId AND kcxx.zt = '1'
GROUP BY s.lbmc, s.fph, s.wzmc, s.gg, s.sl, s.lsj, s.minjj, s.jj, s.unitId, s.unitName, s.zhyz, s.ph, s.pc, s.yxq, s.zje, s.rkbmkc, s.remark, s.sccj, s.scrq, s.productId, s.mindwmc, s.zxdwId, s.zxdwzhyz, s.bmdwId, s.bmdwmc, s.bmdwzhyz, s.minlsj, s.ckbm, s.LastModifyTime, s.OrganizeId, s.warehouseId,s.gjybdm
ORDER BY s.LastModifyTime DESC  ");
            return FindList<VCrkdjmxInfoEntity>(sql.ToString(), param);
        }

        /// <summary>
        /// 删除指定出入库单据和明细
        /// </summary>
        /// <param name="crkId"></param>
        /// <returns></returns>
        public string DeleteCrkdj(long crkId)
        {
            using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
            {
                var crkdj = iKfCrkdjRepo.FindEntity(p => p.Id == crkId);
                if (crkdj == null)
                {
                    return "未找到指定单据";
                }
                if (crkdj.auditState != ((int)EnumAuditState.Temporary).ToString())
                {
                    return "单据审核状态已变更，该功能只支持删除暂存单";
                }
                var mx = ikfCrkmxRepo.IQueryable(p => p.crkId == crkId);
                if (mx != null && mx.Count() > 0)
                {
                    mx.ToList().ForEach(p =>
                    {
                        ikfCrkmxRepo.Delete(p);
                    });
                }
                iKfCrkdjRepo.Delete(crkdj);
                db.Commit();
                return "";
            }
        }

    }
}
