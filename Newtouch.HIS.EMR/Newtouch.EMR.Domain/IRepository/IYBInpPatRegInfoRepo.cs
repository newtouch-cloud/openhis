using Newtouch.EMR.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.EMR.Domain.IRepository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2021-02-03 15:08
    /// 描 述：入院记录信息
    /// </summary>
    public interface IYBInpPatRegInfoRepo : IRepositoryBase<YBInpPatRegInfoEntity>
    {
        void SubmitRegInfo(YBInpPatRegInfoEntity ety);
    }
}