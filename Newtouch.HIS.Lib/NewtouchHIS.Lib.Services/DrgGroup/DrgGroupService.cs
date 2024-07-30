using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Model.DRG;
using static NewtouchHIS.Base.Domain.EnumExtend.DRGEnum;

namespace NewtouchHIS.Lib.Services.DrgGroup
{
    /// <summary>
    /// Drg分组策略
    /// </summary>
    public class DrgGroupService : IDrgGroupService
    {
        private readonly Dictionary<EnumDrgGrouperTypePolicy, Type> _drgGrouperPolicy = new Dictionary<EnumDrgGrouperTypePolicy, Type>();
        private readonly IServiceProvider _serviceProvider;
        public DrgGroupService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            _drgGrouperPolicy.Add(EnumDrgGrouperTypePolicy.ChsDrg11Policy, typeof(IChsDrg11Policy));
            _drgGrouperPolicy.Add(EnumDrgGrouperTypePolicy.DrgWuhan2022Policy, typeof(IDrgWuhan2022Policy));
        }
        public async Task<BusResult<DrgGroupResult>> GroupMedRecordAsync(DrgAreaMedicalRecord record)
        {
            var result = new BusResult<DrgGroupResult>();
            if (record.versionPolicy == null)
            {
                record.versionPolicy = (int)Enum.Parse(typeof(EnumDrgGrouperType), record.version);
            }
            if (_drgGrouperPolicy.TryGetValue((EnumDrgGrouperTypePolicy)record.versionPolicy, out var groupVersion))
            {
                var drgPolicy = _serviceProvider.GetService(groupVersion);
                if (drgPolicy is IDrgGroupBasePolicy execPolicy)
                {
                    result = await execPolicy.GroupMedRecordAsync(record);
                }
            }
            else
            {
                result.code = ResponseResultCode.FAIL;
                result.msg = "暂不支持该区域DRG分组分析";
            }
            return result;
        }
        public async Task<BusResult<List<DrgGroupResult>>> GroupMedRecordAsync(DrgAreaMedicalRecordS record)
        {
            var result = new BusResult<List<DrgGroupResult>>();
            if (record.versionPolicy == null)
            {
                record.versionPolicy = (int)Enum.Parse(typeof(EnumDrgGrouperType), record.version);
            }
            if (_drgGrouperPolicy.TryGetValue((EnumDrgGrouperTypePolicy)record.versionPolicy, out var groupVersion))
            {
                var drgPolicy = _serviceProvider.GetService(groupVersion);
                if (drgPolicy is IDrgGroupBasePolicy execPolicy)
                {
                    result = await execPolicy.GroupMedRecordAsync(record);
                }
            }
            else
            {
                result.code = ResponseResultCode.FAIL;
                result.msg = "暂不支持该区域DRG分组分析";
            }
            return result;
        }

        public async Task<BusResult<DrgGroupFileResult>> GroupFileRecordAsync(DrgAreaFileInfo record)
        {
            var result = new BusResult<DrgGroupFileResult>();
            if (record.versionPolicy == null)
            {
                record.versionPolicy = (int)Enum.Parse(typeof(EnumDrgGrouperType), record.version);
            }
            if (_drgGrouperPolicy.TryGetValue((EnumDrgGrouperTypePolicy)record.versionPolicy, out var groupVersion))
            {
                var drgPolicy = _serviceProvider.GetService(groupVersion);
                if (drgPolicy is IDrgGroupBasePolicy execPolicy)
                {
                    result = await execPolicy.GroupFileRecordAsync(record);
                }
            }
            else
            {
                result.code = ResponseResultCode.FAIL;
                result.msg = "暂不支持该区域DRG分组分析";
            }
            return result;
        }
    }
}
