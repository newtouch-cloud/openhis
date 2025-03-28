using Newtouch.HIS.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IRepository.SystemManage
{
    public interface ISysStaffConsultRepo : IRepositoryBase<SysStaffConsultEntity>
    {
        IList<SysStaffConsultEntity> GetStaffConsultList(string staffId);
    }
}
