using Newtouch.HIS.Domain.Entity.PatientManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IRepository.PatientManage
{
	public interface ICqybUploadPresList04Repo : IRepositoryBase<CqybUploadPresList04Entity>
	{
		void SaveCqybUploadPresList(List<CqybUploadPresList04Entity> entityList, string jytype,string zymzh, string orgId);
	}
}
