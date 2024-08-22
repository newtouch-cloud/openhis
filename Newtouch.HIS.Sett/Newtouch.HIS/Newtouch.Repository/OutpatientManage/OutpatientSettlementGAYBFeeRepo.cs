using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.OutpatientManage;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class OutpatientSettlementGAYBFeeRepo : RepositoryBase<OutpatientSettlementGAYBFeeEntity>, IOutpatientSettlementGAYBFeeRepo
    {
        public OutpatientSettlementGAYBFeeRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public void InsertEntity(OutpatientSettlementGAYBFeeEntity entity)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                entity.Create();
                this.Insert(entity);
                db.Commit();
            }
                
        }
    }
}
