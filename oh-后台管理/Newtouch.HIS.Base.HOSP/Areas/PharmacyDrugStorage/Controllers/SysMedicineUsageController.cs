using Newtouch.Common;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.HIS.Base.HOSP.Areas.PharmacyDrugStorage.Controllers
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

        public override ActionResult Form()
        {
            return View();
        }


        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public ActionResult SubmitForm(SysMedicineUsageEntity entity, int? yfId)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            _sysMedicineUsageRepo.SubmitForm(entity, yfId);
            return Success("操作成功。");
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

        /// <summary>
        /// 新增或修改Form
        /// </summary>
        public ActionResult GetFormJson(int? yfId)
        {
            var entity = _sysMedicineUsageRepo.GetMedicineUsageEntity(yfId);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 删除
        /// </summary>
        public ActionResult DeleteForm(int yfId)
        {
            _sysMedicineUsageRepo.DeleteForm(yfId);
            return Success("操作成功。");
        }

    }
}