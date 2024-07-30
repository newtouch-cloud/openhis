using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FrameworkBase.MultiOrg.Web;
using Newtouch.OR.ManageSystem.Infrastructure;

namespace Newtouch.OR.ManageSystem.Web.Areas.SystemManage.Controllers
{
    public class CommonController : OrgControllerBase
    {
        // GET: SystemManage/Common
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult GetNewFieldUniqueValueDB(string fieldName)
        {
            var data = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValueDB("[NewtouchHIS_Sett].dbo.", fieldName, this.OrganizeId,"" , true);
            return Success("", data); ;
        }
    }
}