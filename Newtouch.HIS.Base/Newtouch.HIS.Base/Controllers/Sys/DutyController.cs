using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.HIS.Base.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class DutyController : ControllerBase
    {
        private readonly ISysDutyRepo _sysDutyRepo;

        public DutyController(ISysDutyRepo sysDutyRepo)
        {
            this._sysDutyRepo = sysDutyRepo;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetGridJson(string keyword)
        {
            var data = _sysDutyRepo.GetList(keyword);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = _sysDutyRepo.GetForm(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(SysDutyEntity entity, string keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            _sysDutyRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }

    }
}