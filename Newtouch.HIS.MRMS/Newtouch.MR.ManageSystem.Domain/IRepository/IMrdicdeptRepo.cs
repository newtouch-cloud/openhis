using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;
using Newtouch.Core.Common;

namespace Newtouch.MR.ManageSystem.Domain.IRepository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2019-11-20 13:45
    /// 描 述：病案科室
    /// </summary>
    public interface IMrdicdeptRepo : IRepositoryBase<MrdicdeptEntity>
    {
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        int SubmitForm(MrdicdeptEntity entity, string keyValue);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        int DeleteForm(string keyValue);

        /// <summary>
        /// 获取病案列表
        /// </summary>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<MrdicdeptEntity> GetDicDeptList(string organizeId);

        /// <summary>
        /// 分页获取病案列表
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<MrdicdeptEntity> GetPagintionDicDeptList(Pagination pagination, string organizeId, string keyword);

    }
}