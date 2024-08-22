using Newtouch.HIS.Application;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Tools;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class SysPatiChargeWaiverController : ControllerBase
    {
        private readonly ISysPatiChargeWaiverApp _sysPatiChargeWaiverApp;

        /// <summary>
        /// 视图
        /// </summary>
        /// <returns></returns>
        public ActionResult SysPatiChargeWaiver()
        {
            return View();
        }
        /// <summary>
        /// 获取有效列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetGridJson(string keyword)
        {
            var data = _sysPatiChargeWaiverApp.GetEffectiveList(keyword);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(int keyValue)
        {
            var entity = _sysPatiChargeWaiverApp.GetEffectiveList(null, keyValue).FirstOrDefault();
            return Content(entity.ToJson());
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteForm(int keyValue)
        {
            _sysPatiChargeWaiverApp.DeleteForm(keyValue);
            return Success("操作成功。");
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(SysPatientChargeWaiverEntity sysPatiChargeWaiverEntity, int? keyValue)
        {
            sysPatiChargeWaiverEntity.zt = sysPatiChargeWaiverEntity.zt == "true" ? "1" : "0";
            _sysPatiChargeWaiverApp.SubmitForm(sysPatiChargeWaiverEntity, keyValue);
            return Success("操作成功。");
        }

    }
}
