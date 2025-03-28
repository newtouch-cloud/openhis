using Newtouch.Common;
using Newtouch.Core.Common.Interface;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Domain.IRepository;

namespace Newtouch.HIS.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class StaffController : ControllerBase
    {
        private readonly ISysStaffRepo _sysStaffRepo;
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
        private readonly ISysDepartmentRepo _sysDepartmentRepo;
        private readonly ISysUserExDmnService _sysUserDmnService;
        private readonly ICache _cache;


        /// <summary>
        /// 关联用户
        /// </summary>
        /// <returns></returns>
        public ActionResult Selector()
        {
            return View();
        }

        /// <summary>
        /// 获取机构人员树 三级（机构+科室+人员）
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetStaffSelecotrTree(string organizeId = null, string keyValue = null, string from = null, bool isShowEmpty = false, bool isExpand = true, int cacheTime = 30, string initIdSelected = null)
        {
            if (string.IsNullOrWhiteSpace(organizeId))
            {
                organizeId = base.GetAuthOrganizeId();  //默认 权限OrganizeId
            }
            //缓存start
            var treeList = _cache.Get<List<TreeViewModel>>(string.Format(CacheKey.OrganizeStaffTreeSetKey, organizeId), () =>
            {
                var organizedata = _sysOrganizeDmnService.GetValidListByParentOrg(organizeId);
                var iitreeList = new List<TreeViewModel>();
                foreach (var orgItem in organizedata)
                {
                    //机构科室集合
                    var orgDetpData = _sysDepartmentRepo.GetList(orgItem.Id);
                    //机构人员集合
                    var orgStaffData = _sysUserDmnService.GetStaffListByOrg(orgItem.Id);
                    #region 机构下所有科室
                    foreach (var deptItem in orgDetpData)
                    {
                        var deptStaffData = orgStaffData.Where(p => p.DepartmentCode == deptItem.Code).ToList();
                        #region 机构下所有用户
                        foreach (var staffItem in deptStaffData)
                        {
                            TreeViewModel staffTree = new TreeViewModel();
                            staffTree.id = staffItem.Id;
                            staffTree.text = staffItem.Name;
                            staffTree.value = staffItem.gh;
                            staffTree.parentId = deptItem.Id;
                            staffTree.hasChildren = false;
                            staffTree.showcheck = true;
                            staffTree.checkstate = 0;   //外面赋值，方便缓存
                            iitreeList.Add(staffTree);
                        }
                        #endregion
                        TreeViewModel deptTree = new TreeViewModel();
                        deptTree.id = deptItem.Id;
                        deptTree.text = deptItem.Name;
                        deptTree.value = deptItem.Code;
                        deptTree.parentId = deptItem.ParentId == null ? orgItem.Id : deptItem.ParentId;
                        deptTree.isexpand = true;  //默认不展开，人太多
                        deptTree.complete = true;
                        deptTree.showcheck = false;
                        deptTree.hasChildren = deptStaffData.Count > 0 || orgDetpData.Any(p => p.ParentId == deptItem.Id);
                        iitreeList.Add(deptTree);
                    }
                    #endregion
                    TreeViewModel tree = new TreeViewModel();
                    tree.id = orgItem.Id;
                    tree.text = orgItem.Name;
                    tree.value = orgItem.Code;
                    tree.parentId = orgItem.Id == organizeId ? null : orgItem.ParentId; //特殊
                    tree.isexpand = isExpand;
                    tree.complete = true;
                    tree.showcheck = false;
                    tree.hasChildren = orgDetpData.Count > 0 || orgStaffData.Count > 0 || organizedata.Any(p => p.ParentId == orgItem.Id);
                    iitreeList.Add(tree);
                }
                return iitreeList;
            }, cacheTime);
            //缓存end
            IList<string> checkedStaffIdList = new List<string>();
            //if (from == "userrelatioinstaff" && !string.IsNullOrWhiteSpace(keyValue))   //用户已关联人员
            //{
            //    checkedStaffIdList = _sysStaffRepo.GetCurStaffIdListByUserId(keyValue);
            //}

            var initIdSelectedArr = (initIdSelected ?? "").Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            checkedStaffIdList = checkedStaffIdList.Union(initIdSelectedArr).Distinct().ToList();

            if (checkedStaffIdList != null && checkedStaffIdList.Count > 0)
            {
                foreach (var treeItem in treeList)
                {
                    if (treeItem.showcheck && checkedStaffIdList.Contains(treeItem.id))
                    {
                        treeItem.checkstate = 1;
                        //同时让上级展开
                        var thisParentNode = treeList.Where(p => p.id == treeItem.parentId).FirstOrDefault();
                        if (thisParentNode != null)
                        {
                            thisParentNode.isexpand = true;  //下级有选中的，这里一定展开之
                        }
                    }
                }
            }
            if (!isShowEmpty)   //不显示多余的
            {
                var count = 1;  //移除的行数
                while (count > 0)
                {
                    count = treeList.RemoveAll(p => !p.showcheck && !p.hasChildren);
                    //重新调整下树 移除 没有下级（下级刚被移了） 又 不显示showcheck的
                    treeList.RemoveAll(p => !p.showcheck && !treeList.Any(sub => sub.parentId == p.id));
                }
            }
            return Content(treeList.TreeViewJson(null));
        }

    }
}