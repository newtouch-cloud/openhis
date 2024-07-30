using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Repository.SystemManage;
using FrameworkBase.MultiOrg.Web;
using Newtouch.Common;
using Newtouch.Core.Common.Utils;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ValueObjects;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Areas.NurseManage.Controllers
{
    public class ExceReportPrintController : OrgControllerBase
    {
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IExceReportPrintDmnService _IExceReportPrintDmnService;
        /// <summary>
        /// 获取病区人员发药树
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPatWardTree()
        {
            string staffId = this.UserIdentity.rygh;
            var wardTree = _IExceReportPrintDmnService.GetPatTree(OrganizeId,staffId);
            var wardonly = wardTree.GroupBy(p => new { p.bqCode, p.bqmc }).Select(p => new { p.Key.bqCode, p.Key.bqmc });


            var treeList = new List<TreeViewModel>();
            foreach (var item in wardonly)
            {
                var patInfo = wardTree.Where(p => p.bqCode == item.bqCode).Where(p => p.zyh != "").Where(p => p.zyh != null).ToList();

                foreach (InpWardPatTreeVO itempat in patInfo)
                {
                    TreeViewModel treepat = new TreeViewModel();
                    treepat.id = itempat.zyh;
                    treepat.text = itempat.zyh + "-" + itempat.hzxm;
                    treepat.value = itempat.zyh;
                    treepat.parentId = item.bqCode;
                    treepat.isexpand = false;
                    treepat.complete = true;
                    treepat.showcheck = true;
                    treepat.checkstate = 0;
                    treepat.hasChildren = false;
                    treepat.Ex1 = "c";
                    treepat.Ex2 = itempat.sex;
                    treepat.Ex3 = itempat.nl;
                    treepat.Ex4 = itempat.hzxm;
                    treeList.Add(treepat);
                }

                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = patInfo.Count == 0 ? false : true;
                tree.id = item.bqCode;
                tree.text = item.bqmc;
                tree.value = item.bqCode;
                tree.parentId = null;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = 0;
                tree.hasChildren = hasChildren;
                tree.Ex1 = "p";
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson(null));
        }

        public ActionResult GetExecDetailGridJson(string zyh,DateTime zxsj,string zxdlb)
        {
            if (string.IsNullOrWhiteSpace(zyh))
            {
                return null;
            }
            var data=  _IExceReportPrintDmnService.GetExecDetailGridJson(OrganizeId, zyh, zxsj, zxdlb);
            return Content(data.ToJson());
        }

        public override ActionResult Index()
        {

            //注射用法控制
            ViewBag.ControlzsyfCode = _sysConfigRepo.GetValueByCode("zsyfpz", OrganizeId);
            //雾化用法控制
            ViewBag.ControlwhyfCode = _sysConfigRepo.GetValueByCode("whyfpz", OrganizeId);
            ViewBag.OrgId = this.OrganizeId;
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            return View();
        }
        }
}