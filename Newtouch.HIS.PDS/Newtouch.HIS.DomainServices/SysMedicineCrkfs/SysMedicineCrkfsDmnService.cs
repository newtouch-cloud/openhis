using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.Entity.V;
using Newtouch.HIS.Domain.IDomainServices;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// 药品出入库方式
    /// </summary>
    public class SysMedicineCrkfsDmnService : DmnServiceBase, ISysMedicineCrkfsDmnService
    {
        public SysMedicineCrkfsDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取出入库方式
        /// </summary>
        /// <param name="crkbz">出入库标志 0：入库  1：出库</param>
        /// <returns></returns>
        public List<SysMedicineCrkfsVEntity> GetList(string crkbz)
        {
            var sql = new StringBuilder("SELECT crkfsId, crkfsCode, crkfsmc, crkbz, zt ");
            sql.AppendLine("FROM NewtouchHIS_Base.dbo.V_S_xt_ypcrkfs ");
            sql.AppendLine("WHERE zt='1' ");
            if (!string.IsNullOrWhiteSpace(crkbz))
            {
                sql.AppendLine("AND crkbz=@crkbz ");
            }
            var param = new DbParameter[] { new SqlParameter("crkbz", crkbz) };
            return FindList<SysMedicineCrkfsVEntity>(sql.ToString(), param);
        }
    }
}