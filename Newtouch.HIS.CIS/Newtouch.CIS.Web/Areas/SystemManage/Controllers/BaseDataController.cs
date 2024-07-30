using System;
using System.Linq;
using System.Web.Mvc;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Web;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IDomainServices.Outpatient;
using Newtouch.Infrastructure;
using Newtouch.Tools;

namespace Newtouch.CIS.Web.Areas.SystemManage.Controllers
{
	public class SysBaseDataController : OrgControllerBase
	{
		private readonly IBaseDataDmnService _baseDataDmnService;
		private readonly ISysDepartmentRepo _sysDepartmentRepo;
		private readonly ISysTCMSyndromeRepo _sysTCMSyndromeRepo;
		private readonly ISysDiagnosisRepo _sysDiagnosisRepo;
        private readonly IOutpatientConsultDmnService _outpatientConsultDmnService;

        /// <summary>
        /// 药品用法 检索
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public JsonResult GetMedicineUsageList(string keyword)
		{
			var list = _baseDataDmnService.GetMedicineUsageList();
			if (!string.IsNullOrWhiteSpace(keyword))
			{
				list = list.Where(a => a.yfCode.ToLower().Contains(keyword.ToLower()) || a.yfmc.ToLower().Contains(keyword.ToLower())).ToList();
			}

			list = list.Where(a => a.yplx == ((int)EnumYpyf.WM).ToString()).ToList();
			return Json(list, JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetZYMedicineUsageList(string keyword)
		{
			var list = _baseDataDmnService.GetMedicineUsageList();
			if (!string.IsNullOrWhiteSpace(keyword))
			{
				list = list.Where(a => a.yfCode.ToLower().Contains(keyword.ToLower()) || a.yfmc.ToLower().Contains(keyword.ToLower())).ToList();
			}

			list = list.Where(a => a.yplx == ((int)EnumYpyf.TCM).ToString()).ToList();
			return Json(list, JsonRequestBehavior.AllowGet);
		}
        public JsonResult GetTCMDjfsUsageList(string keyword)
        {
            var list = _baseDataDmnService.GetDjfsUsageList(this.OrganizeId);
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                list = list.Where(a => a.Code.ToLower().Contains(keyword.ToLower()) || a.Name.ToLower().Contains(keyword.ToLower())).ToList();
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 科室 下拉 
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectDepartmentList(string keyword, string zlks)
		{
			var data = _sysDepartmentRepo.GetList(this.OrganizeId, "1", keyword);
            if (zlks != null && zlks != "")
            {
                data = data.Where(a => a.zlks == zlks).ToList();
            }
            return Content(data.ToJson());
        }

		/// <summary>
		/// 诊断 检索
		/// </summary>
		/// <param name="keyword"></param>
		/// <returns></returns>
		public JsonResult GetDiagnosisList(string keyword, string zdlx, string ybnhlx)
		{
			if (string.IsNullOrEmpty(ybnhlx))
			{
				ybnhlx = null;
			}
			var list = _sysDiagnosisRepo.GetList(this.OrganizeId, keyword, zdlx, ybnhlx);
			return Json(list, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// 症侯 检索
		/// </summary>
		/// <param name="keyword"></param>
		/// <returns></returns>
		public ActionResult GetTCMSymptomsList(string keyword)
		{
			var list = _sysTCMSyndromeRepo.GetList(this.OrganizeId, keyword);
			return Json(list, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// 获取代煎费
		/// </summary>
		/// <returns></returns>
		public JsonResult GetDjFee()
		{
			var fee = _baseDataDmnService.GetDjFee(this.OrganizeId);
			return Json(fee, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// 长临标志
		/// </summary>
		/// <returns></returns>
		public ActionResult GetYzlb()
		{
			var result = new object[] {
					new { yzlb = "长",Name="长" },
					new { yzlb = "临",Name="临" }
				};
			return Content(result.ToJson());
		}

		/// <summary>
		/// 获取医生开药权限
		/// </summary>
		/// <returns></returns>
		public int GetPermissions(string tsypzl,string dlcode,string kssqxjb)
		{
			var orgid = this.OrganizeId;
			var gh = UserIdentity.rygh;
			return _baseDataDmnService.GetPermissions(orgid, gh, tsypzl, dlcode, kssqxjb);
		}

        /// <summary>
        /// 系统诊室
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult SelectConsultList(string ksCode,string keyword)
        {
            var data = _outpatientConsultDmnService.SelectConsultList(ksCode, keyword,this.OrganizeId );
            return Content(data.ToJson());
        }
    }
}