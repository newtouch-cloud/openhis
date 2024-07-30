using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Repository.SystemManage;
using FrameworkBase.MultiOrg.Web;
using Newtouch.Domain.IDomainServices;
using Newtouch.Infrastructure;
using NLog.Client.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Areas.DoctorManage.Controllers
{
    
    public class ApplyController : OrgControllerBase
    {
        // GET: DoctorManage/Apply
        public ActionResult inspection()
        {
            return View();
        }
        public ActionResult Examination()
        {
            return View();
        }

        public ActionResult GetSqdhMethod() {
            var thisDate = DateTime.Now.ToString("yyyyMMdd");
            var data= EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("zy_lsyz.sqdh", OrganizeId, "{0:D6}", true);
            return Success("", thisDate + data);
        }
    }
}