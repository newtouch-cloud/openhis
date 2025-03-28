using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtouch.Tools;
using Newtouch.HIS.Application;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Common;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    public class ExpenseManageController : ControllerBase
    {
        private readonly IModuleApp _moduleApp;

        public ExpenseManageController(IModuleApp moduleApp)
        {
            this._moduleApp = moduleApp;
        }

    }
}
