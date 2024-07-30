using Newtouch.Domain.DTO.InputDto.Inpatient;

namespace Newtouch.Application.Interface.Inpatient
{
   public interface IDoctorserviceApp
    {
        /// <summary>
        /// 修改药品医嘱时，根据医嘱Id获取详情并且展示到医嘱录入界面
        /// </summary>
        /// <param name="yzId"></param>
        /// <param name="yzlx"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
         DoctorServiceparentRequestDto GetYZDetail(string zyh, string yzId, string yzlx, string orgId);
    }
}
