using System.Collections.Generic;
using Newtouch.HIS.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysNationalityRepo : RepositoryBase<SysNationalityVEntity>, ISysNationalityRepo
    {
        public SysNationalityRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 获取有效国籍
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysNationalityVEntity> GetgjList()
        {
            var sql = "select gjId, gjCode,py,gjmc from [NewtouchHIS_Base]..V_S_xt_gj width(nolock) where zt = '1'";
            return this.FindList<SysNationalityVEntity>(sql);
        }
    }
}
