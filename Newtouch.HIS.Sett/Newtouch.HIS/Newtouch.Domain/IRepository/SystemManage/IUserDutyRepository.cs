using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserDutyRepository : IRepositoryBase<UserDutyEntity>
    {
        List<UserDutyEntity> GetListByUserId(string userId);
    }
}
