using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Application.Interface.BusinessManage;
using Newtouch.HIS.Domain.ValueObjects;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.OutpatientManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class RefundController : ControllerBase
    {
        // GET: BusinessManage/Refund

        private readonly IRefundApp _IRefundApp;

        /// <summary>
        /// 发票列表
        /// </summary>
        /// <returns></returns>
        public ActionResult QueFpList()
        {
            return View();
        }


        /// <summary>
        /// 查发票号
        /// </summary>
        /// <param name="kh"></param>
        /// <returns></returns>
        public ActionResult btnQueryFph(string kh, string startTime, string endTime)
        { 

            var data = _IRefundApp.GetFPHByKh(kh, startTime, endTime, "");
            return Success("", data);
   
        }
        /// <summary>
        /// 根据发票的结算内码查询门诊挂号项目
        /// </summary>
        /// <param name="jsnm">结算内码</param>
        /// <returns></returns>
        public ActionResult GetMZJSByJsnm(int jsnm)
        {
 
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
            var data = _IRefundApp.GetGridViewMx(jsnm);
            foreach (var da in data)
            {
                da.IS_RETURN = da.IS_RETURN + "," + da.czh;

            }
            return Success("", data);
        }

        /// <summary>
        /// 退费
        /// </summary>
        public ActionResult btnReturn(string kh, int jsnm, string refundData)
        { 
            bool isReturnAll = true;
            var GridViewMx = Tools.Json.ToList<GridViewMx>(refundData);
            //foreach (var da in GridViewMx)
            //{
            //    string[] sArray = da.IS_RETURN.Split(',');

            //    da.IS_RETURN = sArray[0];

            //    //方便挂号项目一起选中一起取消而设置的特殊成组号
            //    if (sArray[1]== "gh0209")
            //    {
            //        da.czh = "0";
            //    }

            //}
            var result = _IRefundApp.btnReturn(kh, GridViewMx, jsnm, out isReturnAll);
            if (result)
            {
                return Success("", isReturnAll);
            }
            else
            {
                throw new FailedCodeException("HOSP_REFUNG_FAIL");

            }
        }
        //public ActionResult SaveData(object rowData)
        //{

        //   return Success("", rowData);
        //}
        /////*************************************
        /////
        //public ActionResult refund()
        //{

        //    return View();
        //}
        //public string  refundData()
        //{

        //    var data = _IRefundApp.GetGridViewMx(225);

        //    return Tools.Json.ToString(data);
        //}
        //public string EditSave(string song)
        //{
        //    var show = Tools.Json.ToList<GridViewMx>(song);
        //    return "OK";

        //}
        //public void EditSave()
        //{


        //}
    }
}