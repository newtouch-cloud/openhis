using Newtouch.Core.Common;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.HIS.Base.HOSP.Controllers
{
    public class SysMedicineUsageController : ControllerBase
    {
        private readonly ISysMedicineUsageRepo _sysMedicineUsageRepo;

        public SysMedicineUsageController(ISysMedicineUsageRepo sysMedicineUsageRepo)
        {
            this._sysMedicineUsageRepo = sysMedicineUsageRepo;
        }

        // GET: SysMedicineUsage
        public override ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取有效列表
        /// </summary>
        public ActionResult GetGridJson(Pagination pagination, string keyword = null)
        {
            pagination.sidx = "CreateTime desc";
            pagination.sord = "asc";
            var list = _sysMedicineUsageRepo.GetPagintionList(pagination, keyword);
            var data = new
            {
                rows = list,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

    }
}