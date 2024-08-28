using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;

namespace Newtouch.Domain.IRepository
{
    public interface ISysNationalityRepo : IRepositoryBase<SysNationalityVEntity>
    {
        /// <summary>
        /// 获取有效国籍
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysNationalityVEntity> GetgjList();

        IList<SysBrxzVEntity> GetBRXZList(string orgId);
        /// <summary>
        /// 现金支付方式
        /// </summary>
        /// <returns></returns>
        IList<SysCashPaymenVEntity>  GetCashPay();
    }
}
