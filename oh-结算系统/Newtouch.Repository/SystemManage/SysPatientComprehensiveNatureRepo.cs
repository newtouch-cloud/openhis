using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysPatientComprehensiveNatureRepo : RepositoryBase<SysPatientComprehensiveNatureEntity>, ISysPatientComprehensiveNatureRepo
    {
        public SysPatientComprehensiveNatureRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 根据主病人性质 获取 所有
        /// </summary>
        /// <param name="zbrxz"></param>
        /// <returns></returns>
        public IList<SysPatientComprehensiveNatureEntity> GetListByZbrxz(string zbrxz, string orgId)
        {
            return this._dataContext.Set<SysPatientComprehensiveNatureEntity>().Where(p => p.zbrxz == zbrxz && p.OrganizeId == orgId && p.zt == "1")
                .OrderByDescending(p => p.jssx).ToList();
        }

    }
}


