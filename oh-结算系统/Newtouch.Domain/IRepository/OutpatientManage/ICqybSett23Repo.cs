using Newtouch.HIS.Domain.Entity.OutpatientManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IRepository.OutpatientManage
{
	public interface ICqybSett23Repo : IRepositoryBase<CqybSett23Entity>
	{
		void SaveCqybS23Sett(CqybSett23Entity entity, string keyValue);
	}
}
