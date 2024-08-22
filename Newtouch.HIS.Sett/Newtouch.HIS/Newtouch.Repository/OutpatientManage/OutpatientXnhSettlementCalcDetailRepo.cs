using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 门诊新农合结算
    /// </summary>
    public class OutpatientXnhSettlementCalcDetailRepo : RepositoryBase<OutpatientXnhSettlementCalcDetailEntity>, IOutpatientXnhSettlementCalcDetailRepo
    {
        public OutpatientXnhSettlementCalcDetailRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }


    }
}