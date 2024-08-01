using System.Web.Mvc;
using Newtouch.Tools;
using System.Collections.Generic;
using FrameworkBase.Domain.Entity;
using FrameworkBase.Domain.IDomainServices;
using FrameworkBase.Domain.IRepository;
using Newtouch.Common;
using System.Linq;
using Newtouch.Core.Common;

namespace FrameworkBase.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-20 13:03
    /// 描 述：字典分类
    /// </summary>
    [AutoResolveIgnore]
    public class SysItemsTypeController : BaseController
    {
        private readonly ISysItemsTypeRepo _sysItemsTypeRepo;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="sysItemsTypeRepo"></param>
        public SysItemsTypeController(ISysItemsTypeRepo sysItemsTypeRepo)
        {
            this._sysItemsTypeRepo = sysItemsTypeRepo;
        }

        /// <summary>
        /// 下拉选择 数据源（表单选择上级）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectJson()
        {
            var data = _sysItemsTypeRepo.GetValidList();
            var treeList = new List<TreeSelectModel>();
            foreach (SysItemsTypeEntity item in data)
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
        /// 字典分类 左边树形结构
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeJson()
        {
            var data = _sysItemsTypeRepo.GetValidList();
            var treeList = new List<TreeViewModel>();
            foreach (SysItemsTypeEntity item in data)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = data.Any(t => t.ParentId == item.Id);
                tree.id = item.Id;
                tree.text = item.Name;
                tree.value = item.Code;
                tree.parentId = item.ParentId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson(null));
        }

        /// <summary>
        /// 获取树形实体列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeGridJson(string keyword)
        {
            var data = _sysItemsTypeRepo.GetList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.ToList().TreeWhere(t => t.Name.Contains(keyword) || t.Code.Contains(keyword), parentId: "ParentId");
            }
            var treeList = new List<TreeGridModel>();
            foreach (SysItemsTypeEntity item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Any(t => t.ParentId == item.Id);
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
            var data = _sysItemsTypeRepo.FindEntity(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 提交字典分类
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SubmitForm(SysItemsTypeEntity entity, string keyValue)
        {
            entity.ParentId = (string.IsNullOrWhiteSpace(entity.ParentId) || entity.ParentId == "0"
                || entity.ParentId == keyValue)
               ? null : entity.ParentId
               ;
            entity.zt = entity.zt == "true" ? "1" : "0";
            _sysItemsTypeRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }

    }
}