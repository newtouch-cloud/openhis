using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.MR.ManageSystem.Domain.IRepository;
using System.Web.Mvc;
using Newtouch.Tools;
using FrameworkBase.MultiOrg.Web;

namespace Newtouch.MR.ManageSystem.Web.Controllers
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2019-11-20 13:45
    /// 描 述：病案科室与his科室关系表
    /// </summary>
    public class MrreldeptController : OrgControllerBase
    {
        private readonly IMrreldeptRepo _mrreldeptRepo;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="mrreldeptRepo"></param>
        public MrreldeptController(IMrreldeptRepo mrreldeptRepo)
        {
            this._mrreldeptRepo = mrreldeptRepo;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = _mrreldeptRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ActionResult SubmitForm(MrreldeptEntity entity, string keyValue)
        {
            _mrreldeptRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ActionResult DeleteForm(string keyValue)
        {
            _mrreldeptRepo.DeleteForm(keyValue);
            return Success("操作成功。");
        }

    }
}