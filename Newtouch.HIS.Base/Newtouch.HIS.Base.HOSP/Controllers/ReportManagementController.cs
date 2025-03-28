using Newtouch.Common;
using Newtouch.Common.Operator;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Utils;

namespace Newtouch.HIS.Base.HOSP.Controllers
{
    /// <summary>
    /// 药房窗口
    /// </summary>
    public class ReportManagementController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ISysReportDmnService _sysReportDmnService;
        
        public ReportManagementController(ISysReportDmnService ReportDmnService)
        {
            this._sysReportDmnService = ReportDmnService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Form()
        {
            return View();
        }
        public ActionResult MianForm()
        {
            return View();
        }
        public ActionResult GetReportMXTree(string ReportId)
        {
            var ReportMXList = _sysReportDmnService.GetReportMXTree(ReportId);
            return Content(ReportMXList.ToJson());
        }
        public ActionResult GetReportMXData(string ReportId)
        {
            var ReportMXList = _sysReportDmnService.GetReportMXData(ReportId);
            return Content(ReportMXList.ToJson());
        }
        public ActionResult SubmitForm(ReortMXListVO ReortMXListVO,string Type)
        {
           var isSuccess = _sysReportDmnService.SubmitForm(ReortMXListVO, Type);
            return Success(isSuccess);
        }
        /// <summary>
        /// 新增保存主报表
        /// </summary>
        /// <param name="ReortMXListVO"></param>
        /// <returns></returns>
        public ActionResult SubmitFormMain(ReortMXListVO ReortMXListVO)
        {
            var isSuccess = _sysReportDmnService.SubmitFormMain(ReortMXListVO);
            return Success(isSuccess);
        }
        /// <summary>
        /// 停止或者开启报表
        /// </summary>
        /// <param name="ReportId"></param>
        /// <param name="ReportStatus"></param>
        /// <returns></returns>
        public ActionResult ReportStopOrON(string ReportId,string ReportStatus)
        {
            var isSuccess = _sysReportDmnService.ReportStop(ReportId, ReportStatus);
            return Success(isSuccess);
        }
        /// <summary>
        /// 报表明细删除
        /// </summary>
        /// <param name="ReportId"></param>
        /// <returns></returns>
        public ActionResult ReportDel(string ReportId)
        {
            var isSuccess = _sysReportDmnService.ReportDel(ReportId);
            return Success(isSuccess);
        }
        /// <summary>
        /// 主单据删除
        /// </summary>
        /// <param name="ReportId"></param>
        /// <returns></returns>
        public ActionResult ReportDelMain(string ReportId)
        {
            var isSuccess = _sysReportDmnService.ReportDelMain(ReportId);
            return Success(isSuccess);
        }
        /// <summary>
        /// 获取报表内容
        /// </summary>
        /// <returns></returns>
        public ActionResult GetReportTree(string keyword,string ReportType)
        {
            var data = _sysReportDmnService.GetReportTree(keyword, ReportType);
            var treeList = new List<TreeGridModel>();
            foreach (var item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Count(t => t.parentId == item.Id) == 0 ? false : true;
                treeModel.id = item.Id.ToString();
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.parentId == null ? null : item.parentId.ToString();
                treeModel.expanded = hasChildren;
                treeModel.entityJson = item.ToJson();

                treeList.Add(treeModel);

            }
            //foreach (var item in reportList)
            //{
            //    var ConcreteList = _sysReportDmnService.GetReportConcreteTree(item.ReportID);
            //    foreach (var itemct in ConcreteList)
            //    {
            //        if (itemct.ReportCode != null)
            //        {
            //            var ReportMXList = _sysReportDmnService.GetReportMXTree(itemct.ReportCode);
            //            foreach (var itemmx in ReportMXList)
            //            {
            //                TreeViewModel treemx = new TreeViewModel();
            //                treemx.id = itemmx.TemplateID.ToString();
            //                treemx.text = itemmx.ReportNameDes;
            //                treemx.value = itemmx.TemplateID.ToString();
            //                treemx.parentId = itemct.ReportName.ToString();
            //                treemx.isexpand = true;
            //                treemx.complete = true;
            //                treemx.showcheck = true;
            //                treemx.checkstate = 0;
            //                treemx.hasChildren = false;
            //                treeList.Add(treemx);
            //            }
            //        }
            //        TreeViewModel tree1 = new TreeViewModel();
            //        tree1.id = itemct.ReportCode.ToString();
            //        tree1.text = itemct.ReportName;
            //        tree1.value = itemct.ReportID.ToString();
            //        tree1.parentId = itemct.ParentId.ToString();
            //        tree1.isexpand = true;
            //        tree1.complete = true;
            //        tree1.showcheck = true;
            //        tree1.checkstate = 0;
            //        tree1.Ex1 = "r";
            //        tree1.hasChildren = true;
            //        treeList.Add(tree1);
            //    }
            //    TreeViewModel tree = new TreeViewModel();
            //    tree.id = item.ReportID.ToString();
            //    tree.text = item.SystemName;
            //    tree.value = item.ReportCode;
            //    tree.parentId = null;
            //    tree.isexpand = true;
            //    tree.complete = true;
            //    tree.showcheck = true;
            //    tree.checkstate = 0;
            //    tree.hasChildren = true;
            //    treeList.Add(tree);
            //}
            return Content(treeList.TreeGridJson(null));
        }
    }
}