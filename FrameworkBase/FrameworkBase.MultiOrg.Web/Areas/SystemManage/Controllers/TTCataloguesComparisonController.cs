using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace FrameworkBase.MultiOrg.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 三方目录对照
    /// </summary>
    [AutoResolveIgnore]
    public class TTCataloguesComparisonController : OrgControllerBase
    {
        private readonly ITTCataloguesComparisonDmnService _ttCataloguesComparisonDmnService;
        private readonly ITTCataloguesComparisonDetailRepo _ttCataloguesComparisonDetailRepo;
        private readonly ITTCataloguesComparisonMainRepo _ttCataloguesComparisonMainRepo;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="ttCataloguesComparisonDmnService"></param>
        /// <param name="ttCataloguesComparisonDetailRepo"></param>
        /// <param name="ttCataloguesComparisonMainRepo"></param>
        public TTCataloguesComparisonController(ITTCataloguesComparisonDmnService ttCataloguesComparisonDmnService, ITTCataloguesComparisonDetailRepo ttCataloguesComparisonDetailRepo, ITTCataloguesComparisonMainRepo ttCataloguesComparisonMainRepo)
        {
            this._ttCataloguesComparisonDmnService = ttCataloguesComparisonDmnService;
            this._ttCataloguesComparisonDetailRepo = ttCataloguesComparisonDetailRepo;
            this._ttCataloguesComparisonMainRepo = ttCataloguesComparisonMainRepo;
        }

        #region 明细

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult DetailIndex()
        {
            return View();
        }

        /// <summary>
        /// detail grid json
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetDetailGridJson(string keyword, string mainType)
        {
            if (string.IsNullOrWhiteSpace(mainType))
            {
                return Content(new List<TTCataloguesComparisonDetailEntity>().ToJson());
            }
            var data = _ttCataloguesComparisonDetailRepo.GetListByMainId(keyword, mainType);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult DetailFrom()
        {
            return View();
        }

        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetDetailFormJson(string keyValue)
        {
            var entity = _ttCataloguesComparisonDetailRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DetailSubmitForm(TTCataloguesComparisonDetailEntity entity, string keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            entity.OrganizeId = this.OrganizeId;
            _ttCataloguesComparisonDetailRepo.SubmitForm(keyValue, entity);
            return Success("提交成功");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DetailDeleteForm(string keyValue)
        {
            var entity = _ttCataloguesComparisonDetailRepo.FindEntity(keyValue);
            if (entity != null)
            {
                _ttCataloguesComparisonDetailRepo.Delete(entity);
            }
            return Success("删除成功。");
        }

        #endregion 明细

        #region Main

        /// <summary>
        /// 下拉 by Code 获取字典项
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetMainSelectJson()
        {
            var list = _ttCataloguesComparisonDmnService.GetValidMainList(this.OrganizeId);
            var data = list.Select(p => new
            {
                id = p.Id,
                text = p.Code + "-" + p.TTCode + "-" + p.TTMark,
            });
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult MainIndex()
        {
            return View();
        }

        /// <summary>
        /// main grid json
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetMainGridJson(string keyword)
        {
            var data = _ttCataloguesComparisonMainRepo.GetList(this.OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult MainFrom()
        {
            return View();
        }

        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetMainFormJson(string keyValue)
        {
            var entity = _ttCataloguesComparisonMainRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult MainSubmitForm(TTCataloguesComparisonMainEntity entity, string keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            entity.OrganizeId = this.OrganizeId;
            _ttCataloguesComparisonMainRepo.SubmitForm(keyValue, entity);
            return Success("提交成功");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult MainDeleteForm(string keyValue)
        {
            var entity = _ttCataloguesComparisonMainRepo.FindEntity(keyValue);
            if (entity != null)
            {
                _ttCataloguesComparisonMainRepo.Delete(entity);
            }
            return Success("删除成功。");
        }

        #endregion Main

    }
}