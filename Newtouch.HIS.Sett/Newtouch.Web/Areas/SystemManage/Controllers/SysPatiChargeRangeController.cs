using Newtouch.HIS.Application;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Tools;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class SysPatiChargeRangeController : ControllerBase
    {
        // GET: SystemManage/SysPatiChargeRange
        private readonly ISysPatiChargeRangeApp _sysPatiChargeRangeApp;
        private readonly ISysMedicinApp _sysMedicinApp;

        /// <summary>
        /// 视图
        /// </summary>
        /// <returns></returns>
        public ActionResult SysPatiChargeRange()
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
            var data = _sysPatiChargeRangeApp.GetEffectiveList(keyword);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(int keyValue)
        {
            var entity = _sysPatiChargeRangeApp.GetEffectiveList(null, keyValue).FirstOrDefault();
            return Content(entity.ToJson());
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteForm(int keyValue)
        {
            _sysPatiChargeRangeApp.DeleteForm(keyValue);
            return Success("操作成功。");
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(SysPatientChargeRangeEntity sysPatiChargeRangeEntity, int? keyValue)
        {
            sysPatiChargeRangeEntity.zt = sysPatiChargeRangeEntity.zt == "true" ? "1" : "0";
            _sysPatiChargeRangeApp.SubmitForm(sysPatiChargeRangeEntity, keyValue );
            return Success("操作成功。");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keywrod"></param>
        /// <returns></returns>
        public ActionResult GetYpSelectJsonData(string keywrod)
         {
            var list = _sysMedicinApp.GetYpList(keywrod);
            return Content(list.ToJson());
        }

    }
}