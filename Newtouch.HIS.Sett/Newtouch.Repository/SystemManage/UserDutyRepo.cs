using System.Collections.Generic;
using System.Linq;
using Newtouch.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class UserDutyRepo : RepositoryBase<UserDutyEntity>, IUserDutyRepository
    {
        public UserDutyRepo(INewtouchDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public List<UserDutyEntity> GetListByUserId(string userId)
        {
            return this.IQueryable().Where(t => t.zt == ((int)EnumZT.Valid).ToString() && t.UserId == userId).ToList();
        }
    }
}


