using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Web;
using Newtouch.Common;
using Newtouch.Common.Web;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Utils;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Domain.DTO;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects;
using Newtouch.Infrastructure;
using Newtouch.PDS.Requset.Zyypyz;
using Newtouch.Tools;

namespace Newtouch.CIS.Web.Areas.NurseManage.Controllers
{
    public class sldAPPlicationController : OrgControllerBase
    {

        //private readonly IWZsldDmnService _wZsldDmnService;
        //// GET: NurseManage/sldAPPlication
        //public ActionResult WzSldIndex()
        //{
        //    return View();
        //}

        ///// <summary>
        ///// 申领单号
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public ActionResult GetNewCkzksdh()
        //{
        //    var result = "SLD"+string.Format("{0:yyyyMMddHHmmss}", DateTime.Now);
        //    return Content(result);
        //}

        ///// <summary>
        ///// 下拉物资信息
        ///// </summary>
        ///// <param name="key"></param>
        ///// <param name="slksbm"></param>
        ///// <returns></returns>

        //[HttpGet]
        //public ActionResult DepartmentStockListQuery(string key, string slksbm)
        //{
        //    var param = new DepartmentStockListQueryParamDTO
        //    {
        //        key = key,
        //        organizeId = OrganizeId,
        //        warehouseId = slksbm,
        //        zt = "1"
        //    };
        //    var result = _wZsldDmnService.DepartmentStockListQuery(param);
        //    return Content(result.ToJson());
        //}


        ///// <summary>
        ///// 下拉物资批号批次信息
        ///// </summary>
        ///// <param name="proId">物资ID</param>
        ///// <param name="gysId">供应商ID</param>
        ///// <param name="deliveryNo">配送单号</param>
        ///// <param name="keyword"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public ActionResult ProductBatchQuery(string proId,  string slksbm, string keyword = "")
        //{
        //    return Content(_wZsldDmnService.ProductBatchQuery(proId, slksbm, OrganizeId, keyword: keyword).ToJson());
        //}

        ///// <summary>
        ///// 获取库房
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult GetDeptByKf(string keyword)
        //{
        //    var list = _wZsldDmnService.GetList(OrganizeId, keyword ?? "");
        //    return Content(list.ToJson());
        //}

        ///// <summary>
        ///// 获取病区
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult GetDeptBy(string keyword)
        //{
        //    var list = _wZsldDmnService.GetDeptList(OrganizeId, keyword ?? "");
        //    return Content(list.ToJson());
        //}
    }
}