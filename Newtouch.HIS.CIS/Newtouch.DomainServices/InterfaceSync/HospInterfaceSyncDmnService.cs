using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Domain.DTO.InterfaceSync;
using Newtouch.Domain.IDomainServices.InterfaceSync;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Newtouch.Common.Web.APIRequestHelper;

namespace Newtouch.DomainServices
{
    public class HospInterfaceSyncDmnService : DmnServiceBase, IHospInterfaceSyncDmnService
    {
        /// <summary>
        /// 替换接口api 后台接口 SiteBaseAPIHelper
        /// </summary>
        /// <param name="databaseFactory"></param>
        public HospInterfaceSyncDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        #region 后台接口 SiteBaseAPIHelper
        /// <summary>
        /// 更新床位是否占用
        /// </summary>
        /// <param name="cwcode"></param>
        /// <param name="bq"></param>
        /// <param name="sfzy"></param>
        /// <param name="orgId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResponseBase UpdateOccupyByCode(string cwcode, string bq, bool sfzy, string orgId,string user)
        {
            ResponseBase resp = new ResponseBase();
            try
            {
                string val = "0";
                if (sfzy)
                {
                    val = "1";
                }
                string sql = @"update a set a.sfzy=@sfzy,a.LastModifyTime=getdate(),a.LastModifierCode=@user 
from NewtouchHIS_Base.dbo. xt_cw  a
where a.OrganizeId=@orgId and a.zt='1' and a.bqCode=@bq and a.cwCode=@cwcode and isnull(sfzy,'0')<>@sfzy

select @@ROWCOUNT as [rowcount] ";
                resp = FindList<ResponseBase>(sql, new SqlParameter[] {
                    new SqlParameter("orgId",orgId),
                    new SqlParameter("bq",bq),
                    new SqlParameter("sfzy",val),
                    new SqlParameter("user",user),
                    new SqlParameter("cwcode",cwcode)
                }).FirstOrDefault();
                if (resp != null && resp.rowcount > 0)
                {
                    resp.code = ResponseResultCode.SUCCESS;
                }
                else
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.msg = "床位更新失败";
                }
            }
            catch (Exception ex)
            {
                resp.code = ResponseResultCode.ERROR;
                resp.msg = ex.Message;
            }
            return resp;
        }

        #endregion


    }

}
