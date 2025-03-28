using NewtouchHIS.Domain.Entity.PatientCenter;
using NewtouchHIS.Lib.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.IDomainService.PatientCenter
{
    public interface IPatientAddressDmnService : IScopedDependency
    {
        Task<SysPatientAddressEntity> PatientAddressQuery(int patid, string orgId);
        Task<bool> PatientAddressUpdate(SysPatientAddressEntity entity, string user);
        Task<SysPatientAddressEntity> PatientAddressAdd(SysPatientAddressEntity entity, string user);
        Task<bool> PatientAddressDelete(SysPatientAddressEntity entity, string user);
        Task<SysPatientAddressEntity> PatientOrderAddressQuery(string visitNo, string ks, string orgId);
        Task<OutpatientRegistEntity> OutpatientRegistQuery(int patid, string kh, string orgId);
    }
}
