using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysMedicinApp
    {
        /// <summary>
        /// 检索药品（for 下拉）
        /// </summary>
        /// <param name="keywrod"></param>
        /// <returns></returns>
        IList<SysMedicinSimpleInfoVO> GetYpList(string keywrod);

    }
}
