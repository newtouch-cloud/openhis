using System.Web.Mvc;
using FrameworkBase.MultiOrg.Web;

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
