using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    public class OutpatientSettlementRepo : RepositoryBase<OutpatientSettlementEntity>, IOutpatientSettlementRepo
    {
        public OutpatientSettlementRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        /// <summary>
        /// 根据结算jsnm查门诊结算
        /// </summary>
        /// <param name="jsnm"></param>
        /// <returns></returns>
        public OutpatientSettlementEntity SelectMZJS(int jsnm, string orgId)
        {
            return this.IQueryable().Where(a => a.jsnm == jsnm && a.OrganizeId == orgId).FirstOrDefault();
        }

        ///// <summary>
        ///// 根据ghnm查询list
        ///// </summary>
        ///// <param name="orgId"></param>
        ///// <param name="ghnm"></param>
        ///// <returns></returns>
        //public List<OutpatientSettlementEntity> SelectEntityByGhnm(string orgId, int ghnm)
        //{
        //    var list = this.IQueryable().Where(a => a.OrganizeId == orgId && a.zt == "1" && a.ghnm == ghnm).ToList();
        //    return list;
        //}

        /// <summary>
        /// 强制更新发票号
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="fph"></param>
        /// <param name="orgId"></param>
        public void UpdateSettedFph(int jsnm, string fph, string orgId)
        {
            var entity = this.IQueryable().Where(a => a.OrganizeId == orgId && a.zt == "1" && a.jsnm == jsnm).FirstOrDefault();
            if (entity != null)
            {
                entity.fph = fph;
                this.Update(entity);
            }
        }

    }
}
