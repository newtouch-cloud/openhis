using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Newtouch.DomainServices
{
    /// <summary>
    /// 预约挂号
    /// </summary>
    public class SysBespeakRegisterDmnService : DmnServiceBase, ISysBespeakRegisterDmnService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseFactory"></param>
        public SysBespeakRegisterDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// select SysBespeakregister
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public IList<SysBespeakRegisterVO> SelectSysBespeakregister(Pagination pagination, string keyword, string OrganizeId)
        {
            const string sql = @"
SELECT br.Id, br.OrganizeId, dept.Name ksmc, (CASE br.mzlx WHEN '1' THEN '普通门诊' WHEN '2' THEN '急症' WHEN '3' THEN '专家门诊' ELSE '' END) mzlx
,br.regDate, br.regTime, staff.Name zjmc, br.bespeakMaxCount, br.CreateTime, br.CreatorCode, br.LastModifyTime, br.LastModifierCode, br.zt
FROM dbo.xt_bespeakRegister(NOLOCK) br
LEFT JOIN NewtouchHIS_Base.dbo.Sys_Department(NOLOCK) dept ON dept.Code=br.departmentCode AND dept.OrganizeId=br.OrganizeId AND dept.zt='1'
LEFT JOIN NewtouchHIS_Base.dbo.Sys_Staff(NOLOCK) staff ON staff.gh=br.ysgh AND staff.OrganizeId=br.OrganizeId AND staff.zt='1'
WHERE br.OrganizeId=@OrganizeId
AND (dept.Name LIKE '%'+@keyword+'%' OR staff.Name LIKE '%'+@keyword+'%')
";
            var param = new SqlParameter[] {
                new SqlParameter("@OrganizeId", OrganizeId),
                new SqlParameter("@keyword", keyword)
            };
            return QueryWithPage<SysBespeakRegisterVO>(sql, pagination, param);
        }

        /// <summary>
        /// select SysBespeakregister
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="gh"></param>
        /// <param name="OrganizeId"></param>
        /// <param name="regDate"></param>
        /// <param name="regTime"></param>
        /// <returns></returns>
        public IList<SysBespeakRegisterVO> SelectSysBespeakregister(string deptCode, string gh, string OrganizeId, DateTime? regDate, string regTime = "")
        {
            var sql = new StringBuilder(@"
SELECT br.Id, br.OrganizeId, dept.Name ksmc, (CASE br.mzlx WHEN '1' THEN '普通门诊' WHEN '2' THEN '急症' WHEN '3' THEN '专家门诊' ELSE '' END) mzlx
,br.regDate, br.regTime, staff.Name zjmc, br.bespeakMaxCount, br.CreateTime, br.CreatorCode, br.LastModifyTime, br.LastModifierCode, br.zt
FROM dbo.xt_bespeakRegister(NOLOCK) br
LEFT JOIN NewtouchHIS_Base.dbo.Sys_Department(NOLOCK) dept ON dept.Code=br.departmentCode AND dept.OrganizeId=br.OrganizeId AND dept.zt='1'
LEFT JOIN NewtouchHIS_Base.dbo.Sys_Staff(NOLOCK) staff ON staff.gh=br.ysgh AND staff.OrganizeId=br.OrganizeId AND staff.zt='1'
WHERE br.OrganizeId=@OrganizeId
AND br.zt='1'
");
            var param = new List<SqlParameter> {
                new SqlParameter("@OrganizeId", OrganizeId)
            };
            if (!string.IsNullOrWhiteSpace(deptCode))
            {
                sql.AppendLine("AND dept.Code=@deptCode ");
                param.Add(new SqlParameter("@deptCode", deptCode));
            }
            if (!string.IsNullOrWhiteSpace(gh))
            {
                sql.AppendLine("AND staff.gh=@gh ");
                param.Add(new SqlParameter("@gh", gh));
            }
            if (regDate != null && regDate > Convert.ToDateTime("1970-01-01"))
            {
                sql.AppendLine("AND br.regDate=@regDate ");
                param.Add(new SqlParameter("@regDate", regDate));
            }
            if (!string.IsNullOrWhiteSpace(regTime))
            {
                sql.AppendLine("AND br.regTime=@regTime ");
                param.Add(new SqlParameter("@regTime", regTime));
            }
            return FindList<SysBespeakRegisterVO>(sql.ToString(), param.ToArray());
        }

        /// <summary>
        /// 根据科室和排班日期获取系统预约排班
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="regDate"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IList<SysBespeakRegisterVO> SelectSysBespeakregister(string deptCode, DateTime regDate, string organizeId)
        {
            const string sql = @"
SELECT DISTINCT dept.Name ksmc, dept.Code ksCode, staff.gh, staff.Name zjmc  
FROM dbo.xt_bespeakRegister(NOLOCK) br
INNER JOIN NewtouchHIS_Base.dbo.Sys_Staff(NOLOCK) staff ON staff.gh=br.ysgh AND staff.OrganizeId=br.OrganizeId AND staff.zt='1'
LEFT JOIN NewtouchHIS_Base.dbo.Sys_Department(NOLOCK) dept ON dept.Code=br.departmentCode AND dept.OrganizeId=br.OrganizeId AND dept.zt='1'
WHERE br.OrganizeId=@OrganizeId
AND dept.Code=@deptCode
AND br.regDate=@regDate 
";
            var param = new SqlParameter[] {
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@deptCode", deptCode??""),
                new SqlParameter("@regDate", regDate)
            };
            return FindList<SysBespeakRegisterVO>(sql, param);
        }

        /// <summary>
        /// 根据科室获取系统预约排班
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IList<SysBespeakRegisterVO> SelectSysBespeakregister(string deptCode, string organizeId)
        {
            const string sql = @"
SELECT DISTINCT dept.Name ksmc, dept.Code ksCode, staff.gh, staff.Name zjmc  
FROM dbo.xt_bespeakRegister(NOLOCK) br
INNER JOIN NewtouchHIS_Base.dbo.Sys_Staff(NOLOCK) staff ON staff.gh=br.ysgh AND staff.OrganizeId=br.OrganizeId AND staff.zt='1'
LEFT JOIN NewtouchHIS_Base.dbo.Sys_Department(NOLOCK) dept ON dept.Code=br.departmentCode AND dept.OrganizeId=br.OrganizeId AND dept.zt='1'
WHERE br.OrganizeId=@OrganizeId
AND dept.Code=@deptCode ";
            var param = new SqlParameter[] {
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@deptCode", deptCode??"")
            };
            return FindList<SysBespeakRegisterVO>(sql, param);
        }
    }
}