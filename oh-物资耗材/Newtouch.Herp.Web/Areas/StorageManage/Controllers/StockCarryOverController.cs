using System.Web.Mvc;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure;
using Newtouch.Tools;

namespace Newtouch.Herp.Web.Areas.StorageManage.Controllers
{
    /// <summary>
    /// 库存结转
    /// </summary>
    public class StockCarryOverController : ControllerBase
    {
        private readonly IKcKcjzRepo _kcKcjzRepo;
        private readonly IKcKcjzDmnService _kcKcjzDmnService;

        /// <summary>
        /// 给视图中OrganizeId的赋值
        /// </summary>
        /// <returns></returns>
        public override ActionResult Index()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        /// <summary>
        /// 获取历史结转时间
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetlsjzDateTime()
        {
            var list = _kcKcjzRepo.GetlsjzDateTime(Constants.CurrentKf.currentKfId, OrganizeId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// 结转结束时间
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetJzJssjDateTime()
        {
            var list = _kcKcjzRepo.GetlsjzDateTime(Constants.CurrentKf.currentKfId, OrganizeId);
            list.Insert(0, new Domain.ValueObjects.SelectVo { value = "", text = "当前" });
            return Content(list.ToJson());
        }

        /// <summary>
        /// 查询上一次结转的时间
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult QueryLastCarryTime()
        {
            var lastCarryTime = _kcKcjzRepo.GetLastJzData(Constants.CurrentKf.currentKfId, OrganizeId);
            return Success(null, lastCarryTime == null ? "" : lastCarryTime.jzsj.ToString(Constants.DateTimeFormat));
        }

        /// <summary>
        /// 查询已结转物资信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="jzsj"></param>
        /// <param name="keyWork"></param>
        /// <returns></returns>
        public ActionResult SelectCarryOverDetail(Pagination pagination, string jzsj, string keyWork)
        {
            var list = new
            {
                rows = _kcKcjzDmnService.SelectCarryOverDetail(pagination, jzsj, keyWork, Constants.CurrentKf.currentKfId, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        /// <summary>
        /// 结转
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CarryOverProduct()
        {
            var ret = _kcKcjzDmnService.CarryOverProduct(Constants.CurrentKf.currentKfId, OrganizeId, OperatorProvider.GetCurrent().UserCode);
            return string.IsNullOrWhiteSpace(ret) ? Success() : Error("结转失败");
        }
    }
}