using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Model;
using Spire.Xls;
using System.Net.Http.Headers;
using System.Text;

namespace NewtouchHIS.WebAPI.Manage.Controllers
{
    /// <summary>
    /// 文件操作
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly FileManageConfig fileManageConf = ConfigInitHelper.SysConfig!.FileConfig ?? new FileManageConfig();
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UploadFile")]
        public async Task<Dictionary<string, string>> UploadFileAsync(IEnumerable<IFormFile> files)
        {
            var fileStream = files.FirstOrDefault()?.OpenReadStream();
            var fileContent = new StringBuilder();
            if (fileStream != null)
            {
                using var reader = new StreamReader(fileStream!);
                while (reader.Peek() >= 0)
                {
                    fileContent.AppendLine(await reader.ReadLineAsync());
                }
            }

            var result = new Dictionary<string, string>()
            {
                ["fileContent"] = fileContent.ToString()
            };

            return result;
        }
        /// <summary>
        /// web文件上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("WebUploaFile")]
        public async Task<Dictionary<string, string>> UploadFileForWebAsync()
        {
            var fileStream = HttpContext.Request.Form.Files.FirstOrDefault()?.OpenReadStream();
            var fileContent = new StringBuilder();
            if (fileStream != null)
            {
                using var reader = new StreamReader(fileStream!);
                while (reader.Peek() >= 0)
                {
                    fileContent.AppendLine(await reader.ReadLineAsync());
                }
            }

            var result = new Dictionary<string, string>()
            {
                ["fileContent"] = fileContent.ToString()
            };

            return result;
        }
        /// <summary>
        /// Excel上传
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DrgExcelUpload")]
        public async Task<IActionResult> DrgExcelUpload(IFormFile file)
        {
            var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName; // 原文件名（包括路径）
            var extName = filename.Substring(filename.LastIndexOf('.')).Replace("\"", "");// 扩展名
            string shortfilename = $"{Guid.NewGuid()}{extName}";// 新文件名
            string fileSavePath = $"{Directory.GetCurrentDirectory()}{fileManageConf.DrgDataFileDirPathWithDate ?? "/File/Drg/"}"; //文件临时目录，导入完成后 删除
            filename = fileSavePath + shortfilename; // 新文件名（包括路径）
            if (!Directory.Exists(fileSavePath))
            {
                Directory.CreateDirectory(fileSavePath);
            }
            using (FileStream fs = System.IO.File.Create(filename)) // 创建新文件
            {
                await file.CopyToAsync(fs);// 复制文件
                fs.Flush();// 清空缓冲区数据
                           //根据 filename 【文件服务器磁盘路径】可对文件进行业务操作
            }
            if (extName != ".csv")
            {
                //载入xls文档 转化csv
                Workbook workbook = new Workbook();
                workbook.LoadFromFile(filename);
                //获取第一张工作表
                Worksheet sheet = workbook.Worksheets[0];
                //保存为csv格式
                sheet.SaveToFile(filename + ".csv", ",", Encoding.UTF8);
            }

            //处理完成后，删除上传的文件
            //if (System.IO.File.Exists(filename))
            //{
            //    System.IO.File.Delete(filename);
            //}
            return new JsonResult(Path.GetFileName(filename + ".csv"));
        }

        [HttpPost]
        [Route("FileDelete")]
        public async Task<IActionResult> FileDelete(string filepath)
        {
            if (System.IO.File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }
            return new JsonResult("ok");
        }
    }


}
