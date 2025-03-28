using FrameworkBase.MultiOrg.Web;
using Newtouch.Common;
using Newtouch.Core.Common;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ValueObjects;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Areas.NurseManage.Controllers
{
    public class TreatmentPrintController : OrgControllerBase
    {
        private readonly ITreatmentPrintDmnService _iTreatmentPrintDmnService;
        /// <summary>
        /// 获取病区人员执行树
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPatWardTree()
        {

            var wardTree = _iTreatmentPrintDmnService.GetPatTree(OrganizeId);
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

        public ActionResult GetGridJson(Pagination pagination,string zyh,DateTime? kssj, DateTime? jssj,int?zyxz)
        {
            var data = new
            {
                rows = _iTreatmentPrintDmnService.GetDetailGridJson(pagination,OrganizeId, zyh,kssj, jssj,zyxz),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }
    }
}