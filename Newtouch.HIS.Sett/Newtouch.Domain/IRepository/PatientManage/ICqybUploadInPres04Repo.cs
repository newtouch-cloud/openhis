using Newtouch.HIS.Domain.Entity.PatientManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IRepository.PatientManage
{
	public interface ICqybUploadInPres04Repo : IRepositoryBase<CqybUploadInPres04Entity>
	{
		void SaveCqybUploadInPres(List<CqybUploadInPres04Entity> entityList);
	}
}
