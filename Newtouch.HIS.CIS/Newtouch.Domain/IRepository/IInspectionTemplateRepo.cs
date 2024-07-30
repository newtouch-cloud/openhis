using Newtouch.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IInspectionTemplateRepo : IRepositoryBase<InspectionTemplateEntity>
    {
        /// <summary>
        /// 模板列表
        /// </summary>
        /// <returns></returns>
        List<InspectionTemplateEntity> GetList(int type, string orgId);
    }
}
