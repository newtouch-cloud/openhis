using Autofac;
using FrameworkBase.API;
using Microsoft.AspNet.SignalR;
using Newtouch.Core.Redis;
using Newtouch.HIS.API.Common.Filter;
using Newtouch.Infrastructure;
using System;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Newtouch.HIS.Base.API.Notice
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/notice")]
    
    public class NoticeController : ApiController
    {
        [IgnoreTokenDecrypt]
        [HttpGet]
        [Route("getmsg/{nick}/{id}")]
        public string Get(string nick,string id)
        {
            var hub = GlobalHost.ConnectionManager.GetHubContext<noticeHub>();
            hub.Clients.All.showMessage(nick + "," + id + "," + DateTime.Now.ToUniversalTime().ToString());
            return "value";
        }
        [IgnoreTokenDecrypt]
        [HttpPost]
        [Route("sendNoticeToUser")]
        public string SendNoticeToUser(NoticeModel notice)
        {
            if (notice == null || string.IsNullOrWhiteSpace(notice.orgid) || string.IsNullOrWhiteSpace(notice.user))
            {
                throw new Exception("用户及机构信息不可为空！");
            }
            string[] users = notice.user.Split(',');
            var hub = GlobalHost.ConnectionManager.GetHubContext<noticeHub>();
            foreach (var u in users)
            {
                var userNoticeKey = string.Format(CacheKey.NoticeConnectionKey, notice.orgid, "*", u);
                if (RedisHelper.Exists(userNoticeKey))
                {
                    var userNoticeData = RedisHelper.Get<NoticeHubUser>(userNoticeKey);
                    if (userNoticeData.connStu != (int)EnumNoticeUserConnStu.disconn && userNoticeData.connectionId.Count > 0)
                    {
                        foreach (var connectionId in userNoticeData.connectionId)
                        {
                            hub.Clients.Client(connectionId).sendToUser(notice.message,notice);
                        }
                    }
                }
            }
            //string name = Context.User.Identity.Name;

            //foreach (var connectionId in _connections.GetConnections(who))
            //{
            //    hub.Clients.Client(connectionId).addChatMessage(name + ": " + message);
            //}

            return notice.message;
        }


    }
}
