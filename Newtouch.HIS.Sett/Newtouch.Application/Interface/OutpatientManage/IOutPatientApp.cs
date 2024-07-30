using Newtouch.HIS.Domain.DTO.InputDto;
using System.Collections.Generic;

namespace Newtouch.HIS.Application.Interface.OutpatientManage
{
    public interface IOutPatientApp
    {
        /// <summary>
        /// 保存门诊记账
        /// </summary>
        void SaveoutpatientAccountInfo(OutpatAccBasicInfoDto bacDto, List<OutpatAccGridInfoDto> accDto, string orgId);
    }
}
