using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Web.Mvc;
using Newtouch.Infrastructure;
using Newtouch.Core.Common.Exceptions;
using System.Collections.Generic;
using System.Linq;
using Newtouch.Common;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Core.Common;
using Newtouch.Common.Model;

namespace Newtouch.HIS.Base.HOSP.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly ISysUserApp _sysUserApp;
        private readonly ISysUserRepo _sysUserRepo;
        private readonly ISysUserRoleRepo _sysUserRoleRepository;
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly ISysUserStaffRepo _SysUserStaffRepo;
        private readonly ISysUserYfbmRepo _SysUserYfbmRepo;
        private readonly ISysStaffRepo _sysStaffRepo;
        private readonly ISysPharmacyDepartmentRepo _SysPharmacyDepartmentRepo;
        private readonly ISysOrganizeRepository _sysOrganizeRepository;
        private readonly ISysDepartmentRepository _sysDepartmentRepository;
        private readonly IUserRoleAuthDmnService _userRoleAuthDmnService;
        private readonly ISysUserLogOnRepository _sysUserLogOnRepository;

        public UserController(ISysUserApp sysUserApp, ISysUserRepo sysUserRepo
            , ISysUserRoleRepo sysUserRoleRepository, ISysUserDmnService sysUserDmnService
            , ISysStaffRepo sysStaffRepo, ISysOrganizeRepository sysOrganizeRepository
            , ISysDepartmentRepository sysDepartmentRepository
            , ISysUserStaffRepo SysUserStaffRepo
            , ISysPharmacyDepartmentRepo SysPharmacyDepartmentRepo
            , ISysUserYfbmRepo SysUserYfbmRepo
            , IUserRoleAuthDmnService userRoleAuthDmnService
            , ISysUserLogOnRepository sysUserLogOnRepository)
        {
            this._sysUserApp = sysUserApp;
            this._sysUserRepo = sysUserRepo;
            this._sysUserRoleRepository = sysUserRoleRepository;
            this._sysUserDmnService = sysUserDmnService;
            this._sysStaffRepo = sysStaffRepo;
            this._sysOrganizeRepository = sysOrganizeRepository;
            this._sysDepartmentRepository = sysDepartmentRepository;
            this._SysUserStaffRepo = SysUserStaffRepo;
            this._SysPharmacyDepartmentRepo = SysPharmacyDepartmentRepo;
            this._SysUserYfbmRepo = SysUserYfbmRepo;
            this._userRoleAuthDmnService = userRoleAuthDmnService;
            this._sysUserLogOnRepository = sysUserLogOnRepository;
        }

        //grid json
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword, string OrganizeId)
        {
            pagination.sidx = "case when px is null then 1 else 0 end asc,px asc";
            pagination.sord = "asc";
            if (string.IsNullOrWhiteSpace(OrganizeId))
            {
                throw new FailedException("定位当前权限内的组织机构失败");
            }
            var data = new
            {
                rows = _sysUserDmnService.GetPagintionList(pagination, OrganizeId, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = _sysUserDmnService.GetSysUserByUserId(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public ActionResult SubmitForm(SysUserEntity userEntity, SysUserLogOnEntity userLogOnEntity, string keyValue)
        {
            userEntity.TopOrganizeId = Constants.TopOrganizeId;
            userEntity.LanguageType = userEntity.LanguageType == "true" ? "en" : null;
            userEntity.zt = userEntity.zt == "true" ? "1" : "0";
            _sysUserDmnService.SubmitForm(userEntity, userLogOnEntity, keyValue);
            return Success("操作成功。");
        }

        [HttpGet]
        public ActionResult RevisePassword()
        {
            return View();
        }

        [HttpPost]
        [HandlerAjaxOnly]
        //[HandlerAuthorize]
        //[ValidateAntiForgeryToken]
        public ActionResult SubmitRevisePassword(string userPassword, string keyValue)
        {
            _sysUserApp.RevisePassword(userPassword, keyValue);
            return Success("重置密码成功。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        //[HandlerAuthorize]
        //[ValidateAntiForgeryToken]
        public ActionResult DisabledAccount(string keyValue)
        {
            _sysUserLogOnRepository.UpdateLockedStatus(keyValue, true);
            return Success("账户禁用成功。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        //[HandlerAuthorize]
        //[ValidateAntiForgeryToken]
        public ActionResult EnabledAccount(string keyValue)
        {
            _sysUserLogOnRepository.UpdateLockedStatus(keyValue, false);
            return Success("账户启用成功。");
        }

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
        /// 关联药房
        /// </summary>
        /// <returns></returns>
        public ActionResult CorrelationPharmacy()
        {
            return View();
        }

        /// <summary>
        /// 获取系统用户树 三级（机构+科室+用户（人员））
        /// </summary>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult GetSysUserSelectorTree(string organizeId = null, string keyValue = null, string from = null, bool isShowEmpty = false, bool isExpand = true, bool isContansChildOrg = true)
        {
            if (string.IsNullOrWhiteSpace(organizeId))
            {
                organizeId = base.GetAuthOrganizeId();  //默认 权限OrganizeId
            }
            //缓存start
            var organizedata = _sysOrganizeRepository.GetValidListByParentOrg(organizeId);
            if (!isContansChildOrg)
            {
                //仅当前机构
                organizedata = organizedata.Where(p => p.Id == organizeId).ToList();
            }
            var treeList = new List<TreeViewModel>();
            foreach (SysOrganizeEntity orgItem in organizedata)
            {
                //机构科室集合
                var orgDetpData = _sysDepartmentRepository.GetListByOrg(orgItem.Id);
                //机构人员集合（有对应登录账户）
                var orgUserStaffData = _sysUserDmnService.GetSatffVOListByOrg(orgItem.Id);
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
                    //deptTree.hasChildren = deptUserStaffData.Count > 0 || orgDetpData.Any(p => p.ParentId == deptItem.Id);
                    deptTree.hasChildren = true;
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
                tree.hasChildren = orgDetpData.Count > 0 || orgUserStaffData.Count > 0 || organizedata.Any(p => p.ParentId == orgItem.Id);
                treeList.Add(tree);
            }
            //缓存end
            IList<FirstSecond> checkedUserList = null; //已checked的用户
            if (from == "rolerelatioinuser" && !string.IsNullOrWhiteSpace(keyValue))   //角色已关联用户
            {
                checkedUserList = _userRoleAuthDmnService.GetCurUserIdListByRoleId(keyValue);
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

        /// <summary>
        /// 保存Sys_UserStaff
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult submitUserStaff(string userId, string staffIds)
        {
            _SysUserStaffRepo.submitUserStaff(userId, staffIds);
            return Success("操作成功");
        }

        /// <summary>
        /// 获取药房部门 树
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetPharmacyTree(string keyValue, string organizeId)
        {
            if (string.IsNullOrWhiteSpace(organizeId))
            {
                organizeId = base.GetAuthOrganizeId();
            }
            var organizedata = _sysOrganizeRepository.GetValidListByParentOrg(organizeId);
            var treeList = new List<TreeViewModel>();
            foreach (SysOrganizeEntity item in organizedata)
            {
                #region 机构下所有药房
                var curPharmacyList = _SysPharmacyDepartmentRepo.GetCurPharmacyList(keyValue, item.Id);
                var Pharmacydata = _SysPharmacyDepartmentRepo.GetPharmacyListByOrg(item.Id);
                foreach (SysPharmacyDepartmentEntity PharmacyItem in Pharmacydata)
                {
                    TreeViewModel PharmacyTree = new TreeViewModel();
                    PharmacyTree.id = PharmacyItem.OrganizeId + "a88123jkjfwe13120j" + PharmacyItem.yfbmCode;
                    PharmacyTree.text = PharmacyItem.yfbmmc;
                    PharmacyTree.parentId = item.Id;
                    PharmacyTree.hasChildren = false;
                    PharmacyTree.showcheck = true;
                    PharmacyTree.checkstate = curPharmacyList.Contains(PharmacyItem.yfbmCode.ToString()) ? 1 : 0;
                    treeList.Add(PharmacyTree);
                }
                #endregion
                TreeViewModel tree = new TreeViewModel();
                tree.id = item.Id;
                tree.text = item.Name;
                tree.value = item.Code;
                tree.parentId = item.Id == organizeId ? null : item.ParentId;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = false;
                tree.hasChildren = Pharmacydata.Count > 0 || organizedata.Any(p => p.ParentId == item.Id);
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson(null));
        }

        /// <summary>
        /// 保存Sys_UserYfbm
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="yfbmCode"></param>
        /// <returns></returns>
        public ActionResult submitUserYfbm(string userId, string yfbmCode)
        {
            _SysUserYfbmRepo.submitUserYfbm(userId, yfbmCode);
            return Success("操作成功");
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

    }
}
