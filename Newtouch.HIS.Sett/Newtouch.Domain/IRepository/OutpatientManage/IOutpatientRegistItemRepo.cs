using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOutpatientRegistItemRepo : IRepositoryBase<OutpatientRegistItemEntity>
    {
        List<OutpatientRegistItemEntity> SelectRegProj(int ghnm, string orgId);
    }
}
