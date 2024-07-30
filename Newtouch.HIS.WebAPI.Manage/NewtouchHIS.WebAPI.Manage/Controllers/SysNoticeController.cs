using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NewtouchHIS.Lib.Services.SignalR;

namespace NewtouchHIS.WebAPI.Manage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SysNoticeController : ControllerBase
    {
        private readonly IHubContext<HisNoticeHub> _hubContext;
        public SysNoticeController(IHubContext<HisNoticeHub> hubContext)
        { 
            _hubContext=hubContext;
        }
        [HttpGet]
        [Route("PushMsg")]
        public async Task<IActionResult> PushMsg([FromQuery]string content)
        {
            await _hubContext.Clients.All.SendAsync("ShowMsg",new {Title="模拟消息推送",MsgContent="12344444"});
            return Ok(content);
        }



        // GET api/<controller>/5
        [HttpGet]
        [Route("getmsg/{nick}/{id}")]
        public string Get(string nick, string id)
        {
            _hubContext.Clients.All.SendAsync(nick + "," + id + "," + DateTime.Now.ToUniversalTime().ToString());
            return "value";
        }

    }
}
