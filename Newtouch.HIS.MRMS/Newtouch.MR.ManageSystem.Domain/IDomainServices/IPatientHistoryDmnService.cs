using Newtouch.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtouch.MR.ManageSystem.Domain.ValueObjects;

namespace Newtouch.MR.ManageSystem.Domain.IDomainServices
{
	public interface IPatientHistoryDmnService
	{
		IList<zys_zdVO> GetzdList(Pagination pagination, string organizeId, string id);
		IList<zys_zdVO> GetmzzdList(Pagination pagination, string organizeId, string id);
		IList<zys_ssVO> GetssList(Pagination pagination, string organizeId, string id);
		IList<zys_ssVO> GetssList(string organizeId, string id);
		IList<zys_zdVO> GetmzzdList(string organizeId, string id);
		IList<zys_zdVO> GetzdList(string organizeId, string id);
	}
}
