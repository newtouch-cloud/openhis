using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Web.Mvc;
using System.Linq;
using Newtouch.Common;
using System.Collections.Generic;

namespace Newtouch.HIS.Base.HOSP.Controllers
{
    /// <summary>
    /// 系统收费大类
    /// </summary>
    public class ChargeCategoryController : ControllerBase
    {
        private readonly ISysChargeCategoryRepo _SysChargeCategoryRepo;
        private readonly ISysOrganizeDmnService _SysOrganizeDmnService;

        public ChargeCategoryController(ISysChargeCategoryRepo SysChargeCategoryRepo
            , ISysOrganizeDmnService sysOrganizeDmnService)
        {
            this._SysChargeCategoryRepo = SysChargeCategoryRepo;
            this._SysOrganizeDmnService = sysOrganizeDmnService;
        }

        //收费大类树形（医院） 下拉 数据源
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectJson(string organizeId, string treeidFieldName = "Code")
        {
            var data = _SysChargeCategoryRepo.GetValidList(organizeId);
            var treeList = new List<TreeSelectModel>();
            foreach (var item in data)
            {
                if (treeidFieldName == "Code")
                {
                    TreeSelectModel treeModel = new TreeSelectModel();
                    treeModel.id = item.dlCode;
                    treeModel.text = item.dlmc;
                    treeModel.parentId = (item.ParentId.HasValue && item.ParentId.Value != 0)
                        ? data.Where(p => p.dlId == item.ParentId).Select(p => p.dlCode).FirstOrDefault()
                        : null;
                    treeList.Add(treeModel);
                }
                else
                {
                    TreeSelectModel treeModel = new TreeSelectModel();
                    treeModel.id = item.dlId.ToString();
                    treeModel.text = item.dlmc;
                    treeModel.parentId = (item.ParentId.HasValue && item.ParentId.Value != 0) ? item.ParentId.ToString() : null;
                    treeList.Add(treeModel);
                }
            }
            return Content(treeList.TreeSelectJson(null));
        }

        /// <summary>
        /// 获取树形列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetTreeGridJson(string organizeId, string keyword)
        {
            var data = _SysChargeCategoryRepo.GetList(organizeId);
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.ToList().TreeWhereForKeyInt(t => t.dlmc.Contains(keyword), keyValue : "dlId", parentId: "ParentId");
            }
            var treeList = new List<TreeGridModel>();
            foreach (var item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                treeModel.id = item.dlId.ToString();
                bool hasChildren = data.Count(t => t.ParentId == item.dlId) == 0 ? false : true;
                treeModel.id = item.dlId.ToString();
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
            var entity = _SysChargeCategoryRepo.GetForm(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(SysChargeCategoryEntity entity, int? keyValue)
        {
            if (string.IsNullOrWhiteSpace(entity.mzprintreportcode))
            {
                entity.mzprintreportcode = entity.dlCode;
            }
            if (string.IsNullOrWhiteSpace(entity.mzprintbillcode))
            {
                entity.mzprintbillcode = entity.dlCode;
            }
            if (keyValue.HasValue && keyValue.Value > 0 && keyValue.Value == entity.ParentId)
            {
                throw new FailedException("上级大类选择错误");
            }
            if (!string.IsNullOrWhiteSpace(entity.fjCode))
            {
                entity.fjmc = ((Newtouch.Infrastructure.EnumSffjdlmc)int.Parse(entity.fjCode)).ToString();
            }
            entity.zt = entity.zt == "true" ? "1" : "0";
            if (string.IsNullOrWhiteSpace(entity.OrganizeId))
            {
                throw new FailedException("请选择组织机构");
            }
            else if (!_SysOrganizeDmnService.IsMedicalOrganize(entity.OrganizeId))
            {
                throw new FailedException("请选择医疗机构（医院或诊所）");
            }
            _SysChargeCategoryRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }

    }
}