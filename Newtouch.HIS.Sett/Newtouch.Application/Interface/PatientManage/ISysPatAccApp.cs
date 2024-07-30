using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysPatAccApp
    {
        /// <summary>
        /// 根据住院号获取账户信息
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        SysPatientAccountEntity GetAccInfoByZHY(string zyh);
    }
}
