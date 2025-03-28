using System.Collections.Generic;
using System.Linq;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using FrameworkBase.MultiOrg.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysCardRepo : RepositoryBase<SysCardEntity>, ISysCardRepo
    {
        public SysCardRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 获取所有卡
        /// </summary>
        /// <returns></returns>
        public List<SysCardEntity> GetList(string orgId)
        {
            return this.IQueryable().Where(p => p.zt == "1" && p.OrganizeId == orgId).ToList();
        }

        /// <summary>
        /// 根据卡号获取病人内码
        /// </summary>
        /// <param name="cardno"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public string GetPatidByCardNo(string cardno, string orgId)
        {
            var cardEntity = GetCardEntity(cardno, orgId);
            return cardEntity != null ? cardEntity.patid.ToString() : "";
        }

        /// <summary>
        /// 根据卡号获取卡类型
        /// </summary>
        /// <param name="cardno"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public string GetCardType(string cardno, string orgId)
        {
            var cardEntity = GetCardEntity(cardno, orgId);
            return cardEntity != null ? cardEntity.CardType : "";
        }

        /// <summary>
        /// 获取新虚拟卡 卡号
        /// </summary>
        /// <returns></returns>
        public string GetCardSerialNo(string orgId)
        {
            return EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("xt_card", orgId);
        }

        public void SubmitForm(SysCardEntity SysCardEntity, string keyValue, string orgId)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                SysCardEntity.Modify(keyValue);
                this.Update(SysCardEntity);
            }
            else
            {
                SysCardEntity.OrganizeId = orgId;
                SysCardEntity.Create();
                this.Insert(SysCardEntity);
            };
        }

        /// <summary>
        /// 根据卡号和组织机构获取卡信息（先假设不同类型的卡 卡号不会重复）
        /// </summary>
        /// <param name="cardno"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public SysCardEntity GetCardEntity(string cardno, string orgId)
        {
            return IQueryable().FirstOrDefault(p => p.CardNo == cardno && p.OrganizeId == orgId && p.zt == "1");
        }
        
        /// <summary>
        /// 获取卡实体
        /// </summary>
        /// <param name="cardType"></param>
        /// <param name="cardno"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public SysCardEntity GetCardEntity(string cardType, string cardno, string orgId)
        {
            return IQueryable().FirstOrDefault(p => p.CardType == cardType && p.CardNo == cardno && p.OrganizeId == orgId && p.zt == "1");
        }

        public SysCardEntity GetCardEntity(int patid,string cardType, string orgId)
        {
            return IQueryable().FirstOrDefault(p => p.CardType == cardType && p.patid == patid && p.OrganizeId == orgId && p.zt=="1");
        }
    }
}


