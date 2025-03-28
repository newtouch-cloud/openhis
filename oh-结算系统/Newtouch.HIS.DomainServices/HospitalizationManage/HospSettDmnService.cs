using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// 住院结算 实现
    /// </summary>
    public class HospSettDmnService : DmnServiceBase, IHospSettDmnService
    {
        private readonly IFinancialInvoiceRepo _financialInvoiceRepo;
        private readonly IHospSettlementRepo _hospSettlementRepo;

        public HospSettDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 住院结算 保存至数据库
        /// </summary>
        /// <param name="vo"></param>
        public void SettSyncToDB(OutHospSettDBDataUpdateCollectVO vo, string orgId)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                if (vo.zy_js != null)
                {
                    db.Insert(vo.zy_js);
                }
                if (vo.zy_jsmxList != null && vo.zy_jsmxList.Count > 0)
                {
                    foreach (var item in vo.zy_jsmxList)
                    {
                        db.Insert(item);
                    }
                }
                if (vo.zy_jyjsList != null && vo.zy_jyjsList.Count > 0)
                {
                    foreach (var item in vo.zy_jyjsList)
                    {
                        db.Insert(item);
                    }
                }
                if (vo.zy_jyjsmxList != null && vo.zy_jyjsmxList.Count > 0)
                {
                    foreach (var item in vo.zy_jyjsmxList)
                    {
                        db.Insert(item);
                    }
                }
                if (vo.zy_jszffsList != null && vo.zy_jszffsList.Count > 0)
                {
                    foreach (var item in vo.zy_jszffsList)
                    {
                        db.Insert(item);
                    }
                }
                if (vo.xt_brzhszjlList != null && vo.xt_brzhszjlList.Count > 0)
                {
                    foreach (var item in vo.xt_brzhszjlList)
                    {
                        db.Insert(item);
                    }
                }
                if (vo.zy_jsdlList != null && vo.zy_jsdlList.Count > 0)
                {
                    foreach (var item in vo.zy_jsdlList)
                    {
                        item.jsdlId = Comm.GuId();
                        db.Insert(item);
                    }
                }
                HospPatientBasicInfoEntity oldZybrjbxxEntity = null;
                var zybrjbxxEntityList = db.IQueryable<HospPatientBasicInfoEntity>().Where(p => p.zyh == vo.zyh && p.OrganizeId == orgId).ToList();
                if (zybrjbxxEntityList == null || zybrjbxxEntityList.Count != 1)
                {
                    throw new FailedCodeException("HOSP_PATIENT_BASICINFO_IS_NOT_EXIST");
                }
                oldZybrjbxxEntity = zybrjbxxEntityList[0].Clone();
                zybrjbxxEntityList[0].zybz = vo.zybz;
                zybrjbxxEntityList[0].cyrq = vo.cyrq;
                zybrjbxxEntityList[0].LastModifierCode = OperatorProvider.GetCurrent().UserCode;
                zybrjbxxEntityList[0].LastModifyTime = DateTime.Now;
                db.Update(zybrjbxxEntityList[0]);

                //发票号逻辑
                FinancialInvoiceEntity fpUpdateEntity, fpInsertEntity;
                //need organizeId
                _financialInvoiceRepo.UpdateCurrentGetEntitys(vo.fph, OperatorProvider.GetCurrent().UserCode, out fpUpdateEntity, out fpInsertEntity, orgId);
                if (fpUpdateEntity != null)
                {
                    db.Update(fpUpdateEntity);
                }
                if (fpInsertEntity != null)
                {
                    db.Insert(fpInsertEntity);
                }

                db.Commit();

                if (oldZybrjbxxEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldZybrjbxxEntity, zybrjbxxEntityList[0], HospPatientBasicInfoEntity.GetTableName(), oldZybrjbxxEntity.syxh.ToString());
                }
            }
        }

        /// <summary>
        /// 住院结算 取消结算 保存至数据库
        /// </summary>
        /// <param name="vo"></param>
        public void CancelSettSyncToDB(HospCancelSettDBDataUpdateCollectVO vo, string orgId)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                if (vo.cxjs != null)
                {
                    db.Insert(vo.cxjs);
                }
                if (vo.cx_zyjsmxList != null && vo.cx_zyjsmxList.Count > 0)
                {
                    foreach (var item in vo.cx_zyjsmxList)
                    {
                        db.Insert(item);
                    }
                }
                if (vo.cx_zy_jszffsList != null && vo.cx_zy_jszffsList.Count > 0)
                {
                    foreach (var item in vo.cx_zy_jszffsList)
                    {
                        db.Insert(item);
                    }
                }
                if (vo.cx_xt_brzhszjlList != null && vo.cx_xt_brzhszjlList.Count > 0)
                {
                    foreach (var item in vo.cx_xt_brzhszjlList)
                    {
                        db.Insert(item);
                    }
                }

                HospPatientBasicInfoEntity oldZybrjbxxEntity = null;
                var zybrjbxxEntityList = db.IQueryable<HospPatientBasicInfoEntity>().Where(p => p.zyh == vo.zyh && p.OrganizeId == orgId).ToList();
                if (zybrjbxxEntityList == null || zybrjbxxEntityList.Count != 1)
                {
                    throw new FailedCodeException("HOSP_PATIENT_BASICINFO_IS_NOT_EXIST");
                }
                oldZybrjbxxEntity = zybrjbxxEntityList[0].Clone();
                zybrjbxxEntityList[0].zybz = vo.zybz;
                zybrjbxxEntityList[0].cyrq = vo.cyrq;   //应该没有问题
                zybrjbxxEntityList[0].LastModifierCode = OperatorProvider.GetCurrent().UserCode;
                zybrjbxxEntityList[0].LastModifyTime = DateTime.Now;
                db.Update(zybrjbxxEntityList[0]);

                db.Commit();

                if (oldZybrjbxxEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldZybrjbxxEntity, zybrjbxxEntityList[0], HospPatientBasicInfoEntity.GetTableName(), oldZybrjbxxEntity.syxh.ToString());
                }
            }
        }


        #region 住院结算查询
        public IList<HospSettQueryGridVO> GridInPatientQueryGridJson(Pagination pagination, HospSettQueryReq req, string orgId)
        {
            var sql = new StringBuilder();
            sql.Append(@"SELECT js.zyh ,
                zyxx.xm , 
                js.jsksrq ,
                js.jsjsrq ,
                js.zje ,
                CONVERT(VARCHAR(100), js.CreateTime, 120) CreateTime,
                s.Name CreatorCode ,
                js.jszt ,
                js.jsxz ,
		        zyxx.zybz,
		        zyxx.blh,
				zyxx.nlshow,
				ks.Name ks,
				uf.Name zzys,
				convert(varchar(50),zyxx.ryrq,23) ryrq,
				convert(varchar(50),zyxx.cyrq,23) cyrq,
				convert(varchar(50),js.CreateTime,120) sfsj,
				js.xjzf xj,
				(js.zje-js.xjzf) jz,
				(select stuff(( select ','+ zdmc from [Newtouch_CIS]..zy_PatDxInfo where zdlb='1' and zyh=zyxx.zyh for xml path('')),1,1,'') as ryzd ) ryzd,
				(select stuff(( select ','+ zdmc from [Newtouch_CIS]..zy_PatDxInfo where zdlb='2' and zyh=zyxx.zyh for xml path('')),1,1,'') as ryzd ) cyzd,
       case js.jsxz when '1' then '住院结算' when '2' then '中途结算' else '住院结算' end jslx,
	   uf.Name czy
	    FROM    dbo.zy_js js
                INNER JOIN dbo.zy_brjbxx zyxx ON zyxx.zyh = js.zyh and zyxx.OrganizeId =js.OrganizeId and zyxx.zt='1'
                LEFT JOIN NewtouchHIS_Base..V_C_Sys_UserStaff s ON s.Account = js.CreatorCode
                                                           AND s.OrganizeId =js.OrganizeId
                left join  [NewtouchHIS_Base]..V_S_Sys_Department ks on ks.Code=zyxx.ks and ks.OrganizeId=zyxx.OrganizeId and ks.zt='1' 
       LEFT JOIN [NewtouchHIS_Base]..V_C_Sys_UserStaff uf on uf.gh=js.CreatorCode and uf.OrganizeId=zyxx.OrganizeId and uf.zt='1'

        WHERE   js.OrganizeId = @orgId
and js.jsnm not in (select cxjsnm from zy_js where OrganizeId = @orgId
                AND zt = '1' and jsksrq >= @ryrqkssj and jsjsrq < @ryrqjssj)
                AND js.zt = '1'
                and js.jszt='1'
");
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", orgId));
            if (req != null)
            {
                if (!string.IsNullOrWhiteSpace(req.keyword))
                {
                    sql.Append(@" and (zyxx.xm like @keyword or zyxx.blh like @keyword or zyxx.zyh like @keyword)");
                    pars.Add(new SqlParameter("@keyword", "%" + (req.keyword ?? "") + "%"));
                }

                if (req.jsksrq.HasValue)
                {
                    sql.Append(@" and js.jsksrq >= @ryrqkssj");
                    pars.Add(new SqlParameter("@ryrqkssj", req.jsksrq.Value.Date));
                }
                if (req.jsjsrq.HasValue)
                {
                    sql.Append(@" and js.jsjsrq < @ryrqjssj");
                    pars.Add(new SqlParameter("@ryrqjssj", req.jsjsrq.Value.AddDays(1).Date));
                }
                if (!string.IsNullOrWhiteSpace(req.zybz))
                {
                    sql.Append(@"  and zyxx.zybz in (select col from [dbo].f_split(@zybz,','))");
                    pars.Add(new SqlParameter("@zybz", req.zybz));
                }
            }

            return this.QueryWithPage<HospSettQueryGridVO>(sql.ToString(), pagination, pars.ToArray());
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="organizeId"></param>
        /// <param name="keyword"></param>
        /// <param name="fph"></param>
        /// <param name="jsksrq"></param>
        /// <param name="jsjsrq"></param>
        /// <returns></returns>
        public IList<HospSettlementInfoVO> GetPaginationSettlementList(Pagination pagination, string organizeId, string keyword, string fph, DateTime? jsksrq, DateTime? jsjsrq)
        {
            var sql = new StringBuilder();
            sql.Append(@"select zyjs.jsnm, zybrxx.zyh, zybrxx.xm, brxz.brxzmc , zybrxx.ryrq, zybrxx.cyrq, zyjs.fph
, zyjs.zje, zyjs.xjzf, zyjs.CreatorCode, zyjs.CreateTime,case zybrxx.xb when '1' then '男' else '女' end xb,
zybrxx.zjh,convert(varchar(50),zybrxx.csny,120) csrq,'否' isxsr,isnull(mz.mzmc,'汉族') mz,uf.Name zzys,case brxxk.cyfs when '1' then '治愈' when '2' then '好转' when '3' then '转院' when '4' then '死亡' else '好转' end gz
,case brxxk.cyfs when '3' then '医嘱转院' when '4' then '死亡' else '正常离院' end lyfs,brxxk.cyzdmc cyzd,zybrxx.hu_sheng+zybrxx.hu_shi+zybrxx.hu_xian+zybrxx.hu_dz jtzd,ybjs.setl_id zxlsh
from zy_js zyjs
inner join zy_brjbxx zybrxx
on zybrxx.zyh = zyjs.zyh and zybrxx.OrganizeId = zyjs.OrganizeId
left join xt_brxz brxz
on brxz.brxz = zyjs.brxz and brxz.OrganizeId = zyjs.OrganizeId
LEFT JOIN xt_brjbxx xx ON xx.patid = zybrxx.patid  AND xx.zt='1'
        AND zybrxx.OrganizeId=xx.OrganizeId
LEFT JOIN [NewtouchHIS_Base]..V_S_xt_mz mz on mz.mzCode=zybrxx.mz and mz.zt='1'
LEFT JOIN [NewtouchHIS_Base]..V_C_Sys_UserStaff uf on uf.gh=zybrxx.doctor and uf.OrganizeId=zybrxx.OrganizeId and uf.zt='1'
left join [Newtouch_CIS].[dbo].[zy_brxxk] brxxk on brxxk.zyh=zybrxx.zyh and brxxk.OrganizeId=zybrxx.OrganizeId and brxxk.zt='1'
LEFT JOIN [NewtouchHIS_Sett].[dbo].[drjk_zyjs_output] ybjs  ON ybjs.setl_id = zyjs.ybjslsh AND ybjs.zt = '1'
where zyjs.OrganizeId = @orgId
and zyjs.zt = '1' and zyjs.jszt = '1'
and zyjs.jsnm not in (select cxjsnm from zy_js where jszt = '2' and OrganizeId = @orgId)");
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", organizeId));
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql.Append(@" and (zybrxx.xm like @keyword or zybrxx.blh like @keyword or zybrxx.zyh like @keyword)");
                pars.Add(new SqlParameter("@keyword", "%" + (keyword ?? "") + "%"));
            }
            if (jsksrq.HasValue)
            {
                sql.Append(@" and zyjs.CreateTime >= @jsksrq");
                pars.Add(new SqlParameter("@jsksrq", jsksrq.Value.Date));
            }
            if (jsjsrq.HasValue)
            {
                sql.Append(@" and zyjs.CreateTime < @jsjsrq");
                pars.Add(new SqlParameter("@jsjsrq", jsjsrq.Value.AddDays(1).Date));
            }

            return this.QueryWithPage<HospSettlementInfoVO>(sql.ToString(), pagination, pars.ToArray());
        }

        /// <summary>
        /// 待上传的自费结算病人信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="organizeId"></param>
        /// <param name="keyword"></param>
        /// <param name="jsksrq"></param>
        /// <param name="jsjsrq"></param>
        /// <returns></returns>
        public IList<HospSettlementInfoVO> GetPaginationZFSettlementList(Pagination pagination, string organizeId, string keyword, DateTime? jsksrq, DateTime? jsjsrq)
        {
            var sql = new StringBuilder();
            sql.Append(@"select DISTINCT zyjs.jsnm, zybrxx.zyh, zybrxx.xm, brxz.brxzmc , zybrxx.ryrq, zybrxx.cyrq, zyjs.fph
, zyjs.zje, zyjs.xjzf, zyjs.CreatorCode, zyjs.CreateTime,case zybrxx.xb when '1' then '男' else '女' end xb,
zybrxx.zjh,convert(varchar(50),zybrxx.csny,120) csrq,'否' isxsr,isnull(mz.mzmc,'汉族') mz,uf.Name zzys,case brxxk.cyfs when '1' then '治愈' when '2' then '好转' when '3' then '转院' when '4' then '死亡' else '好转' end gz
,case brxxk.cyfs when '3' then '医嘱转院' when '4' then '死亡' else '正常离院' end lyfs,brxxk.cyzdmc cyzd,zybrxx.hu_sheng+zybrxx.hu_shi+zybrxx.hu_xian+zybrxx.hu_dz jtzd,ybjs.setl_id zxlsh,
CASE WHEN drjk.mlbm_id IS NOT NULL THEN '已上传' ELSE '' END AS sfyb
from zy_js zyjs
inner join zy_brjbxx zybrxx
on zybrxx.zyh = zyjs.zyh and zybrxx.OrganizeId = zyjs.OrganizeId
left join xt_brxz brxz
on brxz.brxz = zyjs.brxz and brxz.OrganizeId = zyjs.OrganizeId
LEFT JOIN xt_brjbxx xx ON xx.patid = zybrxx.patid  AND xx.zt='1'
        AND zybrxx.OrganizeId=xx.OrganizeId
LEFT JOIN [NewtouchHIS_Base]..V_S_xt_mz mz on mz.mzCode=zybrxx.mz and mz.zt='1'
LEFT JOIN [NewtouchHIS_Base]..V_C_Sys_UserStaff uf on uf.gh=zybrxx.doctor and uf.OrganizeId=zybrxx.OrganizeId and uf.zt='1'
left join [Newtouch_CIS].[dbo].[zy_brxxk] brxxk on brxxk.zyh=zybrxx.zyh and brxxk.OrganizeId=zybrxx.OrganizeId and brxxk.zt='1'
LEFT JOIN [NewtouchHIS_Sett].[dbo].[drjk_zyjs_output] ybjs  ON ybjs.setl_id = zyjs.ybjslsh AND ybjs.zt = '1'
LEFT JOIN dbo.Drjk_jxcsc_output drjk ON drjk.mlbm_id = CONVERT(VARCHAR(50), zyjs.jsnm)  -- 关联条件
where zyjs.OrganizeId = @orgId
and zyjs.zt = '1' and zyjs.jszt = '1' and zyjs.brxz = '0'
and zyjs.jsnm not in (select cxjsnm from zy_js where jszt = '2' and OrganizeId = @orgId)
");
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", organizeId));
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql.Append(@" and (zybrxx.xm like @keyword or zybrxx.blh like @keyword or zybrxx.zyh like @keyword)");
                pars.Add(new SqlParameter("@keyword", "%" + (keyword ?? "") + "%"));
            }
            if (jsksrq.HasValue)
            {
                sql.Append(@" and zyjs.CreateTime >= @jsksrq");
                pars.Add(new SqlParameter("@jsksrq", jsksrq.Value.Date));
            }
            if (jsjsrq.HasValue)
            {
                sql.Append(@" and zyjs.CreateTime < @jsjsrq");
                pars.Add(new SqlParameter("@jsjsrq", jsjsrq.Value.AddDays(1).Date));
            }

            return this.QueryWithPage<HospSettlementInfoVO>(sql.ToString(), pagination, pars.ToArray());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="jsnm"></param>
        /// <returns></returns>
        public IList<HospSettlementClassificationFeeVO> SettlementDetailsQuery(string organizeId, int jsnm)
        {
            var sql = @"select dljf.dl dlCode, sfdl.dlmc, dljf.je from (
select dl, sum(je) je from
(
select dl, CONVERT(DECIMAL(18,2), isnull(dj,0.00)*isnull(sl,0.00)) je--(isnull(dj, 0) * isnull(sl, 0)) je
from zy_jsmx a
inner join [V_C_Sys_HbtfZyXmjfb] b
on a.xmjfbbh = b.jfbbh
where a.jsnm = @jsnm and a.zt = '1' and isnull(a.xmjfbbh,0)>0

union all

select dl, CONVERT(DECIMAL(18,2), isnull(dj,0.00)*isnull(sl,0.00)) je --(isnull(dj, 0) * isnull(sl, 0)) je
from zy_jsmx a
inner join [V_C_Sys_HbtfZyYpjfb] b --inner join zy_ypjfb b
on a.ypjfbbh = b.jfbbh
where a.jsnm = @jsnm and a.zt = '1' and isnull(a.ypjfbbh,0)>0

) jfmx
group by dl
) as dljf
left join [NewtouchHIS_Base]..V_S_xt_sfdl sfdl
on sfdl.dlCode = dljf.dl and sfdl.OrganizeId = @orgId";
            return this.FindList<HospSettlementClassificationFeeVO>(sql, new[] { new SqlParameter("@orgId", organizeId), new SqlParameter("@jsnm", jsnm) });
        }
        /// <summary>
        /// 出院结算查询 费用明细
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <param name="sfdl"></param>
        /// <param name="mc"></param>
        /// <returns></returns>
        public IList<HospFeeChargeCategoryGroupDetailVO> SettlementDetailsItemsQuery(Pagination pagination, string zyh, string orgId, string sfdl, string jsnms, string mc)
        {
            var sql = new StringBuilder();
            sql.Append(@"select a.dlCode,a.sfxmmc,a.jfdw,a.dlmc,a.dj,sum(a.sl) sl,sum(a.je) je,a.gg,convert(varchar(20),a.tdrq,120) as tdrq,
case isnull(a.zzfbz,0) when '1' then '是' else '否' end zzfbz,a.zyh,
(case when a.zfxz=1 then '自费' when a.zfxz=4 then '甲类' when a.zfxz=5 then '乙类' when a.zfxz=6 then  '丙类' else '其他' end) as zfxz
 from (
select sfdl.dlCode as dlCode, sfxm.sfxmmc AS sfxmmc,sfxm.dw AS jfdw, sfdl.dlmc as dlmc, 
	xm.dj as dj, xm.sl as sl, (xm.sl*xm.dj) AS je,sfxm.gg as gg,sfxm.zfxz as zfxz,sfxm.createtime as tdrq,xm.zzfbz as zzfbz,xm.zyh as zyh
	FROM  [NewtouchHIS_Sett].[dbo].[V_C_Sys_HbtfZyXmjfb] xm 
	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_sfxm sfxm ON sfxm.sfxmCode=xm.sfxm AND sfxm.OrganizeId=xm.OrganizeId
	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_sfdl sfdl ON sfdl.dlCode=xm.dl AND sfdl.OrganizeId = xm.OrganizeId
    inner join zy_jsmx jsmx on jsmx.xmjfbbh = xm.jfbbh
	WHERE  xm.OrganizeId=@orgId and xm.zyh=@zyh and jsmx.jsnm= @jsnms
	UNION ALL
	SELECT sfdl.dlCode as dlCode, yp.ypmc  AS sfxmmc, cfmx.yldw as jfdw, sfdl.dlmc as dlmc,
	cfmx.dj as dj, cfmx.sl as sl, (cfmx.sl*dj) AS je,ypsx.ypgg as gg,yp.zfxz as zfxz,cfmx.createtime as tdrq,cfmx.zzfbz as zzfbz,cfmx.zyh as zyh
	FROM  [NewtouchHIS_Sett].[dbo].[V_C_Sys_HbtfZyYpjfb] cfmx 
	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp ON yp.ypCode=cfmx.yp AND yp.OrganizeId=cfmx.OrganizeId
	inner join NewtouchHIS_Base.[dbo].[xt_ypsx] ypsx on yp.ypId=ypsx.ypId
	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_sfdl sfdl ON sfdl.dlCode = cfmx.dl AND sfdl.OrganizeId=cfmx.OrganizeId
    inner join zy_jsmx jsmx on jsmx.ypjfbbh = cfmx.jfbbh
	WHERE cfmx.OrganizeId=@orgId and cfmx.zyh=@zyh and jsmx.jsnm= @jsnms
	) as a  where dlcode=@dlcode  ");
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", orgId));
            pars.Add(new SqlParameter("@zyh", zyh));
            pars.Add(new SqlParameter("@dlcode", sfdl));
            pars.Add(new SqlParameter("@jsnms", jsnms));
            if (!string.IsNullOrWhiteSpace(mc))
            {
                sql.Append(" and sfxmmc like '%" + mc + "%' ");
            }
            sql.Append(@" group by a.dlCode,a.sfxmmc,a.jfdw,a.dlmc,a.dj,a.gg,a.zfxz,a.tdrq,a.zzfbz,a.zyh ");
            return this.QueryWithPage<HospFeeChargeCategoryGroupDetailVO>(sql.ToString(), pagination, pars.ToArray());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="fph"></param>
        public void UpdateSettInvoiceNo(string orgId, int jsnm, string fph)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                var jsEntity = _hospSettlementRepo.IQueryable().Where(p => p.jsnm == jsnm).FirstOrDefault();
                if (jsEntity == null)
                {

                }

                //发票号逻辑
                //FinancialInvoiceEntity fpUpdateEntity, fpInsertEntity;
                ////need organizeId
                //_financialInvoiceRepo.UpdateCurrentGetEntitys(fph, OperatorProvider.GetCurrent().UserCode, out fpUpdateEntity, out fpInsertEntity, orgId);
                //if (fpUpdateEntity != null)
                //{
                //    db.Update(fpUpdateEntity);
                //}
                //if (fpInsertEntity != null)
                //{
                //    db.Insert(fpInsertEntity);
                //}

                jsEntity.fph = fph;
                db.Update(jsEntity);

                db.Commit();
            }
        }
    }

}
