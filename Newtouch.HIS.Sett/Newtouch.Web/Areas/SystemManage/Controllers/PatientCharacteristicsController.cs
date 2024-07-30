using Newtouch.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class PatientCharacteristicsController : ControllerBase
    {
        private readonly ISysPatientNatureRepo _sysPatiNatureRepo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetTreeGridJson(string keyword)
        {
            var data = _sysPatiNatureRepo.GetbxzcBySearch(null, this.OrganizeId);
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.brxzmc.Contains(keyword) || t.brxz.Contains(keyword) || t.py.Contains(keyword)
                , parentId: "ParentId");
            }
            var treeList = new List<TreeGridModel>();
            foreach (SysPatientNatureEntity item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Any(t => t.ParentId == item.brxzbh);
                treeModel.id = item.brxzbh.ToString();
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.ParentId == null ? null : item.ParentId.ToString();
                treeModel.expanded = hasChildren;
                treeModel.entityJson = item.ToJson();
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeGridJson(null));
        }

        /// <summary>
        /// 病人性质下拉树
        /// </summary>
        /// <returns></returns>
        public ActionResult GetTreeSelectJson()
        {
            var data = _sysPatiNatureRepo.GetbxzcBySearch(null, this.OrganizeId);
            var treeList = new List<TreeSelectModel>();
            foreach (SysPatientNatureEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.brxzbh.ToString();  //选的是Id
                treeModel.text = item.brxzmc;
                treeModel.parentId = item.ParentId == null ? null : item.ParentId.ToString();
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson(null));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteForm(int keyValue)
        {
            _sysPatiNatureRepo.DeleteForm(keyValue, this.OrganizeId);
            return Success("删除成功");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brxzbh"></param>
        /// <returns></returns>
        public ActionResult Form(int brxzbh)
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(string KeyValue)
        {
            var data = _sysPatiNatureRepo.GetForm(KeyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(SysPatientNatureEntity entity, string keyValue)
        {
            if (!string.IsNullOrWhiteSpace(keyValue) && (entity.ParentId ?? -1).ToString() == keyValue)
            {
                return Error("父级选择错误");
            }
            entity.zt = entity.zt == "true" ? "1" : "0";
            entity.pzbz = entity.pzbz == "true" ? "1" : "0";
            entity.OrganizeId = this.OrganizeId;
            _sysPatiNatureRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }

    }
}