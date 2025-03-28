using Newtouch.Core.Common;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;


namespace Newtouch.HIS.Domain.IDomainServices
{
    public interface ISysWardRoomDmnService
    {

        IList<SysWardRoomVO> GetPagintionList(Pagination pagination, string organizeId, string keyword);
        IList<SysWardRoomVO> GetWardRoomList(int? bfId, string organizeId, string keyword);
        IList<SysWardRoomVO> GetWardRoomListValid(string organizeId, string bqCode);
    }
}
