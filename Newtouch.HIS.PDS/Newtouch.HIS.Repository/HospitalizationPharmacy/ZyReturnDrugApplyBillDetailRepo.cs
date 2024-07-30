using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 住院退药申请单明细
    /// </summary>
    public class ZyReturnDrugApplyBillDetailRepo : RepositoryBase<ZyReturnDrugApplyBillDetailEntity>, IZyReturnDrugApplyBillDetailRepo
    {
        public ZyReturnDrugApplyBillDetailRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 根据退药申请Id获取退药申请明细
        /// </summary>
        /// <param name="rabId"></param>
        /// <returns></returns>
        public List<ZyReturnDrugApplyBillDetailEntity> SelectData(string rabId)
        {
            const string sql = @"SELECT * FROM dbo.zy_returnDrugApplyBillDetail(NOLOCK) WHERE rabId=@rabId AND zt='1' AND tysl>0";
            var param = new SqlParameter("@rabId", rabId);
            return _dataContext.Database.SqlQuery<ZyReturnDrugApplyBillDetailEntity>(sql, param).ToList();
        }
    }
}