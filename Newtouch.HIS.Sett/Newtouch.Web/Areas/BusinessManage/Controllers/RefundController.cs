using Newtouch.Common.Exceptions;
using Newtouch.HIS.Application;
using Newtouch.HIS.Application.Interface.BusinessManage;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.BusinessManage.Controllers
{
    public class RefundController : ControllerBase
    {
        // GET: BusinessManage/Refund

        private IRefundApp _IRefundApp;

        public RefundController(IRefundApp RefundApp)
        {
            this._IRefundApp = RefundApp;
        }
      
        /// <summary>
        /// 查发票号
        /// </summary>
        /// <param name="kh"></param>
        /// <returns></returns>
        public ActionResult btnQueryFph(string kh)
        {

            var data = _IRefundApp.GetFPHByKh(kh);
            if (data.ToList().Count > 0)
            {
                
                return Success("", data);
            }
            else
            {
                return Error("查找不到结算信息！");
            }

        }
        /// <summary>
        /// 根据发票的结算内码查询门诊挂号项目
        /// </summary>
        /// <param name="jsnm">结算内码</param>
        /// <returns></returns>
        public ActionResult GetMZJSByJsnm(int jsnm)
        {
            if (jsnm == 0)
            {
                return Error("数据不全，请确认");
            }
            var data = _IRefundApp.GetMZJSByJsnm(jsnm);
            return Success("", data);

        }
        /// <summary>
        /// 按结算内码查门诊项目
        /// </summary>
        /// <param name="jsnm">结算内码</param>
        /// <returns></returns>
        public ActionResult GetGridViewMxByJsnm(int jsnm)
        {
            var data = GetGridViewMx(jsnm);
            //var test = new List<RefundVO>();
            //data.ForEach(p => { p.IS_RETURN = false; test.Add(p); });
            return Success("", data);
        }
        public List<GridViewMx> GetGridViewMx(int jsnm)
        {
            return _IRefundApp.GetGridViewMx(jsnm);
        }
        /// <summary>
        /// 退费
        /// </summary>
        public ActionResult btnReturn(string kh,List<GridViewMx> GridViewMx, int jsnm)
        {
            if (jsnm == 0)
            {
                return Error("数据不全，请确认");
            }
            var result = _IRefundApp.btnReturn(kh, GridViewMx, jsnm);

             return Success("", result);
        }
     
  
    }
}