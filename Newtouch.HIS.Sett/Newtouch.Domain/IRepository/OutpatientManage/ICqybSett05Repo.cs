using Newtouch.HIS.Domain.Entity.OutpatientManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IRepository.OutpatientManage
{
	public interface ICqybSett05Repo : IRepositoryBase<CqybSett05Entity>
	{
		void SaveCqybSett(CqybSett05Entity entity, string keyValue);
	}
}
