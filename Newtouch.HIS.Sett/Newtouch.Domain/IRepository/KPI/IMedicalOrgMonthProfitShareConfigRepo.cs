using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Infrastructure.EF;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2018-01-29 15:52
    /// 描 述：医疗机构利润分成配置
    /// </summary>
    public interface IMedicalOrgMonthProfitShareConfigRepo : IRepositoryBase<MedicalOrgMonthProfitShareConfigEntity>
    {
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        void SubmitForm(MedicalOrgMonthProfitShareConfigEntity entity, string keyValue);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        void DeleteForm(string keyValue);
    }
}