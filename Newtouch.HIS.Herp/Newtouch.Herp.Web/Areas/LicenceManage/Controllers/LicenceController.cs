using System;
using System.Linq;
using System.Web.Mvc;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Utils;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure.Enum;
using Newtouch.Tools;

namespace Newtouch.Herp.Web.Areas.LicenceManage.Controllers
{
    /// <summary>
    /// 证照管理
    /// </summary>
    public class LicenceController : ControllerBase
    {
        private readonly ILicLicenceBelongedRepo _licLicenceBelongedRepo;
        private readonly ILicLicenceDmnService _licLicenceDmnService;
        private readonly ILicLicenceTypeRepo _licLicenceTypeRepo;
        private readonly ILicLicenceRepo _licLicenceRepo;
        private readonly ILicLicenceApp _licLicence;

        #region 主功能

        /// <summary>
        /// 证照维护
        /// </summary>
        /// <returns></returns>
        public override ActionResult Form()
        {
            var maxSize = ConfigurationHelper.GetAppConfigValue("imageMaxSize");
            maxSize = string.IsNullOrWhiteSpace(maxSize) ? 5.ToString() : maxSize;
            ViewData["maxSize"] = Convert.ToInt32(maxSize);
            return View();
        }

        /// <summary>
        /// 获取证照列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyWord"></param>
        /// <param name="belongedId"></param>
        /// <param name="licenceTypeId"></param>
        /// <returns></returns>
        public ActionResult GetLicenceGridJson(Pagination pagination, string keyWord, string belongedId, string licenceTypeId)
        {
            var data = new
            {
                rows = _licLicenceDmnService.GetLicenceList(pagination, keyWord, belongedId, licenceTypeId, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 删除证照
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteLicForm(string keyValue)
        {
            var entity = _licLicenceRepo.FindEntity(p => p.Id == keyValue);
            if (entity == null) return Error("删除失败");
            return _licLicenceRepo.Delete(entity) > 0 ? Success("删除成功") : Error("删除失败");
        }

        /// <summary>
        /// 获取证照信息
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult GetLicenceFormJson(string keyWord)
        {
            var entity = _licLicenceRepo.FindEntity(p => p.Id == keyWord);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 证照维护 表单提交
        /// </summary>
        /// <param name="licLicenceEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult SubmitLicenceForm(LicLicenceEntity licLicenceEntity, string keyValue)
        {
            var file = Request.Files["fileUpload"];
            licLicenceEntity.zt = licLicenceEntity.zt == "on" ? "1" : "0";
            licLicenceEntity.OrganizeId = OrganizeId;
            licLicenceEntity.fileUrl = _licLicence.UploadFile(file);
            return _licLicenceRepo.SubmitForm(licLicenceEntity, keyValue) > 0 ? Success("操作成功", null) : Error("操作失败");
        }

        /// <summary>
        /// 获取文件地址
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetFileUrl(string t)
        {
            var entity = _licLicenceRepo.FindEntity(p => p.Id == t);
            if (entity != null && !string.IsNullOrWhiteSpace(entity.fileUrl))
            {
                var result = new
                {
                    Authority = Request.Url == null ? "" : Request.Url.Authority,
                    uri = entity.fileUrl
                }.ToJson();
                return Content(result);
            }
            return Content("");
        }

        #endregion

        #region 证照类型

        /// <summary>
        /// 证照类型
        /// </summary>
        /// <returns></returns>
        public ActionResult LicenceType()
        {
            return View();
        }

        /// <summary>
        /// 证照类型
        /// </summary>
        /// <returns></returns>
        public ActionResult LicenceTypeForm()
        {
            return View();
        }

        /// <summary>
        /// 获取证照类型列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult GetLicenceTypeGridJson(Pagination pagination, string keyWord = "")
        {
            var data = new
            {
                rows = _licLicenceDmnService.GetLicenceTypeList(pagination, keyWord),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 删除证照类型
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteLicenceType(string keyValue)
        {
            var entity = _licLicenceTypeRepo.FindEntity(p => p.Id == keyValue);
            if (entity == null) return Error("删除失败");
            return _licLicenceTypeRepo.Delete(entity) > 0 ? Success("删除成功") : Error("删除失败");
        }

        /// <summary>
        /// 获取证照类型信息
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult GetLicenceTypeFormJson(string keyWord)
        {
            var entity = _licLicenceTypeRepo.FindEntity(p => p.Id == keyWord);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Submit SyyyForm
        /// </summary>
        /// <param name="source"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult SubmitLicenceTypeForm(LicLicenceTypeEntity source, string keyWord)
        {
            source.zt = source.zt == "true" ? "1" : "0";
            return _licLicenceTypeRepo.SubmitForm(source, keyWord) > 0 ? Success("操作成功") : Error("操作失败");
        }

        /// <summary>
        /// 根据所属ID获取类型明细
        /// </summary>
        /// <param name="belonged"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetlicenceType(string belonged = "")
        {
            return Content(_licLicenceTypeRepo.IQueryable(p => p.belongedId.Contains(belonged) && p.zt == ((int)Enumzt.Enable).ToString()).Select(s => new { id = s.Id, text = s.typeName }).ToJson());
        }

        #endregion

        #region 证照所属

        /// <summary>
        /// 证照类型
        /// </summary>
        /// <returns></returns>
        public ActionResult LicenceBelonged()
        {
            return View();
        }

        /// <summary>
        /// 证照类型
        /// </summary>
        /// <returns></returns>
        public ActionResult LicenceBelongedForm()
        {
            return View();
        }

        /// <summary>
        /// 获取证照所属列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult GetLicenceBelongedGridJson(Pagination pagination, string keyWord = "")
        {
            var data = new
            {
                rows = _licLicenceBelongedRepo.FindList(p => "" == keyWord || p.belonged.Contains(keyWord.Trim()), pagination),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 删除证照所属
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteLicenceBelonged(string keyValue)
        {
            var entity = _licLicenceBelongedRepo.FindEntity(p => p.Id == keyValue);
            if (entity == null) return Error("删除失败");
            return _licLicenceBelongedRepo.Delete(entity) > 0 ? Success("删除成功") : Error("删除失败");
        }

        /// <summary>
        /// 获取证照所属信息
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult GetLicenceBelongedFormJson(string keyWord)
        {
            var entity = _licLicenceBelongedRepo.FindEntity(p => p.Id == keyWord);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 获取证照所属信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetLicenceBelongedForSelect()
        {
            var entity = _licLicenceBelongedRepo.IQueryable(p => p.zt == ((int)Enumzt.Enable).ToString()).Select(p => new { id = p.Id, text = p.belonged }).Distinct();
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Submit LicenceBelongedForm
        /// </summary>
        /// <param name="source"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult SubmitLicenceBelongedForm(LicLicenceBelongedEntity source, string keyWord)
        {
            source.zt = source.zt == "true" ? "1" : "0";
            return _licLicenceBelongedRepo.SubmitForm(source, keyWord) > 0 ? Success("操作成功") : Error("操作失败");
        }

        /// <summary>
        /// 获取所属
        /// </summary>
        /// <returns></returns>
        public ActionResult Getbelonged()
        {
            return Content(_licLicenceBelongedRepo.IQueryable(p => p.zt == ((int)Enumzt.Enable).ToString())
                .Select(s => new { id = s.Id, text = s.belonged })
                .ToJson());
        }
        #endregion
    }
}