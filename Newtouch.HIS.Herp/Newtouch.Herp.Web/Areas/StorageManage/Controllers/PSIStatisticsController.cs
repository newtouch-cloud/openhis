using System.Web.Mvc;
using Newtouch.Core.Common;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Infrastructure;
using Newtouch.Tools;

namespace Newtouch.Herp.Web.Areas.StorageManage.Controllers
{
    /// <summary>
    /// 进销存统计
    /// </summary>
    public class PSIStatisticsController : ControllerBase
    {
        private readonly IKcKcjzDmnService _kcKcjzDmnService;

        /// <summary>
        /// 视图
        /// </summary>
        /// <returns></returns>
        public override ActionResult Index()
        {
            ViewBag.OrganizeId = OrganizeId;
            return base.Index();
        }

        /// <summary>
        /// 进销存统计
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="searchParam"></param>
        /// <returns></returns>
        public ActionResult PSIStatisticsInfoList(Pagination pagination, PsiStatisticsDTO searchParam)
        {
            var list = new
            {
                rows = _kcKcjzDmnService.GetPsiStatistics(pagination, searchParam, Constants.CurrentKf.currentKfId, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }
    }
}