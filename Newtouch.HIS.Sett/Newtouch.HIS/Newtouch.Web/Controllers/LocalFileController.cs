using Newtouch.Infrastructure;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Controllers
{
    /// <summary>
    /// 本地文件系统
    /// </summary>
    public class LocalFileController : ControllerBase
    {
        /// <summary>
        /// 本地文件下载
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ActionResult Download(string path, string fileDownloadName)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return null;
            }
            path = path.DeBase64();

            var localpath = CommmHelper.GetLocalFilePath(path);

            var fi = new System.IO.FileInfo(localpath);
            if (fi.Exists)
            {
                string downloadName = null;
                if (string.IsNullOrWhiteSpace(fileDownloadName))
                {
                    //未指定
                    downloadName = fi.Name;
                }
                else
                {
                    downloadName = fileDownloadName;
                    var dotIndex = downloadName.LastIndexOf(".");
                    if (dotIndex == -1)
                    {
                        downloadName += fi.Extension;
                    }
                    else
                    {
                        downloadName = downloadName.Substring(0, dotIndex) + fi.Extension;
                    }
                }
                var contentType = Newtouch.Core.Common.Utils.FileHelper.GetContentType(fi.Extension);
                if (!string.IsNullOrWhiteSpace(contentType))
                {
                    return File(localpath, contentType, downloadName);
                }
                else
                {
                    return File(localpath, "text/html", downloadName);
                }
            }
            else
            {
                return null;
            }
        }

    }

}