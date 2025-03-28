using Newtouch.Core.Common;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.HIS.Base.HOSP.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class SysMedicineFormulationController : ControllerBase
    {
        private readonly ISysMedicineFormulationRepo _sysMedicineFormulationRepo;

        public SysMedicineFormulationController(ISysMedicineFormulationRepo sysMedicineFormulationRepo)
        {
            this._sysMedicineFormulationRepo = sysMedicineFormulationRepo;
        }

        /// <summary>
        /// 查询药品剂型
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public JsonResult GetListSelectJson(string keyword)
        {
            var list = _sysMedicineFormulationRepo.GetValidList(keyword);
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
                rows = _sysMedicineFormulationRepo.GetPagintionList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }


    }
}