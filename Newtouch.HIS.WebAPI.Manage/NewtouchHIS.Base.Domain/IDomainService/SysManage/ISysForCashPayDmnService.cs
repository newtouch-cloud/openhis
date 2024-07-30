using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.Entity.SysManage;
using NewtouchHIS.Base.Domain.ValueObjects.SysManage;
using NewtouchHIS.Lib.Base;

namespace NewtouchHIS.Base.Domain.IDomainService
{
    public interface ISysForCashPayDmnService : IScopedDependency
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<SysCashPaymentModelEntity>> GetList();
    }
}
