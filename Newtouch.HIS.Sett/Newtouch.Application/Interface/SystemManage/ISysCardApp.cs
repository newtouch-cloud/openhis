using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysCardApp
    {
        /// <summary>
        /// 根据卡号获取病人内码
        /// </summary>
        /// <param name="cardno"></param>
        /// <returns></returns>
        string GetPatidByCardNo(string cardno);


        /// <summary>
        /// 获取新虚拟卡 卡号
        /// </summary>
        /// <returns></returns>
        string GetCardSerialNo();
    }
}
