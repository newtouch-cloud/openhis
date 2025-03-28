using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Web.Mvc;
using Newtouch.Tools;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Common.Exceptions;
using Newtouch.Common;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Base.HOSP.Controllers
{
    /// <summary>
    /// 收费分类
    /// </summary>
    public class SysChargeClassificationController : ControllerBase
    {
        private readonly ISysChargeClassificationRepo _sysChargeClassificationRepo;
        private readonly ISysOrganizeDmnService _SysOrganizeDmnService;

        public SysChargeClassificationController(ISysChargeClassificationRepo sysChargeClassificationRepo
            , ISysOrganizeDmnService sysOrganizeDmnService)
        {
            this._sysChargeClassificationRepo = sysChargeClassificationRepo;
            this._SysOrganizeDmnService = sysOrganizeDmnService;
        }

        //收费树形分类（医院） 下拉 数据源
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectJson(string organizeId, string treeidFieldName = "Code")
        {
            var data = _sysChargeClassificationRepo.GetValidList(organizeId);
            var treeList = new List<TreeSelectModel>();
            foreach (var item in data)
            {
                if (treeidFieldName == "Code")
                {
                    TreeSelectModel treeModel = new TreeSelectModel();
                    treeModel.id = item.flCode;
                    treeModel.text = item.flmc;
                    treeModel.parentId = (item.ParentId.HasValue && item.ParentId.Value != 0)
                        ? data.Where(p => p.flId == item.ParentId).Select(p => p.flCode).FirstOrDefault()
                        : null;
                    treeList.Add(treeModel);
                }
                else
                {
                    TreeSelectModel treeModel = new TreeSelectModel();
                    treeModel.id = item.flId.ToString();
                    treeModel.text = item.flmc;
                    treeModel.parentId = (item.ParentId.HasValue && item.ParentId.Value != 0) ? item.ParentId.ToString() : null;
                    treeList.Add(treeModel);
                }
            }
            return Content(treeList.TreeSelectJson(null));
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetTreeGridJson(string organizeId, string keyword)
        {
            var data = _sysChargeClassificationRepo.GetList(keyword, organizeId);
            var treeList = new List<TreeGridModel>();
            foreach (var item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                treeModel.id = item.flId.ToString();
                bool hasChildren = data.Count(t => t.ParentId == item.flId) == 0 ? false : true;
                treeModel.id = item.flId.ToString();
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = (item.ParentId.HasValue && item.ParentId.Value != 0) ? item.ParentId.ToString() : null;
                treeModel.expanded = hasChildren;
                treeModel.entityJson = item.ToJson();
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeGridJson(null));
        }

        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(int keyValue)
        {
            var entity = _sysChargeClassificationRepo.GetForm(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(SysChargeClassificationEntity entity, int? keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            if (string.IsNullOrWhiteSpace(entity.OrganizeId))
            {
                throw new FailedException("请选择组织机构");
            }
            else if (_SysOrganizeDmnService.IsHasLowerOrganize(entity.OrganizeId))
            {
                throw new FailedException("请选择组织机构（无下级机构）");
            }
            _sysChargeClassificationRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }
    }
}