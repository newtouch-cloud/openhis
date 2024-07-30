using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using System.Web.Mvc;
using Newtouch.Tools;
using System;
using FrameworkBase.MultiOrg.Web;

namespace Newtouch.OR.ManageSystem.Web.Controllers
{
    public class ORApplyInfoController : BaseController
    {
        //// GET: ORApplyInfo
        //public ActionResult Index()
        //{
        //    return View();
        //}

        private readonly IORApplyInfoRepo _oRApplyInfoRepo;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="oRApplyInfoRepo"></param>
        public ORApplyInfoController(IORApplyInfoRepo oRApplyInfoRepo)
        {
            this._oRApplyInfoRepo = oRApplyInfoRepo;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = _oRApplyInfoRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ActionResult SubmitForm(ORApplyInfoEntity entity, string keyValue)
        {
            _oRApplyInfoRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }

        private ActionResult Success(string v)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ActionResult DeleteForm(string keyValue)
        {
            //_oRApplyInfoRepo.DeleteForm(keyValue);
            return Success("操作成功。");
        }
    }
}