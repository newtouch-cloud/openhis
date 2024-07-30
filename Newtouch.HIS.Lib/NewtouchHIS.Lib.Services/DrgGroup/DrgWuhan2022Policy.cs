using NewtouchHIS.DrgGroup.wuhan_2022;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Model.DRG;

namespace NewtouchHIS.Lib.Services.DrgGroup
{
    /// <summary>
    /// 武汉2022版 
    /// </summary>
    public interface IDrgWuhan2022Policy : IDrgGroupBasePolicy
    {
        ///// <summary>
        ///// 单病案分组结果查询
        ///// </summary>
        ///// <param name="record"></param>
        ///// <returns></returns>
        //Task<BusResult<DrgGroupResult>> GroupMedRecord(DrgAreaMedicalRecord record);
        ///// <summary>
        ///// csv文件分组结果查询
        ///// </summary>
        ///// <param name="record"></param>
        ///// <returns></returns>
        //Task<BusResult<DrgGroupFileResult>> GroupFileRecord(DrgAreaFileInfo record);
    }
    public class DrgWuhan2022Policy : DrgGroupBasePolicy, IDrgWuhan2022Policy
    {
        public override async Task<BusResult<DrgGroupResult>> GroupMedRecordAsync(DrgAreaMedicalRecord record)
        {
            var result = new BusResult<DrgGroupResult>();
            var isValid = base.Validata(record);
            if (!isValid)
            {
                result.msg = "关键信息不可为空";
                result.code = ResponseResultCode.FAIL;
                return result;//验证失败则直接返回
            }
            //string str = "22082078,1,24, 9105, 3470,13050201, 6, 1,\"K63.500,K52.910\",\"00.5500,45.4300x010,45.4300x013\"";
            //var recordResult = GroupHelper.GroupRecord(str);
            await Task.Run(() =>
            {
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
            //result.Data = new DrgGroupFileResult { file = recordResult };
            return result;
        }

    }
}
