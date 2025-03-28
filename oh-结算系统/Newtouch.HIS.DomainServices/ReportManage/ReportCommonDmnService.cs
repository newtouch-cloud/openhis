using System;
using System.Collections.Generic;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects;
using System.Data.SqlClient;
using Newtouch.HIS.Domain.Entity;
using System.Linq;
using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.ValueObjects.ReportManage;
using System.Data.Common;
using Newtouch.Core.Common;

namespace Newtouch.HIS.DomainServices
{
	/// <summary>
	/// 
	/// </summary>
	public class ReportCommonDmnService : DmnServiceBase, IReportCommonDmnService
	{
		public ReportCommonDmnService(IDefaultDatabaseFactory databaseFactory)
			: base(databaseFactory)
		{
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="orgId"></param>
		/// <param name="year"></param>
		/// <param name="month"></param>
		/// <returns></returns>
		public IList<RptMonthReportDetailEntity> GetMonthReportConfirmedData(string orgId, int year, int month)
		{
			var sql = "select * from rpt_MonthReport_Detail(nolock) where OrganizeId = @orgId and year = @year and month = @month and zt= '1' order by px";
			return this.FindList<RptMonthReportDetailEntity>(sql, new[] { new SqlParameter("@orgId", orgId)
			,new SqlParameter("@year", year),new SqlParameter("@month", month)});
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="orgId"></param>
		/// <param name="startTime"></param>
		/// <param name="endTime"></param>
		/// <returns></returns>
		public IList<MonthReportMajorCateStatisticsVO> GetMonthReportRealTimeData(string orgId, DateTime startTime, DateTime endTime)
		{
			var sql = @"exec sp_GetMonthReportRealTimeData @orgId, @startTime, @endTime";
			return this.FindList<MonthReportMajorCateStatisticsVO>(sql, new[] { new SqlParameter("@orgId", orgId)
			,new SqlParameter("@startTime", startTime) ,new SqlParameter("@endTime", endTime) });
		}
        public IList<SFDLReportVO> GetSFDLCodeSelectJson(string orgId)
        {
            var sql = @"select * from [NewtouchHIS_Base].[dbo].[V_S_xt_sfdl] where zt='1' and organizeid=@orgId";
            return this.FindList<SFDLReportVO>(sql, new[] { new SqlParameter("@orgId", orgId)});
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="calcZje"></param>
        /// <param name="resultZje"></param>
        /// <param name="details"></param>
        /// <param name="balance"></param>
        /// <param name="balanceReason"></param>
        /// <param name="isregenerate"></param>
        public void SubmitMonthReportData(string orgId, string curOprCode, int year, int month, decimal calcZje, decimal resultZje, IList<MonthReportMajorCateStatisticsVO> details, decimal balance = 0, string balanceReason = null, bool isregenerate = false)
		{
			using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
			{
				//首先检查重复生成
				if (!isregenerate)
				{
					if (db.IQueryable<RptMonthReportDetailEntity>().Any(p => p.OrganizeId == orgId && p.year == year && p.month == month))
					{
						throw new FailedException(year + "年" + month.ToString("D2") + "月报表已生成过，不能重复生成");
					}
				}
				else
				{
					var olds = db.IQueryable<RptMonthReportDetailEntity>().Where(p => p.OrganizeId == orgId && p.year == year && p.month == month);
					foreach (var old in olds)
					{
						old.Modify();
						old.zt = "0";   //置为无效
						db.Update(old);
					}
				}
				var createTime = DateTime.Now;

				decimal realcalcZje = 0;
				var idx = 0;
				for (; idx < details.Count; idx++)
				{
					realcalcZje += details[idx].dlje;
					db.Insert(new RptMonthReportDetailEntity()
					{
						Id = Guid.NewGuid().ToString(),
						OrganizeId = orgId,
						year = year,
						month = month,
						patientType = details[idx].patientType,
						blh = details[idx].blh,
						zyhmzh = details[idx].zyhmzh,
						brxm = details[idx].brxm,
						dl = details[idx].dl,
						dlmc = details[idx].dlmc,
						dlje = details[idx].dlje,
						bz = "",
						CreateTime = createTime,
						CreatorCode = curOprCode,
						px = idx,
						zt = "1",
					});
				}

				if (realcalcZje != calcZje)
				{
					throw new FailedException("费用数据发生变更，请重试");
				}

				if (balance != 0)
				{
					db.Insert(new RptMonthReportDetailEntity()
					{
						Id = Guid.NewGuid().ToString(),
						OrganizeId = orgId,
						year = year,
						month = month,
						patientType = "",
						blh = "",
						zyhmzh = "",
						brxm = "",
						dl = "",
						dlmc = "",
						dlje = balance,
						bz = balanceReason,
						CreateTime = createTime,
						CreatorCode = curOprCode,
						px = idx,
						zt = "1",
					});
					var realResultZje = calcZje + balance;
					if (realResultZje != resultZje)
					{
						throw new FailedException("费用数据发生变更，请重试");
					}
				}
				db.Commit();
			}
		}

		public void turnInFee(string orgId, string czr, DateTime kssj, DateTime jssj)
		{
			var sql = @"exec rpt_Outpatient_turnInDailyFee @kssj,@jssj,@orgId,@doctor";
			this.ExecuteSqlCommand(sql, new[] { new SqlParameter("@kssj", kssj)
				,new SqlParameter("@jssj", jssj) ,new SqlParameter("@orgId", orgId),new SqlParameter("@doctor", czr) });
		}

		//获取大类名称
		public IList<GetDlMc> GetDlMc(string OrganizeId)
		{
			return FindList<GetDlMc>(@"select dlmc,dlcode from  NewtouchHIS_Base..xt_sfdl where OrganizeId=@orgId and zt='1' ", new DbParameter[] { new SqlParameter("@orgId", OrganizeId) });
		}

		public YBSBMX_DCdbfVO yBSBMX_DCdbfs(string types, string ksrq, string jsrq, string orgid)
		{
			YBSBMX_DCdbfVO list = new YBSBMX_DCdbfVO();
			var proceduremc = "";
			var sql = "";
			switch (types)
			{
				case "1":
					proceduremc = "NewtouchHIS_Base..RPT_SETT_医保结算申报明细_职工门诊";
					sql = @"exec " + proceduremc + "  @ksrq, @jsrq ,@orgId";
					list.zgmjzvo = this.FindList<ZGMJZ_Query>(sql, new[] { new SqlParameter("@ksrq", ksrq) ,new SqlParameter("@jsrq", jsrq),new SqlParameter("@orgId", orgid) });
					break;
				case "2":
					proceduremc = "NewtouchHIS_Base..RPT_SETT_医保结算申报明细_职工门诊大病";
					sql = @"exec " + proceduremc + "  @ksrq, @jsrq ,@orgId";
					list.zgdbmzvo = this.FindList<ZGDBMZ_Qury>(sql, new[] { new SqlParameter("@ksrq", ksrq) ,new SqlParameter("@jsrq", jsrq),new SqlParameter("@orgId", orgid) });
					break;
				case "3":
					proceduremc = "NewtouchHIS_Base..RPT_SETT_医保结算申报明细_职工住院";
					sql = @"exec " + proceduremc + "  @ksrq, @jsrq ,@orgId";
					list.zgzyvo = this.FindList<ZGZY_Query>(sql, new[] { new SqlParameter("@ksrq", ksrq) ,new SqlParameter("@jsrq", jsrq),new SqlParameter("@orgId", orgid) });
					break;
				case "4":
					proceduremc = "NewtouchHIS_Base..RPT_SETT_医保结算申报明细_职工特殊";
					sql = @"exec " + proceduremc + "  @ksrq, @jsrq ,@orgId";
					list.zgmjztsvo = this.FindList<ZGMJZTS_Query>(sql, new[] { new SqlParameter("@ksrq", ksrq) ,new SqlParameter("@jsrq", jsrq),new SqlParameter("@orgId", orgid) });
					break;
				case "5":
					proceduremc = "NewtouchHIS_Base..RPT_SETT_医保结算申报明细_职工特殊住院";
					sql = @"exec " + proceduremc + "  @ksrq, @jsrq ,@orgId";
					list.zgzyvo = this.FindList<ZGZY_Query>(sql, new[] { new SqlParameter("@ksrq", ksrq) ,new SqlParameter("@jsrq", jsrq),new SqlParameter("@orgId", orgid) });
					break;
				case "Q":
					proceduremc = "NewtouchHIS_Base..RPT_SETT_医保结算申报明细_居民门诊";
					sql = @"exec " + proceduremc + "  @ksrq, @jsrq ,@orgId";
					list.jmmjzvo = this.FindList<JMMJZ_Query>(sql, new[] { new SqlParameter("@ksrq", ksrq), new SqlParameter("@jsrq", jsrq), new SqlParameter("@orgId", orgid) });
					break;
				case "R":
					proceduremc = "NewtouchHIS_Base..RPT_SETT_医保结算申报明细_居民住院";
					sql = @"exec " + proceduremc + "  @ksrq, @jsrq ,@orgId";
					list.jmzyvo = this.FindList<JMZY_Query>(sql, new[] { new SqlParameter("@ksrq", ksrq), new SqlParameter("@jsrq", jsrq), new SqlParameter("@orgId", orgid) });
					break;
				case "S":
					proceduremc = "NewtouchHIS_Base..RPT_SETT_医保结算申报明细_互助帮困";
					sql = @"exec " + proceduremc + "  @ksrq, @jsrq ,@orgId";
					list.hzbkvo = this.FindList<HZBK_Query>(sql, new[] { new SqlParameter("@ksrq", ksrq), new SqlParameter("@jsrq", jsrq), new SqlParameter("@orgId", orgid) });
					break;
				case "W":
					proceduremc = "NewtouchHIS_Base..RPT_SETT_医保结算申报明细_异地门诊";
					sql = @"exec " + proceduremc + "  @ksrq, @jsrq ,@orgId";
					list.ydmzvo = this.FindList<MZYD_Query>(sql, new[] { new SqlParameter("@ksrq", ksrq), new SqlParameter("@jsrq", jsrq), new SqlParameter("@orgId", orgid) });
					break;
				case "E":
					proceduremc = "NewtouchHIS_Base..RPT_SETT_医保结算申报明细_异地住院";
					sql = @"exec " + proceduremc + "  @ksrq, @jsrq ,@orgId";
					list.ydzyvo = this.FindList<ZYYD_Query>(sql, new[] { new SqlParameter("@ksrq", ksrq), new SqlParameter("@jsrq", jsrq), new SqlParameter("@orgId", orgid) });
					break;
				default:
					break;
			}

			return list;
		}

        //获取收费项目
        public IList<getsfxm> Getsfxm(Pagination pagination, string xzstr,string py,string sfdl, string OrganizeId)
        {
            if (xzstr==null)
            {
                xzstr = "";
            }
            if (py == null)
            {
                py = "";
            }
            string sql = @"  select sfxmcode,sfxmmc,dw,dj from [NewtouchHIS_Base].[dbo].[xt_sfxm] sfxm
  left join [NewtouchHIS_herp]..wz_product wz on wz.productCode=sfxm.sfxmCode
  where sfxm.OrganizeId=@orgId 
  and (sfxmcode in (select * from f_split(@xzstr,',')) or @xzstr='')
  and (sfxm.py like @py+'%' or  sfxmmc like  '%' + @py + '%')
  and (sfdlCode = @sfdl or @sfdl='')
";
            if (sfdl=="18")
            {
                sql += "  and (wz.iswzsame='1')";
            }
            return this.QueryWithPage<getsfxm>(sql, pagination, new[] { new SqlParameter("@xzstr", xzstr), new SqlParameter("@py", py), new SqlParameter("@orgId", OrganizeId), new SqlParameter("@sfdl", sfdl) });
        }
        //获取交易记录库
        public IList<GetJyllk> GetJyjlData(Pagination pagination, string ksrq, string jsrq, string OrganizeId)
        {
            string sql = @"select 
ybjs.setl_id lsh,
'H31011500088' jgdm,
gh.kh kh,
ybjs.psn_no zhbz,
'一般病人' brlx,
'1' jzcs,
convert(varchar(50),ybjs.setl_time,120) jysj,
'110' jslx,
convert(varchar(50),ybjs.medfee_sumamt) jyzfy,
convert(varchar(50),isnull((select sum(fund_payamt) from [NewtouchHIS_Sett]..drjk_mzjs_output_mx where fund_pay_type='本年账户支' and mzh=ybjs.mzh),'0')) dnzhzf,
convert(varchar(50),isnull((select sum(fund_payamt) from [NewtouchHIS_Sett]..drjk_mzjs_output_mx where fund_pay_type='历年账户支' and mzh=ybjs.mzh),'0')) lnzhzf,
'' zfdxj,
'' qfdxj,
'' qfdzh,
convert(varchar(50),ybjs.psn_cash_pay) tcdxj,
convert(varchar(50),ybjs.acct_pay) tcdzh,
convert(varchar(50),ybjs.hifp_pay) tczj,
'' fjdxj,
'' fjdzh,
'' fjzf,
convert(varchar(50),ybjs.inscp_scp_amt) ybfwze,
convert(varchar(50),ybjs.fulamt_ownpay_amt+overlmt_selfpay) fybfwzf,
brxz.brxzmc yblx,
js.fph fphm,
case js.cxjsnm when 0 then '收费' else '退费' end tfbz,
convert(varchar(50),ybjs.setl_time,112) ksrq,
'无减负' jfbz,
'' jfje
from [NewtouchHIS_Sett]..drjk_mzjs_output ybjs
inner join [NewtouchHIS_Sett]..mz_gh gh on ybjs.mzh=gh.mzh and gh.zt='1'
left join [NewtouchHIS_Sett]..xt_brxz brxz on gh.brxz=brxz.brxz and gh.OrganizeId=brxz.OrganizeId and brxz.zt='1'
left join [NewtouchHIS_Sett]..mz_js js on js.ybjslsh=ybjs.setl_id and js.zt='1'
where ybjs.zt='1'
and ybjs.setl_time>=@ksrq 
and ybjs.setl_time<=@jsrq ";
            return this.QueryWithPage<GetJyllk>(sql, pagination, new[] { new SqlParameter("@ksrq", ksrq), new SqlParameter("@jsrq", jsrq), new SqlParameter("@orgId", OrganizeId) });
        }
        public IList<GetJyllk> GetJyjlDatatotxt(string ksrq, string jsrq, string OrganizeId)
        {
            string sql = @"select 
ybjs.setl_id lsh,
'H31011500088' jgdm,
gh.kh kh,
ybjs.psn_no zhbz,
'一般病人' brlx,
'1' jzcs,
convert(varchar(50),ybjs.setl_time,120) jysj,
'110' jslx,
convert(varchar(50),ybjs.medfee_sumamt) jyzfy,
convert(varchar(50),isnull((select sum(fund_payamt) from [NewtouchHIS_Sett]..drjk_mzjs_output_mx where fund_pay_type='本年账户支' and mzh=ybjs.mzh),'0')) dnzhzf,
convert(varchar(50),isnull((select sum(fund_payamt) from [NewtouchHIS_Sett]..drjk_mzjs_output_mx where fund_pay_type='历年账户支' and mzh=ybjs.mzh),'0')) lnzhzf,
'' zfdxj,
'' qfdxj,
'' qfdzh,
convert(varchar(50),ybjs.psn_cash_pay) tcdxj,
convert(varchar(50),ybjs.acct_pay) tcdzh,
convert(varchar(50),ybjs.hifp_pay) tczj,
'' fjdxj,
'' fjdzh,
'' fjzf,
convert(varchar(50),ybjs.inscp_scp_amt) ybfwze,
convert(varchar(50),ybjs.fulamt_ownpay_amt+overlmt_selfpay) fybfwzf,
brxz.brxzmc yblx,
js.fph fphm,
case js.cxjsnm when 0 then '收费' else '退费' end tfbz,
convert(varchar(50),ybjs.setl_time,112) ksrq,
'无减负' jfbz,
'' jfje
from [NewtouchHIS_Sett]..drjk_mzjs_output ybjs
inner join [NewtouchHIS_Sett]..mz_gh gh on ybjs.mzh=gh.mzh and gh.zt='1'
left join [NewtouchHIS_Sett]..xt_brxz brxz on gh.brxz=brxz.brxz and gh.OrganizeId=brxz.OrganizeId and brxz.zt='1'
left join [NewtouchHIS_Sett]..mz_js js on js.ybjslsh=ybjs.setl_id and js.zt='1'
where ybjs.zt='1'
and ybjs.setl_time>=@ksrq 
and ybjs.setl_time<=@jsrq
and gh.OrganizeId=@orgId";
            return this.FindList<GetJyllk>(sql, new[] { new SqlParameter("@ksrq", ksrq), new SqlParameter("@jsrq", jsrq), new SqlParameter("@orgId", OrganizeId) });
        }
        /// <summary>
        /// 门急诊大病挂号库
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="ksrq"></param>
        /// <param name="jsrq"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public IList<OutpatientRegistrationVO> GetMjzdbghData(Pagination pagination, string ksrq, string jsrq, string OrganizeId)
        {
            string sql = @"select 
ybjs.setl_id lsh,
gh.kh kh,
gh.ks ksbm,
ks.Name ksmc,
convert(varchar(50),ybjs.medfee_sumamt-ybjs.inscp_scp_amt) ghf,
convert(varchar(50),ybjs.inscp_scp_amt) zlf,
convert(varchar(50),ybjs.medfee_sumamt) jyzfy,
convert(varchar(50),ybjs.inscp_scp_amt) ybfwze,
convert(varchar(50),ybjs.inscp_scp_amt-ybjs.inscp_scp_amt) fybfwze,
convert(varchar(50),ybjs.setl_time,120) jysj,
brxz.brxzmc yblx,
'110' jslx,
case gh.ghzt when '2' then '退费' else '收费' end tfbz,
gh.patid jzlsh
 from 
[NewtouchHIS_Sett]..mz_gh gh
inner join [NewtouchHIS_Sett]..drjk_mzjs_output ybjs  on ybjs.mzh=gh.mzh 
left join [NewtouchHIS_Base]..V_S_Sys_Department ks on ks.Code=gh.ks and ks.OrganizeId=gh.OrganizeId
left join [NewtouchHIS_Sett]..mz_js js on js.ybjslsh=ybjs.setl_id and js.zt='1'
left join [NewtouchHIS_Sett]..xt_brxz brxz on gh.brxz=brxz.brxz and gh.OrganizeId=brxz.OrganizeId and brxz.zt='1'
where gh.brxz!='0'
and gh.zt='1'
and js.jslx='0'
and ybjs.setl_time>=@ksrq 
and ybjs.setl_time<=@jsrq and gh.OrganizeId=@orgId ";
            return this.QueryWithPage<OutpatientRegistrationVO>(sql, pagination, new[] { new SqlParameter("@ksrq", ksrq), new SqlParameter("@jsrq", jsrq), new SqlParameter("@orgId", OrganizeId) });
        }
        public IList<OutpatientRegistrationVO> GetYbMjzdbghtxt(string ksrq, string jsrq, string OrganizeId)
        {
            string sql = @"select 
ybjs.setl_id lsh,
gh.kh kh,
gh.ks ksbm,
ks.Name ksmc,
convert(varchar(50),ybjs.medfee_sumamt-ybjs.inscp_scp_amt) ghf,
convert(varchar(50),ybjs.inscp_scp_amt) zlf,
convert(varchar(50),ybjs.medfee_sumamt) jyzfy,
convert(varchar(50),ybjs.inscp_scp_amt) ybfwze,
convert(varchar(50),ybjs.inscp_scp_amt-ybjs.inscp_scp_amt) fybfwze,
convert(varchar(50),ybjs.setl_time,120) jysj,
brxz.brxzmc yblx,
'110' jslx,
case gh.ghzt when '2' then '退费' else '收费' end tfbz,
gh.patid jzlsh
 from 
[NewtouchHIS_Sett]..mz_gh gh
inner join [NewtouchHIS_Sett]..drjk_mzjs_output ybjs  on ybjs.mzh=gh.mzh 
left join [NewtouchHIS_Base]..V_S_Sys_Department ks on ks.Code=gh.ks and ks.OrganizeId=gh.OrganizeId
left join [NewtouchHIS_Sett]..mz_js js on js.ybjslsh=ybjs.setl_id and js.zt='1'
left join [NewtouchHIS_Sett]..xt_brxz brxz on gh.brxz=brxz.brxz and gh.OrganizeId=brxz.OrganizeId and brxz.zt='1'
where gh.brxz!='0'
and gh.zt='1'
and js.jslx='0'
and ybjs.setl_time>=@ksrq 
and ybjs.setl_time<=@jsrq and gh.OrganizeId=@orgId";
            return this.FindList<OutpatientRegistrationVO>(sql, new[] { new SqlParameter("@ksrq", ksrq), new SqlParameter("@jsrq", jsrq), new SqlParameter("@orgId", OrganizeId) });
        }
        /// <summary>
        /// 获取门急诊大病加床收费数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="ksrq"></param>
        /// <param name="jsrq"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public IList<OutpatientSickBedAtHomeVO> GetMjzjcsfData(Pagination pagination, string ksrq, string jsrq, string OrganizeId)
        {
            string sql = @"select 
convert(varchar(50),ybjs.setl_id) yblsh,
gh.kh kh,
gh.ks ksbm,
ks.Name ksmc,
jz.jzys ysgh,
jz.jzysmc ysxm,
convert(varchar(50),isnull(jsmx.cf_mxnm,jsmx.mxnm)) cfbh,
(select dlmc from [NewtouchHIS_Base]..xt_sfdl where dlcode=xmmx.dl or dlcode=yp.dlCode) fylb,
isnull(yp.ypmc,sfxm.sfxmmc) xmmc,
isnull(cfmx.dw,xmmx.dw) xmdw,
convert(varchar(50),isnull(cfmx.dj,xmmx.dj)) xmdj,
convert(varchar(50),isnull(cfmx.sl,xmmx.sl)) xmsl,
convert(varchar(50),isnull(cfmx.je,xmmx.je)) xmje,
convert(varchar(50),isnull(cfmx.je,xmmx.je)) jyje,
convert(varchar(50),ybjs.inscp_scp_amt) ybfwje,
'可报' bxbz,
convert(varchar(50),ybjs.setl_time) jysj,
brxz.brxzmc yblx,
'120' jslx,
case ybjs.zt when '0' then '退费' else '收费' end tfbz,
convert(varchar(50),jsmx.jsmxnm) lsh,
'' xflsh,
isnull(ypsx.gjybmc,'') yptym,
isnull(ypsx.pzwh,'') zczh,
isnull(ypsx.ybgg,'') mxxmgg
--isnull((case yp.zfxz when '0' then '可报' else (case xmmx.zfxz when '0' then '可报' else null end) end),) jyje,
 from  [NewtouchHIS_Sett]..drjk_mzjs_output ybjs 
 inner join [NewtouchHIS_Sett]..mz_js js on js.ybjslsh=ybjs.setl_id and js.zt='1'
 inner join [NewtouchHIS_Sett]..mz_gh gh on ybjs.mzh=gh.mzh
 inner join [NewtouchHIS_Base]..V_S_Sys_Department ks on ks.Code=gh.ks and ks.OrganizeId=gh.OrganizeId and ks.zt='1'
 inner join [Newtouch_CIS]..xt_jz jz on gh.mzh=jz.mzh and jz.OrganizeId=gh.OrganizeId and jz.zt='1'
 inner join [NewtouchHIS_Sett]..mz_jsmx jsmx on js.jsnm=jsmx.jsnm and js.OrganizeId=jsmx.OrganizeId and jsmx.zt='1'
 left join [NewtouchHIS_Sett]..mz_cfmx cfmx on jsmx.cf_mxnm=cfmx.cfmxId and cfmx.OrganizeId=jsmx.OrganizeId
 left join [NewtouchHIS_Sett]..mz_xm xmmx on jsmx.mxnm=xmmx.xmnm
 left join [NewtouchHIS_Base]..xt_sfxm sfxm on xmmx.sfxm=sfxm.sfxmCode and xmmx.OrganizeId=xmmx.OrganizeId
 left join [NewtouchHIS_Base]..xt_yp yp on cfmx.yp=yp.ypCode and yp.OrganizeId=cfmx.OrganizeId
 left join [NewtouchHIS_Sett]..xt_brxz brxz on gh.brxz=brxz.brxz and gh.OrganizeId=brxz.OrganizeId and brxz.zt='1'
 left join  [NewtouchHIS_Base]..xt_ypsx ypsx on ypsx.ypCode=yp.ypCode and ypsx.OrganizeId=yp.OrganizeId
 left join  [NewtouchHIS_Base]..V_S_xt_sfdl sfdl on (yp.dlCode=sfdl.dlCode or xmmx.dl=sfdl.dlCode) and sfdl.OrganizeId=yp.OrganizeId
 --left join  [NewtouchHIS_Base]..xt_sfdl sfdl2 on sfdl2.dlCode=xmmx.dl and sfdl2.OrganizeId=yp.OrganizeId

where js.jslx!='0'
and ybjs.setl_time>=@ksrq 
and ybjs.setl_time<=@jsrq and gh.OrganizeId=@orgId ";
            return this.QueryWithPage<OutpatientSickBedAtHomeVO>(sql, pagination, new[] { new SqlParameter("@ksrq", ksrq), new SqlParameter("@jsrq", jsrq), new SqlParameter("@orgId", OrganizeId) });
        }
        public IList<OutpatientSickBedAtHomeVO> GetMjzjcsfDatatxt(string ksrq, string jsrq, string OrganizeId)
        {
            string sql = @"select 
convert(varchar(50),ybjs.setl_id) yblsh,
gh.kh kh,
gh.ks ksbm,
ks.Name ksmc,
jz.jzys ysgh,
jz.jzysmc ysxm,
convert(varchar(50),isnull(jsmx.cf_mxnm,jsmx.mxnm)) cfbh,
(select dlmc from [NewtouchHIS_Base]..xt_sfdl where dlcode=xmmx.dl or dlcode=yp.dlCode) fylb,
isnull(yp.ypmc,sfxm.sfxmmc) xmmc,
isnull(cfmx.dw,xmmx.dw) xmdw,
convert(varchar(50),isnull(cfmx.dj,xmmx.dj)) xmdj,
convert(varchar(50),isnull(cfmx.sl,xmmx.sl)) xmsl,
convert(varchar(50),isnull(cfmx.je,xmmx.je)) xmje,
convert(varchar(50),isnull(cfmx.je,xmmx.je)) jyje,
convert(varchar(50),ybjs.inscp_scp_amt) ybfwje,
'可报' bxbz,
convert(varchar(50),ybjs.setl_time) jysj,
brxz.brxzmc yblx,
'120' jslx,
case ybjs.zt when '0' then '退费' else '收费' end tfbz,
convert(varchar(50),jsmx.jsmxnm) lsh,
'' xflsh,
isnull(ypsx.gjybmc,'') yptym,
isnull(ypsx.pzwh,'') zczh,
isnull(ypsx.ybgg,'') mxxmgg
--isnull((case yp.zfxz when '0' then '可报' else (case xmmx.zfxz when '0' then '可报' else null end) end),) jyje,
 from  [NewtouchHIS_Sett]..drjk_mzjs_output ybjs 
 inner join [NewtouchHIS_Sett]..mz_js js on js.ybjslsh=ybjs.setl_id and js.zt='1'
 inner join [NewtouchHIS_Sett]..mz_gh gh on ybjs.mzh=gh.mzh
 inner join [NewtouchHIS_Base]..V_S_Sys_Department ks on ks.Code=gh.ks and ks.OrganizeId=gh.OrganizeId and ks.zt='1'
 inner join [Newtouch_CIS]..xt_jz jz on gh.mzh=jz.mzh and jz.OrganizeId=gh.OrganizeId and jz.zt='1'
 inner join [NewtouchHIS_Sett]..mz_jsmx jsmx on js.jsnm=jsmx.jsnm and js.OrganizeId=jsmx.OrganizeId and jsmx.zt='1'
 left join [NewtouchHIS_Sett]..mz_cfmx cfmx on jsmx.cf_mxnm=cfmx.cfmxId and cfmx.OrganizeId=jsmx.OrganizeId
 left join [NewtouchHIS_Sett]..mz_xm xmmx on jsmx.mxnm=xmmx.xmnm
 left join [NewtouchHIS_Base]..xt_sfxm sfxm on xmmx.sfxm=sfxm.sfxmCode and xmmx.OrganizeId=xmmx.OrganizeId
 left join [NewtouchHIS_Base]..xt_yp yp on cfmx.yp=yp.ypCode and yp.OrganizeId=cfmx.OrganizeId
 left join [NewtouchHIS_Sett]..xt_brxz brxz on gh.brxz=brxz.brxz and gh.OrganizeId=brxz.OrganizeId and brxz.zt='1'
 left join  [NewtouchHIS_Base]..xt_ypsx ypsx on ypsx.ypCode=yp.ypCode and ypsx.OrganizeId=yp.OrganizeId
 left join  [NewtouchHIS_Base]..V_S_xt_sfdl sfdl on (yp.dlCode=sfdl.dlCode or xmmx.dl=sfdl.dlCode) and sfdl.OrganizeId=yp.OrganizeId
 --left join  [NewtouchHIS_Base]..xt_sfdl sfdl2 on sfdl2.dlCode=xmmx.dl and sfdl2.OrganizeId=yp.OrganizeId

where js.jslx!='0'
and ybjs.setl_time>=@ksrq 
and ybjs.setl_time<=@jsrq and gh.OrganizeId=@orgId";
            return this.FindList<OutpatientSickBedAtHomeVO>(sql, new[] { new SqlParameter("@ksrq", ksrq), new SqlParameter("@jsrq", jsrq), new SqlParameter("@orgId", OrganizeId) });
        }
    }
}
