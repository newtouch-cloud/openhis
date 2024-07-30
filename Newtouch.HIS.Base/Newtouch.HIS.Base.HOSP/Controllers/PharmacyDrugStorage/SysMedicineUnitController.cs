using Newtouch.Common;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.HIS.Base.HOSP.Controllers
{
    public class SysMedicineUnitController : ControllerBase
    {
        private readonly ISysMedicineUnitRepo _sysMedicineUnitRepo;

        public SysMedicineUnitController(ISysMedicineUnitRepo sysMedicineUnitRepo)
        {
            this._sysMedicineUnitRepo = sysMedicineUnitRepo;
        }

        /// <summary>
        /// 获取药品单位列表
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public ActionResult GetListSelectJson(string keyword)
        {
            var data = _sysMedicineUnitRepo.GetValidList(keyword);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetGridJson(string keyword)
        {
            var data = _sysMedicineUnitRepo.GetList(keyword);
            return Content(data.ToJson());
        }

    }
}