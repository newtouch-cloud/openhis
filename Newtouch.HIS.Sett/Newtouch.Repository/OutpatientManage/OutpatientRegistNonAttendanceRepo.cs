using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using System.Linq;
using Newtouch.HIS.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;

namespace Newtouch.HIS.Repository
{
    public class OutpatientRegistNonAttendanceRepo : RepositoryBase<OutpatientRegistNonAttendanceEntity>, IOutpatientRegistNonAttendanceRepo
    {
        public OutpatientRegistNonAttendanceRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 根据ghnm查退号记录
        /// </summary>
        /// <param name="ghnm"></param>
        /// <returns></returns>
        public List<OutpatientRegistNonAttendanceEntity> SelectBackNumList(int ghnm, string orgId)
        {
            return this._dataContext.Set<OutpatientRegistNonAttendanceEntity>().Where(a => a.ghnm == ghnm && a.OrganizeId == orgId).ToList();
        }

    }
}
