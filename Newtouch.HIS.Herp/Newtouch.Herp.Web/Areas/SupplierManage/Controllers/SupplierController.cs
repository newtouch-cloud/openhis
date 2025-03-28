using System.Linq;
using System.Web.Mvc;
using Newtouch.Core.Common;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Domain.ValueObjects;
using Newtouch.Herp.Infrastructure.Enum;
using Newtouch.Tools;

namespace Newtouch.Herp.Web.Areas.SupplierManage.Controllers
{
    /// <summary>
    /// 供应商
    /// </summary>
    public class SupplierController : ControllerBase
    {

        private readonly IGysSupplierRepo _gysSupplierRepo;
        private readonly IGysContactsRepo _gysContactsRepo;
        private readonly IGysSupplierDmnService _gysSupplierDmnService;
        private readonly ISupplierApp _supplierApp;

        /// <summary>
        /// 获取供应商列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult GetSupplierGridJson(Pagination pagination, string keyWord)
        {
            var data = new
            {
                rows = _gysSupplierRepo.GetList(pagination, OrganizeId, keyWord),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// get enable product unit 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetSupplierSelectJson()
        {
            var list = _gysSupplierRepo.IQueryable(p => p.zt == ((int)Enumzt.Enable).ToString()).ToList();
            return Content(list.ToJson());
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteForm(string keyValue)
        {
            _gysSupplierDmnService.DeleteSupplier(keyValue);
            return Success("删除成功");
        }

        /// <summary>
        /// get supplier information
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyWord)
        {
            var supplier = _gysSupplierRepo.FindEntity(p => p.Id == keyWord && p.OrganizeId == OrganizeId);
            return Content(supplier.ToJson());
        }

        /// <summary>
        /// get supplier contacts
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult GetSupplierContactsGridJson(string keyWord)
        {
            var contacts = _gysContactsRepo.IQueryable(p => p.supplierId == keyWord && p.OrganizeId == OrganizeId).ToList();
            return Content(contacts.ToJson());
        }

        /// <summary>
        /// 供应商维护 表单提交
        /// </summary>
        /// <param name="supplierEntity"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult SubmitForm(GysSupplierEntity supplierEntity, string keyWord)
        {
            supplierEntity.zt = supplierEntity.zt == "true" ? "1" : "0";
            supplierEntity.OrganizeId = OrganizeId;
            _supplierApp.SubmitForm(supplierEntity, keyWord);
            return Success("操作成功", null);
        }

        /// <summary>
        /// 添加联系人
        /// </summary>
        /// <param name="contactInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SubmitContacts(SupplierContactVO contactInfo)
        {
            if (contactInfo != null && !string.IsNullOrWhiteSpace(contactInfo.id))
            {
                return _gysContactsRepo.UpdateContact(contactInfo) > 0 ? Success("修改联系人成功") : Error("修改联系人失败"); ;
            }
            return _gysContactsRepo.InserContact(contactInfo, OrganizeId) > 0 ? Success("添加联系人成功") : Error("添加联系人失败");
        }

        /// <summary>
        /// delete contacts
        /// </summary>
        /// <param name="contactsId"></param>
        /// <returns></returns>
        public ActionResult DeleteContacts(string contactsId)
        {
            var entity = _gysContactsRepo.FindEntity(p => p.Id == contactsId && p.OrganizeId == OrganizeId);
            if (entity != null && !string.IsNullOrWhiteSpace(entity.Id)) _gysContactsRepo.Delete(entity);
            return Success("删除联系人成功");
        }

        /// <summary>
        /// 获取生产商
        /// </summary>
        /// <returns></returns>
        public ActionResult GetProducers(string keyword)
        {
            var result = _gysSupplierRepo.GetList(OrganizeId, keyword, (int)EnumSupplierType.Producer, ((int)Enumzt.Enable).ToString());
            return Content(result.ToJson());
        }

        /// <summary>
        /// 获取供应商
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSuppliers(string keyword)
        {
            var result = _gysSupplierRepo.GetList(OrganizeId, keyword, (int)EnumSupplierType.Distributor, ((int)Enumzt.Enable).ToString());
            return Content(result.ToJson());
        }
    }
}