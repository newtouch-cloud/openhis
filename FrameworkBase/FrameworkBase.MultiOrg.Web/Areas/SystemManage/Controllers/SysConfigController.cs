using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.IDomainServices.SystemManage;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Core.Common;
using Newtouch.Tools;
using System.Web.Mvc;

namespace FrameworkBase.MultiOrg.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 系统配置
    /// </summary>
    [AutoResolveIgnore]
    public class SysConfigController : OrgControllerBase
    {
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly ISysConfigDmnService _sysConfigDmn;
        private readonly ISystemConfigTemplateRepo _sysConfigTemplateRepo;


        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="sysConfigRepo"></param>
        /// <param name="sysConfigDmn"></param>
        /// <param name="sysConfigTemplateRepo"></param>
        public SysConfigController(ISysConfigRepo sysConfigRepo, ISysConfigDmnService sysConfigDmn,ISystemConfigTemplateRepo sysConfigTemplateRepo)
        {
            this._sysConfigRepo = sysConfigRepo;
            this._sysConfigDmn = sysConfigDmn;
            this._sysConfigTemplateRepo = sysConfigTemplateRepo;
        }

        /// <summary>
        /// grid json
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetGridJson(string keyword)
        {
            var data = _sysConfigDmn.GetAllConfigs(keyword, this.OrganizeId);
            //data = _sysConfigRepo.GetList(keyword, this.OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = _sysConfigDmn.GetConfigForm(keyValue);
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
            if (string.IsNullOrWhiteSpace(this.OrganizeId))
            {
                return Error("操作失败。");
            }
            entity.zt = entity.zt == "true" ? "1" : "0";
            entity.OrganizeId = this.OrganizeId;
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
        /// 配置View页
        /// </summary>
        /// <returns></returns>
        public ActionResult ConfigsPreviewIndex()
        {
            return View();
        }
        /// <summary>
        /// 公共模板页
        /// </summary>
        /// <returns></returns>
        public ActionResult TemplateIndex()
        {
            return View();
        }
        /// <summary>
        /// 公共模板Form
        /// </summary>
        /// <returns></returns>
        public ActionResult TemplateForm()
        {
            return View();
        }
        /// <summary>
        /// 获取模板列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetGridJsonTmp(string keyword)
        {
            var data = _sysConfigTemplateRepo.GetListTmp(keyword);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 配置模板明细
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJsonTmp(string keyValue)
        {
            // var data = _sysConfigTemplateRepo.GetTemplateInfo(keyValue);
            var data = _sysConfigTemplateRepo.FindEntity(keyValue);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 修改/新增公共模板
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitFormTmp(SysConfigTemplateEntity entity, string keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            _sysConfigTemplateRepo.SubmitFormTmp(entity, keyValue);
            return Success("操作成功。");
        }

        /// <summary>
        /// 删除配置模板
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult DeleteFormTmp(string keyValue)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                _sysConfigTemplateRepo.Delete(p => p.Id == keyValue);
            }
            return Success("删除成功。");
        }
    }
}