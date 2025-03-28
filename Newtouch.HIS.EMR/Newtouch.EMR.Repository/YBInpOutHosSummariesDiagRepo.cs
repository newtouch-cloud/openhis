using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using FrameworkBase.MultiOrg.Repository;
using FrameworkBase.MultiOrg.Infrastructure;

namespace Newtouch.EMR.Repository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2021-03-01 15:24
    /// 描 述：出院小结-诊断
    /// </summary>
    public class YBInpOutHosSummariesDiagRepo : RepositoryBase<YBInpOutHosSummariesDiagEntity>, IYBInpOutHosSummariesDiagRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public YBInpOutHosSummariesDiagRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }

        public void Submit(YBInpOutHosSummariesDiagEntity ety)
        {
            if (ety != null)
            {
                if (!string.IsNullOrWhiteSpace(ety.Id))
                {
                    ety.Modify(ety.Id);
                    this.Update(ety);
                }
                else
                {
                    ety.Create(true, ety.Id);
                    this.Insert(ety);
                }
            }
        }

    }
}