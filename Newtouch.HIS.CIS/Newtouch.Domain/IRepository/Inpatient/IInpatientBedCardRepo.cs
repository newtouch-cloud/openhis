using Newtouch.Domain.Entity.Inpatient;
using Newtouch.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.IRepository.Inpatient
{
	public interface IInpatientBedCardRepo : IRepositoryBase<InpatientBedCardEntity>
	{
		/// <summary>
		/// 保存表单（新增、修改）
		/// </summary>
		/// <param name="entity">实体对象</param>
		/// <param name="keyValue">主键值</param>
		bool SubmitForm(InpatientBedCardEntity entity, string keyValue);


		/// <summary>
		/// 删除数据
		/// </summary>
		/// <param name="keyValue">主键值</param>
		void DeleteForm(string keyValue);
	}
}
