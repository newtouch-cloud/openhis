using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.ExampleManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class SendMailController : ControllerBase
    {
        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SendMail(string account, string title, string content)
        {
            //MailHelper mail = new MailHelper();
            //mail.MailServer = ConfigurationHelper.GetAppConfigValue("MailHost");
            //mail.MailUserName = ConfigurationHelper.GetAppConfigValue("MailUserName");
            //mail.MailPassword = ConfigurationHelper.GetAppConfigValue("MailPassword");
            //mail.MailName = "";
            //mail.Send(account, title, content);
            return Success("发送成功。");
        }
    }
}
