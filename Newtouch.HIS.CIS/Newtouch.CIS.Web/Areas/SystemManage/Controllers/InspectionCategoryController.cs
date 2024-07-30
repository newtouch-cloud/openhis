using FrameworkBase.MultiOrg.Web;
using Newtouch.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Areas.SystemManage.Controllers
{
    public class InspectionCategoryController : OrgControllerBase
    {
        // GET: SystemManage/InspectionCategory
        private readonly IInspectionCategoryRepo _inspectionCategoryRepo;

        [HttpGet]
        [HandlerAjaxOnly]
        /// <summary>
        /// 获取词典列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetTreeGridJson(string keyword)
        {
            var data = _inspectionCategoryRepo.GetListByOrg(this.OrganizeId).ToList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.Where(t => t.dlmc.Contains(keyword) || t.dlCode.Contains(keyword)).ToList();
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
            var entity = _inspectionCategoryRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SubmitForm(InspectionCategoryEntity entity, string keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            entity.OrganizeId = this.OrganizeId;
            _inspectionCategoryRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteForm(string keyValue)
        {
            _inspectionCategoryRepo.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        public ActionResult Chat()
        {
            return View();
        }
    }
}