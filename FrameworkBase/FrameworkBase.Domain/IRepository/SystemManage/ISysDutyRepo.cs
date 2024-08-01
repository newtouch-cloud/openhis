using System.Collections.Generic;
using Newtouch.Infrastructure.EF;
using FrameworkBase.Domain.Entity;

namespace FrameworkBase.Domain.IRepository
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-17 16:08
    /// 描 述：系统岗位
    /// </summary>
    public interface ISysDutyRepo : IRepositoryBase<SysDutyEntity>
    {
        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="keyword">筛选关键字</param>
        IList<SysDutyEntity> GetList(string keyword = null);

        /// <summary>
        /// 获取有效实体列表
        /// </summary>
        /// <param name="keyword">筛选关键字</param>
        IList<SysDutyEntity> GetValidList(string keyword = null);

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        void SubmitForm(SysDutyEntity entity, string keyValue);

    }
}