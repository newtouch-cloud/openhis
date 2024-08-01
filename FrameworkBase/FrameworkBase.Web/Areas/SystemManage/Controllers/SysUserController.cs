using System.Web.Mvc;
using Newtouch.Tools;
using Newtouch.Core.Common;
using Newtouch.Common;
using System.Collections.Generic;
using System.Linq;
using FrameworkBase.Domain.Entity;
using FrameworkBase.Domain.IDomainServices;
using FrameworkBase.Domain.IRepository;
using System;

namespace FrameworkBase.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-17 16:14
    /// 描 述：系统用户
    /// </summary>
    [AutoResolveIgnore]
    public class SysUserController : BaseController
    {
        private readonly ISysUserRepo _sysUserRepo;
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly ISysUserStaffRepo _sysUserStaffRepo;
        private readonly ISysUserLogOnRepo _sysUserLogOnRepo;
        private readonly ISysDepartmentRepo _sysDepartmentRepo;
        private readonly IUserRoleAuthDmnService _userRoleAuthDmnService;
        private readonly ISysUserRoleRepo _sysUserRoleRepo;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="sysUserRepo"></param>
        /// <param name="sysUserDmnService"></param>
        /// <param name="sysUserStaffRepo"></param>
        /// <param name="sysUserLogOnRepo"></param>
        /// <param name="sysDepartmentRepo"></param>
        /// <param name="userRoleAuthDmnService"></param>
        /// <param name="sysUserRoleRepo"></param>
        public SysUserController(ISysUserRepo sysUserRepo
            , ISysUserDmnService sysUserDmnService
            , ISysUserStaffRepo sysUserStaffRepo
            , ISysUserLogOnRepo sysUserLogOnRepo
            , ISysDepartmentRepo sysDepartmentRepo
            , IUserRoleAuthDmnService userRoleAuthDmnService
            , ISysUserRoleRepo sysUserRoleRepo)
        {
            this._sysUserRepo = sysUserRepo;
            this._sysUserDmnService = sysUserDmnService;
            this._sysUserStaffRepo = sysUserStaffRepo;
            this._sysUserLogOnRepo = sysUserLogOnRepo;
            this._sysDepartmentRepo = sysDepartmentRepo;
            this._userRoleAuthDmnService = userRoleAuthDmnService;
            this._sysUserRoleRepo = sysUserRoleRepo;
        }

        /// <summary>
        /// 获取分页实体列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetPagintionGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = _sysUserDmnService.GetPagintionList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = _sysUserRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="userEntity"></param>
        /// <param name="userLogOnEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SubmitForm(SysUserEntity userEntity, SysUserLogOnEntity userLogOnEntity, string keyValue)
        {
            userEntity.LanguageType = userEntity.LanguageType == "true" ? "en" : null;
            userEntity.zt = userEntity.zt == "true" ? "1" : "0";
            _sysUserDmnService.SubmitForm(userEntity, userLogOnEntity, keyValue);
            return Success("操作成功。");
        }

        /// <summary>
        /// 用户选择器
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult Selector()
        {
            return View();
        }

        /// <summary>
        /// 获取系统用户树 两级（科室+用户（人员））
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="from"></param>
        /// <param name="isShowEmpty">是否显示空白节点（科室节点）</param>
        /// <param name="isExpand"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public virtual ActionResult GetSysUserSelectorTree(string keyValue = null, string from = null, bool isShowEmpty = false, bool isExpand = true)
        {
            return ComGetSysUserSelectorTree(keyValue, from, isShowEmpty, isExpand);
        }

        /// <summary>
        /// 提交保存 用户关联人员
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="staffIds"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SubmitUserStaff(string userId, string staffIds)
        {
            _sysUserStaffRepo.SubmitUserStaff(userId, staffIds);
            return Success("操作成功");
        }

        /// <summary>
        /// 重置密码 视图
        /// </summary>
        /// <returns></returns>
        public ActionResult RevisePassword()
        {
            return View();
        }

        /// <summary>
        /// 重置密码 提交
        /// </summary>
        /// <param name="userPassword"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SubmitRevisePassword(string userPassword, string keyValue)
        {
            _sysUserLogOnRepo.RevisePassword(userPassword, keyValue);
            return Success("重置密码成功。");
        }

        /// <summary>
        /// 禁用用户
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult DisabledAccount(string keyValue)
        {
            _sysUserLogOnRepo.UpdateLockedStatus(keyValue, true);
            return Success("账户禁用成功。");
        }

        /// <summary>
        /// 启用用户
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult EnabledAccount(string keyValue)
        {
            _sysUserLogOnRepo.UpdateLockedStatus(keyValue, false);
            return Success("账户启用成功。");
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
        /// <param name="roleList"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult UpdateUserRole(string keyValue, string roleList)
        {
            _userRoleAuthDmnService.UpdateUserRole(keyValue, (roleList ?? "").Split(','));
            return Success("操作成功。");
        }

        /// <summary>
        /// 个人信息视图
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
            _sysUserLogOnRepo.RevisePassword(newpwd, this.UserIdentity.UserId);
            return Success("重置密码成功。");
        }

        #region private

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="from"></param>
        /// <param name="isShowEmpty"></param>
        /// <param name="isExpand"></param>
        /// <param name="funcTreeItemFilter"></param>
        /// <param name="funcCheckedList"></param>
        /// <returns></returns>
        [NonAction]
        protected internal ActionResult ComGetSysUserSelectorTree(string keyValue = null, string from = null, bool isShowEmpty = false, bool isExpand = true
            , Func<List<TreeViewModel>, List<TreeViewModel>> funcTreeItemFilter = null
            , Func<List<string>> funcCheckedList = null)
        {
            var treeList = new List<TreeViewModel>();

            //科室集合
            var detpData = _sysDepartmentRepo.GetValidList();
            //人员集合（有对应登录账户）
            var userStaffData = _sysUserDmnService.GetSatffVOList();

            foreach (SysDepartmentEntity deptItem in detpData)
            {
                var deptUserStaffData = userStaffData.Where(p => p.DepartmentCode == deptItem.Code).ToList();
                foreach (var userStaffVO in deptUserStaffData)
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

                    treeList.Add(staffTree);
                }
                TreeViewModel deptTree = new TreeViewModel();
                deptTree.id = deptItem.Id;
                deptTree.text = deptItem.Name;
                deptTree.value = deptItem.Code;
                deptTree.parentId = deptItem.ParentId;
                deptTree.isexpand = isExpand;  //默认不展开，人太多
                deptTree.complete = true;
                deptTree.showcheck = false;
                //deptTree.hasChildren = deptUserStaffData.Count > 0 || detpData.Any(p => p.ParentId == deptItem.Id);
                deptTree.hasChildren = true;
                treeList.Add(deptTree);
            }

            if (funcTreeItemFilter != null)
            {
                treeList = funcTreeItemFilter(treeList);
            }

            //缓存end
            IList<string> checkedUserList = null; //已checked的用户
            if (from == "rolerelatioinuser" && !string.IsNullOrWhiteSpace(keyValue))   //角色已关联用户
            {
                checkedUserList = _sysUserRoleRepo.GetUserIdListByRoleId(keyValue);
            }

            if (funcCheckedList != null)
            {
                var funccheckedListResult = funcCheckedList();
                if (funccheckedListResult != null && funccheckedListResult.Count > 0)
                {
                    checkedUserList = (checkedUserList ?? new List<string>())
                        .Union(funccheckedListResult).ToList();
                }
            }

            if (checkedUserList != null && checkedUserList.Count > 0)
            {
                foreach (var treeItem in treeList)
                {
                    if (treeItem.showcheck && checkedUserList.Contains(treeItem.value))
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

            return Content(treeList.TreeViewJson(null));
        }

        #endregion

    }
}