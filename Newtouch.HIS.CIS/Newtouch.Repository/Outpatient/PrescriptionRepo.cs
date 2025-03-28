using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Newtouch.Core.Common;
using Newtouch.CIS.APIRequest.Prescription;
using Newtouch.Common.Operator;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class PrescriptionRepo : RepositoryBase<PrescriptionEntity>, IPrescriptionRepo
    {
        public PrescriptionRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 更新处方收费标志和同步标志
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="tbbz"></param>
        /// <param name="sfbz"></param>
        /// <param name="orgId"></param>
        /// <param name="account"></param>
        public void UpdateChargeStatus(string cfh, bool tbbz, bool? sfbz, string orgId, string account)
        {
            var sql = new StringBuilder(@"UPDATE dbo.xt_cf SET tbbz=@tbbz, LastModifyTime=GETDATE(), LastModifierCode=@opr ");
            var param = new List<DbParameter>
            {
                new SqlParameter("@tbbz", tbbz ? 1 : 0),
                new SqlParameter("@opr", account),
                new SqlParameter("@cfh", cfh),
                new SqlParameter("@OrganizeId", orgId)
            };
            if (sfbz != null)
            {
                sql.Append(",sfbz=@sfbz ");
                param.Add(new SqlParameter("@sfbz", (bool)sfbz ? 1 : 0));
            }

            sql.AppendLine("WHERE cfh=@cfh AND OrganizeId=@OrganizeId AND zt='1' ");
            ExecuteSqlCommand(sql.ToString(), param.ToArray());
        }

        /// <summary>
        /// 更新处方收费标志
        /// </summary>
        /// <param name="part"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        public void UpdateChargeStatus(PrescriptionChargeRequest part, string organizeId, string userCode)
        {
            if (part.cfList == null) return;
            foreach (var item in part.cfList)
            {
                UpdateChargeStatus(organizeId, item.cfh, part.sfbz, userCode);
            }
        }

        /// <summary>
        /// 更新处方收费标志
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="cfh"></param>
        /// <param name="sfbz"></param>
        /// <param name="account"></param>
        public void UpdateChargeStatus(string orgId, string cfh, bool sfbz, string account)
        {
            var entity = this.IQueryable().FirstOrDefault(a => a.OrganizeId == orgId && a.cfh == cfh && a.zt == "1");
            if (entity == null)
            {
                throw new FailedException("prescription is not exist", "该处方不存在");
            }

            entity.sfbz = sfbz;
            entity.LastModifierCode = account;
            entity.LastModifyTime = DateTime.Now;

            this.Update(entity);
        }

        /// <summary>
        /// 更新处方退标志
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="cfh"></param>
        /// <param name="tbz"></param>
        /// <param name="account"></param>
        public void UpdateChargeTbz(string orgId, string cfh, bool tbz, string account)
        {
            var entity = this.IQueryable().FirstOrDefault(a => a.OrganizeId == orgId && a.cfh == cfh && a.zt == "1" && a.sfbz);
            if (entity == null)
            {
                throw new FailedException("prescription is not exist", "该处方不存在");
            }

            entity.tbz = tbz;
            entity.LastModifierCode = account;
            entity.LastModifyTime = DateTime.Now;

            this.Update(entity);
        }

        /// <summary>
        /// 获取处方信息
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public PrescriptionEntity FindListByCfh(string cfh, string orgId)
        {
            const string sql = @"SELECT TOP 1 * FROM dbo.xt_cf(NOLOCK) WHERE cfh=@cfh AND OrganizeId=@OrganizeId AND zt='1'";
            var param = new DbParameter[]
            {
                new SqlParameter("@cfh", cfh),
                new SqlParameter("@OrganizeId", orgId)
            };
            return FirstOrDefault<PrescriptionEntity>(sql, param);
        }

        /// <summary>
        /// 获取处方信息
        /// </summary>
        /// <param name="jzId"></param>
        /// <param name="sfbz"></param>
        /// <returns></returns>
        public List<PrescriptionEntity> FindList(string jzId, bool sfbz)
        {
            const string sql = @"
SELECT * FROM dbo.xt_cf(NOLOCK) WHERE jzId=@jzId AND zt='1' AND sfbz=@sfbz
";
            var param = new DbParameter[]
            {
                new SqlParameter("@jzId", jzId),
                new SqlParameter("@sfbz", sfbz?1:0),
            };
            return FindList<PrescriptionEntity>(sql, param);
        }

    }
}
