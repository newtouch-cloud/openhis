
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System;
using System.Data.SqlClient;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 处方主表
    /// </summary>
    public class OutpatientPrescriptionRepo : RepositoryBase<OutpatientPrescriptionEntity>, IOutpatientPrescriptionRepo
    {
        public OutpatientPrescriptionRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 根据cfh获取 有效 处方实体
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="cfh"></param>
        /// <returns></returns>
        public OutpatientPrescriptionEntity GetValidEntityByCfh(string orgId, string cfh)
        {
            var sql = @"select * from mz_cf where cfh = @cfh and OrganizeId = @orgId";
            return this.FirstOrDefault<OutpatientPrescriptionEntity>(sql
                , new[] { new SqlParameter("@cfh", cfh ?? ""), new SqlParameter("@orgId", orgId ?? "") });
        }

        /// <summary>
        /// 根据cfnm获取 有效 处方实体
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="cfnm"></param>
        /// <returns></returns>
        public OutpatientPrescriptionEntity GetValidEntityByCfnm(string orgId, int cfnm)
        {
            var sql = @"select * from mz_cf where cfnm = @cfnm and OrganizeId = @orgId";
            return this.FirstOrDefault<OutpatientPrescriptionEntity>(sql
                , new[] { new SqlParameter("@cfnm", cfnm), new SqlParameter("@orgId", orgId ?? "") });
        }

        /// <summary>
        /// 生成新处方号
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="type">处方类型1药品 2非药品</param>
        /// <returns></returns>
        public string GeneratePresNo(string orgId, int type)
        {
            var cfh = "R" + DateTime.Now.ToString("yyyyMMdd") + type.ToString()
                + "N" + EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("mz_cf.cfh", orgId, "{0:D5}", true);
            return cfh;
        }

    }
}
