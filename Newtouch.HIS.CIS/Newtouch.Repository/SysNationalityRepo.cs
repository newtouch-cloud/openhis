using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Repository
{
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
