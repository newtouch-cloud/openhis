using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ViewModels;

namespace Newtouch.DomainServices
{
    /// <summary>
    /// 医生出诊
    /// </summary>
    public class VisitDeptSetDmnService : DmnServiceBase, IVisitDeptSetDmnService
    {
        public VisitDeptSetDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取出诊医生信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IList<VisitDeptSetVO> SelectVisitDeptSet(Pagination pagination, string keyword, string organizeId)
        {
            const string sql = @"
SELECT DISTINCT ss.Name staffName, su.Account account, vd.ysgh, sdept.Code subordinateDeptCode, sdept.Name subordinateDeptName
FROM dbo.visit_deptSet(NOLOCK) vd
INNER JOIN NewtouchHIS_Base.dbo.Sys_Staff(NOLOCK) ss ON ss.gh=vd.ysgh AND ss.zt='1' AND ss.OrganizeId=vd.OrganizeId
INNER JOIN NewtouchHIS_Base.dbo.Sys_UserStaff(NOLOCK) sus ON sus.StaffId=ss.Id AND sus.zt='1'
INNER JOIN NewtouchHIS_Base.dbo.Sys_User(NOLOCK) su ON su.Id=sus.UserId AND su.zt='1' 
LEFT JOIN NewtouchHIS_Base.dbo.Sys_Department(NOLOCK) sdept ON sdept.Code=ss.DepartmentCode AND sdept.OrganizeId=vd.OrganizeId AND sdept.zt='1'
WHERE vd.zt='1'
AND (vd.ysgh LIKE '%'+@keyword+'%' OR su.Account LIKE '%'+@keyword+'%' OR ss.Name LIKE '%'+@keyword+'%')  
AND vd.OrganizeId=@OrganizeId
";
            var param = new DbParameter[]
            {
                new SqlParameter("@keyword", keyword),
                new SqlParameter("@OrganizeId",organizeId )
            };
            return QueryWithPage<VisitDeptSetVO>(sql, pagination, param);
        }

        /// <summary>
        /// 获取出诊医生明细
        /// </summary>
        /// <param name="ysgh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IList<VisitDeptDetail> SelectVisitDeptSetDetail(string ysgh, string organizeId)
        {
            const string sql = @"
SELECT ss.Name staffName, su.Account account, sdept.Name subordinateDeptName, vdept.Name visitDeptName, vd.*
FROM dbo.visit_deptSet(NOLOCK) vd
INNER JOIN NewtouchHIS_Base.dbo.Sys_Staff(NOLOCK) ss ON ss.gh=vd.ysgh AND ss.zt='1' AND ss.OrganizeId=vd.OrganizeId
INNER JOIN NewtouchHIS_Base.dbo.Sys_UserStaff(NOLOCK) sus ON sus.StaffId=ss.Id AND sus.zt='1'
INNER JOIN NewtouchHIS_Base.dbo.Sys_User(NOLOCK) su ON su.Id=sus.UserId AND su.zt='1' 
LEFT JOIN NewtouchHIS_Base.dbo.Sys_Department(NOLOCK) sdept ON sdept.Code=ss.DepartmentCode AND sdept.OrganizeId=vd.OrganizeId AND sdept.zt='1'
LEFT JOIN NewtouchHIS_Base.dbo.Sys_Department(NOLOCK) vdept ON vdept.Code=vd.visitksCode AND vdept.OrganizeId=vd.OrganizeId AND vdept.zt='1'
WHERE ysgh=@ysgh  
AND vd.OrganizeId=@OrganizeId
";
            var param = new DbParameter[]
            {
                new SqlParameter("@ysgh", ysgh),
                new SqlParameter("@OrganizeId", organizeId)
            };
            return FindList<VisitDeptDetail>(sql, param);
        }

        /// <summary>
        /// 获取出诊医生明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VisitDeptDetail SelectVisitDeptSetInfo(string id)
        {
            const string sql = @"
SELECT ss.Name staffName, su.Account account, sdept.Name subordinateDeptName, vdept.Name visitDeptName, vd.*
FROM dbo.visit_deptSet(NOLOCK) vd
INNER JOIN NewtouchHIS_Base.dbo.Sys_Staff(NOLOCK) ss ON ss.gh=vd.ysgh AND ss.zt='1' AND ss.OrganizeId=vd.OrganizeId
INNER JOIN NewtouchHIS_Base.dbo.Sys_UserStaff(NOLOCK) sus ON sus.StaffId=ss.Id AND sus.zt='1'
INNER JOIN NewtouchHIS_Base.dbo.Sys_User(NOLOCK) su ON su.Id=sus.UserId AND su.zt='1' 
LEFT JOIN NewtouchHIS_Base.dbo.Sys_Department(NOLOCK) sdept ON sdept.Code=ss.DepartmentCode AND sdept.OrganizeId=vd.OrganizeId AND sdept.zt='1'
LEFT JOIN NewtouchHIS_Base.dbo.Sys_Department(NOLOCK) vdept ON vdept.Code=vd.visitksCode AND vdept.OrganizeId=vd.OrganizeId AND vdept.zt='1'
WHERE vd.Id=@vId
";
            var param = new DbParameter[]
            {
                new SqlParameter("@vId", id)
            };
            return FirstOrDefault<VisitDeptDetail>(sql, param);
        }

        /// <summary>
        /// 获取出诊医生信息
        /// </summary>
        /// <param name="ysgh"></param>
        /// <param name="organizeId"></param>
        /// <param name="id">visit_deptSet.id</param>
        /// <returns></returns>
        public VisitDeptDetail SelectDoctorInfo(string ysgh, string organizeId, string id = "")
        {
            const string sql = @"
SELECT ss.Name staffName, su.Account account, sdept.Name subordinateDeptName, vdept.Name visitDeptName, ss.gh ysgh, vd.Id, vd.visitksCode, ISNULL(vd.czlx, 1) czlx
,sdept.Code SubordinateDepartments, vd.zt, ss.OrganizeId
FROM NewtouchHIS_Base.dbo.Sys_Staff(NOLOCK) ss 
INNER JOIN NewtouchHIS_Base.dbo.Sys_UserStaff(NOLOCK) sus ON sus.StaffId=ss.Id AND sus.zt='1'
INNER JOIN NewtouchHIS_Base.dbo.Sys_User(NOLOCK) su ON su.Id=sus.UserId AND su.zt='1' 
LEFT JOIN NewtouchHIS_Base.dbo.Sys_Department(NOLOCK) sdept ON sdept.Code=ss.DepartmentCode AND sdept.OrganizeId=ss.OrganizeId AND sdept.zt='1'
LEFT JOIN dbo.visit_deptSet(NOLOCK) vd ON ss.gh=vd.ysgh AND ss.OrganizeId=vd.OrganizeId  
LEFT JOIN NewtouchHIS_Base.dbo.Sys_Department(NOLOCK) vdept ON vdept.Code=vd.visitksCode AND vdept.OrganizeId=ss.OrganizeId AND vdept.zt='1'
WHERE ss.zt='1' 
AND ss.OrganizeId=@OrganizeId
AND ss.gh=@ysgh
AND (vd.Id=@vId OR ''=ISNULL(@vId,''))
";
            var param = new DbParameter[]
            {
                new SqlParameter("@ysgh", ysgh),
                new SqlParameter("@vId", id),
                new SqlParameter("@OrganizeId", organizeId)
            };
            return FirstOrDefault<VisitDeptDetail>(sql, param);
        }
    }
}