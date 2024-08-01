using FrameworkBase.Domain.Entity;
using FrameworkBase.Domain.IRepository;
using Newtouch.Core.Common;
using Newtouch.Tools;
using System.Web.Mvc;

namespace FrameworkBase.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 系统配置
    /// </summary>
    [AutoResolveIgnore]
    public class SysConfigController : BaseController
    {
        private readonly ISysConfigRepo _sysConfigRepo;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="sysConfigRepo"></param>
        public SysConfigController(ISysConfigRepo sysConfigRepo)
        {
            this._sysConfigRepo = sysConfigRepo;
        }

        /// <summary>
        /// grid json
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetGridJson(string keyword)
        {
            var data = _sysConfigRepo.GetList(keyword);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = _sysConfigRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(SysConfigEntity entity, string keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            _sysConfigRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult DeleteForm(string keyValue)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                _sysConfigRepo.Delete(p => p.Id == keyValue);
            }
            return Success("删除成功。");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ConfigsPreviewIndex()
        {
            return View();
        }




    }
}