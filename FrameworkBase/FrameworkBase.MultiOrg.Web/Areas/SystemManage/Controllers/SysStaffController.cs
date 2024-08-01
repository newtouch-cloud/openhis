using Newtouch.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using Newtouch.Core.Common;
using Newtouch.Tools;

namespace FrameworkBase.MultiOrg.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 系统人员
    /// </summary>
    [AutoResolveIgnore]
    public class SysStaffController : OrgControllerBase
    {
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
        private readonly ISysDepartmentRepo _sysDepartmentRepo;
        private readonly ISysUserDmnService _sysUserDmnService;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="sysOrganizeDmnService"></param>
        /// <param name="sysDepartmentRepo"></param>
        /// <param name="sysUserDmnService"></param>
        public SysStaffController(ISysOrganizeDmnService sysOrganizeDmnService
            , ISysDepartmentRepo sysDepartmentRepo
            , ISysUserDmnService sysUserDmnService)
        {
            this._sysOrganizeDmnService = sysOrganizeDmnService;
            this._sysDepartmentRepo = sysDepartmentRepo;
            this._sysUserDmnService = sysUserDmnService;
        }

        /// <summary>
        /// 人员树 视图
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Selector()
        {
            return View();
        }

        /// <summary>
        /// 获取机构人员树 三级（机构+科室+人员）
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="keyValue"></param>
        /// <param name="from"></param>
        /// <param name="isExpand"></param>
        /// <param name="initIdSelected"></param>
        /// <param name="isContansChildOrg"></param>
        /// <param name="dutyCode">岗位编码（仅某岗位人员）</param>
        /// <param name="isShowEmpty">是否显示空白节点（科室节点）</param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public virtual ActionResult GetStaffSelecotrTree(string organizeId = null, string keyValue = null, string from = null, bool isExpand = true, string initIdSelected = null, bool isContansChildOrg = true
            , string dutyCode = null, bool isShowEmpty = false)
        {
            if (string.IsNullOrWhiteSpace(organizeId))
            {
                organizeId = base.GetAuthOrganizeId();  //默认 权限OrganizeId
            }

            var organizedata = _sysOrganizeDmnService.GetValidListByParentOrg(organizeId);
            if (!isContansChildOrg)
            {
                //仅当前机构
                organizedata = organizedata.Where(p => p.Id == organizeId).ToList();
            }
            var treeList = new List<TreeViewModel>();
            foreach (var orgItem in organizedata)
            {
                //机构科室集合
                var orgDetpData = _sysDepartmentRepo.GetList(orgItem.Id, "1");
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

                        //扩展字段
                        staffTree.Ex1 = deptItem.Code;  //科室Code
                        staffTree.Ex2 = deptItem.Name;  //科室名称
                        treeList.Add(staffTree);
                    }
                    #endregion
                    TreeViewModel deptTree = new TreeViewModel();
                    deptTree.id = deptItem.Id;
                    deptTree.text = deptItem.Name;
                    deptTree.value = deptItem.Code;
                    deptTree.parentId = deptItem.ParentId == null ? orgItem.Id : deptItem.ParentId;
                    deptTree.isexpand = isExpand;  //默认不展开，人太多
                    deptTree.complete = true;
                    deptTree.showcheck = false;
                    deptTree.hasChildren = true;
                    deptTree.Ex6 = "dept";  //仅下面用，意 这是科室节点
                    treeList.Add(deptTree);
                }
                #endregion
                TreeViewModel tree = new TreeViewModel();
                tree.id = orgItem.Id;
                tree.text = orgItem.Name;
                tree.value = orgItem.Code;
                tree.parentId = orgItem.Id == organizeId ? null : orgItem.ParentId; //特殊
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = false;
                tree.hasChildren = true;
                treeList.Add(tree);
            }

            if (!string.IsNullOrWhiteSpace(dutyCode))
            {
                //仅指定岗位的人员
                var dutyStaffIdList = _sysUserDmnService.GetStaffIdListByDutyAndParentOrg(organizeId, dutyCode);
                treeList = treeList.Where(p => !p.showcheck || dutyStaffIdList.Contains(p.id)).ToList();
            }

            IList<string> checkedStaffIdList = new List<string>();

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
                    count = treeList.RemoveAll(p => p.Ex6 == "dept" && !treeList.Any(t => t.parentId == p.id));
                }
            }

            return Content(treeList.TreeViewJson(null));
        }


        #region 岗位人员列表

        /// <summary>
        /// 岗位人员列表
        /// </summary>
        /// <param name="dutyCode"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetStaffListByDutyCode(string dutyCode, string keyword)
        {
            var list = _sysUserDmnService.GetStaffByDutyCode(this.OrganizeId, dutyCode, keyword);
            return Content(list.ToJson());
        }

        #endregion

        #region private 

        /// <summary>
        /// 人员树
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="keyValue"></param>
        /// <param name="from"></param>
        /// <param name="isExpand"></param>
        /// <param name="initIdSelected"></param>
        /// <param name="isContansChildOrg"></param>
        /// <param name="dutyCode"></param>
        /// <param name="isShowEmpty"></param>
        /// <param name="funcTreeItemFilter"></param>
        /// <param name="funcCheckedList"></param>
        /// <returns></returns>
        [NonAction]
        protected internal ActionResult ComGetStaffSelecotrTree(string organizeId = null, string keyValue = null, string from = null, bool isExpand = true, string initIdSelected = null, bool isContansChildOrg = true
            , string dutyCode = null, bool isShowEmpty = false
            , Func<List<TreeViewModel>, List<TreeViewModel>> funcTreeItemFilter = null
            , Func<List<string>> funcCheckedList = null)
        {
            if (string.IsNullOrWhiteSpace(organizeId))
            {
                organizeId = base.GetAuthOrganizeId();  //默认 权限OrganizeId
            }

            var organizedata = _sysOrganizeDmnService.GetValidListByParentOrg(organizeId);
            if (!isContansChildOrg)
            {
                //仅当前机构
                organizedata = organizedata.Where(p => p.Id == organizeId).ToList();
            }
            var treeList = new List<TreeViewModel>();
            foreach (var orgItem in organizedata)
            {
                //机构科室集合
                var orgDetpData = _sysDepartmentRepo.GetList(orgItem.Id, "1");
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

                        //扩展字段
                        staffTree.Ex1 = deptItem.Code;  //科室Code
                        staffTree.Ex2 = deptItem.Name;  //科室名称
                        treeList.Add(staffTree);
                    }
                    #endregion
                    TreeViewModel deptTree = new TreeViewModel();
                    deptTree.id = deptItem.Id;
                    deptTree.text = deptItem.Name;
                    deptTree.value = deptItem.Code;
                    deptTree.parentId = deptItem.ParentId == null ? orgItem.Id : deptItem.ParentId;
                    deptTree.isexpand = isExpand;  //默认不展开，人太多
                    deptTree.complete = true;
                    deptTree.showcheck = false;
                    deptTree.hasChildren = true;
                    deptTree.Ex6 = "dept";  //仅下面用，意 这是科室节点
                    treeList.Add(deptTree);
                }
                #endregion
                TreeViewModel tree = new TreeViewModel();
                tree.id = orgItem.Id;
                tree.text = orgItem.Name;
                tree.value = orgItem.Code;
                tree.parentId = orgItem.Id == organizeId ? null : orgItem.ParentId; //特殊
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = false;
                tree.hasChildren = true;
                treeList.Add(tree);
            }

            if (funcTreeItemFilter != null)
            {
                treeList = funcTreeItemFilter(treeList);
            }

            if (!string.IsNullOrWhiteSpace(dutyCode))
            {
                //仅指定岗位的人员
                var dutyStaffIdList = _sysUserDmnService.GetStaffIdListByDutyAndParentOrg(organizeId, dutyCode);
                treeList = treeList.Where(p => !p.showcheck || dutyStaffIdList.Contains(p.id)).ToList();
            }

            IList<string> checkedStaffIdList = new List<string>();

            var initIdSelectedArr = (initIdSelected ?? "").Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            checkedStaffIdList = checkedStaffIdList.Union(initIdSelectedArr).Distinct().ToList();

            if (funcCheckedList != null)
            {
                var funccheckedListResult = funcCheckedList();
                if (funccheckedListResult != null && funccheckedListResult.Count > 0)
                {
                    checkedStaffIdList = (checkedStaffIdList ?? new List<string>())
                        .Union(funccheckedListResult).ToList();
                }
            }

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
                    count = treeList.RemoveAll(p => p.Ex6 == "dept" && !treeList.Any(t => t.parentId == p.id));
                }
            }

            return Content(treeList.TreeViewJson(null));
        }

        #endregion

    }
}