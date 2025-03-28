using FrameworkBase.MultiOrg.Web;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Tools;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Areas.SystemManage.Controllers
{
    public class DoctorRemarkController : OrgControllerBase
    {
        // GET: SystemManage/DoctorRemark
        private readonly ISysDoctorRemarkRepo _sysDoctorRemarkRepo;
        private readonly IBaseDataDmnService _baseDataDmnService;

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetTreeGridJson(string keyword)
        {
            var data = _sysDoctorRemarkRepo.GetListByOrg(this.OrganizeId).ToList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.Where(t => t.ztmc.Contains(keyword) || t.ztCode.Contains(keyword)).ToList();
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
            var entity = _sysDoctorRemarkRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SubmitForm(SysDoctorRemarkEntity entity, string keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            entity.OrganizeId = this.OrganizeId;
            _sysDoctorRemarkRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteForm(string keyValue)
        {
            _sysDoctorRemarkRepo.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        /// <summary>
        /// 系统嘱托 浮层
        /// </summary>
        /// <returns></returns>

        public ActionResult GetSysDoctorRemark(string keyword)
        {
            var data = _baseDataDmnService.GetSysDoctorRemark(this.OrganizeId,this.UserIdentity.DepartmentCode);
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                data = data.Where(a => a.ztCode.Contains(keyword) || a.ztmc.Contains(keyword)).ToList();
            }
            return Content(data.ToJson());
        }
    }
}