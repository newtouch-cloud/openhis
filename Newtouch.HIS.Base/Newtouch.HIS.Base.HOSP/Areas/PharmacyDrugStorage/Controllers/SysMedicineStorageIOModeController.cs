using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.HIS.Base.HOSP.Areas.PharmacyDrugStorage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class SysMedicineStorageIOModeController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ISysMedicineStorageIOModeRepo _sysMedicineStorageIOModeRepo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SysMedicineUnitRepo"></param>
        public SysMedicineStorageIOModeController(ISysMedicineStorageIOModeRepo sysMedicineStorageIOModeRepo)
        {
            this._sysMedicineStorageIOModeRepo = sysMedicineStorageIOModeRepo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetGridJson(string keyword)
        {
            var data = _sysMedicineStorageIOModeRepo.GetList(keyword);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFormJson(int? keyValue)
        {
            var data = _sysMedicineStorageIOModeRepo.FindEntity(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult submitForm(SysMedicineStorageIOModeEntity entity, int? keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            if (string.IsNullOrWhiteSpace(entity.crkfsCode))
            {
                throw new FailedException("请填写编码");
            }
            _sysMedicineStorageIOModeRepo.SubmitForm(entity, keyValue);
            return Success("操作成功");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteForm(int keyValue)
        {
            var entity = _sysMedicineStorageIOModeRepo.FindEntity(p => p.crkfsId == keyValue);
            if (entity == null) return Error("未找到指定发药方式");
            return _sysMedicineStorageIOModeRepo.Delete(entity) > 0 ? Success() : Error("删除失败");
        }
    }
}