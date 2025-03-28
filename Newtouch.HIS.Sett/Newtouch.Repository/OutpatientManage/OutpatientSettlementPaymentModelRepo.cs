using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    public class OutpatientSettlementPaymentModelRepo : RepositoryBase<OutpatientSettlementPaymentModelEntity>, IOutpatientSettlementPaymentModelRepo
    {
        public OutpatientSettlementPaymentModelRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        /// <summary>
        /// 根据jsnm查结算支付方式
        /// </summary>
        /// <param name="jsnm"></param>
        /// <returns></returns>
        public List<OutpatientSettlementPaymentModelEntity> SelectMzjszffsByJsnm(int jsnm, string orgId)
        {
            return this.IQueryable().Where(a => a.jsnm == jsnm && a.OrganizeId == orgId).ToList();
        }

    }
}
