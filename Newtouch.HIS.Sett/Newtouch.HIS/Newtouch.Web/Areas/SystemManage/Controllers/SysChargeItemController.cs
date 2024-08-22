using System.Web.Mvc;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Core.Common.Interface;
using Newtouch.Common;
using System.Collections.Generic;
using Newtouch.Infrastructure;
using System;
using System.Linq;
using FrameworkBase.MultiOrg.Domain.IDomainServices;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Obsolete("replaced by/SystemManage/BaseData/SelectSfxmYp")]
    public class SysChargeItemController : ControllerBase
    {
        private readonly ISysChargeItemRepo _sysChargeItemRepo;
        private readonly ISysChargeItemDmnService _sysChargeItemDmnService;
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
        private readonly ICache _cache;

        public ActionResult Selector()
        {
            return View();
        }

        /// <summary>
        /// 收费项目 选择 Search
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public JsonResult GetChargeItemSelectData(string keyword, string organizeId, bool? zlfzlbz = true, string mzzybz = null, string dlCode = null)
        {
            if (string.IsNullOrWhiteSpace(organizeId))
            {
                organizeId = this.OrganizeId;
            }
            var list = _sysChargeItemDmnService.SelectSearch(keyword, organizeId, zlfzlbz, mzzybz);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="organizeId"></param>
        /// <param name="zlfzlbz"></param>
        /// <param name="mzzybz"></param>
        /// <returns></returns>
        public JsonResult GetChargeItemAndMedicineSelectData(string keyword, string organizeId, bool? zlfzlbz = true, string mzzybz = null)
        {
            if (string.IsNullOrWhiteSpace(organizeId))
            {
                organizeId = this.OrganizeId;
            }
            var list = _sysChargeItemDmnService.SelectItemAndMedicineSearch(keyword, organizeId, zlfzlbz, mzzybz);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取机构人员树 三级（机构+科室+人员）
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetChargeItemSelecotrTree(string organizeId = null, bool isExpand = true, int cacheTime = 30, string initIdSelected = null)
        {
            if (string.IsNullOrWhiteSpace(organizeId))
            {
                organizeId = base.GetAuthOrganizeId();  //默认 权限OrganizeId
            }

            IList<string> checkedStaffIdList = new List<string>();
            var initIdSelectedArr = (initIdSelected ?? "").Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            checkedStaffIdList = checkedStaffIdList.Union(initIdSelectedArr).Distinct().ToList();
            //缓存start
            var treeList = _cache.Get(string.Format(CacheKey.OrganizeStaffTreeSetKey, organizeId), () =>
            {
                var organizedata = _sysOrganizeDmnService.GetValidListByParentOrg(organizeId);
                var iitreeList = new List<TreeViewModel>();
                foreach (var orgItem in organizedata)
                {
                    //所有收费项目
                    var orgsfxmData = _sysChargeItemDmnService.GetSFDLList(organizeId);
                    foreach (var Item in orgsfxmData)
                    {
                        TreeViewModel deptTree = new TreeViewModel();
                        deptTree.id = Item.dlId.ToString();
                        deptTree.text = Item.dlmc;
                        deptTree.value = Item.dlCode;
                        deptTree.parentId = null;
                        deptTree.isexpand = isExpand;  //默认不展开，人太多
                        deptTree.complete = true;
                        deptTree.showcheck = true;
                        deptTree.hasChildren = false;
                        if (checkedStaffIdList.Contains(Item.dlCode.ToString()))
                        {
                            deptTree.checkstate = 1;
                        }
                        iitreeList.Add(deptTree);
                    }
                }
                return iitreeList;
            }, cacheTime);
            return Content(treeList.TreeViewJson(null));
        }

    }
}
