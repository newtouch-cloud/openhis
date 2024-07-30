using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.HIS.Base.Controllers
{
    public class SysMedicineClassificationController : ControllerBase
    {
        private readonly ISysMedicineClassificationRepo _sysMedicineClassificationRepo;

        public SysMedicineClassificationController(ISysMedicineClassificationRepo sysMedicineClassificationRepo)
        {
            this._sysMedicineClassificationRepo = sysMedicineClassificationRepo;
        }

        /// <summary>
        /// 根据关键字查询药品分类
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public JsonResult GetListSelectJson(string keyword)
        {
            var list = _sysMedicineClassificationRepo.GetValidList(keyword);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = _sysMedicineClassificationRepo.GetPagintionList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFormJson(int? keyValue)
        {
            var data = _sysMedicineClassificationRepo.FindEntity(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult submitForm(SysMedicineClassificationEntity entity, int? keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            if (string.IsNullOrWhiteSpace(entity.ypflCode))
            {
                throw new FailedException("请填写编码");
            }
            if (string.IsNullOrWhiteSpace(entity.ypflmc))
            {
                throw new FailedException("请填写名称");
            }
            _sysMedicineClassificationRepo.submitForm(entity, keyValue);
            return Success("操作成功");
        }




    }
}