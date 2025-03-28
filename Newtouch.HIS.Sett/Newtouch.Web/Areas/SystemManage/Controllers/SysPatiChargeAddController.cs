using Newtouch.HIS.Application;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Tools;
using System.Web.Mvc;
using Newtouch.HIS.Domain.IDomainServices;
using System.Linq;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class SysPatiChargeAddController : ControllerBase
    {
        private readonly ISysPatiChargeAddApp _sysPatiChargeAddApp;
        private readonly ISysChargeAddCategApp _xt_SysChargeAddCategApp;
        private readonly ISysPatiChargeAddDmnService _sysPatiChargeAddDmnService;

        /// <summary>
        /// 视图
        /// </summary>
        /// <returns></returns>
        public ActionResult SysPatiChargeAdd()
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
            var data = _sysPatiChargeAddApp.GetSysPatiChargeAddVoList(keyword);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 附加显示大类
        /// </summary>
        /// <returns></returns>
        public ActionResult GetfjsfdlList(string keyValue)
        {
            var data = _sysPatiChargeAddApp.GetfjsfdlList(keyValue);
            return Json(data);
        }
        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(int keyValue)
        {
            var entity = _sysPatiChargeAddApp.GetSysPatiChargeAddVoList(null, keyValue).FirstOrDefault();
            return Content(entity.ToJson());
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteForm(SysPatientChargeAdditionalEntity SysPatiChargeAddEntity)
        {
            _sysPatiChargeAddApp.DeleteForm(SysPatiChargeAddEntity);
            return Success("操作成功。");
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(SysPatientChargeAdditionalEntity SysPatiChargeAddEntity, int? keyValue)
        {
            SysPatiChargeAddEntity.zt = SysPatiChargeAddEntity.zt == "true" ? "1" : "0";
            _sysPatiChargeAddApp.SubmitForm(SysPatiChargeAddEntity, keyValue);
            return Success("操作成功。");
        }

    }
}
