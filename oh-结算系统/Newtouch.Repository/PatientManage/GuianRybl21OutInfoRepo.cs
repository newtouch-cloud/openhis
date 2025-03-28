using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class GuianRybl21OutInfoRepo : RepositoryBase<GuianRybl21OutInfoEntity>, IGuianRybl21OutInfoRepo
    {
        public GuianRybl21OutInfoRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 根据住院号获取贵安医保住院反馈信息
        /// </summary>
        /// <param name="zyh">住院号</param>
        /// <param name="OrganizeId">组织机构ID</param>
        /// <returns></returns>
        public GuianRybl21OutInfoEntity GetInfoByZyh(string zyh, string OrganizeId)
        {
            var GuianRybl21Info = new List<GuianRybl21OutInfoEntity>();

            GuianRybl21Info = this.IQueryable().Where(p => p.OrganizeId == OrganizeId && p.prm_ykc010 == zyh).ToList();
            if (GuianRybl21Info.Count > 0)
                return GuianRybl21Info[0];
            else
                return null;
        }
    }
}
