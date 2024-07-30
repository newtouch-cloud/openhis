using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Infrastructure;
using Newtouch.Herp.Infrastructure.Enum;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.Herp.Web.Areas.DeliveryManage.Controllers
{
    /// <summary>
    /// 配送单查询
    /// </summary>
    public class DeliveryQueryController : ControllerBase
    {
        private readonly IKfCrkdjDmnService crkdjDmnService;

        /// <summary>
        /// 入库
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult InStorageDeliveryQuery(string keyword)
        {
            var result = crkdjDmnService.SelectInStorageDeliveryInfo(keyword, Constants.CurrentKf.currentKfId, UserIdentity.UserCode, OrganizeId);
            return Content(result.ToJson());
        }

        /// <summary>
        /// 获取已审核配送单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OutStorageDeliveryQuery(string keyword)
        {
            var result = crkdjDmnService.SelectInStorageDeliveryInfo(keyword, Constants.CurrentKf.currentKfId, UserIdentity.UserCode, OrganizeId, ((int)EnumAuditState.Adopt).ToString());
            return Content(result.ToJson());
        }
    }
}