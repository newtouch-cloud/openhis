using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysMedicalOrderFrequencyRepo : IRepositoryBase<SysMedicalOrderFrequencyEntity>
    {

        /// <summary>
        /// 保存
        /// </summary>
        void SubmitForm(SysMedicalOrderFrequencyEntity entity, int? yzpcId);

        /// <summary>
        /// 获得所有列表
        /// </summary>
        List<SysMedicalOrderFrequencyEntity> GetOrderFrequencyList(string orgId, string keyword = null);

        /// <summary>
        /// 修改form
        /// </summary>
        SysMedicalOrderFrequencyEntity GetOrderFrequencyEntity(int? yzpcId, string orgId);

        /// <summary>
        /// 删除
        /// </summary>
        void DeleteForm(string orgId, int yzpcId);
    }
}
