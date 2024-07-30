using Newtouch.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices.Rehabilitation;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Newtouch.HIS.Base.HOSP.Controllers
{
    public class RehabChargeItemController : ControllerBase
    {
        private readonly IRehabChargeItemRepo _rehabChargeItemRepo;
        private readonly IRehabilitationDmnService _rehabilitationDmnService;
        private readonly IRehabChargeClassificationRepo _rehabChargeClassificationRepo;
        public RehabChargeItemController(IRehabChargeClassificationRepo rehabChargeClassificationRepo, IRehabChargeItemRepo rehabChargeItemRepo, IRehabilitationDmnService rehabilitationDmnService)
        {
            this._rehabChargeClassificationRepo = rehabChargeClassificationRepo;
            this._rehabChargeItemRepo = rehabChargeItemRepo;
            this._rehabilitationDmnService = rehabilitationDmnService;
        }

        public override ActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// 收费分类下拉
        /// </summary>
        /// <returns></returns>
        public ActionResult GetsfflBindSelect(string OrganizeId)
        {
            var data = _rehabChargeClassificationRepo.GetRehabChargeClassificationList(OrganizeId,null);
            var treeList = new List<TreeSelectModel>();
            foreach (var item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.Code;
                treeModel.text = item.Name;
                treeModel.parentId = "0";
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        public ActionResult SubmitForm(RehabChargeItemEntity entity, string sfxmId)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            _rehabChargeItemRepo.SubmitForm(entity, sfxmId);
            return Success("操作成功。");
        }


        /// <summary>
        /// 获取有效列表
        /// </summary>
        public ActionResult GetRehabChargeItemList(string keyword, string OrganizeId)
        {
            var data = _rehabilitationDmnService.GetRehabChargeItemList(keyword, OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 新增或修改Form
        /// </summary>
        public ActionResult GetFormJson(string sfxmId, string OrganizeId)
        {
            var entity = _rehabilitationDmnService.GetRehabChargeItemEntity(sfxmId, OrganizeId);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 删除
        /// </summary>
        public ActionResult DeleteForm(string sfxmId, string OrganizeId)
        {
            _rehabChargeItemRepo.DeleteForm(sfxmId, OrganizeId);
            return Success("操作成功。");
        }



    }
}