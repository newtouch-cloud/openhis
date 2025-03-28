using Newtouch.Core.Common;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.HIS.Base.HOSP.Controllers
{
    /// <summary>
    /// 
    /// </summary>
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
            pagination.sidx = "CreateTime desc";
            pagination.sord = "asc";
            var data = new
            {
                rows = _sysMedicineClassificationRepo.GetPagintionList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }
    }
}