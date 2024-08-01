using System.Web.Mvc;
using Newtouch.Tools;
using System.Collections.Generic;
using Newtouch.Core.Common;
using System.Linq;
using Newtouch.Common;
using System;
using FrameworkBase.Domain.Entity;
using FrameworkBase.Domain.IDomainServices;
using FrameworkBase.Domain.IRepository;

namespace FrameworkBase.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-17 16:15
    /// 描 述：系统人员
    /// </summary>
    [AutoResolveIgnore]
    public class SysStaffController : BaseController
    {
        private readonly ISysStaffRepo _sysStaffRepo;
        private readonly ISysStaffDmnService _sysStaffDmnService;
        private readonly ISysDepartmentRepo _sysDepartmentRepo;
        private readonly ISysUserStaffRepo _sysUserStaffRepo;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="sysStaffRepo"></param>
        /// <param name="sysStaffDmnService"></param>
        /// <param name="sysDepartmentRepo"></param>
        /// <param name="sysUserStaffRepo"></param>
        public SysStaffController(ISysStaffRepo sysStaffRepo
            , ISysStaffDmnService sysStaffDmnService
            , ISysDepartmentRepo sysDepartmentRepo
            , ISysUserStaffRepo sysUserStaffRepo)
        {
            this._sysStaffRepo = sysStaffRepo;
            this._sysStaffDmnService = sysStaffDmnService;
            this._sysDepartmentRepo = sysDepartmentRepo;
            this._sysUserStaffRepo = sysUserStaffRepo;
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
                rows = _sysStaffDmnService.GetPaginationStaffList(pagination, keyword),
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
            var entity = _sysStaffRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        /// <param name="asLoginUser"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SubmitForm(SysStaffEntity entity, string keyValue, bool asLoginUser)
        {
            if (string.IsNullOrWhiteSpace(entity.Name))
            {
                return Error("数据不完整，请重新填写提交");
            }
            entity.zt = entity.zt == "true" ? "1" : "0";

            _sysStaffDmnService.SubmitForm(entity, keyValue, asLoginUser);
            return Success("操作成功。");
        }

        /// <summary>
        /// 岗位选择
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Duties()
        {
            return View();
        }

        /// <summary>
        /// 更新 人员岗位
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="dutyList"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult UpdateStaffDuty(string keyValue, string dutyList)
        {
            _sysStaffDmnService.UpdateStaffDuty(keyValue, (dutyList ?? "").Split(','));
            return Success("操作成功。");
        }

        /// <summary>
        /// 关联用户 视图
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Selector()
        {
            return View();
        }

        /// <summary>
        /// 获取人员树 两级（科室+人员）
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="from"></param>
        /// <param name="isShowEmpty">是否显示空白节点（科室节点）</param>
        /// <param name="isExpand"></param>
        /// <param name="initIdSelected"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public virtual ActionResult GetStaffSelecotrTree(string keyValue = null, string from = null, bool isShowEmpty = false, bool isExpand = true, string initIdSelected = null)
        {
            return ComGetStaffSelecotrTree(keyValue, from, isShowEmpty, isExpand, initIdSelected);
        }

        #region private

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="from"></param>
        /// <param name="isShowEmpty"></param>
        /// <param name="isExpand"></param>
        /// <param name="initIdSelected"></param>
        /// <param name="funcTreeItemFilter"></param>
        /// <param name="funcCheckedList"></param>
        /// <returns></returns>
        [NonAction]
        protected internal ActionResult ComGetStaffSelecotrTree(string keyValue = null, string from = null, bool isShowEmpty = false, bool isExpand = true, string initIdSelected = null
            , Func<List<TreeViewModel>, List<TreeViewModel>> funcTreeItemFilter = null
            , Func<List<string>> funcCheckedList = null)
        {
            var treeList = new List<TreeViewModel>();

            //科室集合
            var detpData = _sysDepartmentRepo.GetValidList();
            //人员集合
            var staffData = _sysStaffRepo.GetValidList();

            foreach (SysDepartmentEntity deptItem in detpData)
            {
                var deptStaffData = staffData.Where(p => p.DepartmentCode == deptItem.Code).ToList();
                foreach (SysStaffEntity staffItem in deptStaffData)
                {
                    TreeViewModel staffTree = new TreeViewModel();
                    staffTree.id = staffItem.Id;
                    staffTree.text = staffItem.Name;
                    staffTree.value = staffItem.gh;
                    staffTree.parentId = deptItem.Id;
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
                //deptTree.hasChildren = deptStaffData.Count > 0 || detpData.Any(p => p.ParentId == deptItem.Id);
                deptTree.hasChildren = true;
                treeList.Add(deptTree);
            }

            if (funcTreeItemFilter != null)
            {
                treeList = funcTreeItemFilter(treeList);
            }

            //缓存end
            IList<string> checkedStaffIdList = new List<string>();
            if (from == "userrelatioinstaff" && !string.IsNullOrWhiteSpace(keyValue))   //用户已关联人员
            {
                checkedStaffIdList = _sysUserStaffRepo.GetStaffIdListByUserId(keyValue);
            }

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

            return Content(treeList.TreeViewJson(null));
        }

        #endregion

    }
}