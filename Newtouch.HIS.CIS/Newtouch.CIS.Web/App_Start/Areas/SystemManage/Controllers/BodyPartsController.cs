using FrameworkBase.MultiOrg.Web;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Tools;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Areas.SystemManage.Controllers
{
    public class BodyPartsController : OrgControllerBase
    {
        // GET: SystemManage/BodyParts
        private readonly ISysBodyPartsRepo _sysBodyPartsRepo;
        private readonly IBaseDataDmnService _baseDataDmnService;

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetTreeGridJson(string keyword)
        {
            var data = _sysBodyPartsRepo.GetListByOrg(this.OrganizeId).ToList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.Where(t => t.bwmc.Contains(keyword) || t.bwCode.Contains(keyword)).ToList();
            }
            return Content(data.ToJson());
        }

        /// <summary>
        /// 修改信息时，把信息带到新页面
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = _sysBodyPartsRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SubmitForm(SysBodyPartsEntity entity, string keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            entity.OrganizeId = this.OrganizeId;
            _sysBodyPartsRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteForm(string keyValue)
        {
            _sysBodyPartsRepo.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        /// <summary>
        /// 系统部位 浮层
        /// </summary>
        /// <returns></returns>

        public ActionResult GetSysBodyParts(string keyword)
        {
            var data = _baseDataDmnService.GetSysBodyParts(this.OrganizeId);
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                data = data.Where(a => a.bwCode.Contains(keyword) || a.bwmc.Contains(keyword)).ToList();
            }
            return Content(data.ToJson());
        }
        

        /// <summary>
        /// 影像部位 浮层
        /// </summary>
        /// <returns></returns>

        public ActionResult GetYxBodyParts(string keyword)
        {
            var data = _baseDataDmnService.GetYxBodyParts(this.OrganizeId);
            return Content(data.ToJson());
        }

        public ActionResult GetYxBodyMethod(string keyword) {
            var data = _baseDataDmnService.GetYxBodyMethod(this.OrganizeId,keyword);
            return Content(data.ToJson());
        }
    }
}