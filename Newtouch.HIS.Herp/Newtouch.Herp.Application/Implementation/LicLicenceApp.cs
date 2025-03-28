using System;
using System.IO;
using System.Web;
using Newtouch.Herp.Application.Interface;
using Newtouch.Tools;

namespace Newtouch.Herp.Application.Implementation
{
    /// <summary>
    /// 证照管理
    /// </summary>
    public class LicLicenceApp : AppBase, ILicLicenceApp
    {
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="file"></param>
        /// <returns>图片上传路径</returns>
        public string UploadFile(HttpPostedFileBase file)
        {
            try
            {
                var result = "";
                if (!ValidateFile(file)) return result;
                var urn = Core.Common.Utils.ConfigurationHelper.GetAppConfigValue("fileUrn");
                urn = string.IsNullOrWhiteSpace(urn) ? "/Upload/licence" : urn;
                var url = HttpContext.Current.Request.MapPath(urn);
                if (!FileHelper.IsExistDirectory(url)) FileHelper.CreateDirectory(url);
                var ext = FileWebHelper.FileNameExtension(file.FileName);
                var newFileName = Guid.NewGuid() + ext;
                result = urn + "/" + newFileName;
                var parth = Path.Combine(url, newFileName);
                file.SaveAs(parth);
                return result;
            }
            catch (Exception)
            {
                return "";
            }
        }

        #region private function

        /// <summary>
        /// 图片效验
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool ValidateFile(HttpPostedFileBase file)
        {
            if (!ValidateExt(file)) return false;
            if (!ValidateSize(file)) return false;
            return true;
        }

        /// <summary>
        /// 类型验证
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool ValidateExt(HttpPostedFileBase file)
        {
            var ext = FileWebHelper.FileNameExtension(file.FileName);
            if (string.IsNullOrWhiteSpace(ext)) return false;
            var legalFileUploadExt = Core.Common.Utils.ConfigurationHelper.GetAppConfigValue("legalFileUploadExt") ?? "";
            return legalFileUploadExt.ToLower().Contains(ext.ToLower());
        }

        /// <summary>
        /// 大小验证
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool ValidateSize(HttpPostedFileBase file)
        {
            var maxSize = Core.Common.Utils.ConfigurationHelper.GetAppConfigValue("imageMaxSize");
            maxSize = string.IsNullOrWhiteSpace(maxSize) ? 5.ToString() : maxSize;
            var size = file.ContentLength;
            return size / 1024 / 1024 <= Convert.ToInt32(maxSize);
        }

        #endregion
    }
}
