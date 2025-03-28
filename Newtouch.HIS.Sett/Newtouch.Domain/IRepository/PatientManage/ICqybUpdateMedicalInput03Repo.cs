using Newtouch.HIS.Domain.Entity.PatientManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IRepository.PatientManage
{
    public  interface ICqybUpdateMedicalInput03Repo: IRepositoryBase<CqybUpdateMedicalInput03Entity>
    {
        void SaveCqybUpdateMedicalS03InReg(CqybUpdateMedicalInput03Entity entity, string keyValue);
    }
}
