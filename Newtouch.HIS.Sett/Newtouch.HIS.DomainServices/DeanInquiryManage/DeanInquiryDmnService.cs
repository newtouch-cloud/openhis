using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.Entity.DeanInquiryManage;
using Newtouch.HIS.Domain.IDomainServices.DeanInquiryManage;
using Newtouch.HIS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.DomainServices.DeanInquiryManage
{
    public class DeanInquiryDmnService : DmnServiceBase, IDeanInquiryDmnService
    {

        public DeanInquiryDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
        #region 住院费用分析
        /// <summary>
        /// 抬头
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ksrq"></param>
        /// <param name="jsrq"></param>
        /// <param name="rtype"></param>
        /// <returns></returns>
        public ZYFYFXDTO ZYFYFX_TitleData(string orgId, string ksrq, string jsrq, string rtype)
        {
            var inParameters = new List<SqlParameter>();
            inParameters.Add(new SqlParameter("@kssj", ksrq));
            inParameters.Add(new SqlParameter("@jssj", jsrq));
            inParameters.Add(new SqlParameter("@lx", rtype));
            inParameters.Add(new SqlParameter("@OrganizeId", orgId));
            return this.FirstOrDefault<ZYFYFXDTO>("exec [NewtouchHIS_Sett]..[yzcx_zyfyfx_bunner] @kssj,@jssj,@lx, @OrganizeId", inParameters.ToArray());
        }
        /// <summary>
        /// 院长查询住院费用分析_科室费用分析部分
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ksrq"></param>
        /// <param name="jsrq"></param>
        /// <param name="rtype"></param>
        /// <returns></returns>
        public List<ZYFYFX_KSFYFXDTO> ZYFYFX_KSFYFXDTO(string orgId, string ksrq, string jsrq, string rtype)
        {
            var inParameters = new List<SqlParameter>();
            inParameters.Add(new SqlParameter("@kssj", ksrq));
            inParameters.Add(new SqlParameter("@jssj", jsrq));
            inParameters.Add(new SqlParameter("@lx", rtype));
            inParameters.Add(new SqlParameter("@OrganizeId", orgId));
            return this.FindList<ZYFYFX_KSFYFXDTO>("exec [NewtouchHIS_Sett]..[yzcx_zyfyfx_ksfyfx] @kssj,@jssj,@lx, @OrganizeId", inParameters.ToArray());

        }
        /// <summary>
        /// 出院患者费用分析
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ksrq"></param>
        /// <param name="jsrq"></param>
        /// <param name="rtype"></param>
        /// <returns></returns>
        public List<ZYFYFX_CYHZFYFXDTO> GetCYHZFYFData(string orgId, string ksrq, string jsrq, string rtype)
        {
            var inParameters = new List<SqlParameter>();
            inParameters.Add(new SqlParameter("@kssj", ksrq));
            inParameters.Add(new SqlParameter("@jssj", jsrq));
            inParameters.Add(new SqlParameter("@lx", rtype));
            inParameters.Add(new SqlParameter("@OrganizeId", orgId));
            return this.FindList<ZYFYFX_CYHZFYFXDTO>("exec [NewtouchHIS_Sett]..[yzcx_zyfyfx_cyhzfyfx] @kssj,@jssj,@lx, @OrganizeId", inParameters.ToArray());
        }
        public List<CYHZFYFTJTDTO> GetCYHZFYFTJData(string orgId, string ksrq, string jsrq, string rtype)
        {
            var inParameters = new List<SqlParameter>();
            inParameters.Add(new SqlParameter("@kssj", ksrq));
            inParameters.Add(new SqlParameter("@jssj", jsrq));
            inParameters.Add(new SqlParameter("@lx", rtype));
            inParameters.Add(new SqlParameter("@OrganizeId", orgId));
            return this.FindList<CYHZFYFTJTDTO>("exec [NewtouchHIS_Sett]..[yzcx_zyfyfx_cyhzfyfx_zxt] @kssj,@jssj,@lx, @OrganizeId", inParameters.ToArray());
        }
        #endregion
        /// <summary>
        /// 院长查询-今日动态-banner 
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public JRDT_Banner GetJRDTBanner(string orgId)
        {
            var inParameters = new List<SqlParameter>();
            inParameters.Add(new SqlParameter("@ksrq", DateTime.Now.ToString("yyyy-MM-dd")));
            inParameters.Add(new SqlParameter("@jsrq", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")));
            inParameters.Add(new SqlParameter("@OrganizeId", orgId));
            return this.FirstOrDefault<JRDT_Banner>("exec [NewtouchHIS_Sett]..[yzcx_jrdt_banner] @ksrq,@jsrq, @OrganizeId", inParameters.ToArray());
        }
        /// <summary>
        /// 当日动态_全院收入概况
        /// </summary>
        public List<DailyUpdatesEntiy> DailyUpdates_GetQysr()
        {
            List<DailyUpdatesEntiy> outpatientlist = null;  
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"select '1' xh,'康复医学科' mzks,3 yjz,4 jzz
                union all
                select '2' xh,'康复医学科' mzks,34 yjz,44 jzz
                union all
                select '2' xh,'康复医学科' mzks,34 yjz,44 jzz
                union all
                select '2' xh,'康复医学科' mzks,34 yjz,44 jzz
                union all
                select '2' xh,'康复医学科' mzks,34 yjz,44 jzz
                union all
                select '2' xh,'康复医学科' mzks,34 yjz,44 jzz
                union all
                select '2' xh,'康复医学科' mzks,34 yjz,44 jzz
                union all
                select '2' xh,'康复医学科' mzks,34 yjz,44 jzz");
            //SqlParameter[] param =
            //{
            //        new SqlParameter("@orgId",orgId)
            //};
            outpatientlist = FindList<DailyUpdatesEntiy>(sqlStr.ToString());
            return outpatientlist;
        }

        /// <summary>
        /// 当日动态_今日动态门诊
        /// </summary>
        public List<DailyUpdates_GetJrdtmz> DailyUpdates_GetJrdt(string orgId)
        {
            List<DailyUpdates_GetJrdtmz> outpatientlist = null;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"
                with
t1 as
(
select ks,dept.Name mzks from [NewtouchHIS_Sett]..mz_gh as a left join [NewtouchHIS_Base]..Sys_Department dept on dept.Code=a.ks 
where a.OrganizeId=@orgId and a.zt=1 group by ks,dept.Name  
)
select isnull(djz,0)djz,isnull(jzz,0)jzz,isnull(yjz,0)yjz,t1.ks,t1.mzks from t1 
left join (select count(ks)djz,ks from [NewtouchHIS_Sett]..mz_gh gh where gh.jzbz=1 and OrganizeId=@orgId and zt=1  and ghrq between @ksrq and @jsrq group by ks )tdjz on tdjz.ks=t1.ks
left join (select count(ks)jzz,ks from [NewtouchHIS_Sett]..mz_gh gh1 where gh1.jzbz=2 and OrganizeId=@orgId and zt=1 and ghrq between @ksrq and @jsrq group by ks )tjzz on tjzz.ks=t1.ks
left join (select count(ks)yjz,ks from [NewtouchHIS_Sett]..mz_gh gh2 where gh2.jzbz=3 and OrganizeId=@orgId and zt=1 and ghrq between @ksrq and @jsrq group by ks )tyjz on tyjz.ks=t1.ks
                ");

            var inParameters = new List<SqlParameter>();
            inParameters.Add(new SqlParameter("@ksrq", DateTime.Now.ToString("yyyy-MM-dd")));
            inParameters.Add(new SqlParameter("@jsrq", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")));
            inParameters.Add(new SqlParameter("@orgId", orgId));
            outpatientlist = FindList<DailyUpdates_GetJrdtmz>(sqlStr.ToString(), inParameters.ToArray());
            return outpatientlist;
        }

        /// <summary>
        /// 当日动态_门诊处方
        /// </summary>
        public List<DailyUpdates_GetMzcf> DailyUpdates_GetMzcf(string orgId)
        {
            List<DailyUpdates_GetMzcf> outpatientlist = null;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"select '处方数量' xm,CONVERT(decimal(10,0),count(*))sl from NewtouchHIS_Sett..mz_cf where CreateTime between @ksrq and @jsrq and zt=1 and OrganizeId=@orgId
union all 
select '处方金额' xm,isnull(sum(zje),0) sl from NewtouchHIS_Sett..mz_cf where CreateTime between @ksrq and @jsrq and zt=1 and OrganizeId=@orgId
union all 
select '人均处方费用(元)' xm,isnull(sum(zje)/(
select count(*) from (select patid from NewtouchHIS_Sett..mz_cf  where CreateTime between @ksrq and @jsrq and zt=1 and OrganizeId=@orgId group by patid) a),0)sl from NewtouchHIS_Sett..mz_cf where CreateTime between @ksrq and @jsrq and zt=1 and OrganizeId=@orgId");
            var inParameters = new List<SqlParameter>();
            inParameters.Add(new SqlParameter("@ksrq", DateTime.Now.ToString("yyyy-MM-dd")));
            inParameters.Add(new SqlParameter("@jsrq", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")));
            inParameters.Add(new SqlParameter("@orgId", orgId));
            outpatientlist = FindList<DailyUpdates_GetMzcf>(sqlStr.ToString(), inParameters.ToArray());
            return outpatientlist;
        }

        /// <summary>
        /// 当日动态_门诊费用
        /// </summary>
        public List<DailyUpdates_GetMzfy> DailyUpdates_GetMzfy(string orgId)
        {
            var inParameters = new List<SqlParameter>();
            inParameters.Add(new SqlParameter("@ksrq", DateTime.Now.ToString("yyyy-MM-dd")));
            inParameters.Add(new SqlParameter("@jsrq", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")));
            inParameters.Add(new SqlParameter("@jgdm", orgId));
            return this.FindList<DailyUpdates_GetMzfy>("exec [NewtouchHIS_Sett]..[yzcx_jrdt_mzfy] @ksrq,@jsrq, @jgdm", inParameters.ToArray());

        }

        /// <summary>
        /// 当日动态_住院占床率
        /// </summary>
        public List<DailyUpdates_GetZyzcl> DailyUpdates_GetZyzcl(string orgId)
        {
            List<DailyUpdates_GetZyzcl> outpatientlist = null;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"select '开放床位'xm,count(cwCode)sl from [NewtouchHIS_Base]..xt_cw where OrganizeId=@orgId and zt=1
union all
select '空床位'xm,count(cwCode)sl from [NewtouchHIS_Base]..xt_cw where OrganizeId=@orgId and zt=1 and (sfzy=0 or sfzy is null)
union all
select  '占床率(%)'xm,convert(int,CONVERT(decimal(5,2),COUNT(b.cwCode))/CONVERT(decimal(5,2),COUNT(a.cwCode))*100)sl from [NewtouchHIS_Base]..xt_cw as a 
left join [NewtouchHIS_Base]..xt_cw as b on b.cwCode=a.cwCode and b.zt=1 and b.sfzy=1
where a.OrganizeId=@orgId and a.zt=1");
            var inParameters = new List<SqlParameter>();
            inParameters.Add(new SqlParameter("@orgId", orgId));
            outpatientlist = FindList<DailyUpdates_GetZyzcl>(sqlStr.ToString(), inParameters.ToArray());
            return outpatientlist;
        }

        /// <summary>
        /// 当日动态_门诊挂号统计
        /// </summary>
        public List<DailyUpdates_GetMzghtj> DailyUpdates_GetMzghtj(string orgId)
        {
            List<DailyUpdates_GetMzghtj> outpatientlist = null;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"select '普通门诊'ghlx,count(mjzbz)sl from mz_gh where OrganizeId=@orgId and zt=1 and mjzbz =1 and ghrq between @ksrq and @jsrq
union all
select '急诊'ghlx,count(mjzbz)sl from mz_gh where OrganizeId=@orgId and zt=1 and mjzbz =2 and ghrq between @ksrq and @jsrq
union all
select '专家门诊'ghlx,count(mjzbz)sl from mz_gh where OrganizeId=@orgId and zt=1 and mjzbz =3 and ghrq between @ksrq and @jsrq");
            var inParameters = new List<SqlParameter>();
            inParameters.Add(new SqlParameter("@ksrq", DateTime.Now.ToString("yyyy-MM-dd")));
            inParameters.Add(new SqlParameter("@jsrq", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")));
            inParameters.Add(new SqlParameter("@orgId", orgId));
            outpatientlist = FindList<DailyUpdates_GetMzghtj>(sqlStr.ToString(), inParameters.ToArray());
            return outpatientlist;
        }

        /// <summary>
        /// 当日动态_住院患者统计
        /// </summary>
        public List<DailyUpdates_GetZyhztj> DailyUpdates_GetZyhztj(string orgId)
        {
            List<DailyUpdates_GetZyhztj> outpatientlist = null;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"select '出院人数'xm,count(zyh)sl from [Newtouch_CIS]..zy_brxxk with(nolock) where OrganizeId=@orgId and cqrq between @ksrq and @jsrq and zt=1 and zybz<>9
union all
select '入院人数'xm,count(zyh)sl from ..zy_brjbxx  with(nolock) where OrganizeId=@orgId and ryrq between @ksrq and @jsrq and zt=1
union all
select '手术人数'xm,count(*)sl from(
select zyh, count(1)sl from [Newtouch_EMR]..mr_basy_ss where OrganizeId=@orgId and SSJCZRQ between @ksrq and @jsrq and zt=1 group by zyh)temp
union all
select '死亡人数'xm,count(ZYH)sl from [Newtouch_EMR]..mr_basy where OrganizeId=@orgId and CYSJ between @ksrq and @jsrq and zt=1 and LYFS=5");
            var inParameters = new List<SqlParameter>();
            inParameters.Add(new SqlParameter("@ksrq", DateTime.Now.ToString("yyyy-MM-dd")));
            inParameters.Add(new SqlParameter("@jsrq", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")));
            inParameters.Add(new SqlParameter("@orgId", orgId));
            outpatientlist = FindList<DailyUpdates_GetZyhztj>(sqlStr.ToString(), inParameters.ToArray());
            return outpatientlist;
        }

        /// <summary>
        /// 业务排名_门诊医生工作量
        /// </summary>
        /// <returns></returns>
        public List<BusinessRankingEntiy> BusinessRankingEntiy_mzysgzl(string kssj, string jssj,string OrganizeId)
        {
            List<BusinessRankingEntiy> outpatientlist = null;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"
            select stf.Name ys,dp.Name mzks,case when rs is null then 0 else rs end mzl from NewtouchHIS_Base..Sys_Staff stf
            left join NewtouchHIS_Base..Sys_Department dp on stf.DepartmentCode=dp.Code and dp.OrganizeId=stf.OrganizeId
            left join (select jzys,count(jzId) rs from Newtouch_CIS..xt_jz where jzzt=3 and zlkssj is not null and zljssj
            between @kssj and @jssj  and OrganizeId=@orgId group by jzys)as jz on stf.gh=jz.jzys 
            where dp.mzzybz in(0,1) and stf.OrganizeId=@orgId
            order by dp.Name
                ");
            outpatientlist = FindList<BusinessRankingEntiy>(sqlStr.ToString(), new[] { new SqlParameter("@orgId", OrganizeId), new SqlParameter("@kssj", kssj), new SqlParameter("@jssj", jssj) });
            return outpatientlist;
        }

        /// <summary>
        /// 业务排名_门诊科室收入排名
        /// </summary>
        /// <returns></returns>
        public List<BusinessRankingEntiy_mzkssr> BusinessRankingEntiy_mzkssr(string kssj, string jssj, string OrganizeId)
        {
            List<BusinessRankingEntiy_mzkssr> outpatientlist = null;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"
                select mzsr.Name mzks,isnull(sum(mzsr.xmsr),0)+isnull(sum(mzsr.ypsr),0) mzsr,isnull(sum(mzsr.ypsr),0) ypsr,
                case when sum(mzsr.ypsr) is null then '0%' when sum(mzsr.xmsr) is null then '100%' else convert(varchar(50),
                convert(decimal(5,2),sum(mzsr.ypsr)/(sum(mzsr.xmsr)+sum(mzsr.ypsr))*100))+'%' end  ypzb from　(
                select dp.Name ,sum(xm.je) xmsr,sum(yp.je) ypsr from
                NewtouchHIS_Base..Sys_Department dp 
                left join (select ks,case when jszt=1 then je else -je end je from  NewtouchHIS_Sett..mz_js js
                left join NewtouchHIS_Sett..mz_jsmx mx on js.jsnm=mx.jsnm and js.OrganizeId=mx.OrganizeId
                left join NewtouchHIS_Sett..mz_xm xm on mx.mxnm=xm.xmnm and xm.OrganizeId=mx.OrganizeId
                where mx.mxnm is not null and   mx.CreateTime between @kssj and  @jssj and js.OrganizeId=@orgId)as xm on dp.Code=xm.ks
                left join (select ks,case when jszt=1 then je else -je end je from  NewtouchHIS_Sett..mz_js js
                left join NewtouchHIS_Sett..mz_jsmx mx on js.jsnm=mx.jsnm and js.OrganizeId=mx.OrganizeId
                left join NewtouchHIS_Sett..mz_cfmx xm on mx.cf_mxnm=xm.cfmxId and xm.OrganizeId=mx.OrganizeId
                left join NewtouchHIS_Sett..mz_cf cf on cf.cfnm=xm.cfnm and cf.OrganizeId=xm.OrganizeId
                where mx.cf_mxnm is not null and mx.CreateTime between @kssj and @jssj and js.OrganizeId=@orgId)as yp on dp.Code=yp.ks
                where dp.mzzybz in(0,1) and dp.OrganizeId=@orgId
                group by dp.Name)as mzsr group by mzsr.Name order by ypzb desc,mzsr desc
                ");
            outpatientlist = FindList<BusinessRankingEntiy_mzkssr>(sqlStr.ToString(), new[] { new SqlParameter("@orgId", OrganizeId), new SqlParameter("@kssj", kssj), new SqlParameter("@jssj", jssj) });
            return outpatientlist;
        }

        /// <summary>
        /// 业务排名_住院业务科室排名
        /// </summary>
        /// <returns></returns>
        public List<BusinessRankingEntiy_zyywks> BusinessRankingEntiy_zyywks(string kssj,string jssj,string orgId)
        {
            List<BusinessRankingEntiy_zyywks> outpatientlist = null;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"
            with t as(select * from NewtouchHIS_Base..xt_bq where zt=1 and OrganizeId=@OrganizeId) select  
                            t1.bqCode,t1.bqmc zyks,ISNULL(ryrs.sl,0) ryrs,ISNULL(cyrs.sl,0) cyrs,fyxx.zje zyzfy,
							convert(decimal(20,2),(zyts.sl*0.01*100/cyrs.sl) )pjzyts  ,isnull(ssrs.sl,0) ssrs,convert(decimal(24,2),(fyxx.zje/cyrs.sl)) cjfy
                    from t t1 
                    left join(select bq, COUNT(1) sl 
                    from zy_brjbxx zyzt 
                    where  zyzt.zt=1 and zyzt.OrganizeId=@OrganizeId 
                    and ryrq between @kssj and @jssj
                    group by bq) ryrs on  t1.bqCode=ryrs.bq

                    left join(select  bq, COUNT(1) sl 
                    from zy_brjbxx zyzt 
                    where  zyzt.zt=1 and zyzt.OrganizeId=@OrganizeId  and zyzt.zybz<>9  and cyrq is not null
                    and cyrq between @kssj and @jssj
                    group by  bq ) cyrs on  t1.bqCode=cyrs.bq
 
				    
					left join (select WardCode,sum(zje) zje from zy_js  js left join Newtouch_CIS..zy_brxxk brxx on js.zyh=brxx.zyh
					where   jsjsrq   between @kssj and @jssj
					group by WardCode 
					) fyxx on fyxx.WardCode=t1.bqCode
					
					left join (
					 select  bq,sum(Newtouch_EMR.dbo.get_zyts(ryrq,cyrq)) sl from zy_brjbxx where ryrq is not null and cyrq is not null
					  and  cyrq  between @kssj and @jssj and  zybz<>9
					 group by  bq) zyts on zyts.bq=t1.bqCode

					left join ( select bq,count(1) sl from   Newtouch_EMR..mr_basy_ss ss left join zy_brjbxx brxx  on ss.ZYH=brxx.zyh
					where ss.zt=1 and brxx.zt=1 and ss.OrganizeId=@OrganizeId		
					 and  ss.CreateTime between @kssj and @jssj
					group by bq ) ssrs on ssrs.bq=t1.bqCode
                ");
            var param = new DbParameter[] {
                new SqlParameter("@OrganizeId",orgId),
                new SqlParameter("@kssj",kssj),
                new SqlParameter("@jssj",jssj),
            };
            outpatientlist = FindList<BusinessRankingEntiy_zyywks>(sqlStr.ToString(), param);
            return outpatientlist;
        }

        /// <summary>
        /// 业务排名_医生业务排名
        /// </summary>
        /// <returns></returns>
        public List<BusinessRankingEntiy_ysyw> BusinessRankingEntiy_ysyw(string kssj, string jssj, string orgId,string bqcode)
        {
            List<BusinessRankingEntiy_ysyw> outpatientlist = null;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"
               	with t as(select  CreatorCode from zy_brjbxx where  bq=@bq	 group by CreatorCode) select  
                        staff.Name ys,ISNULL(ryrs.sl,0) ryrs,ISNULL(cyrs.sl,0) cyrs,fyxx.zje zyzfy,
							convert(decimal(20,2),(zyts.sl*0.01*100/cyrs.sl) )pjzyts  ,isnull(ssrs.sl,0) ssrs,convert(decimal(24,2),(fyxx.zje/cyrs.sl)) cjfy
                    from t t1 
					  
					  left join(select CreatorCode, COUNT(1) sl 
                    from zy_brjbxx zyzt(nolock) 
                    where  zyzt.zt=1 and zyzt.OrganizeId=@OrganizeId
                    and ryrq  between @kssj and @jssj and bq=@bq	
                    group by CreatorCode) ryrs on  t1.CreatorCode=ryrs.CreatorCode

                    left join(select  CreatorCode, COUNT(1) sl 
                    from zy_brjbxx zyzt (nolock) 
                    where  zyzt.zt=1 and zyzt.OrganizeId=@OrganizeId and zyzt.zybz<>9  and cyrq is not null
                    and cyrq  between @kssj and @jssj and bq=@bq	
                    group by  CreatorCode ) cyrs on  t1.CreatorCode=cyrs.CreatorCode
 
				    
					left join (select js.CreatorCode,sum(zje) zje from zy_js (nolock)  js left join Newtouch_CIS..zy_brxxk brxx(nolock)  on js.zyh=brxx.zyh
					where  jsjsrq between @kssj and @jssj and WardCode=@bq	
					group by js.CreatorCode 
					) fyxx on fyxx.CreatorCode=t1.CreatorCode
					
					left join (
					 select  CreatorCode,sum(Newtouch_EMR.dbo.get_zyts(ryrq,cyrq)) sl from zy_brjbxx(nolock)  where ryrq is not null and cyrq is not null
					  and cyrq  between @kssj and @jssj and  zybz<>9 and bq=@bq	
					 group by  CreatorCode) zyts on zyts.CreatorCode=t1.CreatorCode

					left join ( select ss.CreatorCode,count(1) sl from   Newtouch_EMR..mr_basy_ss(nolock)  ss left join zy_brjbxx brxx(nolock)   on ss.ZYH=brxx.zyh
					where ss.zt=1 and brxx.zt=1 and ss.OrganizeId=@OrganizeId	and  brxx.bq=@bq	
					 and ss.CreateTime between @kssj and @jssj
					group by ss.CreatorCode ) ssrs on ssrs.CreatorCode=t1.CreatorCode

					left join NewtouchHIS_Base..Sys_Staff staff on t1.CreatorCode=staff.gh
                ");
            var param = new DbParameter[] {
                new SqlParameter("@OrganizeId",orgId),
                new SqlParameter("@kssj",kssj),
                new SqlParameter("@jssj",jssj),
                new SqlParameter("@bq",bqcode),
            };
            outpatientlist = FindList<BusinessRankingEntiy_ysyw>(sqlStr.ToString(), param);
            return outpatientlist;
        }

        /// <summary>
        /// 门诊综合分析_抬头数据
        /// </summary>
        /// <returns></returns>
        public OutpatientComprehensiveAnalysisEntiy OutpatientComprehensiveAnalysisEntiy_TitleData(string orgId,string ksrq,string jsrq,string rtype)
        {
            var inParameters = new List<SqlParameter>();
            inParameters.Add(new SqlParameter("@ksrq", ksrq));
            inParameters.Add(new SqlParameter("@jsrq", jsrq));
            inParameters.Add(new SqlParameter("@rtype", rtype));
            inParameters.Add(new SqlParameter("@OrganizeId", orgId));
            return this.FirstOrDefault<OutpatientComprehensiveAnalysisEntiy>("exec [NewtouchHIS_Sett]..[yzcx_mzzhfx_tt] @ksrq,@jsrq,@rtype, @OrganizeId", inParameters.ToArray());
        }

        /// <summary>
        /// 门诊综合分析_门诊患者分析
        /// </summary>
        /// <returns></returns>
        public OutpatientComprehensiveAnalysisEntiy_Mzhzfx OutpatientComprehensiveAnalysisEntiy_Mzhzfx(string orgId, string ksrq, string jsrq)
        {
            var inParameters = new List<SqlParameter>();
            inParameters.Add(new SqlParameter("@ksrq", ksrq));
            inParameters.Add(new SqlParameter("@jsrq", jsrq));
            inParameters.Add(new SqlParameter("@OrganizeId", orgId));
            return this.FirstOrDefault<OutpatientComprehensiveAnalysisEntiy_Mzhzfx>("exec [NewtouchHIS_Sett]..[yzcx_mzzhfx_tjt] @ksrq,@jsrq, @OrganizeId", inParameters.ToArray());
        }

        /// <summary>
        /// 门诊工作量_门诊科室工作量
        /// </summary>
        public List<OutpatientWorkloadEntiy> OutpatientWorkloadEntiy_Mzksgzl(string ksrq, string jsrq, string OrganizeId)
        {
            List<OutpatientWorkloadEntiy> outpatientlist = null;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"
select dp.name mzks,dp.Code DepartmentCode, isnull(mzzlrc.rs,0) mzzlrc,isnull(cfsl.cf,0) cfsl,isnull(kycfs.ky,0) kycfs,isnull(fycfs.fy,0) fycfs,isnull(kfcfs.kf,0) kfcfs
from NewtouchHIS_Base..Sys_Department dp 
left join (select jzks,count(jz.jzId) rs from Newtouch_CIS..xt_jz jz where  jz.jzzt=3 and jz.zlkssj is not null  and jz.zt=1  and zlkssj between @ksrq and @jsrq group by jzks) mzzlrc on  dp.Code=mzzlrc.jzks
left join(select ks,count(cfh) cf from Newtouch_CIS..xt_cf cf where   cf.zt=1 and cf.sfbz=1  and CreateTime between @ksrq and @jsrq and OrganizeId=@orgId group by ks) cfsl on  dp.Code=cfsl.ks
left join(select ks,count(cfh) ky from Newtouch_CIS..xt_cf cf where   cf.zt=1 and cf.sfbz=1  and CreateTime between @ksrq and @jsrq and cflx in ('1','2') and OrganizeId=@orgId group by ks) kycfs on  dp.Code=kycfs.ks
left join(select ks,count(distinct fy.cfh) fy from Newtouch_CIS..xt_cf cf left join   NewtouchHIS_PDS..mz_cfypczjl fy on cf.cfh=fy.cfh where fy.CreateTime between @ksrq and @jsrq and operateType=1 and cf.zt=1 and cf.sfbz=1 and OrganizeId=@orgId group by ks) fycfs on  dp.Code=fycfs.ks
left join(select ks,count(cfh) kf from Newtouch_CIS..xt_cf cf where   cf.zt=1 and cf.sfbz=1 and CreateTime between @ksrq and @jsrq and cflx='3' and OrganizeId=@orgId group by ks) kfcfs on  dp.Code=kfcfs.ks
where dp.OrganizeId=@orgId and mzzybz in(0,1)
order by mzzlrc desc");
            outpatientlist = FindList<OutpatientWorkloadEntiy>(sqlStr.ToString(), new[] {
                new SqlParameter("@orgId", OrganizeId),
                new SqlParameter("@ksrq", ksrq),
                new SqlParameter("@jsrq", jsrq) });
            return outpatientlist;
        }

        /// <summary>
        /// 门诊工作量_医生工作量
        /// </summary>
        public List<OutpatientWorkloadEntiy_ysgzl> OutpatientWorkloadEntiy_Ysgzl(string ksrq, string jsrq, string OrganizeId,string ks)
        {
            List<OutpatientWorkloadEntiy_ysgzl> outpatientlist = null;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"select stf.Name,isnull(mzzlrc.rs,0) mzzlrc,isnull(cfsl.cf,0) cfsl,isnull(kycfs.ky,0) kycfs,isnull(fycfs.fy,0) fycfs,isnull(kfcfs.kf,0) kfcfs from NewtouchHIS_Base..Sys_Department dp
left join  NewtouchHIS_Base..Sys_Staff stf on dp.Code=stf.DepartmentCode and dp.OrganizeId=stf.OrganizeId 
left join (select jzys,count(jz.jzId) rs from Newtouch_CIS..xt_jz jz where  jz.jzzt=3 and jz.zlkssj is not null  and jz.zt=1  and zlkssj between @ksrq and @jsrq group by jzys) mzzlrc on  stf.gh=mzzlrc.jzys
left join(select ys,count(cfh) cf from Newtouch_CIS..xt_cf cf where   cf.zt=1 and cf.sfbz=1  and CreateTime between @ksrq and @jsrq and OrganizeId=@orgId group by ys) cfsl on  stf.gh=cfsl.ys
left join(select ys,count(cfh) ky from Newtouch_CIS..xt_cf cf where   cf.zt=1 and cf.sfbz=1  and CreateTime between @ksrq and @jsrq and cflx in ('1','2') and OrganizeId=@orgId group by ys) kycfs on  stf.gh=kycfs.ys
left join(select ys,count(distinct fy.cfh) fy from Newtouch_CIS..xt_cf cf left join   NewtouchHIS_PDS..mz_cfypczjl fy on cf.cfh=fy.cfh where fy.CreateTime between @ksrq and @jsrq and operateType=1 and cf.zt=1 and cf.sfbz=1 and OrganizeId=@orgId group by ys) fycfs on  stf.gh=fycfs.ys
left join(select ys,count(cfh) kf from Newtouch_CIS..xt_cf cf where   cf.zt=1 and cf.sfbz=1 and CreateTime between @ksrq and @jsrq and cflx='3' and OrganizeId=@orgId group by ys) kfcfs on  stf.gh=kfcfs.ys
where dp.OrganizeId=@orgId and mzzybz in(0,1) and DepartmentCode=@ks
order by mzzlrc desc");
            outpatientlist = FindList<OutpatientWorkloadEntiy_ysgzl>(sqlStr.ToString(), new[] { new SqlParameter("@orgId", OrganizeId), new SqlParameter("@ksrq", ksrq),
                new SqlParameter("@jsrq", jsrq),
                new SqlParameter("@ks", ks) });
            return outpatientlist;
        }

        /// <summary>
        /// 门诊费用分析_门诊费用分类分析
        /// </summary>
        public List<OutpatientCostEntiy> OutpatientCostEntiy_Mzfyflfx(string orgId,string ksrq, string jsrq)
        {
            List<OutpatientCostEntiy> outpatientlist = null;
            StringBuilder sqlStr = new StringBuilder();
            
            sqlStr.Append(@"select '总收入' srfl, isnull(sum(zfy.je),0) je from 
(select ks,case when jszt=1 then je else -je end je from  NewtouchHIS_Sett..mz_js js
left join NewtouchHIS_Sett..mz_jsmx mx on js.jsnm=mx.jsnm and js.OrganizeId=mx.OrganizeId
left join NewtouchHIS_Sett..mz_xm xm on mx.mxnm=xm.xmnm and xm.OrganizeId=mx.OrganizeId
where mx.mxnm is not null and mx.CreateTime between @ksrq and @jsrq and js.OrganizeId=@orgId
union all
 select ks,case when jszt=1 then je else -je end je from  NewtouchHIS_Sett..mz_js js
left join NewtouchHIS_Sett..mz_jsmx mx on js.jsnm=mx.jsnm and js.OrganizeId=mx.OrganizeId
left join NewtouchHIS_Sett..mz_cfmx xm on mx.cf_mxnm=xm.cfmxId and xm.OrganizeId=mx.OrganizeId
left join NewtouchHIS_Sett..mz_cf cf on cf.cfnm=xm.cfnm and cf.OrganizeId=xm.OrganizeId
where mx.cf_mxnm is not null and mx.CreateTime between @ksrq and @jsrq and js.OrganizeId=@orgId)as zfy

union all
select '药品费' srfl, isnull(sum(yp.je),0) je from
(select ks,case when jszt=1 then je else -je end je from  NewtouchHIS_Sett..mz_js js
left join NewtouchHIS_Sett..mz_jsmx mx on js.jsnm=mx.jsnm and js.OrganizeId=mx.OrganizeId
left join NewtouchHIS_Sett..mz_cfmx xm on mx.cf_mxnm=xm.cfmxId and xm.OrganizeId=mx.OrganizeId 
left join NewtouchHIS_Sett..mz_cf cf on cf.cfnm=xm.cfnm and cf.OrganizeId=xm.OrganizeId
where mx.cf_mxnm is not null and mx.CreateTime between @ksrq and @jsrq and js.OrganizeId=@orgId and xm.dl in ('01','0002000066','03'))as yp

union all
select '耗材费' srfl, isnull(sum(xm.je),0) je from
(select ks,case when jszt=1 then je else -je end je from  NewtouchHIS_Sett..mz_js js
left join NewtouchHIS_Sett..mz_jsmx mx on js.jsnm=mx.jsnm and js.OrganizeId=mx.OrganizeId
left join NewtouchHIS_Sett..mz_xm xm on mx.mxnm=xm.xmnm and xm.OrganizeId=mx.OrganizeId
where mx.mxnm is not null and mx.CreateTime between @ksrq and @jsrq and js.OrganizeId=@orgId and dl in ('18','116','00000064'))as xm

union all
select '检验费' srfl, isnull(sum(xm.je),0) je from
(select ks,case when jszt=1 then je else -je end je from  NewtouchHIS_Sett..mz_js js
left join NewtouchHIS_Sett..mz_jsmx mx on js.jsnm=mx.jsnm and js.OrganizeId=mx.OrganizeId
left join NewtouchHIS_Sett..mz_xm xm on mx.mxnm=xm.xmnm and xm.OrganizeId=mx.OrganizeId
where mx.mxnm is not null and mx.CreateTime between @ksrq and @jsrq and js.OrganizeId=@orgId and dl='104')as xm

union all
select '检查费' srfl, isnull(sum(xm.je),0) je from
(select ks,case when jszt=1 then je else -je end je from  NewtouchHIS_Sett..mz_js js
left join NewtouchHIS_Sett..mz_jsmx mx on js.jsnm=mx.jsnm and js.OrganizeId=mx.OrganizeId
left join NewtouchHIS_Sett..mz_xm xm on mx.mxnm=xm.xmnm and xm.OrganizeId=mx.OrganizeId
where mx.mxnm is not null and mx.CreateTime between @ksrq and @jsrq and js.OrganizeId=@orgId and  dl in ('04','00000054','00000046'))as xm

union all
select '治疗费' srfl, isnull(sum(xm.je),0) je from
(select ks,case when jszt=1 then je else -je end je from  NewtouchHIS_Sett..mz_js js
left join NewtouchHIS_Sett..mz_jsmx mx on js.jsnm=mx.jsnm and js.OrganizeId=mx.OrganizeId
left join NewtouchHIS_Sett..mz_xm xm on mx.mxnm=xm.xmnm and xm.OrganizeId=mx.OrganizeId
where mx.mxnm is not null and mx.CreateTime between @ksrq and @jsrq and js.OrganizeId=@orgId and dl in ('08','11','00000045','20','00000056','00000055','00000053','00000052'))as xm


union all
select '其他' srfl, isnull(sum(xm.je),0) je from
(select ks,case when jszt=1 then je else -je end je from  NewtouchHIS_Sett..mz_js js
left join NewtouchHIS_Sett..mz_jsmx mx on js.jsnm=mx.jsnm and js.OrganizeId=mx.OrganizeId
left join NewtouchHIS_Sett..mz_xm xm on mx.mxnm=xm.xmnm and xm.OrganizeId=mx.OrganizeId
where mx.mxnm is not null and mx.CreateTime between @ksrq and @jsrq and js.OrganizeId=@orgId and dl   in ('19','00000049','00000048','00000047', '121','99') )as xm");

            var inParameters = new List<SqlParameter>();
            inParameters.Add(new SqlParameter("@ksrq", ksrq));
            inParameters.Add(new SqlParameter("@jsrq", jsrq));
            inParameters.Add(new SqlParameter("@orgId", orgId));
            outpatientlist = FindList<OutpatientCostEntiy>(sqlStr.ToString(), inParameters.ToArray());
            return outpatientlist;
        }

        /// <summary>
        /// 门诊费用分析_门诊费用分类分析_图表（患者人均费用分析图表也用这个字段都是一样的）
        /// </summary>
        public List<OutpatientCostEntiy_Mzfyflfx_tb> OutpatientCostEntiy_Mzfyflfx_tb(string orgId, string ksrq, string jsrq, string rtype)
        {
            var inParameters = new List<SqlParameter>();
            inParameters.Add(new SqlParameter("@kssj", ksrq));
            inParameters.Add(new SqlParameter("@jssj", jsrq));
            inParameters.Add(new SqlParameter("@OrganizeId", orgId));
            inParameters.Add(new SqlParameter("@lx", rtype));
            return this.FindList<OutpatientCostEntiy_Mzfyflfx_tb>("exec [NewtouchHIS_Sett]..[yzcx_mzfyfx_mzfyflzxt] @kssj,@jssj,@lx,@OrganizeId", inParameters.ToArray());
        }

        /// <summary>
        /// 门诊费用分析_患者人均费用分析
        /// </summary>
        public List<OutpatientCostEntiy_Hzrjfyfx> OutpatientCostEntiy_Hzrjfyfx(string orgId, string ksrq, string jsrq)
        {
            List<OutpatientCostEntiy_Hzrjfyfx> outpatientlist = null;
            StringBuilder sqlStr = new StringBuilder();

            sqlStr.Append(@"select '总收入' srfl,case when isnull(sum(zfy.je),0)=0 or count(distinct patid)=0 then isnull(sum(zfy.je),0) else isnull(sum(zfy.je),0)/count(distinct patid) end je from 
(select ks,case when jszt=1 then je else -je end je,js.patid from  NewtouchHIS_Sett..mz_js js
left join NewtouchHIS_Sett..mz_jsmx mx on js.jsnm=mx.jsnm and js.OrganizeId=mx.OrganizeId
left join NewtouchHIS_Sett..mz_xm xm on mx.mxnm=xm.xmnm and xm.OrganizeId=mx.OrganizeId
where mx.mxnm is not null and mx.CreateTime between @ksrq and @jsrq and js.OrganizeId=@orgId
union all
 select ks,case when jszt=1 then je else -je end je,js.patid  from  NewtouchHIS_Sett..mz_js js
left join NewtouchHIS_Sett..mz_jsmx mx on js.jsnm=mx.jsnm and js.OrganizeId=mx.OrganizeId
left join NewtouchHIS_Sett..mz_cfmx xm on mx.cf_mxnm=xm.cfmxId and xm.OrganizeId=mx.OrganizeId
left join NewtouchHIS_Sett..mz_cf cf on cf.cfnm=xm.cfnm and cf.OrganizeId=xm.OrganizeId
where mx.cf_mxnm is not null and mx.CreateTime between @ksrq and @jsrq and js.OrganizeId=@orgId)as zfy

union all
select '药品费' srfl, case when isnull(sum(yp.je),0)=0 or count(distinct patid)=0 then isnull(sum(yp.je),0) else isnull(sum(yp.je),0)/count(distinct patid) end je from
(select ks,case when jszt=1 then je else -je end je,js.patid  from  NewtouchHIS_Sett..mz_js js
left join NewtouchHIS_Sett..mz_jsmx mx on js.jsnm=mx.jsnm and js.OrganizeId=mx.OrganizeId
left join NewtouchHIS_Sett..mz_cfmx xm on mx.cf_mxnm=xm.cfmxId and xm.OrganizeId=mx.OrganizeId 
left join NewtouchHIS_Sett..mz_cf cf on cf.cfnm=xm.cfnm and cf.OrganizeId=xm.OrganizeId
where mx.cf_mxnm is not null and mx.CreateTime between @ksrq and @jsrq and js.OrganizeId=@orgId and xm.dl in ('01','0002000066','03'))as yp

union all
select '耗材费' srfl, case when isnull(sum(xm.je),0)=0 or count(distinct patid)=0 then isnull(sum(xm.je),0) else isnull(sum(xm.je),0)/count(distinct patid) end je from
(select ks,case when jszt=1 then je else -je end je,js.patid  from  NewtouchHIS_Sett..mz_js js
left join NewtouchHIS_Sett..mz_jsmx mx on js.jsnm=mx.jsnm and js.OrganizeId=mx.OrganizeId
left join NewtouchHIS_Sett..mz_xm xm on mx.mxnm=xm.xmnm and xm.OrganizeId=mx.OrganizeId
where mx.mxnm is not null and mx.CreateTime between @ksrq and @jsrq and js.OrganizeId=@orgId and dl in ('18','116','00000064'))as xm

union all
select '检验费' srfl, case when isnull(sum(xm.je),0)=0 or count(distinct patid)=0 then isnull(sum(xm.je),0) else isnull(sum(xm.je),0)/count(distinct patid) end je from
(select ks,case when jszt=1 then je else -je end je,js.patid  from  NewtouchHIS_Sett..mz_js js
left join NewtouchHIS_Sett..mz_jsmx mx on js.jsnm=mx.jsnm and js.OrganizeId=mx.OrganizeId
left join NewtouchHIS_Sett..mz_xm xm on mx.mxnm=xm.xmnm and xm.OrganizeId=mx.OrganizeId
where mx.mxnm is not null and mx.CreateTime between @ksrq and @jsrq and js.OrganizeId=@orgId and dl='104')as xm

union all
select '检查费' srfl, case when isnull(sum(xm.je),0)=0 or count(distinct patid)=0 then isnull(sum(xm.je),0) else isnull(sum(xm.je),0)/count(distinct patid) end je from
(select ks,case when jszt=1 then je else -je end je,js.patid  from  NewtouchHIS_Sett..mz_js js
left join NewtouchHIS_Sett..mz_jsmx mx on js.jsnm=mx.jsnm and js.OrganizeId=mx.OrganizeId
left join NewtouchHIS_Sett..mz_xm xm on mx.mxnm=xm.xmnm and xm.OrganizeId=mx.OrganizeId
where mx.mxnm is not null and mx.CreateTime between @ksrq and @jsrq and js.OrganizeId=@orgId and dl in ('04','00000054','00000046'))as xm

union all
select '治疗费' srfl,case when isnull(sum(xm.je),0)=0 or count(distinct patid)=0 then isnull(sum(xm.je),0) else isnull(sum(xm.je),0)/count(distinct patid) end je from
(select ks,case when jszt=1 then je else -je end je,js.patid  from  NewtouchHIS_Sett..mz_js js
left join NewtouchHIS_Sett..mz_jsmx mx on js.jsnm=mx.jsnm and js.OrganizeId=mx.OrganizeId
left join NewtouchHIS_Sett..mz_xm xm on mx.mxnm=xm.xmnm and xm.OrganizeId=mx.OrganizeId
where mx.mxnm is not null and mx.CreateTime between @ksrq and @jsrq and js.OrganizeId=@orgId and dl in ('08','11','00000045','20','00000056','00000055','00000053','00000052') )as xm

union all
select '其他' srfl, case when isnull(sum(xm.je),0)=0 or count(distinct patid)=0 then isnull(sum(xm.je),0) else isnull(sum(xm.je),0)/count(distinct patid) end je from
(select ks,case when jszt=1 then je else -je end je,js.patid  from  NewtouchHIS_Sett..mz_js js
left join NewtouchHIS_Sett..mz_jsmx mx on js.jsnm=mx.jsnm and js.OrganizeId=mx.OrganizeId
left join NewtouchHIS_Sett..mz_xm xm on mx.mxnm=xm.xmnm and xm.OrganizeId=mx.OrganizeId
where mx.mxnm is not null and mx.CreateTime between @ksrq and @jsrq and js.OrganizeId=@orgId and dl   in ('19','00000049','00000048','00000047', '121','99'))as xm");

            var inParameters = new List<SqlParameter>();
            inParameters.Add(new SqlParameter("@ksrq", ksrq));
            inParameters.Add(new SqlParameter("@jsrq", jsrq));
            inParameters.Add(new SqlParameter("@orgId", orgId));
            outpatientlist = FindList<OutpatientCostEntiy_Hzrjfyfx>(sqlStr.ToString(), inParameters.ToArray());
            return outpatientlist;
        }

        /// <summary>
        /// 门诊费用分析_患者人均费用分析_图标
        /// </summary>
        public List<OutpatientCostEntiy_Mzfyflfx_tb> OutpatientCostEntiy_Hzrjfyfx_tb(string orgId, string ksrq, string jsrq, string rtype)
        {
            var inParameters = new List<SqlParameter>();
            inParameters.Add(new SqlParameter("@kssj", ksrq));
            inParameters.Add(new SqlParameter("@jssj", jsrq));
            inParameters.Add(new SqlParameter("@OrganizeId", orgId));
            inParameters.Add(new SqlParameter("@lx", rtype));
            return this.FindList<OutpatientCostEntiy_Mzfyflfx_tb>("exec [NewtouchHIS_Sett]..[yzcx_mzfyfx_hzrjfyfxzxt] @kssj,@jssj,@lx, @OrganizeId", inParameters.ToArray());
        }

        /// <summary>
        /// 门诊效益_门诊效益明细
        /// </summary>
        public List<OutpatientBenefitsEntiy> OutpatientCostEntiy_Mzxymx(string orgId, string ksrq, string jsrq)
        {
            List<OutpatientBenefitsEntiy> outpatientlist = null;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"select dp.name mzks,dp.Code DeptmentCode, isnull(mzzlrc.rs,0) mzzlrc,isnull(mzsr.sr,0) mzsr,isnull(rjfy.fy,0)  rjfy,isnull(zffyzb.zfzb,0) zffyzb ,ISNULL(ybfyzb.ybzb,0) ybfyzb,isnull(pjzlsc.zlsc,0) pczlsc,isnull(fzl.fzl,0)  fzl
from NewtouchHIS_Base..Sys_Department dp 
left join (select jzks,count(jz.jzId) rs from Newtouch_CIS..xt_jz jz where  jz.jzzt=3 and jz.zlkssj is not null  and jz.zt=1  and zlkssj between @ksrq and @jsrq group by jzks) mzzlrc on  dp.Code=mzzlrc.jzks
left join(select jz.jzks,sum(zb.zje) sr from NewtouchHIS_Sett..mz_js zb
  left join (select gh.ghnm,jz.jzks from NewtouchHIS_Sett..mz_gh gh left join Newtouch_CIS..xt_jz jz on gh.mzh=jz.mzh and jz.OrganizeId=gh.OrganizeId where gh.OrganizeId=@OrganizeId) jz on zb.ghnm=jz.ghnm 
  where zb.CreateTime between @ksrq and @jsrq and jszt=1 and jsnm not in (select cxjsnm from NewtouchHIS_Sett..mz_js where CreateTime between @ksrq and @jsrq and jszt=2)  and zb.OrganizeId=@OrganizeId
  group by jz.jzks) mzsr on dp.Code=mzsr.jzks
left join (select jz.jzks,convert(numeric(18,2),sum(zb.zje)/count(distinct zb.ghnm))  fy from NewtouchHIS_Sett..mz_js zb
  left join (select gh.ghnm,jz.jzks from NewtouchHIS_Sett..mz_gh gh left join Newtouch_CIS..xt_jz jz on gh.mzh=jz.mzh and jz.OrganizeId=gh.OrganizeId where gh.OrganizeId=@OrganizeId) jz on zb.ghnm=jz.ghnm 
  where zb.CreateTime between @ksrq and @jsrq and jszt=1 and jsnm not in (select cxjsnm from NewtouchHIS_Sett..mz_js where CreateTime between @ksrq and @jsrq and jszt=2)  and zb.OrganizeId=@OrganizeId
  group by jz.jzks) rjfy on dp.Code=rjfy.jzks
left join (select jz.jzks,case when sum(ybjs.psn_part_amt) is null then 100 when sum(zb.zje) is null then 0 else convert(numeric(18,2),sum(ybjs.psn_part_amt)/sum(zb.zje)*100) end zfzb from NewtouchHIS_Sett..mz_js zb
  left join (select gh.ghnm,jz.jzks from NewtouchHIS_Sett..mz_gh gh left join Newtouch_CIS..xt_jz jz on gh.mzh=jz.mzh and jz.OrganizeId=gh.OrganizeId where gh.OrganizeId=@OrganizeId) jz on zb.ghnm=jz.ghnm 
  left join cqyb_OutPut05 yb05 on yb05.jsnm=zb.jsnm and yb05.zt='1'
  left join drjk_mzjs_output (NOLOCK) ybjs on ybjs.setl_id=yb05.jylsh and ybjs.zt='1'  
  where zb.CreateTime between @ksrq and @jsrq and jszt=1 and zb.jsnm not in (select cxjsnm from NewtouchHIS_Sett..mz_js where CreateTime between @ksrq and @jsrq and jszt=2)  and zb.OrganizeId=@OrganizeId
  group by jz.jzks
) zffyzb on dp.Code=zffyzb.jzks
left join(select jz.jzks,case when sum(ybjs.psn_part_amt) is null or sum(zb.zje) is null or  convert(numeric(18,2),sum(zb.zje)-sum(ybjs.psn_part_amt))=0.00 then 0  else  convert(numeric(18,2),(sum(zb.zje)-sum(ybjs.psn_part_amt))/sum(zb.zje)*100) end ybzb from NewtouchHIS_Sett..mz_js zb
left join (select gh.ghnm,jz.jzks from NewtouchHIS_Sett..mz_gh gh left join Newtouch_CIS..xt_jz jz on gh.mzh=jz.mzh and jz.OrganizeId=gh.OrganizeId where gh.OrganizeId=@OrganizeId) jz on zb.ghnm=jz.ghnm 
left join cqyb_OutPut05 yb05 on yb05.jsnm=zb.jsnm and yb05.zt='1'
 left join drjk_mzjs_output (NOLOCK) ybjs on ybjs.setl_id=yb05.jylsh and ybjs.zt='1'  
where zb.CreateTime between @ksrq and @jsrq and jszt=1 and zb.jsnm not in (select cxjsnm from NewtouchHIS_Sett..mz_js where CreateTime between @ksrq and @jsrq and jszt=2)  and zb.OrganizeId=@OrganizeId
group by jz.jzks) ybfyzb on dp.Code=ybfyzb.jzks
left join(select jz.jzks,(DATEDIFF([MINUTE],zlkssj,zljssj ))/count(jzId) zlsc from Newtouch_CIS..xt_jz jz
where jzzt=3 and zlkssj is not null and jz.zlkssj between @ksrq and @jsrq and jz.OrganizeId=@OrganizeId and jz.zljssj is not null
group by jz.jzks,zlkssj,zljssj) pjzlsc on dp.Code=pjzlsc.jzks
left join (select jz.jzks,case when count(jzId)=0  then 0 else convert(numeric(18,2),convert(numeric(18,2),fz.rs) /convert(numeric(18,2),count(jzId))*100) end fzl  from Newtouch_CIS..xt_jz jz
left join (select jzks,count(jzId) rs from Newtouch_CIS..xt_jz where jzzt=3 and zlkssj is not null and cfzbz=1 and zlkssj between @ksrq and @jsrq and OrganizeId=@OrganizeId group by jzks) fz on jz.jzks=fz.jzks
where jzzt=3 and zlkssj is not null and jz.zlkssj between @ksrq and @jsrq and jz.OrganizeId=@OrganizeId
group by jz.jzks,fz.rs) fzl on dp.Code=fzl.jzks
where dp.OrganizeId=@OrganizeId and mzzybz=1
order by mzzlrc desc");
            var inParameters = new List<SqlParameter>();
            inParameters.Add(new SqlParameter("@ksrq", ksrq));
            inParameters.Add(new SqlParameter("@jsrq", jsrq));
            inParameters.Add(new SqlParameter("@OrganizeId", orgId));
            outpatientlist = FindList<OutpatientBenefitsEntiy>(sqlStr.ToString(), inParameters.ToArray());
            return outpatientlist;
        }

        /// <summary>
        /// 门诊效益_医生效益
        /// </summary>
        public List<OutpatientBenefitsEntiy_Ysxy> OutpatientCostEntiy_Ysxy(string orgId, string ksrq, string jsrq,string dpCode)
        {
            List<OutpatientBenefitsEntiy_Ysxy> outpatientlist = null;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"select stf.name mzks,isnull(mzzlrc.rs,0) mzzlrc,isnull(mzsr.sr,0) mzsr,isnull(rjfy.fy,0)  rjfy,isnull(zffyzb.zfzb,0) zffyzb ,ISNULL(ybfyzb.ybzb,0) ybfyzb,isnull(pjzlsc.zlsc,0) pczlsc,isnull(fzl.fzl,0)  fzl
from NewtouchHIS_Base..Sys_Department dp 
left join  NewtouchHIS_Base..Sys_Staff stf on dp.Code=stf.DepartmentCode and dp.OrganizeId=stf.OrganizeId 
left join (select jzys,count(jz.jzId) rs from Newtouch_CIS..xt_jz jz where  jz.jzzt=3 and jz.zlkssj is not null  and jz.zt=1  and zlkssj between @ksrq and @jsrq group by jzys) mzzlrc on  stf.gh=mzzlrc.jzys
left join(select jz.jzys,sum(zb.zje) sr from NewtouchHIS_Sett..mz_js zb
  left join (select gh.ghnm,jz.jzys from NewtouchHIS_Sett..mz_gh gh left join Newtouch_CIS..xt_jz jz on gh.mzh=jz.mzh and jz.OrganizeId=gh.OrganizeId where gh.OrganizeId=@OrganizeId) jz on zb.ghnm=jz.ghnm 
  where zb.CreateTime between @ksrq and @jsrq and jszt=1 and jsnm not in (select cxjsnm from NewtouchHIS_Sett..mz_js where CreateTime between @ksrq and @jsrq and jszt=2)  and zb.OrganizeId=@OrganizeId
  group by jz.jzys) mzsr on stf.gh=mzsr.jzys
left join (select jz.jzys,convert(numeric(18,2),sum(zb.zje)/count(distinct zb.ghnm))  fy from NewtouchHIS_Sett..mz_js zb
  left join (select gh.ghnm,jz.jzys from NewtouchHIS_Sett..mz_gh gh left join Newtouch_CIS..xt_jz jz on gh.mzh=jz.mzh and jz.OrganizeId=gh.OrganizeId where gh.OrganizeId=@OrganizeId) jz on zb.ghnm=jz.ghnm 
  where zb.CreateTime between @ksrq and @jsrq and jszt=1 and jsnm not in (select cxjsnm from NewtouchHIS_Sett..mz_js where CreateTime between @ksrq and @jsrq and jszt=2)  and zb.OrganizeId=@OrganizeId
  group by jz.jzys) rjfy on stf.gh=rjfy.jzys
left join (select jz.jzys,case when sum(ybjs.psn_part_amt) is null then 100 when sum(zb.zje) is null then 0 else convert(numeric(18,2),sum(ybjs.psn_part_amt)/sum(zb.zje)*100) end zfzb from NewtouchHIS_Sett..mz_js zb
  left join (select gh.ghnm,jz.jzys from NewtouchHIS_Sett..mz_gh gh left join Newtouch_CIS..xt_jz jz on gh.mzh=jz.mzh and jz.OrganizeId=gh.OrganizeId where gh.OrganizeId=@OrganizeId) jz on zb.ghnm=jz.ghnm 
  left join cqyb_OutPut05 yb05 on yb05.jsnm=zb.jsnm and yb05.zt='1'
  left join drjk_mzjs_output (NOLOCK) ybjs on ybjs.setl_id=yb05.jylsh and ybjs.zt='1'  
  where zb.CreateTime between @ksrq and @jsrq and jszt=1 and zb.jsnm not in (select cxjsnm from NewtouchHIS_Sett..mz_js where CreateTime between @ksrq and @jsrq and jszt=2)  and zb.OrganizeId=@OrganizeId
  group by jz.jzys
) zffyzb on stf.gh=zffyzb.jzys
left join(select jz.jzys,case when sum(ybjs.psn_part_amt) is null or sum(zb.zje) is null or  convert(numeric(18,2),sum(zb.zje)-sum(ybjs.psn_part_amt))=0.00 then 0  else  convert(numeric(18,2),(sum(zb.zje)-sum(ybjs.psn_part_amt))/sum(zb.zje)*100) end ybzb from NewtouchHIS_Sett..mz_js zb
left join (select gh.ghnm,jz.jzys from NewtouchHIS_Sett..mz_gh gh left join Newtouch_CIS..xt_jz jz on gh.mzh=jz.mzh and jz.OrganizeId=gh.OrganizeId where gh.OrganizeId=@OrganizeId) jz on zb.ghnm=jz.ghnm 
left join cqyb_OutPut05 yb05 on yb05.jsnm=zb.jsnm and yb05.zt='1'
 left join drjk_mzjs_output (NOLOCK) ybjs on ybjs.setl_id=yb05.jylsh and ybjs.zt='1'  
where zb.CreateTime between @ksrq and @jsrq and jszt=1 and zb.jsnm not in (select cxjsnm from NewtouchHIS_Sett..mz_js where CreateTime between @ksrq and @jsrq and jszt=2)  and zb.OrganizeId=@OrganizeId
group by jz.jzys) ybfyzb on stf.gh=ybfyzb.jzys
left join(select jz.jzys,(DATEDIFF([MINUTE],zlkssj,zljssj ))/count(jzId) zlsc from Newtouch_CIS..xt_jz jz
where jzzt=3 and zlkssj is not null and jz.zlkssj between @ksrq and @jsrq and jz.OrganizeId=@OrganizeId and jz.zljssj is not null
group by jz.jzys,zlkssj,zljssj) pjzlsc on stf.gh=pjzlsc.jzys
left join (select jz.jzys,case when count(jzId)=0  then 0 else convert(numeric(18,2),convert(numeric(18,2),fz.rs) /convert(numeric(18,2),count(jzId))*100) end fzl  from Newtouch_CIS..xt_jz jz
left join (select jzys,count(jzId) rs from Newtouch_CIS..xt_jz where jzzt=3 and zlkssj is not null and cfzbz=1 and zlkssj between @ksrq and @jsrq and OrganizeId=@OrganizeId group by jzys) fz on jz.jzys=fz.jzys
where jzzt=3 and zlkssj is not null and jz.zlkssj between @ksrq and @jsrq and jz.OrganizeId=@OrganizeId
group by jz.jzys,fz.rs) fzl on stf.gh=fzl.jzys
where dp.OrganizeId=@OrganizeId and mzzybz=1 and DepartmentCode=@DepartmentCode
order by mzzlrc desc");
            var inParameters = new List<SqlParameter>();
            inParameters.Add(new SqlParameter("@ksrq", ksrq));
            inParameters.Add(new SqlParameter("@jsrq", jsrq));
            inParameters.Add(new SqlParameter("@OrganizeId", orgId));
            inParameters.Add(new SqlParameter("@DepartmentCode", dpCode));
            outpatientlist = FindList<OutpatientBenefitsEntiy_Ysxy>(sqlStr.ToString(), inParameters.ToArray());
            return outpatientlist;
        }


        /// <summary>
        /// 门诊效益_历史趋势
        /// </summary>
        public List<KSXYPMBySJSRDTO> OutpatientCostEntiy_Lsqs(string orgId, string ksrq, string jsrq, string rType, string type)
        {
            var inParameters = new List<SqlParameter>();
            inParameters.Add(new SqlParameter("@kssj", ksrq));
            inParameters.Add(new SqlParameter("@jssj", jsrq));
            inParameters.Add(new SqlParameter("@OrganizeId", orgId));
            inParameters.Add(new SqlParameter("@lx", rType));
            inParameters.Add(new SqlParameter("@type", type));
            return this.FindList<KSXYPMBySJSRDTO>("exec [NewtouchHIS_Sett]..[yzcx_mzfyfx_lxqszxt] @kssj,@jssj,@lx,@type,@OrganizeId", inParameters.ToArray());
        }
        /// <summary>
        /// 科室效益排名-诊疗人次
        /// </summary>
        /// <returns></returns>
        public List<KSXYPMDTO> GetKSXYPM(string orgId, string ksrq, string jsrq)
        {
            List<KSXYPMDTO> outpatientlist = null;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"select dp.Name,count(jzId) keyword from Newtouch_CIS..xt_jz jz
left join NewtouchHIS_Base..Sys_Department dp on jz.jzks=dp.Code and jz.OrganizeId=dp.OrganizeId
where jzzt=3 and zlkssj is not null and jz.OrganizeId=@OrganizeId  and jz.zlkssj between @ksrq and @jsrq and jz.zt=1
group by dp.Name");
            var param = new DbParameter[] {
                new SqlParameter("@OrganizeId",orgId),
                new SqlParameter("@ksrq",ksrq),
                new SqlParameter("@jsrq",jsrq)
            };
            outpatientlist = FindList<KSXYPMDTO>(sqlStr.ToString(), param);
            return outpatientlist;
        }
        /// <summary>
        /// 科室效益排名-实际收入
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ksrq"></param>
        /// <param name="jsrq"></param>
        /// <param name="rtype"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<KSXYPMBySJSRDTO> GetKSXYPMBySJSY(string orgId, string ksrq, string jsrq)
        {
            List<KSXYPMBySJSRDTO> outpatientlist = null;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"select dp.name,sum(zb.zje) keyword from NewtouchHIS_Sett..mz_js zb
left join (select gh.ghnm,jz.jzks from NewtouchHIS_Sett..mz_gh gh left join Newtouch_CIS..xt_jz jz on gh.mzh=jz.mzh and jz.OrganizeId=gh.OrganizeId where gh.OrganizeId=@OrganizeId) jz on zb.ghnm=jz.ghnm 
left join NewtouchHIS_Base..Sys_Department dp on jz.jzks=dp.Code and dp.OrganizeId=zb.OrganizeId
where zb.CreateTime between @ksrq and @jsrq and jszt=1 and jsnm not in (select cxjsnm from NewtouchHIS_Sett..mz_js where CreateTime between @ksrq and @jsrq and jszt=2)  and zb.OrganizeId=@OrganizeId
group by dp.name");
            var param = new DbParameter[] {
                new SqlParameter("@OrganizeId",orgId),
                new SqlParameter("@ksrq",ksrq),
                new SqlParameter("@jsrq",jsrq)
            };
            outpatientlist = FindList<KSXYPMBySJSRDTO>(sqlStr.ToString(), param);
            return outpatientlist;
        }

        /// <summary>
        /// 药品top 50 排行
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="OrganizeId"></param>
        /// <param name="yptype"></param>
        /// <returns></returns>
        public List<DrugTrackingEntity> DrugTrackingEntity_Lsqs(string kssj,string jssj,string OrganizeId,string yptype)
        {
            List<DrugTrackingEntity> outpatientlist = null;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"
               select top 50 yp.ypCode ypdm, yp.ypmc,yp.ypgg,yp.zxdw,(sl*dj)je,sl from (
			           select ypCode,sum(sl) sl,dj from (
                        select czjl.ypCode,sum(case when operateType=2 then (0-czjl.sl)  else czjl.sl end)sl,dj from NewtouchHIS_PDS..zy_ypyzczjl czjl
				        left join NewtouchHIS_PDS..zy_ypyzxx yzxx on czjl.yzId=yzxx.yzId
				        where   czjl.CreateTime between convert(datetime,@kssj,120) and convert(datetime,@jssj,120)  and  (yzxx.fybz=2 or yzxx.fybz=3) and yzxx.OrganizeId=@OrganizeId group by czjl.ypCode,dj  --住院发药退药  
                        union all
                        select czjl.ypCode,sum(case when operateType=2 then (0-czjl.sl) else czjl.sl end )sl,dj from NewtouchHIS_PDS..mz_cfypczjl czjl
				        left join NewtouchHIS_PDS..mz_cfmx cfmx on  czjl.mzcfmxId=cfmx.Id where czjl.CreateTime between convert(datetime,@kssj,120) and  convert(datetime,@jssj,120)  and cfmx.OrganizeId=@OrganizeId
				          group by czjl.ypCode,dj    --门诊发药退药  
                        ) a  where sl>0  group by ypCode,dj) ypsl left join NewtouchHIS_Base..V_C_xt_yp yp on  ypsl.ypCode=yp.ypCode  and OrganizeId=OrganizeId 
                        left join NewtouchHIS_Base..xt_sfdl sfdl on yp.dlCode=  sfdl.dlCode
                ");
            if (yptype == "全部")
            {
                sqlStr.Append(@"  group by yp.ypmc,yp.ypgg,yp.zxdw, sl ,dj ,yp.ypCode order by sl desc ");
            }
            else
            {
                sqlStr.Append(@"   where dlmc =@yptype group by yp.ypmc,yp.ypgg,yp.zxdw, sl ,dj,yp.ypCode order by sl desc ");
            }

            var param = new DbParameter[] {
                new SqlParameter("@OrganizeId",OrganizeId),
                new SqlParameter("@kssj",kssj),
                new SqlParameter("@jssj",jssj),
                new SqlParameter("@yptype",yptype),
            };
            outpatientlist = FindList<DrugTrackingEntity>(sqlStr.ToString(), param);
            return outpatientlist;
        }

        /// <summary>
        /// 损益排行
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public List<Profitandlossranking> Profitandlossranking_Lsqs(string kssj, string jssj, string OrganizeId)
        {
            List<Profitandlossranking> outpatientlist = null;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"
             select ypsy.syyy,sum(sl*pfj) pfjze,sum(sl*lsj) lsjze,count(1) sl from (
                select ypsyxx.Syyy,sum(Sysl)sl,Ypdm from  NewtouchHIS_PDS..xt_yp_syxx syxx left join  NewtouchHIS_PDS..xt_ypsyyy ypsyxx ON syxx.syyy = ypsyxx.syyyId 
                where syxx.CreateTime between convert(datetime,@kssj,120) and convert(datetime,@jssj,120) and OrganizeId=@OrganizeId group by ypsyxx.Syyy,Ypdm
                ) ypsy left join  NewtouchHIS_Base..V_C_xt_yp yp on  ypsy.Ypdm=yp.ypCode   where syyy is not null
                group  by syyy order by pfjze desc 
                ");
            var param = new DbParameter[] {
                new SqlParameter("@OrganizeId",OrganizeId),
                new SqlParameter("@kssj",kssj),
                new SqlParameter("@jssj",jssj),
            };
            outpatientlist = FindList<Profitandlossranking>(sqlStr.ToString(), param);
            return outpatientlist;
        }

        /// <summary>
        /// 医生开单排行
        /// </summary>
        /// <param name="ypcode"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public List<DoctorBillingRanking> DoctorBillingRanking_Lsqs(string kssj, string jssj, string ypcode,string OrganizeId)
        {
            List<DoctorBillingRanking> outpatientlist = null;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"
					select Name,SUM(zje)zje,SUM(sl)sl from (
					select sum(case when operateType=2 then (0-czjl.sl) else czjl.sl end ) sl,(sum(case when operateType=2 then (0-czjl.sl) else czjl.sl end )*dj)zje ,Name
					from  NewtouchHIS_PDS.dbo.zy_ypyzxx yzxx 
					left join NewtouchHIS_PDS..zy_ypyzczjl  czjl on yzxx.yzId=czjl.yzId  
					 left join NewtouchHIS_Base..Sys_Staff staff on yzxx.ysgh=staff.gh and yzxx.OrganizeId=staff.OrganizeId
					where (yzxx.fybz=2 or yzxx.fybz=3) and   czjl.ypCode=@ypcode and yzxx.OrganizeId=@OrganizeId and  czjl.CreateTime between convert(datetime,@kssj,120) and convert(datetime,@jssj,120)
					group by Name,dj
					union all
					 select sum(case when operateType=2 then (0-czjl.sl) else czjl.sl end )  sl,(sum(case when operateType=2 then (0-czjl.sl) else czjl.sl end )*dj)zje ,Name from  NewtouchHIS_PDS..mz_cfypczjl czjl left join 
					 NewtouchHIS_PDS..mz_cfmx cfmx on czjl.mzcfmxId=cfmx.Id
				  	 left join NewtouchHIS_Base..Sys_Staff staff on cfmx.CreatorCode=staff.gh and cfmx.OrganizeId=staff.OrganizeId
					 where czjl.ypCode=@ypcode and cfmx.OrganizeId=@OrganizeId  and  czjl.CreateTime between convert(datetime,@kssj,120) and convert(datetime,@jssj,120)
					 group by Name,dj) xm where xm.zje>0
					 group by Name order by sl desc
                ");
            var param = new DbParameter[] {
                new SqlParameter("@OrganizeId",OrganizeId),
                new SqlParameter("@ypcode",ypcode),
                new SqlParameter("@kssj",kssj),
                new SqlParameter("@jssj",jssj),
            };
            outpatientlist = FindList<DoctorBillingRanking>(sqlStr.ToString(), param);
            return outpatientlist;
        }

        /// <summary>
        /// 耗材销量排名TOP50
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public List<ConsumablestatisticsEntity> Consumablestatistics_Lsqs(string kssj, string jssj, string OrganizeId)
        {
            List<ConsumablestatisticsEntity> outpatientlist = null;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"
            select top 50 sfxmmc,sfxmCode,gg,dw,sum(sl)sl,sum(zje) zje from (
                select b.sfxmmc,b.sfxmCode,b.gg,b.dw,sl,sum(je) zje from V_C_Sys_WsfZyXmjfb a 
                left join NewtouchHIS_Base..xt_sfxm b on a.sfxm=b.sfxmCode and a.OrganizeId=b.OrganizeId 
                left join NewtouchHIS_Base..xt_sfdl c on b.sfdlCode=c.dlCode and a.OrganizeId=c.OrganizeId
                where  c.dlmc='材料费'and a.zt=1 and a.CreateTime between convert(datetime,@kssj,120) and convert(datetime,@jssj,120) and a.OrganizeId=@OrganizeId group by  b.sfxmmc,b.sfxmCode,sl,b.gg,b.dw
                union all 
                select b.sfxmmc,b.sfxmCode,b.gg,b.dw,sum(sl) sl,sum(je) zje  from mz_xm a 
                left join NewtouchHIS_Base..xt_sfxm b on a.sfxm=b.sfxmCode and a.OrganizeId=b.OrganizeId 
                left join NewtouchHIS_Base..xt_sfdl c on b.sfdlCode=c.dlCode and a.OrganizeId=c.OrganizeId
                where c.dlmc='材料费'and a.zt=1 and a.CreateTime between convert(datetime,@kssj,120) and convert(datetime,@jssj,120) and a.OrganizeId=@OrganizeId  group by  b.sfxmmc,b.sfxmCode,sl,b.gg,b.dw
                ) c group by sfxmmc,sfxmCode,gg,dw order by sl desc
                ");
            var param = new DbParameter[] {
                new SqlParameter("@OrganizeId",OrganizeId),
                new SqlParameter("@kssj",kssj),
                new SqlParameter("@jssj",jssj),
            };
            outpatientlist = FindList<ConsumablestatisticsEntity>(sqlStr.ToString(), param);
            return outpatientlist;
        }

        /// <summary>
        /// 耗材医生排名
        /// </summary>
        /// <param name="sfxmdm"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public List<DoctorBillingRanking> DoctorBillingRankingHC_Lsqs(string kssj, string jssj, string sfxmdm, string OrganizeId)
        {
            List<DoctorBillingRanking> outpatientlist = null;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"
                      select Name,sl,zje from(
                        select ys,sum(sl) sl,sum(je)zje from mz_xm where sfxm=@sfxmdm and zt=1 and OrganizeId=@OrganizeId and CreateTime between convert(datetime,@kssj,120) and convert(datetime,@jssj,120) group by ys  
                        union all 
                        select ys,sum(sl) sl,sum(je)zje from V_C_Sys_WsfZyXmjfb  where sfxm=@sfxmdm and OrganizeId=@OrganizeId and CreateTime between convert(datetime,@kssj,120) and convert(datetime,@jssj,120)  and zt=1 group by ys 
                        ) sfxm left join NewtouchHIS_Base..Sys_Staff staff on sfxm.ys=staff.gh
                         order by zje desc
                ");
            var param = new DbParameter[] {
                new SqlParameter("@OrganizeId",OrganizeId),
                new SqlParameter("@sfxmdm",sfxmdm),
                new SqlParameter("@kssj",kssj),
                new SqlParameter("@jssj",jssj),
            };
            outpatientlist = FindList<DoctorBillingRanking>(sqlStr.ToString(), param);
            return outpatientlist;
        }

        /// <summary>
        /// 耗材损益排行
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public List<Profitandlossranking> ProfitandlossrankingHC_Lsqs(string kssj, string jssj, string OrganizeId)
        {
            List<Profitandlossranking> outpatientlist = null;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"
                select '报损'syyy,   0.00 pfjze, 0.00 lsjze,0 sl  
                  union all
                  select '报溢'syyy,   0.00 pfjze, 0.00 lsjze,0 sl  
            
                ");
            outpatientlist = FindList<Profitandlossranking>(sqlStr.ToString());
            return outpatientlist;
        }

        /// <summary>
        /// 入院趋势
        /// </summary>
        /// <param name="time"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public List<NumberTrendEntity> cyqs_Lsqs(string time, string OrganizeId)
        {
            List<NumberTrendEntity> outpatientlist = null;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"
                      with t as
                            (
                            select  convert(varchar(10),dateadd(DAY,t2.number,t1.day),120) mount from 
                            (select substring(convert(varchar,@time,120),1,7)+'-01' day) t1, 
                            (select number from MASTER..spt_values WHERE TYPE='P' AND number>=0 and number<=31) t2 
                            where convert(varchar(10),dateadd(DAY,t2.number,t1.day),120) like substring(convert(varchar,@time,120),1,7)+'%'
                            )
                            select  
                                    t1.mount ,count(brjbxx.ryrq) sl
                            from t t1  left join zy_brjbxx brjbxx on mount=CONVERT(varchar(10),brjbxx.ryrq,25) and zt=1 and OrganizeId=@OrganizeId
                            group by t1.mount ;
                ");
            var param = new DbParameter[] {
                new SqlParameter("@OrganizeId",OrganizeId),
                new SqlParameter("@time",time),
            };
            outpatientlist = FindList<NumberTrendEntity>(sqlStr.ToString(), param);
            return outpatientlist;
        }


        /// <summary>
        /// 出院趋势
        /// </summary>
        /// <param name="time"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public List<NumberTrendEntity> ryqs_Lsqs(string time, string OrganizeId)
        {
            List<NumberTrendEntity> outpatientlist = null;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"
                      with t as
                            (
                            select  convert(varchar(10),dateadd(DAY,t2.number,t1.day),120) mount from 
                            (select substring(convert(varchar,@time,120),1,7)+'-01' day) t1, 
                            (select number from MASTER..spt_values WHERE TYPE='P' AND number>=0 and number<=31) t2 
                            where convert(varchar(10),dateadd(DAY,t2.number,t1.day),120) like substring(convert(varchar,@time,120),1,7)+'%'
                            )
                            select  
                                    t1.mount ,count(brjbxx.cyrq) sl
                            from t t1  left join zy_brjbxx brjbxx on mount=CONVERT(varchar(10),brjbxx.cyrq,25) and zt=1 and OrganizeId=@OrganizeId
                            group by t1.mount ;
                ");
            var param = new DbParameter[] {
                new SqlParameter("@OrganizeId",OrganizeId),
                new SqlParameter("@time",time),
            };
            outpatientlist = FindList<NumberTrendEntity>(sqlStr.ToString(), param);
            return outpatientlist;
        }


        /// <summary>
        /// 转院趋势
        /// </summary>
        /// <param name="time"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public List<NumberTrendEntity> zzqs_Lsqs(string time, string OrganizeId)
        {
            List<NumberTrendEntity> outpatientlist = null;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"
                      with t as
                            (
                            select  convert(varchar(10),dateadd(DAY,t2.number,t1.day),120) mount from 
                            (select substring(convert(varchar,@time,120),1,7)+'-01' day) t1, 
                            (select number from MASTER..spt_values WHERE TYPE='P' AND number>=0 and number<=31) t2 
                            where convert(varchar(10),dateadd(DAY,t2.number,t1.day),120) like substring(convert(varchar,@time,120),1,7)+'%'
                            )
                            select  
                                    t1.mount ,count(brjbxx.ryrq) sl
                            from t t1  left join zy_brjbxx brjbxx on mount=CONVERT(varchar(10),brjbxx.ryrq,25) and zt=1 and OrganizeId=@OrganizeId and (brjbxx.zybz=2 or brjbxx.zybz=3) 
                            group by t1.mount ;
                ");
            var param = new DbParameter[] {
                new SqlParameter("@OrganizeId",OrganizeId),
                new SqlParameter("@time",time),
            };
            outpatientlist = FindList<NumberTrendEntity>(sqlStr.ToString(), param);
            return outpatientlist;
        }

        public List<NumberPersonEntity> Rstj_Lsqs(string kssj, string jssj, string OrganizeId)
        {
            List<NumberPersonEntity> outpatientlist = null;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"
                    with t as
                    (
                    select  convert(varchar(10),dateadd(DAY,t2.number,t1.day),120) mount from 
                    (select substring(convert(varchar,convert(datetime,@kssj,120),120),1,7)+'-01' day) t1, 
                    (select number from MASTER..spt_values WHERE TYPE='P' AND number>=0 and number<=31) t2 
                    where convert(varchar(10),dateadd(DAY,t2.number,t1.day),120) like substring(convert(varchar,convert(datetime,@kssj,120),120),1,7)+'%'
                    )
                    select  
                            t1.mount  ,ISNULL(ryrs.sl,0) rysl,ISNULL(cyrs.sl,0) cysl,ISNULL(zyrs.sl,0) zysl
                    from t t1 

                    left join(select CONVERT(varchar(10),ryrq,25) mon, COUNT(1) sl 
                    from zy_brjbxx zyzt 
                    where  zyzt.zt=1 and zyzt.OrganizeId=@OrganizeId 
                    and CONVERT(varchar(10),ryrq,25)  between convert(datetime,@kssj,120) and convert(datetime,@jssj,120)
                    group by CONVERT(varchar(10),ryrq,25) ) ryrs on  t1.mount=ryrs.mon

                    left join(select CONVERT(varchar(10),cyrq,25) mon, COUNT(1) sl 
                    from zy_brjbxx zyzt 
                    where  zyzt.zt=1 and zyzt.OrganizeId=@OrganizeId  and zyzt.zybz<>9  and cyrq is not null
                    and CONVERT(varchar(10),cyrq,25)  between convert(datetime,@kssj,120) and convert(datetime,@jssj,120)
                    group by CONVERT(varchar(10),cyrq,25) ) cyrs on  t1.mount=cyrs.mon
 
                    left join(select CONVERT(varchar(10),cyrq,25) mon, COUNT(1) sl 
                    from zy_brjbxx zyzt left join Newtouch_EMR..mr_basy basy
					on zyzt.zyh=basy.ZYH and zyzt.OrganizeId=basy.OrganizeId
                    where  zyzt.zt=1 and zyzt.OrganizeId=@OrganizeId  and zyzt.zt=1 
					and  basy.RYTJ=3					 
                    and CONVERT(varchar(10),cyrq,25)  between convert(datetime,@kssj,120) and convert(datetime,@jssj,120)
                    group by  CONVERT(varchar(10),cyrq,25) ) zyrs on  t1.mount=zyrs.mon

                ");
            var param = new DbParameter[] {
                new SqlParameter("@OrganizeId",OrganizeId),
                new SqlParameter("@kssj",kssj),
                new SqlParameter("@jssj",jssj),
            };
            outpatientlist = FindList<NumberPersonEntity>(sqlStr.ToString(), param);
            return outpatientlist;
        }

        public List<DeparNumberEntity> Ksgzl_Lsqs(string kssj, string jssj, string OrganizeId)
        {
            List<DeparNumberEntity> outpatientlist = null;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"
					with t as(select * from NewtouchHIS_Base..xt_bq where zt=1 and OrganizeId=@OrganizeId) select  
                            t1.bqCode,t1.bqmc,ISNULL(ryrs.sl,0) rysl,ISNULL(cyrs.sl,0) cysl,ISNULL(zyrs.sl,0) zysl,isnull(ssrs.sl,0) ssrs
							,convert(decimal(20,2),(zyts.sl*0.01*100/cyrs.sl) )pjzyr  
                    from t t1 
                    left join(select bq, COUNT(1) sl 
                    from zy_brjbxx zyzt 
                    where  zyzt.zt=1 and zyzt.OrganizeId=@OrganizeId 
                    and CONVERT(varchar(10),ryrq,25)  between convert(datetime,@kssj,120) and convert(datetime,@jssj,120)
                    group by bq) ryrs on  t1.bqCode=ryrs.bq

                    left join(select  bq, COUNT(1) sl 
                    from zy_brjbxx zyzt 
                    where  zyzt.zt=1 and zyzt.OrganizeId=@OrganizeId  and zyzt.zybz<>9  and cyrq is not null
                    and CONVERT(varchar(10),cyrq,25)  between convert(datetime,@kssj,120) and convert(datetime,@jssj,120)
                    group by  bq ) cyrs on  t1.bqCode=cyrs.bq
 
                    left join(	select bq, COUNT(1) sl 
                    from zy_brjbxx zyzt left join Newtouch_EMR..mr_basy basy
					on zyzt.zyh=basy.ZYH and zyzt.OrganizeId=basy.OrganizeId
                    where  zyzt.zt=1 and zyzt.OrganizeId=@OrganizeId  and zyzt.zt=1 
					and  basy.RYTJ=3
                    and CONVERT(varchar(10),cyrq,25)  between convert(datetime,@kssj,120) and convert(datetime,@jssj,120)
                    group by bq  ) zyrs on  t1.bqCode=zyrs.bq
				
                    left join ( select bq,count(1) sl from   Newtouch_EMR..mr_basy_ss ss left join zy_brjbxx brxx  on ss.ZYH=brxx.zyh
					where ss.zt=1 and brxx.zt=1 and ss.OrganizeId=@OrganizeId		
					 and CONVERT(varchar(10),ss.CreateTime,25)  between convert(datetime,@kssj,120) and convert(datetime,@jssj,120)
					group by bq ) ssrs on ssrs.bq=t1.bqCode

					left join (
					 select  bq,sum(Newtouch_EMR.dbo.get_zyts(ryrq,cyrq)) sl from zy_brjbxx where ryrq is not null and cyrq is not null
					  and CONVERT(varchar(10),cyrq,25)  between convert(datetime,@kssj,120) and convert(datetime,@jssj,120) and  zybz<>9
					 group by  bq) zyts on zyts.bq=t1.bqCode


                ");
            var param = new DbParameter[] {
                new SqlParameter("@OrganizeId",OrganizeId),
                new SqlParameter("@kssj",kssj),
                new SqlParameter("@jssj",jssj),
            };
            outpatientlist = FindList<DeparNumberEntity>(sqlStr.ToString(), param);
            return outpatientlist;
        }

        public List<StaffNumberEntity> YSgzl_Lsqs(string kssj, string jssj,string bq, string OrganizeId)
        {
            List<StaffNumberEntity> outpatientlist = null;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"
						with t as(select  CreatorCode from zy_brjbxx where  bq=@bq	 group by CreatorCode) select  
                          staff.Name,ISNULL(ryrs.sl,0) rysl,ISNULL(cyrs.sl,0) cysl,ISNULL(zyrs.sl,0) zysl,isnull(ssrs.sl,0) ssrs
							,convert(decimal(20,2),(zyts.sl*0.01*100/cyrs.sl) )pjzyr  
                    from t t1 
				
                    left join(select CreatorCode, COUNT(1) sl 
                    from zy_brjbxx zyzt 
                    where  zyzt.zt=1 and zyzt.OrganizeId=@OrganizeId and  bq=@bq	
                    and CONVERT(varchar(10),ryrq,25)  between convert(datetime,@kssj,120) and convert(datetime,@jssj,120)
                    group by CreatorCode) ryrs on  t1.CreatorCode=ryrs.CreatorCode

                    left join(select  CreatorCode, COUNT(1) sl 
                    from zy_brjbxx zyzt 
                    where  zyzt.zt=1 and zyzt.OrganizeId=@OrganizeId  and zyzt.zybz<>9  and cyrq is not null and  bq=@bq	
                    and CONVERT(varchar(10),cyrq,25)  between convert(datetime,@kssj,120) and convert(datetime,@jssj,120)
                    group by  CreatorCode ) cyrs on  t1.CreatorCode=cyrs.CreatorCode
 
                    left join(select basy.CreatorCode, COUNT(1) sl 
                    from zy_brjbxx zyzt left join Newtouch_EMR..mr_basy basy
					on zyzt.zyh=basy.ZYH and zyzt.OrganizeId=basy.OrganizeId
                    where  zyzt.zt=1 and zyzt.OrganizeId=@OrganizeId  and zyzt.zt=1 
					and  basy.RYTJ=3
					and  bq=@bq	
                    and CONVERT(varchar(10),cyrq,25)  between convert(datetime,@kssj,120) and convert(datetime,@jssj,120)
                    group by basy.CreatorCode ) zyrs on  t1.CreatorCode=zyrs.CreatorCode


					left join ( select brxx.CreatorCode,count(1) sl from   Newtouch_EMR..mr_basy_ss ss left join zy_brjbxx brxx  on ss.ZYH=brxx.zyh
					where ss.zt=1 and brxx.zt=1 and ss.OrganizeId=@OrganizeId	and bq=@bq		 
					 and CONVERT(varchar(10),ss.CreateTime,25)  between convert(datetime,@kssj,120) and convert(datetime,@jssj,120)
					group by brxx.CreatorCode ) ssrs on ssrs.CreatorCode=t1.CreatorCode

					left join (
					 select  CreatorCode,sum(Newtouch_EMR.dbo.get_zyts(ryrq,cyrq)) sl from zy_brjbxx where ryrq is not null and cyrq is not null
					  and CONVERT(varchar(10),cyrq,25)  between convert(datetime,@kssj,120) and convert(datetime,@jssj,120) and  zybz<>9 and bq=@bq	
					 group by  CreatorCode) zyts on zyts.CreatorCode=t1.CreatorCode

					 left join NewtouchHIS_Base..Sys_Staff staff on t1.CreatorCode=staff.gh

                ");
            var param = new DbParameter[] {
                new SqlParameter("@OrganizeId",OrganizeId),
                new SqlParameter("@kssj",kssj),
                new SqlParameter("@jssj",jssj),
                new SqlParameter("@bq",bq),
            };
            outpatientlist = FindList<StaffNumberEntity>(sqlStr.ToString(), param);
            return outpatientlist;
        }
    }
}
