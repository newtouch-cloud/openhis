using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FrameworkBase.MultiOrg.Web;
using Newtouch.Common;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ValueObjects;
using Newtouch.Core.Common;
using Newtouch.Tools;
using Newtouch.Common.Operator;

namespace Newtouch.CIS.Web.Areas.NurseManage.Controllers
{
    public class OrderAuditController : OrgControllerBase
    {
        private readonly IOrderAuditDmnService _OrderAuditDmnService;
        

        public OrderAuditController(IOrderAuditDmnService OrderAuditDmnService)
        {
            this._OrderAuditDmnService = OrderAuditDmnService;
        }


        // GET: NurseManage/OrderAudit
        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ActionResult GetGridJson(Pagination pagination, string patList, string organizeId)
        {
            var data = new
            {
                rows = _OrderAuditDmnService.GetOrderAuditYZList(pagination,patList),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        //public ActionResult OrderAuditSubmit(string orderList)
        //{
        //    var cqyzList = new List<InpatientLongTermOrderEntity>();
        //    var lsyzList = new List<InpatientSTATOrderEntity>();

        //    var data = new
        //    {
        //        rows = _OrderAuditDmnService.GetOrderAuditYZList(pagination, patList),
        //        total = pagination.total,
        //        page = pagination.page,
        //        records = pagination.records,
        //    };
        //    return Content(data.ToJson());
        //}

        public ActionResult submitOrderList(IList<OrderAuditVO> orderList)
        {
            //entity.zt = entity.zt == "true" ? "1" : "0";
            OperatorModel user = this.UserIdentity;
            _OrderAuditDmnService.OrderAuditSubmit(user, orderList);
            return Success("审核成功");
        }

        public ActionResult submitOrderListbyPat(string patList,int yzxz)
        {
            OperatorModel user = this.UserIdentity;
            _OrderAuditDmnService.OrderAuditSubmitbyPat(user, patList,yzxz);
            return Success("审核成功");
        }

        /// <summary>
        /// 获取病区患者待审核医嘱树
        /// </summary>
        /// <param name="aa"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult GetPatWardTree()
        {
            string staffId = this.UserIdentity.StaffId;
            var wardTree = _OrderAuditDmnService.GetWardTree(staffId);
            var patTree = _OrderAuditDmnService.GetPatTree(staffId);

            var treeList = new List<TreeViewModel>();
            foreach (InpWardPatTreeVO item in wardTree)
            {
                var patInfo = patTree.Where(p => p.bqCode == item.bqCode).ToList();
                
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
    }
}