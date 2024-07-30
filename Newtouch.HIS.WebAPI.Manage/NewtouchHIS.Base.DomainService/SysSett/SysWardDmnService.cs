using Microsoft.Data.SqlClient;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base.EnumExtend;
using System.Data.Common;

namespace NewtouchHIS.Base.DomainService
{
    public class SysWardDmnService : BaseDmnService<SysWardVEntity>, ISysWardDmnService
    {
        /// <summary>
        /// 获取有效
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<List<SysWardVEntity>> GetbqList(string orgId)
        {
            var result = await GetByWhereWithAttr<SysWardVEntity>(p => p.zt == "1" && p.OrganizeId == orgId);
            return result;
        }
        /// <summary>
        /// 获取科室绑定的病区
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ks"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<List<SysDepartmentWardRelationVO>> GetWardbyDept(string orgId, string ks, string keyword)
        {
            string sql = @"select a.bqCode bq,a.bqmc,c.Code ks,c.Name ksmc
            from NewtouchHIS_Base.dbo.xt_bq a with(nolock)
            left join NewtouchHIS_Base.dbo.Sys_DepartmentWardRelation b with(nolock) on a.OrganizeId=b.OrganizeId and a.bqCode=b.bqCode and b.zt='1' 
            left join NewtouchHIS_Base.dbo.Sys_Department c  with(nolock) on b.OrganizeId=c.OrganizeId and b.DepartmentId=c.Id and c.zt='1'
            where a.OrganizeId=@orgId and a.zt='1'";
            if (!string.IsNullOrWhiteSpace(ks))
            {
                sql += " and b.bqCode=@ks ";
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and a.bqmc like @keyword ";
            }
            return await GetListBySqlQuery<SysDepartmentWardRelationVO>(DBEnum.BaseDb.ToString(), sql, new List<DbParameter> {
                            new SqlParameter("orgId",orgId),
                            new SqlParameter("ks",ks??""),
                            new SqlParameter("keyword","%"+keyword??""+"%"),
                        });
            // List<SysDepartmentWardRelationVO> result = await GetJoinList<SysWardVEntity, SysDepartmentWardRelationEntity, SysDepartmentVEntity>((a, b, c) => new JoinQueryInfos(
            //JoinType.Left, a.bqCode == b.bqCode,
            //JoinType.Left, b.DepartmentId == c.Id
            //), 
            //(a, b, c) => new 
            //{ 
            //    bq = a.bqCode,
            //    bqmc = a.bqmc,
            //    ks = c.Code,
            //    ksmc = c.Name
            //},
            //true, (a, b) => a.OrganizeId == orgId &&(b.bqCode == "" || b.bqCode == ks) && (a.bqmc == "" || a.bqmc == keyword), 
            //false,
            //null);
            //return result;
        }
    }
}
