using Newtouch.HIS.Application;
using Newtouch.Tools;
using System.Web.Mvc;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Common;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    public class SysMRChargeClassController : ControllerBase
    {
        // GET: SystemManage/SysMediRecChargeCateg
        private ISysMRChargeClassApp _sysMRChargeClassApp;

        public SysMRChargeClassController(ISysMRChargeClassApp sysMRChargeClassApp)
        {
            this._sysMRChargeClassApp = sysMRChargeClassApp;
        }
        /// <summary>
        /// 视图
        /// </summary>
        /// <returns></returns>
        public ActionResult SysMRChargeClass()
        {
            return View();
        }
        /// <summary>
        /// 获得有效列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetGridJson(string keyword)
        {
            var data = _sysMRChargeClassApp.GetEffectiveList(keyword);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(int keyValue)
        {

            var entity = _sysMRChargeClassApp.GetForm(keyValue);
            return Content(entity.ToJson());
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteForm(SysMedicalRecordChargeCategoryEntity sysMRChargeClassEntity)
        {
            _sysMRChargeClassApp.DeleteForm(sysMRChargeClassEntity);
            return Success("操作成功。");
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sysMRChargeClassEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(SysMedicalRecordChargeCategoryEntity sysMRChargeClassEntity, int? keyValue)
        {
            sysMRChargeClassEntity.zt = sysMRChargeClassEntity.zt == "true" ? "1" : "0";
            _sysMRChargeClassApp.SubmitForm(sysMRChargeClassEntity, keyValue);
            return Success("操作成功。");
        }

        /// <summary>
        /// 获取中文拼音
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public ActionResult GetSpell(string strText)
        {
            var py = StringUtil.GetChineseSpell(strText);
            return Content(py);
        }
    }
}