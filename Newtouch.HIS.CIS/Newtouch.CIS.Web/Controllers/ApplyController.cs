using FrameworkBase.MultiOrg.Web;
using Newtouch.HIS.Domain.IRepository;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Controllers
{
    public class ApplyController : OrgControllerBase
    {

        private readonly IInspectionTemplateRepo _inspectionTemplateRepo;
        private readonly ITemplateGroupPackageRepo _templateGroupPackageRepo;
        private readonly IInspectionCategoryRepo _inspectionCategoryRepo;

        /// <summary>
        /// 检验
        /// </summary>
        /// <returns></returns>
        public ActionResult Inspection()
        {
            return View();
        }

        /// <summary>
        /// 检查
        /// </summary>
        /// <returns></returns>
        public ActionResult Examination()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}

