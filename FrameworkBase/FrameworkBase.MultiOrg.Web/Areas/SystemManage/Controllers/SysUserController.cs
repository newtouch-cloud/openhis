using Newtouch.Tools;
using System.Web.Mvc;
using Newtouch.Common;
using System.Collections.Generic;
using System.Linq;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Common.Model;
using Newtouch.Core.Common;
using System;

namespace FrameworkBase.MultiOrg.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 系统用户
    /// </summary>
    [AutoResolveIgnore]
    public class SysUserController : OrgControllerBase
    {
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
        private readonly ISysDepartmentRepo _sysDepartmentRepo;
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly IUserRoleAuthDmnService _userRoleAuthDmnService;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="sysOrganizeDmnService"></param>
        /// <param name="sysDepartmentRepo"></param>
        /// <param name="sysUserDmnService"></param>
        /// <param name="userRoleAuthDmnService"></param>
        public SysUserController(ISysOrganizeDmnService sysOrganizeDmnService
            , ISysDepartmentRepo sysDepartmentRepo
            , ISysUserDmnService sysUserDmnService
            , IUserRoleAuthDmnService userRoleAuthDmnService)
        {
            this._sysOrganizeDmnService = sysOrganizeDmnService;
            this._sysDepartmentRepo = sysDepartmentRepo;
            this._sysUserDmnService = sysUserDmnService;
            this._userRoleAuthDmnService = userRoleAuthDmnService;
        }

        /// <summary>
        /// 用户树 视图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult Selector()
        {
            return View();
        }

        /// <summary>
        /// 获取系统用户树 三级（机构+科室+用户）
        /// 要求 用户已关联人员
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="keyValue"></param>
        /// <param name="from"></param>
        /// <param name="isExpand"></param>
        /// <param name="isContansChildOrg"></param>
        /// <param name="isShowEmpty">是否显示空白节点（科室）</param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public virtual ActionResult GetSysUserSelectorTree(string organizeId = null, string keyValue = null, string from = null, bool isExpand = true, bool isContansChildOrg = true
            , bool isShowEmpty = false)
        {
            return ComGetSysUserSelectorTree(organizeId, keyValue, from,
                isExpand, isContansChildOrg, isShowEmpty);
        }

        /// <summary>
        /// 个人信息 视图
        /// </summary>
        /// <returns></returns>
        public ActionResult PersonalInfo()
        {
            return View();
        }

        /// <summary>
        /// 提交 重置个人密码
        /// </summary>
        /// <param name="newpwd"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SubmitPersonalPassword(string newpwd)
        {
            _sysUserDmnService.RevisePassword(newpwd, this.UserIdentity.UserId);
            return Success("重置密码成功。");
        }

        /// <summary>
        /// 用户列表 grid json
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetPagintionGridJson(Pagination pagination, string keyword, string organizeId)
        {
            if (string.IsNullOrWhiteSpace(organizeId))
            {
                return Error("定位当前权限内的组织机构失败");
            }
            var data = new
            {
                rows = _sysUserDmnService.GetPagintionUserList(pagination, organizeId, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 角色选择
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Roles()
        {
            return View();
        }

        /// <summary>
        /// 更新 用户角色
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="organizeId"></param>
        /// <param name="roleList"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult UpdateUserRole(string keyValue, string organizeId, string roleList)
        {
            _userRoleAuthDmnService.UpdateUserRole(keyValue, organizeId, (roleList ?? "").Split(','));
            return Success("操作成功。");
        }

        #region private methods

        /// <summary>
        /// 用户树
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="keyValue"></param>
        /// <param name="from"></param>
        /// <param name="isExpand"></param>
        /// <param name="isContansChildOrg"></param>
        /// <param name="isShowEmpty"></param>
        /// <param name="funcTreeItemFilter"></param>
        /// <param name="funcCheckedList"></param>
        /// <returns></returns>
        [NonAction]
        protected internal ActionResult ComGetSysUserSelectorTree(string organizeId = null, string keyValue = null, string from = null, bool isExpand = true, bool isContansChildOrg = true
            , bool isShowEmpty = false
            , Func<List<TreeViewModel>, List<TreeViewModel>> funcTreeItemFilter = null
            , Func<List<FirstSecond>> funcCheckedList = null)
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
                //机构人员集合（有对应登录账户）
                var orgUserStaffData = _sysUserDmnService.GetSatffVOListByOrg(orgItem.Id);
                #region 机构下所有科室
                foreach (var deptItem in orgDetpData)
                {
                    var deptUserStaffData = orgUserStaffData.Where(p => p.DepartmentCode == deptItem.Code).ToList();
                    #region 机构下所有用户
                    foreach (var userStaffVO in deptUserStaffData)
                    {
                        TreeViewModel staffTree = new TreeViewModel();
                        staffTree.id = userStaffVO.StaffId;
                        staffTree.text = userStaffVO.Name;
                        staffTree.value = userStaffVO.UserId;
                        staffTree.Code = userStaffVO.UserCode;
                        staffTree.parentId = deptItem.Id;
                        staffTree.hasChildren = false;
                        staffTree.showcheck = true;
                        staffTree.checkstate = 0;   //外面赋值，方便缓存

                        //扩展字段
                        //staffTree.Ex1 = deptItem.Code;  //科室Code
                        //staffTree.Ex2 = deptItem.Name;  //科室名称
                        staffTree.Ex3 = orgItem.Id; //closest OrganizeId

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
                #region 人员无所属科室
                foreach (var userStaffVO in orgUserStaffData.Where(p => string.IsNullOrWhiteSpace(p.DepartmentCode)).ToList())
                {
                    TreeViewModel staffTree = new TreeViewModel();
                    staffTree.id = userStaffVO.StaffId;
                    staffTree.text = userStaffVO.Name;
                    staffTree.value = userStaffVO.UserId;
                    staffTree.Code = userStaffVO.UserCode;
                    staffTree.parentId = orgItem.Id;
                    staffTree.hasChildren = false;
                    staffTree.showcheck = true;
                    staffTree.checkstate = 0;   //外面赋值，方便缓存

                    //扩展字段
                    //staffTree.Ex1 = deptItem.Code;  //科室Code
                    //staffTree.Ex2 = deptItem.Name;  //科室名称
                    staffTree.Ex3 = orgItem.Id; //closest OrganizeId

                    treeList.Add(staffTree);
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
                tree.hasChildren = true;
                treeList.Add(tree);
            }

            if (funcTreeItemFilter != null)
            {
                treeList = funcTreeItemFilter(treeList);
            }

            IList<FirstSecond> checkedUserList = null; //已checked的用户
            if (from == "rolerelatioinuser" && !string.IsNullOrWhiteSpace(keyValue))   //角色已关联用户
            {
                checkedUserList = _userRoleAuthDmnService.GetCurUserIdListByRoleId(keyValue);
            }

            if (funcCheckedList != null)
            {
                var funccheckedListResult = funcCheckedList();
                if (funccheckedListResult != null && funccheckedListResult.Count > 0)
                {
                    checkedUserList = (checkedUserList ?? new List<FirstSecond>())
                        .Union(funccheckedListResult).ToList();
                }
            }

            if (checkedUserList != null && checkedUserList.Count > 0)
            {
                foreach (var treeItem in treeList)
                {
                    if (treeItem.showcheck && checkedUserList.Any(p => p.First == treeItem.value && p.Second == treeItem.Ex3))
                    {
                        treeItem.checkstate = 1;
                        //同时让上级展开
                        var thisParentNode = treeList.Where(p => p.id == treeItem.parentId).FirstOrDefault();
                        if (thisParentNode != null)
                        {
                            thisParentNode.isexpand = true;  //下级有选中的，这里一定展开之（科室）
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
