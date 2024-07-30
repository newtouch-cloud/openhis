using FrameworkBase.MultiOrg.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.MRQC.Web
{
    /// <summary>
    /// Controller基类
    /// </summary>
    [HandlerLogin]
    public abstract class ControllerBase : OrgControllerBase
    {

    }
}