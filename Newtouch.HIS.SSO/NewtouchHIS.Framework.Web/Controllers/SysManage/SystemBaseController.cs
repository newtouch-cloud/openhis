using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Framework.Filter;

namespace NewtouchHIS.Framework.Web.Controllers.SysManage
{
    /// <summary>
    /// 通用查询汇总
    /// </summary>
    public class SystemBaseController : OrgControllerBase
    {
        private readonly ISystemAppService _systemApp;
        public SystemBaseController(ISystemAppService systemApp)
        {
            _systemApp = systemApp;
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<IActionResult> GetRegAppList()
        {
            var list = await _systemApp.GetRegAppListAsync();
            return Content(list.ToJson());
        }
    }
}
