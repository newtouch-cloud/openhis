using Newtouch.Common.Operator;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysConfigOutpaientRepo : RepositoryBase<SysConfigOutpaientEntity>, ISysConfigOutpaientRepo
    {
        public SysConfigOutpaientRepo(INewtouchDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public SysConfigOutpaientEntity SelectSysConfigOutPatient(string dm)
        {
            var OrganizeId = OperatorProvider.GetCurrent().OrganizeId;
            return this.IQueryable().Where(a => a.dm == dm && a.OrganizeId == OrganizeId).FirstOrDefault();
        }

        /// 根据代码获取配置信息
        public string GetSysConfig(string dm)
        {
            var OrganizeId = OperatorProvider.GetCurrent().OrganizeId;
            string pz = null;
            var pzData = this.IQueryable().FirstOrDefault(a => a.dm == dm && a.OrganizeId == OrganizeId);
            if (pzData!=null) 
            {
                pz = pzData.pz;
            }
            return pz;
        }
    }
}


