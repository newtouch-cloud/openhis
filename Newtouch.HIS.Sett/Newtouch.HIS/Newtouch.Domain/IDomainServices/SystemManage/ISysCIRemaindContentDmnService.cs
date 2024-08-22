using Newtouch.Core.Common;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices.SystemManage
{
    public interface ISysCIRemaindContentDmnService
    {
        SysCIRemindContentVO GetnrFormJson(int keyvalue);

        List<SysCIRemindContentVO> GetGridBySearch(Pagination pagination, string keyword);
    }
}
