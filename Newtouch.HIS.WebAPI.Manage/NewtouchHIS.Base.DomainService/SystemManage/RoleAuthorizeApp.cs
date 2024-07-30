using Mapster;
using Microsoft.Data.SqlClient;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.Entity.SysManage;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.EnumExtend;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.DataBaseSvr;
using SqlSugar;
using System.Data.Common;

namespace NewtouchHIS.Base.DomainService
{
    public class RoleAuthorizeApp : BaseDmnService<SysRoleAuthorizeEntity>, IRoleAuthorizeApp
    {
        public RoleAuthorizeApp()
        {
           
        }
        /// <summary>
        /// 获取角色授权列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<List<SysRoleAuthorizeEntity>> GetValidList(string roleId)
        {
            var data = await GetByWhere<SysRoleAuthorizeEntity>(DBEnum.UnionDb.ToString(), p => p.zt == "1" && p.RoleId == roleId);
            return data;
        }

    }
}
