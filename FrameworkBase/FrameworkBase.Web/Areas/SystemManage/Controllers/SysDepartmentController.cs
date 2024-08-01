using System.Web.Mvc;
using Newtouch.Tools;
using Newtouch.Common;
using System.Linq;
using System.Collections.Generic;
using FrameworkBase.Domain.Entity;
using FrameworkBase.Domain.IRepository;
using Newtouch.Core.Common;

namespace FrameworkBase.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-17 15:56
    /// 描 述：系统科室
    /// </summary>
    [AutoResolveIgnore]
    public class SysDepartmentController : BaseController
    {
        private readonly ISysDepartmentRepo _sysDepartmentRepo;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="sysDepartmentRepo"></param>
        public SysDepartmentController(ISysDepartmentRepo sysDepartmentRepo)
        {
            this._sysDepartmentRepo = sysDepartmentRepo;
        }

        /// <summary>
        /// 科室 下拉 数据源
        /// </summary>
        /// <param name="treeidFieldName"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectJson(string treeidFieldName = "Code")
        {
            var data = _sysDepartmentRepo.GetValidList().ToList();
            var treeList = new List<TreeSelectModel>();
            foreach (SysDepartmentEntity item in data)
            {
                if (treeidFieldName == "Code")
                {
                    TreeSelectModel treeModel = new TreeSelectModel();
                    treeModel.id = item.Code;
                    treeModel.text = item.Name;
                    treeModel.parentId = item.ParentId == null ? null :
                        data.Where(p => p.Id == item.ParentId).Select(p => p.Code).FirstOrDefault();
                    treeList.Add(treeModel);
                }
                else
                {
                    TreeSelectModel treeModel = new TreeSelectModel();
                    treeModel.id = item.Id;
                    treeModel.text = item.Name;
                    treeModel.parentId = item.ParentId;
                    treeList.Add(treeModel);
                }
            }
            return Content(treeList.TreeSelectJson(null));
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
            var data = _sysDepartmentRepo.GetList().ToList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.Name.Contains(keyword) || t.Code.Contains(keyword), parentId: "ParentId");
            }
            var treeList = new List<TreeGridModel>();
            foreach (SysDepartmentEntity item in data)
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
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = _sysDepartmentRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SubmitForm(SysDepartmentEntity entity, string keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            if (!string.IsNullOrWhiteSpace(keyValue) && keyValue == entity.ParentId)
            {
                return Error("上级科室选择错误");
            }
            _sysDepartmentRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }

    }
}