using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using System.Web.Mvc;
using Newtouch.Tools;
using Newtouch.Core.Common;
using System;

namespace Newtouch.OR.ManageSystem.Web.Controllers
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2019-10-31 14:15
    /// 描 述：手术字典表
    /// </summary>
    public class OROperationController : Controller
    {
        private readonly IOROperationRepo _oROperationRepo;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="oROperationRepo"></param>
        public OROperationController(IOROperationRepo oROperationRepo)
        {
            this._oROperationRepo = oROperationRepo;
        }

        /// <summary>
        /// 获取分页实体列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="keyword">筛选关键字</param>
        /// <returns></returns>
        public ActionResult GetPagintionGridJson(Pagination pagination, string keyword)
        {
            var list = new
            {
                rows = _oROperationRepo.GetPagintionList(pagination, keyword,""),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = _oROperationRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ActionResult SubmitForm(OROperationEntity entity, string keyValue)
        {
            _oROperationRepo.SubmitForm(entity, keyValue);
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
            _oROperationRepo.DeleteForm(keyValue);
            return Success("操作成功。");
        }

    }
}