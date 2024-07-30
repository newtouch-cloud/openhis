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
    public class UserController : FrameworkBase.MultiOrg.Web.Areas.SystemManage.Controllers.SysUserController
    {
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
        private readonly ISysDepartmentRepo _sysDepartmentRepo;
        private readonly ISysUserExDmnService _sysUserDmnService;
        private readonly IUserRoleAuthDmnService _userRoleAuthDmnService;
        private readonly ICache _cache;


        /// <summary>
        /// 用户选择器
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Selector()
        {
            return View();
        }

        /// <summary>
        /// 获取系统用户树 三级（机构+科室+用户（人员））
        /// </summary>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult GetSysUserSelectorTree(string organizeId = null, string keyValue = null, string from = null, bool isShowEmpty = false, bool isExpand = true, int cacheTime = 30)
        {
            if (string.IsNullOrWhiteSpace(organizeId))
            {
                organizeId = base.GetAuthOrganizeId();  //默认 权限OrganizeId
            }
            //带缓存的系统用户树
            var treeList = _cache.Get<List<TreeViewModel>>(string.Format(CacheKey.OrganizeUserTreeSetKey, organizeId), () => {
                var organizedata = _sysOrganizeDmnService.GetValidListByParentOrg(organizeId);
                var iitreeList = new List<TreeViewModel>();
                foreach (var orgItem in organizedata)
                {
                    //机构科室集合
                    var orgDetpData = _sysDepartmentRepo.GetList(orgItem.Id);
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
                            iitreeList.Add(staffTree);
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
                        deptTree.hasChildren = deptUserStaffData.Count > 0 || orgDetpData.Any(p => p.ParentId == deptItem.Id);
                        iitreeList.Add(deptTree);
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
                    tree.hasChildren = orgDetpData.Count > 0 || orgUserStaffData.Count > 0 || organizedata.Any(p => p.ParentId == orgItem.Id);
                    iitreeList.Add(tree);
                }
                return iitreeList;
            }, cacheTime);

            IList<string> checkedUserIdList = null; //已checked的用户
            if (from == "rolerelatioinuser" && !string.IsNullOrWhiteSpace(keyValue))   //角色已关联用户
            {
                checkedUserIdList = _userRoleAuthDmnService.GetCurUserIdListByRoleId(keyValue);
            }

            if (checkedUserIdList != null && checkedUserIdList.Count > 0)
            {
                foreach (var treeItem in treeList)
                {
                    if (treeItem.showcheck && checkedUserIdList.Contains(treeItem.value))
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

        /// <summary>
        /// 个人信息
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

    }
}
