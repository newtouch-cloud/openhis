using System.Web.Mvc;
using Newtouch.Tools;
using System.Collections.Generic;
using Newtouch.Common;
using System.Linq;
using FrameworkBase.Domain.Entity;
using FrameworkBase.Domain.IDomainServices;
using FrameworkBase.Domain.IRepository;
using Newtouch.Core.Common;

namespace FrameworkBase.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-17 16:13
    /// 描 述：系统菜单
    /// </summary>
    [AutoResolveIgnore]
    public class SysModuleController : BaseController
    {
        private readonly ISysModuleRepo _sysModuleRepo;
        private readonly ISysModuleDmnService _sysModuleDmnService;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="sysModuleRepo"></param>
        /// <param name="sysModuleDmnService"></param>
        public SysModuleController(ISysModuleRepo sysModuleRepo
            , ISysModuleDmnService sysModuleDmnService)
        {
            this._sysModuleRepo = sysModuleRepo;
            this._sysModuleDmnService = sysModuleDmnService;
        }

        /// <summary>
        /// 下拉（选择菜单 编辑 上级）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectJson()
        {
            var data = _sysModuleRepo.GetValidList();
            var treeList = new List<TreeSelectModel>();
            foreach (SysModuleEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.Id;
                treeModel.text = item.Name;
                treeModel.parentId = item.ParentId;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson(null));
        }

        /// <summary>
        /// 获取树形实例列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeGridJson(string keyword)
        {
            var data = _sysModuleRepo.GetList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.Name.Contains(keyword), parentId: "ParentId");
            }
            var treeList = new List<TreeGridModel>();
            foreach (SysModuleEntity item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                treeModel.id = item.Id;
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.ParentId;
                treeModel.expanded = hasChildren;
                treeModel.entityJson = item.ToJson();
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeGridJson(null));
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = _sysModuleRepo.FindEntity(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SubmitForm(SysModuleEntity moduleEntity, string keyValue)
        {
            moduleEntity.zt = moduleEntity.zt == "true" ? "1" : "0";
            _sysModuleRepo.SubmitForm(moduleEntity, keyValue);
            return Success("操作成功。");
        }

        /// <summary>
        /// 删除菜单（物理删除）
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult DeleteForm(string keyValue)
        {
            _sysModuleDmnService.DeleteForm(keyValue);
            return Success("删除成功。");
        }


    }
}