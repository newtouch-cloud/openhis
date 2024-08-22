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
			return FindList<GetDlMc>(@"select dlmc,dlcode from  NewtouchHIS_Base..xt_sfdl where OrganizeId=@orgId", new DbParameter[] { new SqlParameter("@orgId", OrganizeId) });
		}
	}
}
