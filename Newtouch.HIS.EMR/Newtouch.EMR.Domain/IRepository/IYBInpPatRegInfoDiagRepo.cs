using Newtouch.EMR.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.EMR.Domain.IRepository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2021-03-01 15:24
    /// 描 述：入院登记-诊断
    /// </summary>
    public interface IYBInpPatRegInfoDiagRepo : IRepositoryBase<YBInpPatRegInfoDiagEntity>
    {
        void Submit(YBInpPatRegInfoDiagEntity ety);
    }
}