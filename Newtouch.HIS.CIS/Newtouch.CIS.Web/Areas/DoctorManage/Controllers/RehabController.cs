using FrameworkBase.MultiOrg.Web;
using Newtouch.Domain.IDomainServices;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Areas.DoctorManage.Controllers
{
    public class RehabController : OrgControllerBase
	{
        private readonly IDoctorserviceDmnService _iDoctorserviceDmnService;
        public ActionResult GetSfxmZxksSelectJson(string sfxmCode)
        {
            var Zxks = _iDoctorserviceDmnService.GetSfxmZxksSelectJson(OrganizeId);
            return Content(Zxks.ToJson());
        }
        public ActionResult GetSfxmZxksSelectJson2(string sfxmCode, string keyword)
       {
            var Zxks = _iDoctorserviceDmnService.GetSfxmZxksSelectJson(OrganizeId, keyword);
            return Content(Zxks.ToJson());
        }
    }
}