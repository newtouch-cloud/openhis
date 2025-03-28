using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.DTO.InputDto.OutpatientAccounting;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.DaySettleManage;
using Newtouch.HIS.Domain.ValueObjects.HospitalizationManage;
using Newtouch.HIS.Domain.ValueObjects.PatientManage;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Newtouch.HIS.DomainServices
{
    public class DaySettleDmnService : DmnServiceBase, IDaySettleDmnService
    {
        public DaySettleDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        #region 日结算查询

        /// <summary>
        /// 门诊日结算查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="zxzt"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<DaySettleDetailVO> GetOutPatientSettListJson(Pagination pagination, DateTime? kssj, DateTime? jssj, string orgId, string mzzybz)
        {
            StringBuilder strSql = new StringBuilder();
            var pars = new List<SqlParameter>();
            strSql.Append(@"SELECT a.*,b.Name CreatorName FROM NewtouchHIS_Sett.dbo.xt_daysettle a
            LEFT JOIN NewtouchHIS_Base..V_C_Sys_UserStaff b 
ON b.Account = a.CreatorCode AND b.zt='1' AND b.OrganizeId=a.OrganizeId
where 1 = 1 and a.OrganizeId = @orgId and a.zt = '1' and a.mzzybz = @mzzybz");
            pars.Add(new SqlParameter("@orgId", orgId));
            pars.Add(new SqlParameter("@mzzybz", mzzybz));

            var list = this.QueryWithPage<DaySettleDetailVO>(strSql.ToString(), pagination, pars.ToArray()).ToList();
            return list;
        }

        /// <summary>
        /// 获取上次日结算时间
        /// </summary>
        /// <returns></returns>
        public string getLastSettDate(string mzzybz)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT TOP 1 [CreateTime] ");
            strSql.Append(" FROM [NewtouchHIS_Sett].[dbo].[xt_daysettle] a where a.zt = '1' and a.mzzybz=@mzzybz order by CreateTime desc");
            SqlParameter[] param =
                {
                    new SqlParameter("@mzzybz",mzzybz)
                };
            var list = this.FindList<DaySettleDetailVO>(strSql.ToString(), param).ToList();
            if (list.Count <= 0)
            {
                return "";
            }
            else
            {
                return list[0].CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }
        #endregion

        #region 保存数据
        /// <summary>
        /// 
        /// </summary>
        public void SaveOutpatientDaySettleInfo(DateTime? kssj, DateTime jssj, string CreatorCode, string orgId)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                var param = new List<SqlParameter>();
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT ISNULL(zje,0) as zje,fph,jsnm");
                strSql.Append(" FROM [NewtouchHIS_Sett].[dbo].[mz_js] a where a.CreateTime <= @jssj and a.zt = '1' AND cxjsnm=0 AND ISNULL(tbz, 0) = 0 AND jszt=1 ");
                param.Add(new SqlParameter("@jssj", jssj));
                if (kssj != null)
                {
                    strSql.Append(" and a.CreateTime >= @kssj");
                    param.Add(new SqlParameter("@kssj", kssj));
                }
                
                var list = this.FindList<JsDetailVO>(strSql.ToString(), param.ToArray()).ToList();
                if (list.Count <= 0)
                {
                    throw new FailedException("查询不到近期结算记录");
                }
                decimal bcje = 0;
                string fphs = string.Empty;
                string jsnms = string.Empty;
                foreach (var item in list)
                {
                    bcje += item.zje;
                    fphs += item.fph + ",";
                    jsnms += item.jsnm + ",";
                }
                if (!string.IsNullOrEmpty(fphs))
                {
                    fphs = fphs.Substring(0, fphs.Length - 1);
                }
                if (!string.IsNullOrEmpty(jsnms))
                {
                    jsnms = jsnms.Substring(0, jsnms.Length - 1);
                }
                DaySettleEntity daySettleOb = new DaySettleEntity();
                daySettleOb.bcje = bcje;
                daySettleOb.fphs = fphs;
                daySettleOb.Id = Guid.NewGuid().ToString();
                daySettleOb.CreatorCode = CreatorCode;
                daySettleOb.CreateTime = DateTime.Now;
                daySettleOb.jsnms = jsnms;
                daySettleOb.mzzybz = "0";
                daySettleOb.zt = "1";
                if (kssj != null)
                {
                    daySettleOb.LastJsTime = kssj;
                }
                daySettleOb.OrganizeId = orgId;
                db.Insert(daySettleOb);

                //获取结算明细
                //var jsmxList = db.IQueryable<DaySettleMxEntity>(p => p.Id == "").ToList();
                //foreach (var item in jsmxList)
                //{
                //    db.Insert(item);
                //}

                db.Commit();
            }
        }

        /// <summary>
        /// 住院日结算
        /// </summary>
        public void SaveInpatientDaySettleInfo(DateTime? kssj, DateTime jssj, string CreatorCode, string orgId)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                var param = new List<SqlParameter>();
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT ISNULL(zje,0) as zje,fph,jsnm");
                strSql.Append(" FROM [NewtouchHIS_Sett].[dbo].[zy_js] a where a.CreateTime <= @jssj and a.zt = '1' ");
                param.Add(new SqlParameter("@jssj", jssj));
                if (kssj != null)
                {
                    strSql.Append(" and a.CreateTime >= @kssj");
                    param.Add(new SqlParameter("@kssj", kssj));
                }

                var list = this.FindList<JsDetailVO>(strSql.ToString(), param.ToArray()).ToList();
                if (list.Count <= 0)
                {
                    throw new FailedException("查询不到近期结算记录");
                }
                decimal bcje = 0;
                string fphs = string.Empty;
                foreach (var item in list)
                {
                    bcje += item.zje;
                    fphs += item.fph + ",";
                }
                if (!string.IsNullOrEmpty(fphs))
                {
                    fphs = fphs.Substring(0, fphs.Length - 1);
                }
                DaySettleEntity daySettleOb = new DaySettleEntity();
                daySettleOb.bcje = bcje;
                daySettleOb.fphs = fphs;
                daySettleOb.Id = Guid.NewGuid().ToString();
                daySettleOb.CreatorCode = CreatorCode;
                daySettleOb.CreateTime = DateTime.Now;
                daySettleOb.mzzybz = "1";
                daySettleOb.zt = "1";
                if (kssj != null)
                {
                    daySettleOb.LastJsTime = kssj;
                }
                daySettleOb.OrganizeId = orgId;
                db.Insert(daySettleOb);

                //获取结算明细
                //var jsmxList = db.IQueryable<DaySettleMxEntity>(p => p.Id == "").ToList();
                //foreach (var item in jsmxList)
                //{
                //    db.Insert(item);
                //}

                db.Commit();
            }
        }

        /// <summary>
        /// 取消门诊日结算
        /// </summary>
        public void CancleDaySettleInfo(string Id, string UserCode)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                DaySettleEntity daySettleOb = db.IQueryable<DaySettleEntity>(p => p.Id == Id && p.zt == "1").FirstOrDefault();

                daySettleOb.LastModifierCode = UserCode;
                daySettleOb.LastModifyTime = DateTime.Now;
                daySettleOb.zt = "0";
                daySettleOb.Modify();
                db.Update(daySettleOb);

                db.Commit();
            }
        }
		#endregion


		public IList<ybzdinfo> Getclr_type()
		{
			StringBuilder sqlStr = new StringBuilder();
			sqlStr.Append(@" select distinct q.clr_type value,b.label from(
select distinct clr_type from  drjk_zyjs_output
union all
select distinct clr_type from  drjk_mzjs_output
)q
left join yb_zdinfo b on b.type='CLR_TYPE' and q.clr_type=b.value
order by q.clr_type");
			SqlParameter[] param =
			{

			};
			var list = this.FindList<ybzdinfo>(sqlStr.ToString(), param).ToList();
			return list;
		}

		public IList<ybzdinfo> Getinsutype()
		{
			StringBuilder sqlStr = new StringBuilder();
			sqlStr.Append(@" select distinct q.insutype value,b.label from(
select distinct insutype from  drjk_zyjs_output
union all
select distinct insutype from  drjk_mzjs_output
)q
left join yb_zdinfo b on b.type='INSUTYPE' and q.insutype=b.value
order by q.insutype");
			SqlParameter[] param =
			{

			};
			var list = this.FindList<ybzdinfo>(sqlStr.ToString(), param).ToList();
			return list;
		}

		/// <summary>
		/// 获取清算对总帐
		/// </summary>
		/// <param name="organizeId">机构代码</param>
		/// <param name="ksrq">开始日期</param>
		/// <param name="jsrq">结束日期</param>
		/// <returns></returns>
		public IList<Qsdzz> GetQsdzzs(string organizeId, string ksrq, string jsrq, string qslx, string xz)
		{
			StringBuilder sqlStr = new StringBuilder();
			sqlStr.Append(@"exec his_se_qsdzz @organizeId=@organizeId,@ksrq=@ksrq,@jsrq=@jsrq,@qslx=@qslx,@xz=@xz");
			SqlParameter[] param =
			{
				new SqlParameter("@organizeId", organizeId),
				new SqlParameter("@ksrq", ksrq),
				new SqlParameter("@jsrq", jsrq),
				new SqlParameter("@qslx", qslx),
				new SqlParameter("@xz", xz)
			};
			var list = this.FindList<Qsdzz>(sqlStr.ToString(), param).ToList();
			return list;
		}

		public int inserqssq(System.Xml.XmlDocument ybqssqs, string orgid)
		{
			//string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(ybqssqs);
			//System.Xml.XmlDocument xmlstr = (System.Xml.XmlDocument)Newtonsoft.Json.JsonConvert.DeserializeXmlNode(jsonstr, "root");
			StringBuilder sqlStr = new StringBuilder();
			sqlStr.Append(@" exec his_se_inserqssq @orgid=@orgids ,@xml=@xmls ");
			SqlParameter[] param =
			{
				new SqlParameter("@orgids", orgid),
				new SqlParameter("@xmls", ybqssqs.InnerXml),
				//new SqlParameter("@tfrq", tfsj),
			};
			return this.ExecuteSqlCommand(sqlStr.ToString(), param);
		}

		/// <summary>
		/// 获取清算明细（上半部分）
		/// </summary>
		/// <param name="organizeId">机构代码</param>
		/// <param name="ksrq">开始日期</param>
		/// <param name="jsrq">结束日期</param>
		/// <returns></returns>
		public IList<Qsmx> GetQsmx(string organizeId, string ksrq, string jsrq, string jslx, string xzlx)
		{
			StringBuilder sqlStr = new StringBuilder();
			sqlStr.Append(@"exec his_se_qsmx @organizeId=@organizeId,@ksrq=@ksrq,@jsrq=@jsrq,@jslx=@jslx,@xzlx=@xzlx");
			SqlParameter[] param =
			{
				new SqlParameter("@organizeId", organizeId),
				new SqlParameter("@ksrq", ksrq),
				new SqlParameter("@jsrq", jsrq),
				new SqlParameter("@jslx", jslx),
				new SqlParameter("@xzlx", xzlx)
			};
			var list = this.FindList<Qsmx>(sqlStr.ToString(), param).ToList();
			return list;
		}

		/// <summary>
		/// 获取清算明细（下半部分）
		/// </summary>
		/// <param name="organizeId">机构代码</param>
		/// <param name="ksrq">开始日期</param>
		/// <param name="jsrq">结束日期</param>
		/// <returns></returns>
		public IList<Qsmx_1> GetQsmx_1(string organizeId, string ksrq, string jsrq, string jslx, string xzlx)
		{
			StringBuilder sqlStr = new StringBuilder();
			sqlStr.Append(@"exec his_se_qsmx_1 @organizeId=@organizeId,@ksrq=@ksrq,@jsrq=@jsrq,@jslx=@jslx ,@xzlx=@xzlx");
			SqlParameter[] param =
			{
				new SqlParameter("@organizeId", organizeId),
				new SqlParameter("@ksrq", ksrq),
				new SqlParameter("@jsrq", jsrq),
				new SqlParameter("@jslx", jslx),
				new SqlParameter("@xzlx", xzlx)
			};
			var list = this.FindList<Qsmx_1>(sqlStr.ToString(), param).ToList();
			return list;
		}

		/// <summary>
		/// 获取清算申请
		/// </summary>
		/// <param name="organizeId">机构代码</param>
		/// <param name="ksrq">开始日期</param>
		/// <param name="jsrq">结束日期</param>
		/// <param name="sfyd">是否异地</param>
		/// <returns></returns>
		public IList<Qssq> GetQssq(string organizeId, string ksrq, string jsrq, string sfyd)
		{
			StringBuilder sqlStr = new StringBuilder();
			sqlStr.Append(@"exec his_se_qssq @organizeId=@organizeId,@ksrq=@ksrq,@jsrq=@jsrq,@sfyd=@sfyd");
			SqlParameter[] param =
			{
				new SqlParameter("@organizeId", organizeId),
				new SqlParameter("@ksrq", ksrq),
				new SqlParameter("@jsrq", jsrq),
				new SqlParameter("@sfyd", sfyd)
			};
			var list = this.FindList<Qssq>(sqlStr.ToString(), param).ToList();
			return list;
		}

		/// <summary>
		/// 获取清算回退
		/// </summary>
		/// <param name="organizeId">机构代码</param>
		/// <param name="ksrq">开始日期</param>
		/// <param name="jsrq">结束日期</param>
		/// <returns></returns>
		public IList<Qsht> GetQsht(string organizeId, string ksrq, string jsrq)
		{
			StringBuilder sqlStr = new StringBuilder();
			sqlStr.Append(@"exec his_se_qsht @organizeId=@organizeId,@ksrq=@ksrq,@jsrq=@jsrq");
			SqlParameter[] param =
			{
				new SqlParameter("@organizeId", organizeId),
				new SqlParameter("@ksrq", ksrq),
				new SqlParameter("@jsrq", jsrq)
			};
			var list = this.FindList<Qsht>(sqlStr.ToString(), param).ToList();
			return list;
		}

		/// <summary>
		/// 日对账历史数据
		/// </summary>
		/// <param name="orgid"></param>
		/// <param name="ksrq"></param>
		/// <param name="jsrq"></param>
		/// <returns></returns>
		public IList<RdrlsList> lsdzList(string orgid,string ksrq, string jsrq)
		{
			StringBuilder sqlStr = new StringBuilder();
			sqlStr.Append(@"select * from NewtouchHIS_Sett..Ybjk_DZ_output where czrq>=@ksrq and czrq<=@jsrq +' 23:59:59' ");
			SqlParameter[] param =
			{
				new SqlParameter("@ksrq", ksrq),
				new SqlParameter("@jsrq", jsrq),
			};
			var list = this.FindList<RdrlsList>(sqlStr.ToString(), param).ToList();
			return list;
		}

		public IList<RdrNewList> Newdzfysj(string organizeId, string rq)
		{
			StringBuilder sqlStr = new StringBuilder();
			sqlStr.Append(@"exec his_se_rdzfysj @organizeId=@organizeId,@rq=@rq ");
			SqlParameter[] param =
			{
				new SqlParameter("@organizeId", organizeId),
				new SqlParameter("@rq", rq)
			};
			var list = this.FindList<RdrNewList>(sqlStr.ToString(), param).ToList();
			return list;
		}
        /// <summary>
		/// 日对账历史数据
		/// </summary>
		/// <param name="orgid"></param>
		/// <param name="ksrq"></param>
		/// <param name="jsrq"></param>
		/// <returns></returns>
		public IList<HistoryCheckVO> GetHistoryCheckList(string orgid, string ksrq, string jsrq)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"select  lsh,cardid,czrq jysj,'挂号' lx,curaccountamt dn,hisaccountamt ln,zfdxjzfs+tcdxjzfs+tcdxjzfs xj,
tczfs tc,0 df,ybjsfwfyze-totalexpense flzf,fjdzhzfs+fjdxjzfs+tcdzhzfs+tcdxjzfs+fybjsfwfyze grzf,gh.xm,gh.mzh mzbh,mzjs.jzdyh hm,'' zd,'' bz
 from NewtouchHIS_Sett..Ybjk_SH02_Output mzjs
 inner join mz_gh gh on mzjs.mzh=gh.mzh and gh.zt='1'
 where mzjs.zt='1'
 and mzjs.czrq>=@ksrq
and mzjs.czrq<=@jsrq
union all
select  lsh,cardid,czrq jysj,'收费' lx,curaccountamt dn,hisaccountamt ln,zfdxjzfs+tcdxjzfs+tcdxjzfs xj,
tczfs tc,0 df,ybjsfwfyze-totalexpense flzf,fjdzhzfs+fjdxjzfs+tcdzhzfs+tcdxjzfs+fybjsfwfyze grzf,gh.xm,gh.mzh mzbh,mzjs.jzdyh hm,'' zd,'' bz
 from NewtouchHIS_Sett..Ybjk_SI12_Output mzjs
  inner join mz_gh gh on mzjs.mzh=gh.mzh and gh.zt='1'
   where mzjs.zt='1'
 and mzjs.czrq>=@ksrq
and mzjs.czrq<=@jsrq
");
            SqlParameter[] param =
            {
                new SqlParameter("@ksrq", ksrq),
                new SqlParameter("@jsrq", jsrq),
            };
            var list = this.FindList<HistoryCheckVO>(sqlStr.ToString(), param).ToList();
            return list;
        }
    }
}
