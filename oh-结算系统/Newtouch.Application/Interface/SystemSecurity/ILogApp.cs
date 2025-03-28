using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Core.Common;

namespace Newtouch.HIS.Application
{
    public interface ILogApp
    {
        List<SysLogEntity> GetList(Pagination pagination, string queryJson);

        void RemoveLog(string keepTime);


    }
}
