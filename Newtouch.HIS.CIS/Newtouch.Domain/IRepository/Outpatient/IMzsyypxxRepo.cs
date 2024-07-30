using Newtouch.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// 门诊输液药品信息
    /// </summary>
    public interface IMzsyypxxRepo : IRepositoryBase<MzsyypxxEntity>
    {
        void Exec(List<long> syIds,string OrganizeId);
        void CanCelExec(List<long> syIds, string OrganizeId);
    }
}
