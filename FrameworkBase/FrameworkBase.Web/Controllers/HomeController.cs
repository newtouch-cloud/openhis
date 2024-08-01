using Newtouch.Common;
using Newtouch.HIS.Web.Core.Attributes;
using System;
using System.Web.Mvc;

namespace FrameworkBase.Web.Controllers
{
    /// <summary>
    /// 首页
    /// </summary>
    public abstract class HomeController : BaseController
    {
        /// <summary>
        /// 欢迎首页（iframe）
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Default()
        {
            return View();
        }

        /// <summary>
        /// 页面Load 时间记录
        /// </summary>
        /// <param name="url"></param>
        /// <param name="enterTime"></param>
        /// <param name="ms"></param>
        /// <returns></returns>
        [TrackerIgnore]
        public virtual ActionResult PageLoadMoniter(string url, DateTime? enterTime, int ms)
        {
            var str = Newtouch.Tools.Json.ToJson(new
            {
                EnterTime = enterTime ?? DateTime.MinValue,
                CostMSec = ms,
                Path = url,
                UserIdentity = this.UserIdentity.UserCode,
            });

            AppLogger.Instance.Moniter(str);

            return null;
        }

    }
}