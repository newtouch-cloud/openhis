
using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    public interface IOutpatientRegistNonAttendanceRepo : IRepositoryBase<OutpatientRegistNonAttendanceEntity>
    {
        List<OutpatientRegistNonAttendanceEntity> SelectBackNumList(int ghnm, string orgId);
    }
}
