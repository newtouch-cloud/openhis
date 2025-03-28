using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity.Settlement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IRepository.Settlement
{
	public interface ISysSurgeryRepo : IRepositoryBase<SurgeryEntity>
	{

		/// <summary>
		/// 
		/// </summary>
		/// <param name="OrganizeId"></param>
		/// <param name="Pagination"></param>
		/// <param name="keyword"></param>
		/// <returns></returns>
		IList<SurgeryEntity> SurgeryGetGridJson(Pagination Pagination, string keyword = null);


		/// <summary>
		/// 
		/// </summary>
		/// <param name="OrganizeId"></param>
		/// <param name="Pagination"></param>
		/// <param name="keyword"></param>
		/// <returns></returns>
		SurgeryEntity SurgeryUpGetGridJson(int keyword );

		void SubmitForm(SurgeryEntity entity, int? keyValue);
	}
}
