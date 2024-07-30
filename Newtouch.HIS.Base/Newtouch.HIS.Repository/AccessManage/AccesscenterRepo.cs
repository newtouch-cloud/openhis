using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2022-06-28 21:50
    /// 描 述：接口注入
    /// </summary>
    public class AccesscenterRepo : RepositoryBase<AccesscenterEntity>, IAccesscenterRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public AccesscenterRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }

    }
}