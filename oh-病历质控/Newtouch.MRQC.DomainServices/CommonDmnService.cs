using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Domain.ValueObjects;
using FrameworkBase.MultiOrg.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MRQC.Domain.IDomainServices
{
    public class CommonDmnService : DmnServiceBase, ICommonDmnService
    {
        private readonly ISysConfigRepo _sysConfigRepo;
        public CommonDmnService(IDefaultDatabaseFactory databaseFactory, ISysConfigRepo SysConfigRepo)
            : base(databaseFactory)
        {
            _sysConfigRepo = SysConfigRepo;
        }
        /// <summary>
        /// 根据DutyCode（职位）查询员工列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="dutyCode"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<SysDutyStaffVO> GetStaffByDutyCode(string orgId, string dutyCode, string keyword = null)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT DISTINCT
                                StaffName ,
                                StaffPY ,
                                StaffGh ,
                                c.Code ks ,
                                c.py kspy ,
                                c.Name ksmc
                        FROM    [NewtouchHIS_Base]..V_C_Sys_StaffDuty(nolock) AS A
                                LEFT JOIN [NewtouchHIS_Base]..V_S_Sys_Staff(nolock) AS b ON a.StaffGh = b.gh and b.zt = '1' and b.OrganizeId = @OrganizeId
                                INNER JOIN [NewtouchHIS_Base]..V_S_Sys_Department(nolock) AS C ON b.DepartmentCode = C.Code
                                                              AND C.OrganizeId = @OrganizeId
                                                              WHERE A.zt = '1'  AND a.OrganizeId = @OrganizeId");
            var par1 = new List<SqlParameter>() {
                     new SqlParameter("@OrganizeId",orgId),
                     new SqlParameter("@DutyCode",dutyCode??"")};
            if (!string.IsNullOrWhiteSpace(dutyCode))
            {
                strSql.Append("  and DutyCode = @DutyCode ");
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                strSql.Append(" and (StaffName like @searchkeyword or StaffPY like @searchkeyword or StaffGh like @searchkeyword)");
                par1.Add(new SqlParameter("searchkeyword", "%" + keyword + "%"));
            }
            return this.FindList<SysDutyStaffVO>(strSql.ToString(), par1.ToArray());
        }
    }
}
