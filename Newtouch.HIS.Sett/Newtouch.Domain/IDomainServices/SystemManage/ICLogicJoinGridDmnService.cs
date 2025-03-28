using System.Collections.Generic;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using Newtouch.Core.Common;

namespace Newtouch.HIS.Domain.IDomainServices
{
    public interface ICLogicJoinGridDmnService
    {
        /// <summary>
        /// 病人收费算法grid显示
        /// </summary>
        /// <returns></returns>
        /// <summary>
        /// 搜索病人收费算法
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        List<PatiChargeLogicVO> GetPatiChargeLogicBySearch(Pagination pagination, string keyword, string orgId);
        /// <summary>
        /// 编辑页面时，加载选中行的对象
        /// </summary>
        /// <param name="keyvalue"></param>
        /// <returns></returns>
        List<PatiChargeLogicVO> GetPatiChargeLogicFirst(string keyvalue);
    }
}
