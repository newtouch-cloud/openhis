﻿using Newtouch.Common;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System.Web.Mvc;
using Newtouch.Common.Operator;
using Newtouch.EMR.Domain.IDomainServices;

namespace Newtouch.EMR.Web.Controllers
{
    /// <summary>
    /// 首页
    /// </summary>
    public class HomeController : FrameworkBase.MultiOrg.Web.Controllers.HomeController
    {
        private readonly IMRHomePageDmnService _sysUserDmnService;
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public override ActionResult Index()
        {
            var userName = OperatorProvider.GetCurrent().UserCode;
            if (userName != "root")
            {
                var rygh = OperatorProvider.GetCurrent().rygh;
                ViewBag.gjybdm = _sysUserDmnService.GetYbdmByGh(rygh);
            }
            return base.Index();
        }

        /// <summary>
        /// 获取业务字段的随机产生值（自增+Format）
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="orgIdIsStar"></param>
        /// <param name="topOrgIdIsStar"></param>
        /// <param name="initFormat"></param>
        /// <param name="initFieldLength"></param>
        /// <returns></returns>
        public ActionResult GetNewFieldUniqueValue(string fieldName, string initFormat = "", int initFieldLength = 0)
        {
            var orgId = this.OrganizeId;
            if (string.IsNullOrWhiteSpace(initFormat) && initFieldLength > 0)
            {
                initFormat = "{0:D" + initFieldLength + "}";
            }
            string value = null;
            if (string.IsNullOrWhiteSpace(orgId) || initFormat == null)
            {
                value = null;
            }
            else
            {
                value = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue(fieldName, orgId, initFormat);
            }
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = value }.ToJson());
        }
    }
}
