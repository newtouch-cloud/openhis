using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Domain.Api;
using Newtouch.Domain.IDomainServices.Inpatient;
using Newtouch.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.DomainServices.Inpatient
{
    public class DeptMedicineApplyNoDmnService : DmnServiceBase, IDeptMedicineApplyNoDmnService
    {
        public DeptMedicineApplyNoDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
        #region 科室备药
        /// <summary>
        /// 查询备药申请单状态
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="sqdArr"></param>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        public List<DeptApplyNoResp> SelectApplyNoStatus(string orgId, string sqdArr, string UserCode)
        {

            const string sql = @"
select sqzt,djh sqd from Newtouch_CIS.[dbo].zy_bqksby 
where  organizeid=@orgId and zt=1 and djh in ( select col from f_split(@sqdArr,',') )
";
            var param = new DbParameter[]
            {
                new SqlParameter("@sqdArr", sqdArr),
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@UserCode",UserCode)
            };
            var sqdh = this.FindList<DeptApplyNoResp>(sql, param.ToArray());
            return sqdh;
        }

        /// <summary>
        /// 更新申请单状态
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="sqdArr"></param>
        /// <returns></returns>
        public int UpdateApplyNoStatus(string orgId, string sqdArr,string UserCode)
        {

            const string sql = @"
update Newtouch_CIS.[dbo].zy_bqksby set shzt=5 ,lastmodifiercode=@UserCode,lastmodifytime=GETDATE()
where  organizeid=@orgId and zt=1 and djh in ( select col from f_split(@sqdArr,',') )
";
            var param = new DbParameter[]
            {
                new SqlParameter("@sqdArr", sqdArr),
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@UserCode",UserCode)
            };
            var i= this.ExecuteSqlCommand(sql, param.ToArray());
            return i;
        }
        public string SelectKcthApplyNoStatus(string orgId, string sqdArr, string UserCode)
        {

            const string sql = @"
select thzt sqd from Newtouch_CIS.[dbo].[zy_ksbyth] 
where  organizeid=@orgId and zt=1 and djh =@djh
";
            var param = new DbParameter[]
            {
                new SqlParameter("@djh", sqdArr),
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@UserCode",UserCode)
            };
            var sqdh = this.FindList<string>(sql, param.ToArray()).FirstOrDefault();
            return sqdh;
        }

        /// <summary>
        /// 更新退科室库存申请单状态
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="sqdArr"></param>
        /// <returns></returns>
        public int UpdateReturnApplyNoStatus(string orgId, string sqdArr, string UserCode)
        {

            const string sql = @"
update Newtouch_CIS.[dbo].[zy_ksbyth] set shzt=5 ,lastmodifiercode=@UserCode,lastmodifytime=GETDATE()
where  organizeid=@orgId and zt=1 and djh=@sqdArr
";
            var param = new DbParameter[]
            {
                new SqlParameter("@sqdArr", sqdArr),
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@UserCode",UserCode)
            };
            var i = this.ExecuteSqlCommand(sql, param.ToArray());
            return i;
        }

        #endregion
    }
}
