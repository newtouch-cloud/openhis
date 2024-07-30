using FrameworkBase.MultiOrg.Web;
using Newtouch.Common;
using Newtouch.Common.Operator;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.HIS.Web
{
    /// <summary>
    /// Controller基类
    /// </summary>
    [HandlerLogin]
    public abstract class ControllerBase : OrgControllerBase
    {

    }

}
