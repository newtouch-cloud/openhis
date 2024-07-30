using NewtouchHIS.DrgGroup.chs_drg_11;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Model.DRG;

namespace NewtouchHIS.Lib.Services.DrgGroup
{
    /// <summary>
    /// CHS-DRG 1.1标准版 
    /// </summary>
    public interface IChsDrg11Policy : IDrgGroupBasePolicy
    {
        //Task<BusResult<DrgGroupResult>> GroupMedRecordAsync(DrgAreaMedicalRecord record);
        //Task<BusResult<DrgGroupFileResult>> GroupFileRecordAsync(DrgAreaFileInfo record);
        //Task<BusResult<List<DrgGroupResult>>> GroupMedRecordAsync(DrgAreaMedicalRecordS record);
    }
    public class ChsDrg11Policy : DrgGroupBasePolicy, IChsDrg11Policy
    {
        //private readonly IChsDrg11Group _chsDrg11Group;
        //public ChsDrg11Policy(IChsDrg11Group chsDrg11Group)
        //{
        //    _chsDrg11Group = chsDrg11Group;
        //}
        public override async Task<BusResult<DrgGroupResult>> GroupMedRecordAsync(DrgAreaMedicalRecord record)
        {
            var result = new BusResult<DrgGroupResult>();
            var isValid = base.Validata(record);
            if (!isValid)
            {
                result.msg = "参数校验失败";
                result.code = ResponseResultCode.FAIL;
                return result;//验证失败则直接返回
            }
            await Task.Run(() =>
            {
                //string str = "22058878,2,88,32460,,13040503,94,1,K80.302|K80.305|K83.109|K72.905|Z90.408|E14.900x001,51.8803|51.8701|54.5100x005|45.1301";
                //var recordResult = GroupHelper.GroupRecord(str);
                var recordResult = GroupHelper.GroupRecord(new MedicalRecord
                {
                    Index = record.Index,
                    gender = record.gender,
                    age = record.age,
                    ageDay = record.ageDay,
                    weight = record.weight,
                    dept = record.dept,
                    inHospitalTime = record.inHospitalTime,
                    leavingType = record.leavingType,
                    zdList = record.zdList,
                    ssList = record.ssList,
                    remark = record.remark,
                });
                if (recordResult != null)
                {
                    result.Data = new DrgGroupResult
                    {
                        Index = recordResult.Index,
                        status = recordResult.status,
                        messages = recordResult.messages,
                        mdc = recordResult.mdc,
                        adrg = recordResult.adrg,
                        drg = recordResult.drg
                    };
                    result.code = ResponseResultCode.SUCCESS;
                }
                else
                {
                    result.code = ResponseResultCode.FAIL;
                    result.msg = "DRG分组器未返回数据，请联系管理员";
                }
            });

            return result;
        }

        public override async Task<BusResult<DrgGroupFileResult>> GroupFileRecordAsync(DrgAreaFileInfo record)
        {
            var result = new BusResult<DrgGroupFileResult>();
            var isValid = base.Validata(ref record);
            if (!isValid)
            {
                result.msg = "参数校验失败";
                result.code = ResponseResultCode.FAIL;
                return result;
            }
            if (string.IsNullOrWhiteSpace(record.FilePath))
            {
                result.msg = $"文件路径查找失败";
                result.code = ResponseResultCode.FAIL;
                return result;
            }
            var recordResult = GroupHelper.GroupFileRecord(record.FilePath, record.FileCols);
            if (string.IsNullOrWhiteSpace(recordResult))
            {
                result.msg = "分组结果获取失败";
                result.code = ResponseResultCode.FAIL;
                return result;
            }
            var resultCols = "Index,gender,age,ageDay,weight,dept,inHospitalTime,leavingType,zdList,ssList,remark,status,messages,mdc,adrg,drg";
            var resultcsv = await GroupHelper.GroupFileReadAsync(recordResult, resultCols.Split(','));
            if (resultcsv.Any())
            {
                List<DrgMedicalRecordResult> csvRead = new List<DrgMedicalRecordResult>();
                foreach (var r in resultcsv)
                {
                    var rArray = r.Split(",");
                    csvRead.Add(new DrgMedicalRecordResult()
                    {
                        Index = rArray[0],
                        gender = rArray[1],
                        age = Convert.ToInt32(rArray[2]),
                        ageDay = Convert.ToInt32(rArray[3]),
                        weight = Convert.ToInt32(rArray[4]),
                        dept = rArray[5],
                        inHospitalTime = Convert.ToInt32(rArray[6]),
                        leavingType = rArray[7],
                        zdList = rArray[8].Split('|'),
                        ssList = rArray[9].Split('|'),
                        remark = rArray[10],
                        status = rArray[11],
                        messages = rArray[12].Split('|').ToList(),
                        mdc = rArray[13],
                        adrg = rArray[14],
                        drg = rArray[15],
                    });
                }
                result.code = ResponseResultCode.SUCCESS;
                result.Data = new DrgGroupFileResult { rows = csvRead, file = Path.GetFileName(recordResult) };
            }
            await base.FileRecordEndProcAsync(record);
            return result;
        }

        public override async Task<BusResult<List<DrgGroupResult>>> GroupMedRecordAsync(DrgAreaMedicalRecordS record)
        {
            var result = new BusResult<List<DrgGroupResult>>();
            //string str = "22058878,2,88,32460,,13040503,94,1,K80.302|K80.305|K83.109|K72.905|Z90.408|E14.900x001,51.8803|51.8701|54.5100x005|45.1301";
            //var recordResult = GroupHelper.GroupRecord(str);
            if (record == null || (record.medicalArr!.Length == 0 && record.medicalList!.Count == 0))
            {
                throw new FailedException("关键信息不可为空");
            }
            if (record.medicalList!.Count > 0)
            {
                List<string> list = new List<string>();
                record.medicalList.ForEach(e => list.Add(e.ToInputString())); ;
                list.AddRange(record.medicalArr.ToList());
                record.medicalArr = list.ToArray();
            }
            var recordResult = await GroupHelper.GroupRecord(record.medicalArr);
            if (recordResult != null)
            {
                result.Data = recordResult.Select(p => new DrgGroupResult
                {
                    Index = p.Index,
                    status = p.status,
                    messages = p.messages,
                    mdc = p.mdc,
                    adrg = p.adrg,
                    drg = p.drg
                }).ToList();
                result.code = ResponseResultCode.SUCCESS;
            }
            else
            {
                result.code = ResponseResultCode.FAIL;
                result.msg = "DRG分组器未返回数据，请联系管理员";
            }
            return result;
        }


    }
}
