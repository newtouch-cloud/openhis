using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application
{
    public interface ISysDepartmentApp
    {

        List<SysDepartmentEntity> GetList();
        object GetksList();
    }
}
