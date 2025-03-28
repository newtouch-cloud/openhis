using Newtouch.HIS.Application;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class SysChargeProjPriceAdjController : ControllerBase
    {
        // GET: SystemManage/SysChargeProjPriceAdj
        private readonly ISysChargeProjPriceAdjApp _sysChargeProjPriceAdjApp;

        /// <summary>
        /// 视图
        /// </summary>
        /// <returns></returns>
        public ActionResult SysChargeProjPriceAdj()
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
            var data = _sysChargeProjPriceAdjApp.GetEffectiveList(keyword);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 新增 项目调价不允许修改
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(int? keyValue)
        {
            var entity = _sysChargeProjPriceAdjApp.GetForm(keyValue);
            return Content(entity.ToJson());
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteForm(SysChargeItemPriceAdjustEntity sysChargeProjPriceAdjEntity)
        {
            _sysChargeProjPriceAdjApp.DeleteForm(sysChargeProjPriceAdjEntity);
            return Success("操作成功。");
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(SysChargeItemPriceAdjustEntity sysChargeProjPriceAdjEntity, int? keyValue)
        {
            _sysChargeProjPriceAdjApp.SubmitForm(sysChargeProjPriceAdjEntity, keyValue);
            return Success("操作成功。");
        }
    }
}