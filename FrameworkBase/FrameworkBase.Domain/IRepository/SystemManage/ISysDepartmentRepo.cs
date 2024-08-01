using System.Collections.Generic;
using Newtouch.Infrastructure.EF;
using FrameworkBase.Domain.Entity;

namespace FrameworkBase.Domain.IRepository
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-17 15:56
    /// 描 述：系统科室
    /// </summary>
    public interface ISysDepartmentRepo : IRepositoryBase<SysDepartmentEntity>
    {
        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="keyword">筛选关键字</param>
        IList<SysDepartmentEntity> GetList(string keyword = null);

        /// <summary>
        /// 获取有效科室列表
        /// </summary>
        /// <param name="keyword">筛选关键字</param>
        IList<SysDepartmentEntity> GetValidList(string keyword = null);

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        void SubmitForm(SysDepartmentEntity entity, string keyValue);

        /// <summary>
        /// 根据Code获取名称
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        string GetNameByCode(string code);

    }
}