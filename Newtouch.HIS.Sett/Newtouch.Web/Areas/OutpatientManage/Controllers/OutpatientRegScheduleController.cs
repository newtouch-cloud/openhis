using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Common.Operator;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.IRepository.OutpatientManage;
using Newtouch.Tools;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.OutpatientManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class OutpatientRegScheduleController : ControllerBase
    {
        private readonly ISysDepartmentRepo _sysDepartmentRepo;
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly ISysRegistSpecialDiseaseRepo _sysRegistSpecialDiseaseRepo;
        private readonly IOutpatientRegistScheduleRepo _outpatientRegistScheduleRepo;
        private readonly IOutPatientDmnService _outPatientDmnService;

        public override ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //[HandlerAuthorize]
        public override ActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// 获取科室
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSysDepartmentList()
        {
            var data = _sysDepartmentRepo.GetList(OperatorProvider.GetCurrent().OrganizeId, "1");
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取医生
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="dutyCode"></param>
        /// <returns></returns>
        public ActionResult GetStaffByDutyCode()
        {
            var data = _sysUserDmnService.GetStaffByDutyCode(OperatorProvider.GetCurrent().OrganizeId, "Doctor");
            return Json(data);
        }

        /// <summary>
        /// 获取挂号专病
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectSysChargeItemByghzbList()
        {
            var data = _sysRegistSpecialDiseaseRepo.SelectSysChargeItemByghzbList(OperatorProvider.GetCurrent().OrganizeId);
            return Json(data);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ghpbId"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(OutpatientRegistScheduleEntity entity, int? ghpbId)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            _outpatientRegistScheduleRepo.SubmitForm(entity, ghpbId);
            return Success("操作成功。");
        }

        /// <summary>
        /// 获取有效列表
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ActionResult GetOutpatientRegistScheduleList(string keyword, string organizeId)
        {
            var data = _outPatientDmnService.GetOutpatientRegistScheduleList(organizeId, keyword);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ActionResult GetFormJson(int ghpbId)
        {
            var entity = _outPatientDmnService.GetOutpatientRegistScheduleList(null, null, ghpbId).FirstOrDefault();
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ActionResult DeleteForm(int ghpbId, string OrganizeId)
        {
            _outpatientRegistScheduleRepo.DeleteForm(ghpbId, OrganizeId);
            return Success("操作成功。");
        }


    }
}