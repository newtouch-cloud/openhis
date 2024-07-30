using Newtouch.Core.Common;
using Newtouch.HIS.Domain.IDomainServices.OutpatientManage;
using Newtouch.HIS.Domain.ValueObjects.OutpatientManage;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.OutpatientManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class CancelOutpatientRegController : ControllerBase
    {
        private readonly ICancelOutpatientRegDmnService _cancelOutpatientRegDmnService;

        // GET: OutpatientManage/CancelRegister
        public ActionResult CancelRegister()
        {
            return View();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="blh"></param>
        /// <param name="xm"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        public ActionResult SelectRegisterlist(Pagination pagination, string blh, string xm, DateTime? kssj, DateTime? jssj)
        {
            var list = new
            {
                rows = _cancelOutpatientRegDmnService.SelectRegisterlist(pagination, this.OrganizeId, blh, xm, kssj, jssj),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        /// <summary>
        /// 取消挂号
        /// </summary>
        /// <param name="ghnm"></param>
        /// <returns></returns>
        public ActionResult SaveCancelRegister(List<SaveCancelRegisterGhnmVO> list)
        {
            _cancelOutpatientRegDmnService.SaveCancelRegister(this.OrganizeId, list);
            return Success();
        }
    }
}