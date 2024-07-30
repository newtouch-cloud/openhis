using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;
using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.Lib.Services.SignalR
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("Cors")]
    public class HisNoticeHub : Hub
    {
        #region 客户端调用
        public void SendNotice(string name, string message)
        {
            Clients.All.SendAsync(name, message);
        }

        /// <summary>
        /// 记录当前用户的连接ID，方便后面给客户端用户推送站内信通知消息。
        /// OnConnectedAsync当与集线器建立新连接时调用。 
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            var user = Context.UserIdentifier;
            //var userId = _currentUserService.GetUserId();
            //await _userService.SaveWebSocketIdAsync(userId, Context.ConnectionId);
            //Console.WriteLine($"新的连接:{Context.ConnectionId},userId={userId}");
            ////处理当前用户的未读消息
            //var unreadMessage = await _messageService.GetMyMessageUnReadNumberAsync(userId);
            //var message = new
            //{
            //    UnReadNumber = unreadMessage,
            //    MessageList = new List<MessageDTO>()
            //};
            //await Clients.Client(Context.ConnectionId).SendAsync(MesssageCenter.NewMessageNotify, message);
            await base.OnConnectedAsync();
        }

        #endregion
        //public readonly LoginManagerService _LoginManagerService;
        //public SignalRMessageHub(LoginManagerService adminUserService)
        //{
        //    _LoginManagerService = adminUserService;
        //}
        /// <summary>
        /// 建立SignalR的客户端id与token的关系
        /// </summary>
        public static Dictionary<string, AuthIdentity> UserIdAndCid = new Dictionary<string, AuthIdentity>();
        /// <summary>
        /// 建立连接初始化方法
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        //public async Task Init(string token)
        //{
        //    var login = _LoginManagerService.GetLoginUserInRedisByToken(token);
        //    UserIdentity userData = new UserIdentity()
        //    {
        //        SignalConnId = Context.ConnectionId,
        //        Token = token,
        //        UserName = login.user_name,
        //        Account = login.user_full_name
        //    };
        //    UserIdAndCid.Add(Context.ConnectionId, userData);
        //}





        /// <summary>
        /// 主要作用是，当用户的WebSocket断开连接后，清空他的连接ID。
        /// OnDisconnectedAsync当与集线器的连接终止时调用。
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            //var userId = _currentUserService.GetUserId();
            //Console.WriteLine($"断开连接:{Context.ConnectionId},userId={userId}");
            //await _userService.SaveWebSocketIdAsync(userId, string.Empty);
            if (UserIdAndCid.Keys.Contains(Context.ConnectionId))
            {
                //UserData userData = UserIdAndCid[Context.ConnectionId];
                //if (userData != null)
                //{
                //    userData.Timer.Stop();  //停止定时器
                //    userData.Timer.Dispose();  //释放定时器

                //}
                UserIdAndCid.Remove(Context.ConnectionId);
            }

            await base.OnDisconnectedAsync(exception);
        }



    }


}
