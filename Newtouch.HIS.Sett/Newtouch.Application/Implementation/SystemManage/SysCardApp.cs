using FrameworkBase.MultiOrg.Application;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class SysCardApp : AppBase, ISysCardApp
    {
        private readonly ISysCardRepo _sysCardRepository;
        //other Repositories or DomainServices
        
        /// <summary>
        /// 根据卡号获取病人内码
        /// </summary>
        /// <param name="cardno"></param>
        /// <returns></returns>
        public string GetPatidByCardNo(string cardno)
        {
            return _sysCardRepository.GetPatidByCardNo(cardno, this.OrganizeId);
        }

        /// <summary>
        /// 获取新虚拟卡 卡号
        /// </summary>
        /// <returns></returns>
        public string GetCardSerialNo()
        {
            return _sysCardRepository.GetCardSerialNo(this.OrganizeId);
        }
    }
}
