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
    public class OutpatientAccountDetailRepo : RepositoryBase<OutpatientAccountDetailEntity>, IOutpatientAccountDetailRepo
    {
        public OutpatientAccountDetailRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 根据记账计划明细Id获取list
        /// </summary>
        /// <returns></returns>
        public IList<OutpatientAccountDetailEntity> GetListbyjzjhids(string orgId, IList<string> mxIdList)
        {
            return this.IQueryable().Where(p => p.zt == "1" && p.OrganizeId == orgId && mxIdList.Contains(p.jzjhmxId)).ToList();
        }
    }
}
