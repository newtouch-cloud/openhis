using Newtouch.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.HIS.Base.HOSP.Areas.PharmacyDrugStorage.Controllers
{
    public class SysMedicineUnitController : ControllerBase
    {       
        /// <summary>
        /// 
        /// </summary>
        private readonly ISysMedicineUnitRepo _SysMedicineUnitRepo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SysMedicineUnitRepo"></param>
        public SysMedicineUnitController(ISysMedicineUnitRepo SysMedicineUnitRepo)
        {
            this._SysMedicineUnitRepo = SysMedicineUnitRepo;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetGridJson(string keyword)
        {
            var data = _SysMedicineUnitRepo.GetList(keyword);
            return Content(data.ToJson());
        }

        // GET: PharmacyUnit
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFormJson(int? keyValue)
        {
            var data = _SysMedicineUnitRepo.FindEntity(keyValue);
            return Content(data.ToJson());
        }

        public ActionResult submitForm(SysMedicineUnitEntity entity, int? keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            if (string.IsNullOrWhiteSpace(entity.ypdwCode))
            {
                throw new FailedException("请填写编码");
            }
            _SysMedicineUnitRepo.submitForm(entity, keyValue);
            return Success("操作成功");
        }

    }
}