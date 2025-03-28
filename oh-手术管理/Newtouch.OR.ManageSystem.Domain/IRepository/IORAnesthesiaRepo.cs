using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;
using Newtouch.Core.Common;

namespace Newtouch.OR.ManageSystem.Domain.IRepository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2019-10-31 15:44
    /// 描 述：麻醉字典表
    /// </summary>
    public interface IORAnesthesiaRepo : IRepositoryBase<ORAnesthesiaEntity>
    {
        /// <summary>
        /// 获取分页实体列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="keyword">筛选关键字</param>
        /// <returns></returns>
        IList<ORAnesthesiaEntity> GetPagintionList(Pagination pagination, string keyword,string organizeId);
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        int SubmitForm(ORAnesthesiaEntity entity, string keyValue);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        int DeleteForm(string keyValue);
    }
}