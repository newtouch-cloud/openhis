using Newtouch.HIS.Application;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    public class SysAgInsurChargeCategController : ControllerBase
    {
        private ISysAgInsurChargeCategApp _sysAgInsurChargeCategApp;

        public SysAgInsurChargeCategController(ISysAgInsurChargeCategApp sysAgInsurChargeCategApp)
        {
            this._sysAgInsurChargeCategApp = sysAgInsurChargeCategApp;
        }
        /// <summary>
        /// 视图
        /// </summary>
        /// <returns></returns>
        public ActionResult SysAgInsurChargeCateg()
        {
            return View();
        }
        /// <summary>
        /// 获取有效列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetGridJson(int keyword)
        {
            var data = _sysAgInsurChargeCategApp.GetEffectiveList(keyword);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(int keyValue)
        {
            var entity = _sysAgInsurChargeCategApp.GetForm(keyValue);
            return Content(entity.ToJson());
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteForm(int keyValue)
        {
            _sysAgInsurChargeCategApp.DeleteForm(keyValue);
            return Success("操作成功。");
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(SysAgriInsuranceChargeCategoryEntity sysAgInsurChargeCategEntity, int keyValue)
        {
            _sysAgInsurChargeCategApp.SubmitForm(sysAgInsurChargeCategEntity, keyValue);
            return Success("操作成功。");
        }


    }
}
