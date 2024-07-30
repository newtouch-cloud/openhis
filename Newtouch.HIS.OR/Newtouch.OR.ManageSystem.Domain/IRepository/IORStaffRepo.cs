using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;
using Newtouch.Core.Common;

namespace Newtouch.OR.ManageSystem.Domain.IRepository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2019-11-06 10:22
    /// 描 述：手术人员
    /// </summary>
    public interface IORStaffRepo : IRepositoryBase<ORStaffEntity>
    {
        /// <summary>
        /// 获取分页实体列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="keyword">筛选关键字</param>
        /// <returns></returns>
        IList<ORStaffEntity> GetPagintionList(Pagination pagination, string keyword,string organizeId);
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        int SubmitForm(ORStaffEntity entity, string keyValue);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        int DeleteForm(string keyValue);

		/// <summary>
		/// 获取浮框人员列表
		/// </summary>
		/// <param name="organizeId"></param>
		/// <param name="keyword"></param>
		/// <returns></returns>
		IList<ORStaffEntity> GetFloatStaffList(string organizeId, string keyword);


	}
}