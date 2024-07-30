using Newtouch.Domain.Entity;

namespace Newtouch.Application.Interface
{
    /// <summary>
    /// 用法联动
    /// </summary>
    public interface IUsageLinkageApp
    {
        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sysUsageLinkageEntity"></param>
        /// <returns></returns>
        string SubmitForm(SysUsageLinkageEntity sysUsageLinkageEntity);
    }
}