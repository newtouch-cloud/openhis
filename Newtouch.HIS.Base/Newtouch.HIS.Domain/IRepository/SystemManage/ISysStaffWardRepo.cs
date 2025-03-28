using Newtouch.HIS.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IRepository
{
    public interface ISysStaffWardRepo:IRepositoryBase<SysStaffWardEntity>
    {
        /// <summary>
        /// 查找员工病区绑定信息
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        IList<SysStaffWardEntity> GetStaffWardList(string staffId);
    }
}
