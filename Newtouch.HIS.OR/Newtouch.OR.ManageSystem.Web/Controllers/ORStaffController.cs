using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using System.Web.Mvc;
using Newtouch.Tools;
using FrameworkBase.MultiOrg.Web;

namespace Newtouch.OR.ManageSystem.Web.Controllers
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2019-11-06 10:22
    /// 描 述：手术人员
    /// </summary>
    public class ORStaffController : OrgControllerBase
    {
        private readonly IORStaffRepo _oRStaffRepo;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="oRStaffRepo"></param>
        public ORStaffController(IORStaffRepo oRStaffRepo)
        {
            this._oRStaffRepo = oRStaffRepo;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = _oRStaffRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ActionResult SubmitForm(ORStaffEntity entity, string keyValue)
        {
            _oRStaffRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ActionResult DeleteForm(string keyValue)
        {
            _oRStaffRepo.DeleteForm(keyValue);
            return Success("操作成功。");
        }

    }
}