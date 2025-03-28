using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.VO;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// xt_yp_ksfypz
    /// </summary>
    public interface ISysYpksfypzEntityRepo : IRepositoryBase<SysYpksfypzEntity>
    {
        List<CodeNameVO> GetCodeName(string organizeId, int yjbmjb, string keyword = null);
        IList<SysYpksfypzEntity> GetPagintionList(Pagination pagination, string keyword,string organizeId);
        void SubmitForm(SysYpksfypzEntity FypzDTO, string keyValue);
        void DeleteForm(string keyValue);
    }
}
