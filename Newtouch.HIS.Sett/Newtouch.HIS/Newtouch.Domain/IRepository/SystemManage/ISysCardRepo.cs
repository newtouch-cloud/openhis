using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysCardRepo : IRepositoryBase<SysCardEntity>
    {
        /// <summary>
        /// 获取所有卡
        /// </summary>
        /// <returns></returns>
        List<SysCardEntity> GetList(string orgId);

        /// <summary>
        /// 根据卡号获取病人内码
        /// </summary>
        /// <param name="cardno"></param>
        /// <returns></returns>
        string GetPatidByCardNo(string cardno, string orgId);

        /// <summary>
        /// 获取新虚拟卡 卡号
        /// </summary>
        /// <returns></returns>
        string GetCardSerialNo(string orgId);

        void SubmitForm(SysCardEntity SysPatBasicInfoEntity, string keyValue, string orgId);

        /// <summary>
        /// 根据卡号获取卡类型
        /// </summary>
        /// <param name="cardno"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetCardType(string cardno, string orgId);

        /// <summary>
        /// 根据卡号和组织机构获取卡信息（先假设不同类型的卡 卡号不会重复）
        /// </summary>
        /// <param name="cardno"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        SysCardEntity GetCardEntity(string cardno, string orgId);

        /// <summary>
        /// 获取卡实体
        /// </summary>
        /// <param name="cardType"></param>
        /// <param name="cardno"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        SysCardEntity GetCardEntity(string cardType, string cardno, string orgId);
        SysCardEntity GetCardEntity(int patid, string cardType, string orgId);
    }
}
