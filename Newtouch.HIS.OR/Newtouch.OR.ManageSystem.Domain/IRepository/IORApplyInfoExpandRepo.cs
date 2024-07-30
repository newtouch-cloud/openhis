using Newtouch.Infrastructure.EF;
using Newtouch.OR.ManageSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.OR.ManageSystem.Domain.IRepository
{
	public interface IORApplyInfoExpandRepo : IRepositoryBase<ORApplyInfoExpandEntity>
	{
		/// <summary>
		/// 保存表单（新增、修改）
		/// </summary>
		/// <param name="entity">实体对象</param>
		/// <param name="keyValue">主键值</param>
		int SubmitForm(ORApplyInfoExpandEntity entity, string keyValue);

		/// <summary>
		/// 删除数据
		/// </summary>
		/// <param name="keyValue">申请单号</param>
		void DeleteForm(string keyValue, string orgId);

		/// <summary>
		/// 根据申请编号获取多个手术信息
		/// </summary>
		/// <param name="orgId"></param>
		/// <param name="applyNo"></param>
		/// <returns></returns>
		List<ORApplyInfoExpandEntity> getApplyExtendByApplyno(string orgId, string applyNo);
	}
}
