using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.ReportManage;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface IReportCommonDmnService
    {
        /// <summary>
        /// 
        /// </summary>
        IList<RptMonthReportDetailEntity> GetMonthReportConfirmedData(string orgId, int year, int month);
        
        /// <summary>
        /// 
        /// </summary>
        IList<MonthReportMajorCateStatisticsVO> GetMonthReportRealTimeData(string orgId, DateTime startTime, DateTime endTime);
        IList<SFDLReportVO> GetSFDLCodeSelectJson(string orgId);
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
        void SubmitMonthReportData(string orgId, string curOprCode, int year, int month, decimal calcZje, decimal resultZje
            , IList<MonthReportMajorCateStatisticsVO> details, decimal balance = 0, string balanceReason = null
            , bool isregenerate = false);

	    void turnInFee(string orgId, string czr, DateTime kssj, DateTime jssj);

		//获取大类名称
		IList<GetDlMc> GetDlMc(string orgId);


		YBSBMX_DCdbfVO yBSBMX_DCdbfs(string types, string ksrq, string jsrq, string orgid);

        IList<getsfxm> Getsfxm(Pagination pagination, string xzstr,string py,string sfdl,string orgId);
        IList<GetJyllk> GetJyjlData(Pagination pagination, string ksrq, string jsrq, string orgId);
        IList<GetJyllk> GetJyjlDatatotxt(string ksrq, string jsrq, string orgId);
        IList<OutpatientRegistrationVO> GetMjzdbghData(Pagination pagination, string ksrq, string jsrq, string orgId);
        IList<OutpatientRegistrationVO> GetYbMjzdbghtxt(string ksrq, string jsrq, string orgId);
        IList<OutpatientSickBedAtHomeVO> GetMjzjcsfData(Pagination pagination, string ksrq, string jsrq, string orgId);
        IList<OutpatientSickBedAtHomeVO> GetMjzjcsfDatatxt(string ksrq, string jsrq, string orgId);
    }
}
