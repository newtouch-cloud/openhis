using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Web;
using Newtouch.MR.ManageSystem.Domain.IDomainServices;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.MR.ManageSystem.Web.Areas.SystemManage.Controllers
{
    public class CommonController : OrgControllerBase
    {
#pragma warning disable CS0649 // Field 'CommonController._CommonDmnService' is never assigned to, and will always have its default value null
        private readonly ICommonDmnService _CommonDmnService;
#pragma warning restore CS0649 // Field 'CommonController._CommonDmnService' is never assigned to, and will always have its default value null
#pragma warning disable CS0649 // Field 'CommonController._sysDiagnosisRepo' is never assigned to, and will always have its default value null
        private readonly ISysDiagnosisRepo _sysDiagnosisRepo;
#pragma warning restore CS0649 // Field 'CommonController._sysDiagnosisRepo' is never assigned to, and will always have its default value null
#pragma warning disable CS0649 // Field 'CommonController._SysDepartmentRepo' is never assigned to, and will always have its default value null
        private readonly ISysDepartmentRepo _SysDepartmentRepo;
#pragma warning restore CS0649 // Field 'CommonController._SysDepartmentRepo' is never assigned to, and will always have its default value null

        /// <summary>
        /// 获取系统诊断列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDiagList()
        {
            var ety = _CommonDmnService.ZdList(OrganizeId, "");
            return Content(ety.ToJson());
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
        /// 手术 检索
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>	
        public JsonResult GetOperationList(string keyword, bool type)
        {
            var list = _CommonDmnService.OpList(this.OrganizeId, keyword, type);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 麻醉方式
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAnesList(string keyword)
        {
            var ety = _CommonDmnService.AnesList(OrganizeId, keyword);
            return Content(ety.ToJson());
        }

        public ActionResult GetNotchGradeList(string keyword)
        {
            var ety = _CommonDmnService.NotchGradeList(OrganizeId, keyword);
            return Content(ety.ToJson());
        }

        public ActionResult GetCommonList(string keyword,string type)
        {
            var ety = _CommonDmnService.DicCommonList(OrganizeId, keyword,type);
            return Content(ety.ToJson());
        }

        public ActionResult GetDepartment(string keyword)
        {
            var ety = _SysDepartmentRepo.GetList(this.OrganizeId, "1",keyword);
            return Content(ety.ToJson());
        }

        public ActionResult GetXtbrxz(string keyword)
        {
            var ety = _CommonDmnService.BrxzList(this.OrganizeId, keyword);
            return Content(ety.ToJson());
        }
        /// <summary>
        /// 国籍
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetNationality(string keyword)
        {
            var ety = _CommonDmnService.DicNationalityList(this.OrganizeId, keyword);
            return Content(ety.ToJson());
        }
        /// <summary>
        /// 民族
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetNationas(string keyword)
        {
            var ety = _CommonDmnService.DicNationsList(this.OrganizeId, keyword);
            return Content(ety.ToJson());
        }

        /// <summary>
        /// 岗位人员列表
        /// </summary>
        /// <param name="dutyCode"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetStaffListByDutyCode(string dutyCode, string keyword)
        {
            var list = _CommonDmnService.GetStaffByDutyCode(this.OrganizeId, dutyCode, keyword);
            return Content(list.ToJson());
        }

        public ActionResult GetHisSfdl(string keyword)
        {
            var list = _CommonDmnService.GetHisSfdl(OrganizeId,keyword);
            return Content(list.ToJson());
        }
    }
}