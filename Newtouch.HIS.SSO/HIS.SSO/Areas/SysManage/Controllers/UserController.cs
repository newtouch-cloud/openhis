using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Framework.Web.Controllers.SysManage;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Framework.Filter;

namespace HIS.SSO.Areas.SysManage.Controllers
{
    [Area("SysManage")]
    public class UserController :  OrgControllerBase
    {
        private readonly ISysRoleDmnService _sysRoleDmnService;
        private readonly ISysDepartmentDmnService _sysDepartmentDmnService;
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly IUserRoleAuthDmnService _userRoleAuthDmnService;
        private readonly ISysOrgDmnService _sysOrgDmnService;

        public UserController(ISysRoleDmnService sysRoleDmnService, ISysDepartmentDmnService sysDepartmentDmnService, ISysUserDmnService sysUserDmnService, IUserRoleAuthDmnService userRoleAuthDmnService, ISysOrgDmnService sysOrgDmnService)
        {
            _sysRoleDmnService = sysRoleDmnService;
            _sysDepartmentDmnService = sysDepartmentDmnService;
            _sysUserDmnService = sysUserDmnService;
            _userRoleAuthDmnService = userRoleAuthDmnService;
            _sysOrgDmnService = sysOrgDmnService;
        }
        /// <summary>
        /// 列表页
        /// </summary>
        /// <returns></returns>
        public IActionResult Selector()
        {
            ViewBag.OrganizeId = this.UserIdentity?.OrganizeId;
            ViewBag.TopOrganizeId = this.UserIdentity?.TopOrganizeId;
            return View();
        }
        public IActionResult UserRoles()
        {
            ViewBag.OrganizeId = this.UserIdentity?.OrganizeId;
            ViewBag.TopOrganizeId = this.UserIdentity?.TopOrganizeId;
            return View();
        }
        public async Task<ActionResult> GetPagintionGridJson(string organizeId, string keyValue = null)
        {
            var data = await _sysUserDmnService.GetSatffVOListByOrgorKeyvalue(OrganizeId, keyValue);
            return Content(data.ToJson());

        }
            /// <summary>
            /// 获取系统用户树 三级（机构+科室+用户（人员））
            /// </summary>
            /// <returns></returns>
            public async Task<ActionResult> GetSysUserSelectorTree(string organizeId = null, string keyValue = null, string from = null, bool isShowEmpty = false, bool isExpand = true, bool isContansChildOrg = true)
        {
            if (string.IsNullOrWhiteSpace(organizeId))
            {
                organizeId = base.GetAuthOrganizeId();  //默认 权限OrganizeId
            }
            //缓存start
            var organizedata = await _sysOrgDmnService.GetOrganizeTree(organizeId);
            if (!isContansChildOrg)
            {
                //仅当前机构
                organizedata = organizedata.Where(p => p.OrganizeId == organizeId).ToList();
            }
            var treeList = new List<TreeViewModel>();
            foreach (SysOrgVo orgItem in organizedata)
            {
                //机构科室集合
                var orgDetpData = await _sysDepartmentDmnService.GetListByOrg(orgItem.OrganizeId);
                //机构人员集合（有对应登录账户）
                var orgUserStaffData = await _sysUserDmnService.GetSatffVOListByOrg(orgItem.OrganizeId, this.UserIdentity.TopOrganizeId);
                #region 机构下所有科室
                foreach (SysDepartmentEntity deptItem in orgDetpData)
                {
                    var deptUserStaffData = orgUserStaffData.Where(p => p.DepartmentCode == deptItem.Code).ToList();
                    #region 机构下所有用户
                    foreach (SysUserStaffVO userStaffVO in deptUserStaffData)
                    {
                        TreeViewModel staffTree = new TreeViewModel();
                        staffTree.id = userStaffVO.StaffId;
                        staffTree.text = userStaffVO.Name;
                        staffTree.value = userStaffVO.UserId;
                        staffTree.parentId = deptItem.Id;
                        staffTree.Code = userStaffVO.UserCode;
                        staffTree.hasChildren = false;
                        staffTree.showcheck = true;
                        staffTree.checkstate = 0;   //外面赋值，方便缓存

                        staffTree.Ex3 = orgItem.OrganizeId; //closest OrganizeId

                        treeList.Add(staffTree);
                    }
                    #endregion
                    TreeViewModel deptTree = new TreeViewModel();
                    deptTree.id = deptItem.Id;
                    deptTree.text = deptItem.Name;
                    deptTree.value = deptItem.Code;
                    deptTree.parentId = deptItem.ParentId == null ? orgItem.OrganizeId : deptItem.ParentId;
                    deptTree.isexpand = isExpand;  //默认不展开，人太多
                    deptTree.complete = true;
                    deptTree.showcheck = false;
                    //deptTree.hasChildren = deptUserStaffData.Count > 0 || orgDetpData.Any(p => p.ParentId == deptItem.Id);
                    deptTree.hasChildren = true;
                    treeList.Add(deptTree);
                }
                #endregion
                TreeViewModel tree = new TreeViewModel();
                tree.id = orgItem.OrganizeId;
                tree.text = orgItem.Name;
                tree.value = orgItem.Code;
                tree.parentId = orgItem.OrganizeId == organizeId ? null : orgItem.ParentId; //特殊
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = false;
                tree.hasChildren = orgDetpData.Count > 0 || orgUserStaffData.Count > 0 || organizedata.Any(p => p.ParentId == orgItem.OrganizeId);
                treeList.Add(tree);
            }
            //缓存end
            IList<FirstSecond> checkedUserList = null; //已checked的用户
            if (from == "rolerelatioinuser" && !string.IsNullOrWhiteSpace(keyValue))   //角色已关联用户
            {
                checkedUserList = await _userRoleAuthDmnService.GetCurUserIdListByRoleId(keyValue);
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
