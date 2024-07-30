using Newtouch.HIS.Application;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class SysChargeAddCategController : ControllerBase
    {
        private readonly ISysChargeAddCategApp _SysChargeAddCategApp;
        private readonly ISysPatiChargeAddApp _SysPatiChargeAddApp;

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
        public ActionResult GetGridJson(int keyword)
        {
            var data = _SysChargeAddCategApp.GetEffectiveList(keyword);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteForm(int keyValue)
        {
            _SysChargeAddCategApp.DeleteForm(keyValue);
            return Success("操作成功。");
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sysChargeAddCategEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(SysChargeAdditionalCategoryEntity sysChargeAddCategEntity, int keyValue)
        {
            _SysChargeAddCategApp.SubmitForm(sysChargeAddCategEntity, keyValue);
            return Success("操作成功。");
        }
        //附加显示大类下拉框
        public ActionResult GetSysChargeAddCategSelect()
        {
            var treeList = _SysChargeAddCategApp.GetSysChargeAddCategSelect();
            return Content(treeList);
        }
    }
}
