using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysRegistSpecialDiseaseRepo : RepositoryBase<SysRegistSpecialDiseaseEntity>, ISysRegistSpecialDiseaseRepo
    {
        public SysRegistSpecialDiseaseRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 获取所有挂号专病
        /// </summary>
        /// <param name="ghzb"></param>
        /// <returns></returns>
        public List<SysRegistSpecialDiseaseEntity> SelectSysChargeItemByghzbList(string orgId)
        {
            return this.IQueryable().Where(a => a.zt == "1" && a.OrganizeId == orgId).ToList();
        }

        /// <summary>
        /// 根据ghzbbh  add by caishanshan
        /// </summary>
        /// <param name="sfxm"></param>
        /// <returns></returns>
        public SysRegistSpecialDiseaseEntity SelectSysChargeItemByghzb(string ghzb, string orgId)
        {
            return this.IQueryable().Where(a => a.ghzb == ghzb && a.zt == "1" && a.OrganizeId == orgId).FirstOrDefault();
        }

    }
}


