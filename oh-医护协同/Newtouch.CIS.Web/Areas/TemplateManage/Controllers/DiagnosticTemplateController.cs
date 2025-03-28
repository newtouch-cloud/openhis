using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Web;
using Newtouch.Common;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Areas.TemplateManage.Controllers
{
    public class DiagnosticTemplateController : OrgControllerBase
    {
        private readonly IComDiagnosisRepo _ComDiagnosisRepo;
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DelDiagnosticTemplate(string Id)
        {
            _ComDiagnosisRepo.DelDiagnosticTemplate(Id);
            return Success("删除成功");
        }
    }
}