using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Core.Common;
using Newtouch.Tools;
using System.Web.Mvc;

namespace FrameworkBase.MultiOrg.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-17 16:13
    /// 描 述：系统日志
    /// </summary>
    [AutoResolveIgnore]
    public class SysLogController : OrgControllerBase
    {
        private readonly ISysLogRepo _sysLogRepo;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="_sysLogRepo"></param>
        public SysLogController(ISysLogRepo _sysLogRepo)
        {
            this._sysLogRepo = _sysLogRepo;
        }

        /// <summary>
        /// 分页列表 grid json
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
                rows = _sysLogRepo.GetPaginationList(pagination, this.OrganizeId, queryJson),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

    }
}
