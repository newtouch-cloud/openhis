using System.Collections.Generic;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects.PatientManage;
using System;
using Newtouch.HIS.Domain.ValueObjects.HospitalizationManage;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.DTO.InputDto.OutpatientAccounting;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.HIS.Domain.ValueObjects.DaySettleManage;

namespace Newtouch.HIS.Domain.IDomainServices
{
	/// <summary>
	/// 
	/// </summary>
	public interface IDaySettleDmnService
	{
		/// <summary>
		/// 门诊日结算查询
		/// </summary>
		/// <param name="pagination"></param>
		/// <param name="keyword"></param>
		/// <param name="kssj"></param>
		/// <param name="jssj"></param>
		/// <param name="orgId"></param>
		/// <returns></returns>
		IList<DaySettleDetailVO> GetOutPatientSettListJson(Pagination pagination, DateTime? kssj, DateTime? jssj, string orgId, string mzzybz);

		/// <summary>
		/// 门诊日结算保存
		/// </summary>
		/// <param name="kssj">开始时间</param>
		void SaveOutpatientDaySettleInfo(DateTime? kssj, DateTime jssj, string CreatorCode, string orgId);

		/// <summary>
		/// 住院日结算保存
		/// </summary>
		/// <param name="kssj">开始时间</param>
		void SaveInpatientDaySettleInfo(DateTime? kssj, DateTime jssj, string CreatorCode, string orgId);

		/// <summary>
		/// 获取上次日结算时间
		/// </summary>
		/// <returns></returns>
		string getLastSettDate(string mzzybz);

		/// <summary>
		/// 取消门诊日结算
		/// </summary>
		/// <param name="Id"></param>
		void CancleDaySettleInfo(string Id, string UserCode);


		IList<ybzdinfo> Getclr_type();
		IList<ybzdinfo> Getinsutype();

		/// <summary>
		/// 获取清算对总帐
		/// </summary>
		/// <param name="organizeId">机构代码</param>
		/// <param name="ksrq">开始日期</param>
		/// <param name="jsrq">结束日期</param>
		/// <returns></returns>
		IList<Qsdzz> GetQsdzzs(string organizeId, string ksrq, string jsrq, string qslx, string xz);

		int inserqssq(System.Xml.XmlDocument ybqssq, string orgid);

		/// <summary>
		/// 获取清算明细（上半部分）
		/// </summary>
		/// <param name="organizeId">机构代码</param>
		/// <param name="ksrq">开始日期</param>
		/// <param name="jsrq">结束日期</param>
		/// <param name="jslx">结算类型</param>
		/// <returns></returns>
		IList<Qsmx> GetQsmx(string organizeId, string ksrq, string jsrq, string jslx, string xzlx);

		/// <summary>
		/// 获取清算明细（下半部分）
		/// </summary>
		/// <param name="organizeId">机构代码</param>
		/// <param name="ksrq">开始日期</param>
		/// <param name="jsrq">结束日期</param>
		/// <param name="jslx">结算类型</param>
		/// <returns></returns>
		IList<Qsmx_1> GetQsmx_1(string organizeId, string ksrq, string jsrq, string jslx, string xzlx);


		/// <summary>
		/// 获取清算申请
		/// </summary>
		/// <param name="organizeId">机构代码</param>
		/// <param name="ksrq">开始日期</param>
		/// <param name="jsrq">结束日期</param>
		/// <param name="sfyd">是否异地</param>
		/// <returns></returns>
		IList<Qssq> GetQssq(string organizeId, string ksrq, string jsrq, string sfyd);

		///// <summary>
		///// 清算回退
		///// </summary>
		///// <param name="organizeId">机构代码</param>
		///// <param name="ksrq">开始日期</param>
		///// <param name="jsrq">结束日期</param>
		///// <returns></returns>
		IList<Qsht> GetQsht(string organizeId, string ksrq, string jsrq);


		/// <summary>
		/// 日对账历史数据
		/// </summary>
		/// <param name="orgid"></param>
		/// <param name="ksrq"></param>
		/// <param name="jsrq"></param>
		/// <returns></returns>
		IList<RdrlsList> lsdzList(string orgid, string ksrq, string jsrq);

		IList<RdrNewList> Newdzfysj(string orgid, string rq);
        /// <summary>
        /// 获取本地对账明细数据
        /// </summary>
        /// <param name="orgid"></param>
        /// <param name="ksrq"></param>
        /// <param name="jsrq"></param>
        /// <returns></returns>
        IList<HistoryCheckVO> GetHistoryCheckList(string orgid, string ksrq, string jsrq);
        
    }
}
