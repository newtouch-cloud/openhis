using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class InspectionTemplateRepo : RepositoryBase<InspectionTemplateEntity>, IInspectionTemplateRepo
    {
        public InspectionTemplateRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 模板列表
        /// </summary>
        /// <returns></returns>
        public List<InspectionTemplateEntity> GetList(int type, string orgId)
        {
            var list = this.IQueryable().Where(a => a.OrganizeId == orgId && a.Type == type).ToList();
            return list;
        }
    }
}
