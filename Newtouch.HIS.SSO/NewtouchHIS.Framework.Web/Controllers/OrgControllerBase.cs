using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;

namespace NewtouchHIS.Framework.Web.Controllers
{
    /// <summary>
    /// 多机构 Controller基类
    /// </summary>
    public abstract class OrgControllerBase : BaseController
    {
        protected OrgControllerBase()
        {
        }


        /// <summary>
        /// 组织机构Id
        /// </summary>
        protected string? OrganizeId
        {
            get
            {
                if (this.UserIdentity == null)
                {
                    return null;
                }
                return this.UserIdentity.OrganizeId;
            }
        }


        /// <summary>
        /// 获取权限Org
        /// </summary>
        /// <param name="throwFailedException"></param>
        /// <returns></returns>
        [NonAction]
        protected string? GetAuthOrganizeId(bool throwFailedException = true)
        {
            if (UserIdentity == null)
            {
                return null;
            }
            string? orgId;
            if (UserIdentity.IsAdministrator || UserIdentity.IsRoot)
            {
                //如果是系统管理员 则让其拉所有组织机构的数据
                orgId = ConfigInitHelper.SysConfig?.Top_OrganizeId;
            }
            else
            {
                //默认用当前关联用户的OrganizeId
                orgId = UserIdentity.OrganizeId;
            }
            if (throwFailedException && string.IsNullOrWhiteSpace(orgId))
            {
                throw new FailedException("定位当前权限内的组织机构失败");
            }
            return orgId;
        }

        #region token
        protected string GetUserCacheKey => SystemKey.AssemblyUserTokenKey(UserIdentity.UserCode, OrganizeId);




        #endregion


    }
}
