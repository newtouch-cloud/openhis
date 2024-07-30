using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// 药品信息
    /// </summary>
    public class SysMedicineExDmnService : DmnServiceBase, ISysMedicineExDmnService
    {
        public SysMedicineExDmnService(IDatabaseFactory databaseFactory, bool needIoc = true) : base(databaseFactory, needIoc)
        {
        }

        /// <summary>
        /// 查询药品详情
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        public SysMedicineComplexVEntity GetYpDetails(string orgId, string ypCode)
        {
            const string sql = @"select * from [NewtouchHIS_Base]..V_C_xt_yp where zt = '1' and ypCode = @ypCode and OrganizeId = @orgId";
            DbParameter[] pars = {
                new SqlParameter("@orgId", orgId), new SqlParameter("@ypCode", ypCode)
            };
            return FirstOrDefaultNoLog<SysMedicineComplexVEntity>(sql, pars);
        }
    }
}