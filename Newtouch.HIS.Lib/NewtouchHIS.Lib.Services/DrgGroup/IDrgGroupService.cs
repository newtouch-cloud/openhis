using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Model.DRG;

namespace NewtouchHIS.Lib.Services.DrgGroup
{
    public interface IDrgGroupService
    {
        /// <summary>
        /// 查询患者入组情况
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        Task<BusResult<DrgGroupResult>> GroupMedRecordAsync(DrgAreaMedicalRecord record);
        /// <summary>
        /// 批量查询患者入组情况
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        Task<BusResult<List<DrgGroupResult>>> GroupMedRecordAsync(DrgAreaMedicalRecordS record);
        /// <summary>
        /// 上传文件分析入组情况
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        Task<BusResult<DrgGroupFileResult>> GroupFileRecordAsync(DrgAreaFileInfo record);
    }
}
