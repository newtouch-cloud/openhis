using Newtouch.Common.Operator;
using Newtouch.Core.Common.Exceptions;
using System.Web.Mvc;

namespace FrameworkBase.MultiOrg.Web
{
    /// <summary>
    /// Controller基类
    /// </summary>
    public abstract class OrgControllerBase : BaseController
    {
        /// <summary>
        /// 组织机构Id
        /// </summary>
        protected string OrganizeId
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
        protected string GetAuthOrganizeId(bool throwFailedException = true)
        {
            var opr = this.UserIdentity;
            string orgId = null;
            if (opr.IsAdministrator || opr.IsRoot)
            {
                //如果是系统管理员 则让其拉所有组织机构的数据
                orgId = Infrastructure.ConstantsBase.TopOrganizeId; 
            }
            else
            {
                //默认用当前关联用户的OrganizeId
                orgId = opr.OrganizeId;   
            }
            if (throwFailedException && string.IsNullOrWhiteSpace(orgId))
            {
                throw new FailedException("定位当前权限内的组织机构失败");
            }
            return orgId;
        }

    }

}
