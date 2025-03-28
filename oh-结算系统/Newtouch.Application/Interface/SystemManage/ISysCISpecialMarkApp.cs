using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using Newtouch.Core.Common;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysCISpecialMarkApp
    {
        void DeleteForm(int keyValue);
        /// <summary>
        /// A页面带一个对象到B页面
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysCISpecialMarkVO GetForm(int keyValue);

        void SubmitForm(SysChargeItemSpecialMarkEntity xt_sfxmtsbzEntity, string keyValue);

        List<SysCISpecialMarkVO> GetListBySearch(Pagination pagination, string keyword);

    }
}
