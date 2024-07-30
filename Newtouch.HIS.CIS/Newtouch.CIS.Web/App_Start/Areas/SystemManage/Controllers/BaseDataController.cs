using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Web;
using Newtouch.Domain.IDomainServices;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Areas.SystemManage.Controllers
{
    public class SysBaseDataController : OrgControllerBase
    {
        private readonly IBaseDataDmnService _baseDataDmnService;
        private readonly ISysDepartmentRepo _sysDepartmentRepo;
        private readonly ISysTCMSyndromeRepo _sysTCMSyndromeRepo;
        private readonly ISysDiagnosisRepo _sysDiagnosisRepo;

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
                list = list.Where(a =>  a.yfCode.ToLower().Contains(keyword.ToLower()) || a.yfmc.ToLower().Contains(keyword.ToLower())).ToList();
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

        /// <summary>
        /// 科室 下拉 
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectDepartmentList(string keyword)
        {
            var data = _sysDepartmentRepo.GetList(this.OrganizeId, "1", keyword);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 诊断 检索
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public JsonResult GetDiagnosisList(string keyword,string zdlx)
        {
            var list = _sysDiagnosisRepo.GetList(this.OrganizeId, keyword, zdlx);
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

    }
}