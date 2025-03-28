using System.Web;

namespace Newtouch.Herp.Application.Interface
{
    /// <summary>
    /// 证照管理
    /// </summary>
    public interface ILicLicenceApp
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        string UploadFile(HttpPostedFileBase file);
    }
}