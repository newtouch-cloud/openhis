using Newtouch.HIS.Domain.DTO.InputDto.Rehab;
using Newtouch.HIS.Domain.IDomainServices;
using System.Web.Mvc;

namespace Newtouch.HIS.Sett.Rehab.Controllers
{
    /// <summary>
    /// 病人（信息）
    /// </summary>
    public class PatientController : ControllerBase
    {
        private IPatientBasicInfoDmnService _PatientBasicInfoDmnService;
        public PatientController(IPatientBasicInfoDmnService PatientBasicInfoDmnService)
        {
            this._PatientBasicInfoDmnService = PatientBasicInfoDmnService;
        }
        public ActionResult AddPatient()
        {
            return View();
        }

        public ActionResult ShowPatientInfo()
        {
            return View();
        }

        public ActionResult GetGridJson(PatientInputDto entity)
        {
            return Content("");
        }

        public ActionResult MZRegister()
        {
            return View();
        }

        public ActionResult ZYRegister()
        {
            return View();
        }
    }
}