using Newtouch.Tools;
using System.Web.Mvc;
using Newtouch.HIS.Application;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Web.Areas.SystemSecurity.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class DbBackupController : ControllerBase
    {
        private readonly IDbBackupApp _dbBackupApp;

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(string queryJson)
        {
            var data = _dbBackupApp.GetList(queryJson);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public ActionResult SubmitForm(DbBackupEntity dbBackupEntity)
        {
            dbBackupEntity.F_FilePath = Server.MapPath("~/Resource/DbBackup/" + dbBackupEntity.F_FileName + ".bak");
            dbBackupEntity.F_FileName = dbBackupEntity.F_FileName + ".bak";
            _dbBackupApp.SubmitForm(dbBackupEntity);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        //[HandlerAuthorize]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            _dbBackupApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
        [HttpPost]
        //[HandlerAuthorize]
        public void DownloadBackup(string keyValue)
        {
            var data = _dbBackupApp.GetForm(keyValue);
            string filename = Server.UrlDecode(data.F_FileName);
            string filepath = Server.MapPath(data.F_FilePath);
            if (System.IO.File.Exists(filepath))
            {
                FileWebHelper.DownLoadold(filepath, filename);
            }
        }
    }
}
