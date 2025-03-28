using Newtouch.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysConfigHospRepo : RepositoryBase<SysConfigHospEntity>, ISysConfigHospRepo
    {
        public SysConfigHospRepo(INewtouchDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 根据代码获取住院配置
        /// </summary>
        /// <param name="dm"></param>
        /// <returns></returns>
        public SysConfigHospEntity GetByDM(string dm)
        {
            if (string.IsNullOrWhiteSpace(dm)) return null;
            return this.IQueryable().Where(p => p.zt == ((int)EnumZT.Valid).ToString() && dm.Equals(p.dm)).FirstOrDefault();
        }

        /// <summary>
        /// 根据dm获取配置
        /// </summary>
        /// <param name="dm"></param>
        /// <returns></returns>
        public string GetPZByDM(string dm)
        {
            if (string.IsNullOrWhiteSpace(dm)) return null;
            return this.IQueryable().Where(p => p.zt == ((int)EnumZT.Valid).ToString() && dm.Equals(p.dm)).Select(p => p.pz).FirstOrDefault();
        }

    }
}


