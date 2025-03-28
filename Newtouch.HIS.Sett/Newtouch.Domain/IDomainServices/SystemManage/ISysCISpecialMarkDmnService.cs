using Newtouch.Common;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using Newtouch.Tools;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices.SystemManage
{
    public interface ISysCISpecialMarkDmnService
    {
        List<SysCISpecialMarkVO> GetGridBySearch(Pagination panigation, string keyword);
        SysCISpecialMarkVO GetFormJson(int keyValue);
    }
}
