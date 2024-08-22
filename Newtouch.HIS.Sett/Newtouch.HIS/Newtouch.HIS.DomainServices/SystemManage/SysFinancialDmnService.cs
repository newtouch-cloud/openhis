using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace Newtouch.HIS.DomainServices
{
    public class SysFinancialDmnService : DmnServiceBase, ISysFinancialDmnService
    {

        public SysFinancialDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public IList<FinanceReceiptVO> GetFinancialInvoiceList(string keyValue, string OrganizeId)
        {
            var sqlStr3 = new StringBuilder();

            sqlStr3.Append(@"SELECT  a.cwsjId ,
                        a.szm ,
                        a.qssjh ,
                        a.dqsjh ,
                        a.jssjh ,
                        staff.Name ry
                FROM    dbo.cw_sj a
                        LEFT JOIN NewtouchHIS_Base..V_C_Sys_UserStaff staff ON staff.Account = a.ry
                        AND a.OrganizeId = staff.OrganizeId
                        WHERE   a.zt = '1'
                        AND a.OrganizeId = @orgId
                        AND ((a.ry = @Id or @Id='') or (staff.Name = @Id or @Id=''))");                    

            return this.FindList<FinanceReceiptVO>(sqlStr3.ToString(), new[] {
                        new SqlParameter("@orgId", OrganizeId),
                        new SqlParameter("@Id", keyValue??"")
                }); ;
        }

        public IList<FinanceInvoiceVO> GetCwfpList(string keyValue, string OrganizeId)
        {
            var sqlStr3 = new StringBuilder();

            sqlStr3.Append(@"SELECT  a.fpdm ,
                        a.szm ,
                        a.lyry ,
                        a.dqfph ,
                        a.qsfph ,a.jsfph,a.zt,a.CreateTime,a.CreatorCode,
                        staff.Name ry
                FROM    dbo.cw_fp a
                        LEFT JOIN NewtouchHIS_Base..V_C_Sys_UserStaff staff ON staff.Account = a.lyry
                        AND a.OrganizeId = staff.OrganizeId
                        WHERE   a.zt = '1'
                        AND a.OrganizeId = @orgId
                        AND ((a.lyry = @Id or @Id='') or (staff.Name = @Id or @Id=''))");

            return this.FindList<FinanceInvoiceVO>(sqlStr3.ToString(), new[] {
                        new SqlParameter("@orgId", OrganizeId),
                        new SqlParameter("@Id", keyValue??"")
                }); ;
        }
    }
}
