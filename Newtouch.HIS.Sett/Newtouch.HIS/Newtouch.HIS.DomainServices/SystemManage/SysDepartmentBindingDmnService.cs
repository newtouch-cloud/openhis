using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices.SystemManage;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using System.Data.SqlClient;

namespace Newtouch.HIS.DomainServices.SystemManage
{
    public class SysDepartmentBindingDmnService : DmnServiceBase, ISysDepartmentBindingDmnService
    {
        public SysDepartmentBindingDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }


        public SysDepartmentBindingVO GetSaffEntity(string keyValue, string OrganizeId)
        {
            var sql = @"select gender as xb,
        zjlx,zjh 
        from [NewtouchHIS_Base].[dbo].[Sys_Staff](nolock) where zt = '1'
        and gh=@gh and organizeid=@OrganizeId";
            SqlParameter[] para = {
                new SqlParameter("@gh",keyValue),
                new SqlParameter("@OrganizeId",OrganizeId)
            };
            return this.FirstOrDefault<SysDepartmentBindingVO>(sql, para);
        }
    }
}
