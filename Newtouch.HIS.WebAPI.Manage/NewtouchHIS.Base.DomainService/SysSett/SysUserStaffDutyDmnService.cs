using Microsoft.Data.SqlClient;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base.EnumExtend;
using System.Data.Common;

namespace NewtouchHIS.Base.DomainService
{
    public class SysUserStaffDutyDmnService : BaseDmnService<SysStaffDutyComplexVEntity>, ISysUserStaffDutyDmnService
    {
        /// <summary>
        /// 人员岗位关联关系 List
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public async Task<List<SysStaffDutyComplexVEntity>> GetStaffDutyListByOrganizeId(string orgId, string staffId = null)
        {
            if (staffId == null || staffId == "")
            {
                return await GetByWhereWithAttr<SysStaffDutyComplexVEntity>(p => p.zt == "1" && p.OrganizeId == orgId);

            }
            else {
                return await GetByWhereWithAttr<SysStaffDutyComplexVEntity>(p => p.zt == "1" && p.OrganizeId == orgId && p.StaffId == staffId);

            }
        }
        /// <summary>
        /// 查询员工列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<List<SysDutyStaffVO>> GetStaffByDutyCode(string orgId, string keyword = null)
        {
            string sql = @"SELECT DISTINCT
                                StaffName ,
                                StaffPY ,
                                StaffGh ,
                                c.Code ks ,
                                c.py kspy ,
                                c.Name ksmc
                        FROM    [NewtouchHIS_Base]..V_C_Sys_StaffDuty(nolock) AS A
                                LEFT JOIN [NewtouchHIS_Base]..V_S_Sys_Staff(nolock) AS b ON a.StaffGh = b.gh and b.zt = '1' and b.OrganizeId = @OrganizeId
                                INNER JOIN [NewtouchHIS_Base]..V_S_Sys_Department(nolock) AS C ON b.DepartmentCode = C.Code AND C.OrganizeId = @OrganizeId
                        WHERE A.zt = '1' AND a.OrganizeId = @OrganizeId
                        and ((StaffName like '%'+@searchkeyword+'%' or StaffPY like '%'+@searchkeyword+'%' or StaffGh like '%'+@searchkeyword+'%') or DutyCode = @searchkeyword)";

            return await GetListBySqlQuery<SysDutyStaffVO>(DBEnum.BaseDb.ToString(), sql,
               new List<DbParameter>() {
                new SqlParameter("@OrganizeId",orgId),
                new SqlParameter("searchkeyword", keyword)
            });
            //var result =  GetJoinList<SysStaffDutyComplexVEntity, SysStaffVEntity, SysDepartmentVEntity>((a, b, c) => new JoinQueryInfos(
            //JoinType.Left, a.StaffGh == b.gh,
            //JoinType.Left, b.DepartmentCode == c.Code
            //), (a, b, c) => new {
            //    StaffName=  a.StaffName,
            //    StaffPY= a.StaffPY,
            //    StaffGh= a.StaffGh,
            //    ks = c.Code,
            //    kspy = c.py,
            //    ksmc = c.Name 
            //},true, (a, b) => a.OrganizeId == orgId, false, null);
            //return await result;
        }
    }
}
