using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Framework.Web.Controllers.SysManage;

namespace HIS.SSO.Areas.SysManage.Controllers
{
    [Area("SysManage")]
    public class NoticeController : SysNoticeBaseController
    {
        public NoticeController(IMsgQueueDmnService msgQueueDmn) : base(msgQueueDmn)
        {
        }

    }
}
