using System;
using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity.PatientManage;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IRepository.PatientManage
{
    public interface ICqybMedicalInPut02Repo : IRepositoryBase<CqybMedicalInPut02Entity>
    {
        void SaveCqybMedicalS02InReg(CqybMedicalInPut02Entity entity, string keyValue);
    }
}
