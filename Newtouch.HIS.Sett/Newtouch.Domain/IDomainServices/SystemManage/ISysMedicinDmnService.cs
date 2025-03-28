using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysMedicinDmnService
    {
        /// <summary>
        /// 检索药品（for 下拉）
        /// </summary>
        /// <param name="keywrod"></param>
        /// <returns></returns>
        IList<SysMedicinSimpleInfoVO> GetYpList(string keywrod);

    }
}
