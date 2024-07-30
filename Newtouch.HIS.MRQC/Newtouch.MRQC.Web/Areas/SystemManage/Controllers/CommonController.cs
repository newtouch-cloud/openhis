using FrameworkBase.MultiOrg.Web;
using Newtouch.MRQC.Domain.IDomainServices;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.MRQC.Web.Areas.SystemManage.Controllers
{
    public class CommonController : OrgControllerBase
    {
       
        private readonly ICommonDmnService _CommonDmnService;

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
    }
}