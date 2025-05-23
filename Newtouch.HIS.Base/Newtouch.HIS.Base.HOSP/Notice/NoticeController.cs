﻿using Microsoft.AspNet.SignalR;
using Newtouch.Common.Operator;
using Newtouch.Core.Redis;
using Newtouch.HIS.Base.HOSP.Controllers;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Newtouch.HIS.Base.HOSP.Notice
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/notice")]
    public class NoticeController : ApiController
    {
        private readonly static string _appId = System.Configuration.ConfigurationManager.AppSettings["AppId"].ToString();
        [HttpGet]
        [Route("getmsg/{nick}/{id}")]
        public string Get(string nick, string id)
        {
            var hub = GlobalHost.ConnectionManager.GetHubContext<noticeHub>();
            hub.Clients.All.showMessage(nick + "," + id + "," + DateTime.Now.ToUniversalTime().ToString());
            return "value";
        }

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
                            hub.Clients.Client(connectionId).sendToUser(notice.message);
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
