using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.Infrastructure.EF;
using Newtouch.Core.Common;
using System.Collections.Generic;
using Newtouch.OR.ManageSystem.Domain.DTO.OutputDto;

namespace Newtouch.OR.ManageSystem.Domain.IRepository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2019-10-31 14:15
    /// 描 述：手术字典表
    /// </summary>
    public interface IOROperationRepo : IRepositoryBase<OROperationEntity>
    {
        /// <summary>
        /// 获取分页实体列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="keyword">筛选关键字</param>
        IList<OROperationEntity> GetPagintionList(Pagination pagination, string keyword,string organizeId);

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        int SubmitForm(OROperationEntity entity, string keyValue);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        int DeleteForm(string keyValue);
        /// <summary>
        /// 获取手术列表
        /// </summary>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<OROperationEntity> GetOperationList(string organizeId,string keyword);
		IList<SysOpListDto> OpList(string orgId, string keyword, bool type);

	}
}