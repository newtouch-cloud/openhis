using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Model.DRG;

namespace NewtouchHIS.Lib.Services.DrgGroup
{
    public interface IDrgGroupBasePolicy
    {
        /// <summary>
        /// 病例批量分组分析
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        Task<BusResult<List<DrgGroupResult>>> GroupMedRecordAsync(DrgAreaMedicalRecordS record);
        /// <summary>
        /// 患者病案首页分组分析
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        Task<BusResult<DrgGroupResult>> GroupMedRecordAsync(DrgAreaMedicalRecord record);
        /// <summary>
        /// 文件数据分组分析
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        Task<BusResult<DrgGroupFileResult>> GroupFileRecordAsync(DrgAreaFileInfo record);

    }
    public abstract class DrgGroupBasePolicy : IDrgGroupBasePolicy
    {
        protected virtual bool Validata(DrgAreaMedicalRecord request)
        {
            if (request.zdList.Length == 0)
            {
                throw new FailedException("诊断信息不可为空");
            }
            return true;
        }
        protected virtual bool Validata(ref DrgAreaFileInfo request)
        {
            if (request.FileName.Length == 0)
            {
                throw new FailedException("文件信息不可为空");
            }
            //默认上传路径
            var defaultPath = $"{Directory.GetCurrentDirectory()}{ConfigInitHelper.SysConfig!.FileConfig!.DrgDataFileDirPath ?? ""}";
            if (!(!string.IsNullOrWhiteSpace(request.FilePath) && File.Exists(defaultPath + request.FilePath + request.FileName)))
            {
                string[] match = { $"{request.FileName}*.csv", request.FileName };
                var files = match.SelectMany(g => Directory.EnumerateFiles(defaultPath, g, SearchOption.AllDirectories)).ToList();
                request.FilePath = files.FirstOrDefault(p => !p.Contains("_result.csv"));
                if (string.IsNullOrWhiteSpace(request.FilePath))
                {
                    throw new FailedException("请先上传相关Drg文件");
                }
            }
            else
            {
                request.FilePath = defaultPath + request.FilePath + request.FileName;
            }
            return true;
        }
        protected virtual async Task FileRecordEndProcAsync(DrgAreaFileInfo request)
        {
            await Task.Run(() =>
            {
                var defaultPath = $"{Directory.GetCurrentDirectory()}{ConfigInitHelper.SysConfig!.FileConfig!.DrgDataFileDirPath ?? ""}";
                string[] match = { $"{request.FileName}*.csv", request.FileName };
                var files = match.SelectMany(g => Directory.EnumerateFiles(defaultPath, g, SearchOption.AllDirectories)).ToList();
                if (request.IsTmpFile ?? true)
                {
                    foreach (var f in files)
                    {
                        if (File.Exists(f))
                        {
                            File.Delete(f);
                        }
                    }
                }
            });

        }
        public virtual async Task<BusResult<List<DrgGroupResult>>> GroupMedRecordAsync(DrgAreaMedicalRecordS record)
        {
            return await Task.Run(() => new BusResult<List<DrgGroupResult>>() { code = ResponseResultCode.FAIL, msg = "接口未授权或功能尚未开通" });
        }
        public virtual async Task<BusResult<DrgGroupResult>> GroupMedRecordAsync(DrgAreaMedicalRecord record)
        {
            return await Task.Run(() => new BusResult<DrgGroupResult>() { code = ResponseResultCode.FAIL, msg = "接口未授权或功能尚未开通" });
        }
        public virtual async Task<BusResult<DrgGroupFileResult>> GroupFileRecordAsync(DrgAreaFileInfo record)
        {
            return await Task.Run(() => new BusResult<DrgGroupFileResult>() { code = ResponseResultCode.FAIL, msg = "接口未授权或功能尚未开通" });
        }

    }
}
