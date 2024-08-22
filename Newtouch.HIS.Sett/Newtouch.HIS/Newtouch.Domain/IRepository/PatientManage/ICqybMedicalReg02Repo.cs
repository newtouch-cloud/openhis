using Newtouch.HIS.Domain.Entity.PatientManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IRepository.PatientManage
{
	public interface ICqybMedicalReg02Repo : IRepositoryBase<CqybMedicalReg02Entity>
	{
		void SaveCqybMedicalReg(CqybMedicalReg02Entity entity, string keyValue);
	}
}
