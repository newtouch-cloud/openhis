using NewtouchHIS.Domain.InterfaceObjets.MRQC;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.EnumExtend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.IDomainService.MRQC
{
    public interface IMedicalRecordApplyDmnService : IScopedDependency
    {
        Task<MedicalBlApplyResponse> AddMedicalApplyRecord(MedicalBlApplyVo req, DBEnum db = DBEnum.MrQcDb);
    }
}
