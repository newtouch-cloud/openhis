using FrameworkBase.MultiOrg.Domain.IDomainServices;
using Newtouch.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Common.Utils;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.ReportManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class ReportController : ControllerBase
    {
        private readonly IReportCommonDmnService _reportCommonDmnService;
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly IHospFeeDmnService _hospFeeDmnService;
        private readonly IPatientBasicInfoDmnService _patientBasicInfoDmnService;
	    private readonly IRptrptMzRjbRepo _rptrptMzRjbRepo;

		#region

		public ActionResult RevenueDetailesByPatientReportCompare() {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// 月报表
        /// </summary>
        /// <returns></returns>
        public ActionResult MonthReportIndex()
        {
            return View();
        }

        public ActionResult MonthlyProfit() {
            ReportingServiceCom();
            return View();
        }

        public ActionResult TherapistPerformance() {
            ReportingServiceCom();
            return View();
        }

        public ActionResult PatientDataDetail() {
            ReportingServiceCom();
            return View();
        }
        public ActionResult ChargeTypeQuery()
        {
            ReportingServiceCom();
            return View();
        }
        public ActionResult OutpatientWorkloadQuery()
        {
            ReportingServiceCom();
            return View();
        }
        public ActionResult OutpatientSituationQuery()
        {
            ReportingServiceCom();
            return View();
        }
        public ActionResult OutpatientQuery()
        {
            ReportingServiceCom();
            return View();
        }
        public ActionResult NormalMedicineInsuranceQuery()
        {
            ReportingServiceCom();
            return View();
        }
        public ActionResult AllMedicineInsuranceQuery()
        {
            ReportingServiceCom();
            return View();
        }
        public ActionResult GetStaffByDutyCode(string orgId)
        {
            var data = _sysUserDmnService.GetStaffByDutyCode(orgId ?? OrganizeId, "RehabDoctor");//获取治疗师
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获取药库药房下拉框数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetSFDLCodeSelectJson()
        {
            var data = _reportCommonDmnService.GetSFDLCodeSelectJson(OrganizeId);
            var treeList = new List<TreeSelectModel>
            {
                new TreeSelectModel{
                id="",
                text="未选择"}
            };
            if (data != null)
            {
                foreach (SFDLReportVO item in data)
                {
                    TreeSelectModel treeModel = new TreeSelectModel();
                    treeModel.id = item.dlcode;
                    treeModel.text = item.dlmc;
                    treeList.Add(treeModel);

                }
                return Content(treeList.TreeSelectJson(null));
            }
            else
            {
                return Content("");
            }
        }

        /// <summary>
        /// 月报表 请求查询 实时 的 数据
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult MonthReportRealTimeData(int year, int month)
        {
            var data = _reportCommonDmnService.GetMonthReportRealTimeData(this.OrganizeId, new DateTime(year, month, 1), new DateTime(year, month, 1).AddMonths(1));
            return Success(null, data);
        }

        /// <summary>
        /// 月报表 固化 调整
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="calcZje"></param>
        /// <returns></returns>
        public ActionResult MonthReportDataConfirmPreview()
        {
            return View();
        }

        public ActionResult NewOutpatientFeeStatistics()
        {
            ReportingServiceCom();

            return View();
        }
        /// <summary>
        /// 月报表 固化 确认
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="calcZje"></param>
        /// <param name="resultZje"></param>
        /// <param name="details"></param>
        /// <param name="balance"></param>
        /// <param name="balanceReason"></param>
        /// <param name="isregenerate">重新生成 标志</param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult SubmitMonthReportData(int year, int month, decimal calcZje, decimal resultZje
            , IList<MonthReportMajorCateStatisticsVO> details, decimal balance = 0, string balanceReason = null
            , bool isregenerate = false)
        {
            if (resultZje <= 0)
            {
                throw new FailedException("费用合计结果应为正数，请确认");
            }
            if (calcZje + balance != resultZje)
            {
                throw new FailedException("金额计算错误，请确认<br/>（费用+补差!=结果）");
            }
            _reportCommonDmnService.SubmitMonthReportData(this.OrganizeId, this.UserIdentity.UserCode, year, month, calcZje, resultZje
                , details, balance, balanceReason
                , isregenerate);

            return Success("报表生成成功");
        }

        public ActionResult RehabilitationCategoryTreatDetail()
        {
            ReportingServiceCom();
            return View();
        }

        public ActionResult RevenueDetailesByTherapistReportCompare()
        {
            ReportingServiceCom();
            return View();
        }
        #endregion

        #region 治疗师工作统计

        /// <summary>
        /// 治疗师工作统计
        /// </summary>
        /// <returns></returns>
        public ActionResult TherapeWorkStatistics()
        {
            ReportingServiceCom();

            return View();
        }

        #endregion

        #region 分类清单 - 住院病人

        /// <summary>
        /// 分类清单 - 住院病人
        /// </summary>
        /// <returns></returns>
        public ActionResult InpatientClassification()
        {
            ReportingServiceCom();

            return View();
        }

        #endregion

        #region 分类清单 - 门诊病人

        /// <summary>
        /// 分类清单 - 门诊病人
        /// </summary>
        /// <returns></returns>
        public ActionResult OutpatientClassification()
        {
            ReportingServiceCom();

            return View();
        }

        #endregion

        #region 结算单 - 住院病人

        /// <summary>
        /// 结算单 - 住院病人
        /// </summary>
        /// <returns></returns>
        public ActionResult InpatientSett()
        {
            ReportingServiceCom();

            return View();
        }

        #endregion

        #region 结算单 - 门诊病人

        /// <summary>
        /// 结算单 - 门诊病人
        /// </summary>
        /// <returns></returns>
        public ActionResult OutpatientSett()
        {
            ReportingServiceCom();

            return View();
        }

        /// <summary>
        /// 结算单 - 门诊病人
        /// </summary>
        /// <returns></returns>
        public ActionResult OutpatientSettFl()
        {
            ReportingServiceCom();

            return View();
        }

        #endregion

        #region 逐单收入报表

        /// <summary>
        /// 逐单收入报表
        /// </summary>
        /// <returns></returns>
        public ActionResult OutpatientFeeStatistics()
        {
            ReportingServiceCom();

            return View();
        }

        public ActionResult GetdotorcByDutyCode(string orgId)
        {
            var data = _sysUserDmnService.GetStaffByDutyCode(orgId ?? OrganizeId, "Doctor");//获取医生
            return Content(data.ToJson());
        }

        #endregion

        #region 费用一日清

        /// <summary>
        /// 费用一日清
        /// </summary>
        /// <returns></returns>
        public ActionResult InpatientDayFee()
        {
            
            //_hospFeeDmnService.SyncPatFee(OrganizeId, "", 0);//同步项目费用
            //_hospFeeDmnService.SyncPatFee(OrganizeId, "", 1);//同步药品费用
            ReportingServiceCom();

            return View();
        }

        public ActionResult GetInpatientcryrq(string zyh)
        {
            var hospPatientBasic = _patientBasicInfoDmnService.GetSysBasicVagueByZHY(zyh, this.OrganizeId);
            return Content(hospPatientBasic.ToJson());
        }

        [HttpPost]
        public ActionResult SynInpatientDayFee(string zyh)
        {
            if(!string.IsNullOrWhiteSpace(zyh))
            {
                _hospFeeDmnService.SyncPatFee(OrganizeId, zyh, 0);//同步项目费用
                _hospFeeDmnService.SyncPatFee(OrganizeId, zyh, 1);//同步药品费用
            }
            return Success();
        }
       

        /// <summary>
        /// 绩效指标（费用 人次）
        /// </summary>
        /// <returns></returns>
        public ActionResult PerformanceIndicatorFee()
        {
            ReportingServiceCom();

            return View();
        }

        #endregion

        #region 费用结算清单
        public ActionResult InpatientPatFeeDetail()
        {
            ReportingServiceCom();

            return View();
        }
        #endregion
        #region 执行计划查询
        public ActionResult TreatmentplanQuery()
        {
            ReportingServiceCom();

            return View();
        }
        #endregion
        #region 执行计划查询门诊
        public ActionResult TreatmentplanQueryOutpatient()
        {
            ReportingServiceCom();

            return View();
        }
        #endregion
        #region 门诊日结报表
        public ActionResult ChargeDailysettlement()
        {
            ReportingServiceCom();

            return View();
        }
        #endregion
        #region 住院医保费用统计报表
        public ActionResult HealthInsuranceReport()
        {
            ReportingServiceCom();

            return View();
        }
		#endregion

		#region 医院垫付金额明细
		public ActionResult HosAdvanceAmount()
		{
			ReportingServiceCom();
			return View();
		}
		#endregion

		#region 病人就诊信息（就诊次数 等）
		/// <summary>
		/// 病人就诊信息（就诊次数 等）
		/// </summary>
		/// <returns></returns>
		public ActionResult OutpatientTreatmentInfo()
		{
			ReportingServiceCom();
			return View();
		}
		#endregion

		/// <summary>
		/// 服务代码盈利
		/// </summary>
		/// <returns></returns>
		public ActionResult RevenueDetailesReport()
        {
            ReportingServiceCom();

            return View();
        }

        /// <summary>
        /// 服务代码盈利（治疗师分组）
        /// </summary>
        /// <returns></returns>
        public ActionResult RevenueDetailesByTherapistReport()
        {
            ReportingServiceCom();

            return View();
        }

        /// <summary>
        /// 同步服务代码盈利
        /// </summary>
        /// <returns></returns>
        public ActionResult SyncRevenueDetailesReport()
        {
            ReportingServiceCom();

            return View();
        }

        /// <summary>
        /// 同步服务代码盈利（治疗师分组）
        /// </summary>
        /// <returns></returns>
        public ActionResult SyncRevenueDetailesByTherapistReport()
        {
            ReportingServiceCom();

            return View();
        }

        /// <summary>
        /// 病人收费总览
        /// </summary>
        /// <returns></returns>
        public ActionResult PatientRevenue()
        {
            ReportingServiceCom();

            return View();
        }

        public ActionResult PatientRevenueTB()
        {
            ReportingServiceCom();

            return View();
        }
        /// <summary>
        /// 门诊病人一览表
        /// </summary>
        /// <returns></returns>
        public ActionResult OutpatientListChart()
        {
            ReportingServiceCom();

            return View();
        }
        /// <summary>
        /// 住院病人一览表
        /// </summary>
        /// <returns></returns>
        public ActionResult InpatientListChart()
        {
            ReportingServiceCom();

            return View();
        }
        /// <summary>
        /// 治疗师下拉
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRehabDoctorList(string orgId)
        {
            if (string.IsNullOrWhiteSpace(orgId))
            {
                orgId = this.OrganizeId;
            }
            var data = _sysUserDmnService.GetStaffByDutyCode(orgId, "RehabDoctor");
            return Content(data.ToJson());
        }

		

		/// <summary>
		/// 病人收费执行报表
		/// </summary>
		/// <returns></returns>
		public ActionResult PatientChargeExecution()
        {
            ReportingServiceCom();

            return View();
        }
        /// <summary>
        /// 月度申报结算表（医保）
        /// </summary>
        /// <returns></returns>
        public ActionResult SettMonthForYB()
        {
            ReportingServiceCom();

            return View();
        }
        public ActionResult GetdoctorByDutyCode(string orgId)
        {
            var data = _sysUserDmnService.GetStaffByDutyCode(orgId ?? OrganizeId, "Doctor");//获取医生
            return Content(data.ToJson());
        }

        /// <summary>
        /// 住院全院报表
        /// </summary>
        /// <returns></returns>
        public ActionResult InpatientWhileHospital()
        {
            ReportingServiceCom();
            //return View();
            return View();
        }

        /// <summary>
        /// 门诊全院报表
        /// </summary>
        /// <returns></returns>
        public ActionResult OutpatientWhileHospital()
        {
            ReportingServiceCom();
            //return View();
            return View();
        }

        public ActionResult PrintCryRegisterReport() {
            ReportingServiceCom();
            //return View();
            return View();
        }
       

        public ActionResult OutpWorkStacDeptReport () {
            ReportingServiceCom();
            return View();
        }

        //费用一日清视图
        public ActionResult PrintReport()
        {
            ReportingServiceCom();

           return View();
        }


        //分类汇总日清单视图
        public ActionResult SubtotalsReport()
        {
            ReportingServiceCom();

            return View();
        }

        //住院费用明细视图
        public ActionResult Hospitalizationcost() {
            ReportingServiceCom();

            return View();
        }

        //住院病人分类清单
        public ActionResult Hospitalizationclassification()
        {
            ReportingServiceCom();

            return View();
        }





        #region 新农合申报
        /// <summary>
        /// 门诊新农合申报明细
        /// </summary>
        /// <returns></returns>
        public ActionResult OutpatientXnhBcmxsqd()
        {
            ReportingServiceCom();
            ViewBag.usercode = UserIdentity.UserCode;
            return View();
        }

        public ActionResult OutpatientXnhBcsqd()
        {
            ReportingServiceCom();
            ViewBag.usercode = UserIdentity.UserCode;
            return View();
        }

        public ActionResult InpatientXnhBcsqd()
        {
            ReportingServiceCom();
            ViewBag.usercode = UserIdentity.UserCode;
            return View();
        }

        public ActionResult InpatientXnhBcmxsqd()
        {
            ReportingServiceCom();
            ViewBag.usercode = UserIdentity.UserCode;
            return View();
        }
        #endregion

        #region
        /// <summary>
        /// 门诊日结算表
        /// </summary>
        /// <returns></returns>
        public ActionResult OutpatientDailyFee()
        {
            ReportingServiceCom();
	        RptrptMzRjbEntity mzRjbEntity= _rptrptMzRjbRepo.GetLastMzrjEntity(this.OrganizeId, this.UserIdentity.UserCode);
            ViewBag.usercode = UserIdentity.UserCode;
			ViewBag.kssj = mzRjbEntity.jssj.ToString("yyyy-MM-dd HH:mm:ss");
            ViewBag.OrgId = this.OrganizeId;
            return View();
        }

	    public ActionResult turnInFee(DateTime kssj,DateTime jssj)
	    {
			_reportCommonDmnService.turnInFee(this.OrganizeId,this.UserIdentity.UserCode,kssj,jssj);
		    return Success();
	    }
	    public ActionResult turnInFeeSelect(string keyword)
	    {
			var rjList = _rptrptMzRjbRepo.GetLastMzrjEntityList(this.OrganizeId, this.UserIdentity.UserCode, keyword);
			return Content(rjList.ToJson());
	    }
		/// <summary>
		/// 门诊结算明细信息表
		/// </summary>
		/// <returns></returns>
		public ActionResult OutpatientSettDetailsInfo()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// 科室财务核算汇总表
        /// </summary>
        /// <returns></returns>
        public ActionResult DepartmentFinancialAccSummary()
        {
            ReportingServiceCom();
            return View();
        }

        public ActionResult HosPovertyVisitSummary()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// 科室业务收入统计表
        /// </summary>
        /// <returns></returns>
        public ActionResult DepartmentBusinessIncomeSummary()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// 药品分类统计表
        /// </summary>
        /// <returns></returns>
        public ActionResult MedicineClassificationSettedSummary()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// 门诊收费员工作量统计表
        /// </summary>
        /// <returns></returns>
        public ActionResult OutpatientTollCollectorWorkloadSummary()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// 门诊医生工作量统计表
        /// </summary>
        /// <returns></returns>
        public ActionResult OutpatientDoctorWorkloadSummary()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// 项目分类统计表
        /// </summary>
        /// <returns></returns>
        public ActionResult ChargeItemClassificationSettedSummary()
        {
            ReportingServiceCom();
            return View();
        }

        public ActionResult InpatientCrytjb()
        {
            ReportingServiceCom();
            return View();
        }

	    public ActionResult OutpatientKsstjb()
	    {
		    ReportingServiceCom();
		    return View();
	    }

	    public ActionResult HosMedicalIncome()
	    {
		    ReportingServiceCom();
		    return View();
		}

	    #endregion

		#region private methods

		/// <summary>
		/// 
		/// </summary>
		private void ReportingServiceCom()
        {
            
            ViewBag.OrgId = this.OrganizeId;
            ViewBag.topOrgId = Constants.TopOrganizeId;
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            ViewBag.IsHospAdministrator = this.UserIdentity.IsHospAdministrator.ToString().ToLower();  //是否是医院管理员
            ViewBag.CurUserCode = this.UserIdentity.UserCode;
            ViewBag.curUsergh = UserIdentity.rygh;
        }

		#endregion

		#region 医保
	    public ActionResult OutpatientCqYbDz()
	    {
		    ReportingServiceCom();
		    return View();
	    }
        #endregion
        #region 医保自费科室结算费用统计
        public ActionResult HosExpenseSettDeatilInfo()
        {
            ReportingServiceCom();
            return View();
        }
        public ActionResult HosExpenseSettInfo()
        {
            ReportingServiceCom();
            return View();
        }
        public ActionResult HosYbPatientDrYbSettInfo()
        {
            ReportingServiceCom();
            return View();
        }
        public ActionResult HosYbPatientNoInfo()
        {
            ReportingServiceCom();
            return View();
        }
        public ActionResult HosYbPatientSettDetailInfo()
        {
            ReportingServiceCom();
            return View();
        }
        public ActionResult HosYbPatientSettInfo()
        {
            ReportingServiceCom();
            return View();
        }
        public ActionResult HosYbSettApplyInfo()
        {
            ReportingServiceCom();
            return View();
        }

        public ActionResult HosAcceptingGold()
        {
            ReportingServiceCom();
            return View();
        }
        public ActionResult HosMedicalBill()
        {
            ReportingServiceCom();
            return View();
        }
        public ActionResult HosFeeSettSummry()
        {
            ReportingServiceCom();
            return View();
        }
        #endregion

         //科室收入明细
        public ActionResult Department_income_statement() {
            ReportingServiceCom();

            return View();
        }

        //科室药品成本价
        public ActionResult Drugcostprice() {
            ReportingServiceCom();

            return View();
        }

		/// <summary>
		/// 获取大类名称
		/// </summary>
		/// <returns></returns>
		public ActionResult GetDlMc()
		{
			var data = _reportCommonDmnService.GetDlMc(this.OrganizeId);
			return Content(data.ToJson());
		}

        #region 特病门诊统计
        public ActionResult TBStatistics()
        {
            ReportingServiceCom();

            return View();
        }
        #endregion

		public ActionResult MZCBFYFX()
		{
			ReportingServiceCom();

			return View();
		}

		public ActionResult FYXMMXQuery()
		{
			ReportingServiceCom();

			return View();
		}

	}
}