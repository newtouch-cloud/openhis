using System.Web.Mvc;
using Newtouch.Tools;
using FrameworkBase.Domain.IRepository;
using Newtouch.Core.Common;

namespace FrameworkBase.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-17 16:13
    /// 描 述：系统日志
    /// </summary>
    [AutoResolveIgnore]
    public class SysLogController : BaseController
    {
        private readonly ISysLogRepo _sysLogRepo;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="sysLogRepo"></param>
        public SysLogController(ISysLogRepo sysLogRepo)
        {
            this._sysLogRepo = sysLogRepo;
        }

        /// <summary>
        /// 获取分页实体列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetPagintionGridJson(Pagination pagination, string queryJson)
        {
            var data = new
            {
                rows = _sysLogRepo.GetPaginationList(pagination, queryJson),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

    }
}