using System.Web.Mvc;
using Newtouch.HIS.Domain.IDomainServices;

namespace Newtouch.HIS.Web.Areas.OutpatientManage.Controllers
{
    public class DealGuiAnNotConfirmController : ControllerBase
    {
        private readonly ICommonDmnService _commonDmnService;
        // 处理不确定交易视图
        public ActionResult DealGuiAnNotConfirm()
        {
            return View();
        }

        public ActionResult GetJyyzmByJylsh(string jylsh)
        {
            return Content(_commonDmnService.Gzyb_GetJyyzmByJylsh(jylsh, this.OrganizeId ));
        }
    }
}