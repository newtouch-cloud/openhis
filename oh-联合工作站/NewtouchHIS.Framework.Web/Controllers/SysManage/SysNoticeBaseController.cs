using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Filter;
using static NewtouchHIS.Lib.Base.BaseEnum;

namespace NewtouchHIS.Framework.Web.Controllers.SysManage
{
    /// <summary>
    /// 消息管理
    /// </summary>
    public class SysNoticeBaseController : OrgControllerBase
    {
        protected readonly IMsgQueueDmnService _msgQueueDmn;
        public SysNoticeBaseController(IMsgQueueDmnService msgQueueDmn)
        {
            _msgQueueDmn = msgQueueDmn;
        }
        [HttpGet]
        [HandlerAuthorize]
        public override IActionResult Index()
        {
            ViewBag.IsHospAdministrator = UserIdentity?.IsHospAdministrator;
            return View();
        }
        /// <summary>
        /// 获取消息列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public async Task<IActionResult> GetNoticeList(OLPagination<MsgNoticeQueueQueryVO> request)
        {
            if (request.queryParams == null)
            {
                request.queryParams = new MsgNoticeQueueQueryVO()
                {
                    OrganizeId = OrganizeId,
                    NoticeStu = (int)NoticeStuEnum.UnSend,
                    ksrq = DateTime.Now.AddMonths(-12)
                };
            }
            else
            {
                request.queryParams.OrganizeId = request.queryParams.OrganizeId ?? OrganizeId;
            }
            if (request.queryParams.msgtype == 1)
            {
                request.queryParams.Recipient = UserIdentity?.UserCode;
            }
            else if(request.queryParams.msgtype == 2)
            {
                request.queryParams.SendFrom = UserIdentity?.UserCode;
            }
            request.queryParams.OrganizeId = OrganizeId;
            var data = await _msgQueueDmn.NoticeQueryPage(request, "");
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        public async Task<IActionResult> NoticeStuRead(string keyValue)
        {
            if(!string.IsNullOrEmpty(keyValue))
            {
                var result = await _msgQueueDmn.NoticeStuModify(keyValue, UserIdentity.UserCode, NoticeStuEnum.Read);
                if(result)
                {
                    return Success("消息已读");
                }
            }
            return Content("消息已读任务处理失败");
        }

    }
}
