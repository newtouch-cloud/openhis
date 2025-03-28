using Newtouch.EMR.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.EMR.Domain.IRepository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2021-03-01 15:24
    /// 描 述：出院小结-诊断
    /// </summary>
    public interface IYBInpOutHosSummariesDiagRepo : IRepositoryBase<YBInpOutHosSummariesDiagEntity>
    {
        void Submit(YBInpOutHosSummariesDiagEntity ety);
    }
}