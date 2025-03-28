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
    /// 描 述：手术室
    /// </summary>
    public class ORRoomController : OrgControllerBase
    {
        private readonly IORRoomRepo _oRRoomRepo;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="oRRoomRepo"></param>
        public ORRoomController(IORRoomRepo oRRoomRepo)
        {
            this._oRRoomRepo = oRRoomRepo;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = _oRRoomRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ActionResult SubmitForm(ORRoomEntity entity, string keyValue)
        {
            _oRRoomRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ActionResult DeleteForm(string keyValue)
        {
            _oRRoomRepo.DeleteForm(keyValue);
            return Success("操作成功。");
        }

    }
}