using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.IDomainServices.KnowledgeBaseManage;
using Newtouch.HIS.Domain.ValueObjects.KnowledgeBaseManage;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Newtouch.HIS.DomainServices.KnowledgeBaseManage
{
    public class MedicalInsuranceDmnService:DmnServiceBase, IMedicalInsuranceDmnService
    {
        public MedicalInsuranceDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取医保备案列表
        /// </summary>
        /// <param name="blh"></param>
        /// <param name="ksrq"></param>
        /// <param name="jsrq"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<SysMedicalInsuranceFilingVO> SelectMedicalInsuranceFilingList(Pagination pagination, string keyword, string orgId, string ybbabId=null)
        {
            if (string.IsNullOrEmpty(orgId))
            {
                return null;
            }
            StringBuilder sqlStr = new StringBuilder();
            IList<SqlParameter> inSqlParameterList = new List<SqlParameter>();
            sqlStr.Append(@"
SELECT ybbab.*,
        brjbxx.blh,
		brjbxx.xm,
		brjbxx.csny,
		brjbxx.zjh
FROM xt_ybbab ybbab
LEFT JOIN xt_brjbxx brjbxx
    ON brjbxx.patId=ybbab.patId
    AND brjbxx.OrganizeId=@orgId
WHERE ybbab.OrganizeId=@orgId
                        ");
            inSqlParameterList.Add(new SqlParameter("@orgId", orgId.Trim()));
            if (!string.IsNullOrEmpty(keyword))
            {
                sqlStr.Append(" and brjbxx.blh like @keyword ");
                inSqlParameterList.Add(new SqlParameter("@keyword", "%" + keyword.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(ybbabId))
            {
                sqlStr.Append(" and ybbab.ybbabId=@ybbabId");
                inSqlParameterList.Add(new SqlParameter("@ybbabId", ybbabId.Trim()));
            }
            var list = this.QueryWithPage<SysMedicalInsuranceFilingVO>(sqlStr.ToString(), pagination, inSqlParameterList.ToArray()).ToList();
            return list;
        }
        
    }
}
