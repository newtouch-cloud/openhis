using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.HIS.Base.HOSP.Areas.Sys.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class SysNationController : ControllerBase
    {
        private readonly ISysNationRepo _sysNationRepo;

        /// <summary>
        /// 
        /// </summary>
        public SysNationController(ISysNationRepo sysNationRepo)
        {
            this._sysNationRepo = sysNationRepo;
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(string keyword)
        {
            var data = _sysNationRepo.GetList(keyword);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(int keyValue)
        {
            var entity = _sysNationRepo.GetForm(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(SysNationEntity entity, int? keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            _sysNationRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }

    }
}