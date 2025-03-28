using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysItemsDetailRepo : RepositoryBase<SysItemsDetailEntity>, ISysItemsDetailRepo
    {
        public SysItemsDetailRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取 组织机构 字典项List
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysItemsDetailEntity> GetListByTopOrg(string topOrganizeId, string itemId = null, string keyword = null)
        {
            var expression = ExtLinq.True<SysItemsDetailEntity>();
            expression = expression.And(t => t.TopOrganizeId == "*" || t.TopOrganizeId == topOrganizeId);
            if (!string.IsNullOrEmpty(itemId))
            {
                expression = expression.And(t => t.ItemId == itemId);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.Name.Contains(keyword));
                expression = expression.Or(t => t.Code.Contains(keyword));
            }
            return this.IQueryable(expression).OrderBy(t => t.px).ToList();
        }

    }
}
