using Newtouch.Core.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Infrastructure
{
    public class CommmHelper
    {
        /// <summary>
        /// 获取本地文件对应的本地路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetLocalFilePath(string path)
        {
            var configFileExportBaseDir = ConfigurationHelper.GetAppConfigValue("LocalFileBaseDir");
            if (string.IsNullOrWhiteSpace(configFileExportBaseDir))
            {
                configFileExportBaseDir = "D:\\";
            }
            var filePath = (configFileExportBaseDir + path).Replace("\\\\", "\\");

            var iIndex = filePath.LastIndexOf("\\");
            var dirPath = filePath.Substring(0, iIndex);
            if (!System.IO.Directory.Exists(dirPath))
            {
                System.IO.Directory.CreateDirectory(dirPath);
            }

            return filePath;
        }

    }
}
