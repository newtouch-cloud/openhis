using Newtouch.Core.Common;
using Newtouch.Core.Common.Utils;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Configuration;

namespace Newtouch.HIS.Web.Areas.OutpatientManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class OutpatientChargeQueryController : ControllerBase
    {
        // GET: BusinessManage/OutPatienChargeQuery
        private readonly IOutPatienChargeQueryApp _outPatienChargeQueryApp;
        private readonly IOutPatientChargeQueryDmnService _outPatienChargeQueryDmnService;

        /// <summary>
        /// 门诊收费查询
        /// </summary>
        /// <returns></returns>
        public ActionResult OutpatientChargeQuery()
        {
            //ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            //ViewBag.OrgId = this.OrganizeId;

            //return View();
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            ViewBag.OrgId = this.OrganizeId;
            ViewBag.CurUserCode = this.UserIdentity.UserCode;
            ViewBag.curUsergh = UserIdentity.rygh;
            return View();
        }

        public ActionResult OutpatientSettQuery()
        {
            return View();
        }

        /// <summary>
        /// 挂号收费条件查询
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="fph"></param>
        /// <param name="xm"></param>
        /// <param name="pagination"></param>
        /// <param name="createTimestart"></param>
        /// <param name="createTimeEnd"></param>
        /// <param name="syy"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult SelectRegChargeQuery(Pagination pagination, DateTime? createTimestart, DateTime? createTimeEnd, DateTime? sfrqTimestart, DateTime? sfrqTimeEnd, string kh = "", string fph = "", string jsfph = "", string xm = "", string syy = "", string zxlsh = "")
        {
            IList<OutPatientRegChargeMVO> list;
            var chargeQueryList = new
            {
                rows = (list = _outPatienChargeQueryDmnService.RegChargeQuery(pagination, kh, fph, jsfph, xm, syy, createTimestart, createTimeEnd, sfrqTimestart, sfrqTimeEnd, zxlsh)),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };

            var jsnms = string.Join(",", list.Select(p => p.jsnm).Distinct());
            if (!string.IsNullOrWhiteSpace(jsnms))
            {
                var zffsList = _outPatienChargeQueryDmnService.GetSettZffsResultList(this.OrganizeId, jsnms);
                foreach (var js in list)
                {
                    js.zffsmcstr = string.Join(",", zffsList.Where(p => p.jsnm == js.jsnm).Select(p => p.xjzffsmc));
                }
            }

            return Content(chargeQueryList.ToJson());
        }

        /// <summary>
        /// 挂号收费查询明细
        /// </summary>
        /// <param name="jsnm"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult GetRecordsByJsnm(string jsnm)
        {
            return Success(null, _outPatienChargeQueryApp.ChargeRecordsQuery(jsnm));
        }

        /// <summary>
        /// 重打/补打发票
        /// </summary>
        /// <returns></returns>
        public ActionResult OutpatientReprintOrSupplementPrint()
        {
            return View();
        }

        /// <summary>
        /// 重打/补打发票 mz_js List
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="jsnm"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="kh"></param>
        /// <param name="fph"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult LoadMzjsRecords(Pagination pagination, string jsnm, DateTime? startDate, DateTime? endDate, string kh, string yfph)
        {
            var chargeQueryList = new
            {
                rows = _outPatienChargeQueryApp.LoadMzjsRecords(pagination, jsnm, startDate, endDate, kh, yfph),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(chargeQueryList.ToJson());
        }
        /// <summary>
        /// 重打/补打发票 mz_jsmx List
        /// </summary>
        /// <param name="jsnmStr"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult LoadMzjsMXRecords(string jsnmStr)
        {
            OutPatientReprintOrSuppPrintSettleDetailDto settleDetailDto = _outPatienChargeQueryApp.LoadMzjsMXRecords(jsnmStr);
            return Success(null, settleDetailDto);

        }

        /// <summary>
        /// 打印明细
        /// </summary>
        /// <param name="jsnmStr"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult PrintMxData(string jsnmStr)
        {
            _outPatienChargeQueryApp.PrintMxData(jsnmStr);
            return Success();
        }

        /// <summary>
        /// 检查是否有可打印的数据
        /// </summary>
        /// <param name="jsnm"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult CheckPintInfo(string jsnm)
        {
            _outPatienChargeQueryApp.CheckPintInfo(jsnm);
            return Success();
        }

        /// <summary>
        /// 打印发票
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="pageFph"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult PrintInvoice(string jsnm, bool isGh)
        {
            _outPatienChargeQueryApp.PrintInvoice(jsnm, isGh);
            return Success();
        }
         
        /// <summary>
        /// 补打
        /// </summary>
        /// <param name="jsnm"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult SupplementPrint(string jsnm, bool isGh)
        {
            _outPatienChargeQueryApp.SupplementPrint(jsnm, isGh);
            return Success();
        }

        /// <summary>
        /// 重打
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="pageFph"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult RePrint(string jsnm, string pageFph, bool isGh)
        {
            _outPatienChargeQueryApp.RePrint(jsnm, pageFph, isGh);
            return Success();
        }


    }
}