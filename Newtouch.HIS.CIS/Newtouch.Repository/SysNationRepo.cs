using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Domain.IRepository;
using System.Collections.Generic;

namespace Newtouch.Repository
{
    public class SysNationRepo : RepositoryBase<SysNationVEntity>, ISysNationRepo
    {

        public SysNationRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 获取有效民族下拉框
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysNationVEntity> GetmzList()
        {
            var sql = "select mzCode, mzmc, py from [NewtouchHIS_Base]..V_S_xt_mz width(nolock) where zt = '1'";
            return this.FindList<SysNationVEntity>(sql);
        }

    }
}
