using NewtouchHIS.Base.Domain.Model;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Services.HttpService;

namespace NewtouchHIS.Framework.Web.Implementation
{
    public interface ISysUserAppService : IScopedDependency
    {
        /// <summary>
        /// 系统用户列表（分页）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PageResponseRow<List<SysUserVO>>> GetSysUserPageAsync(OLPagination<QueryParamsBase> request);
        /// <summary>
        /// 机构用户树（科室分组）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<BsTreeSelectExtDataModel>> GetOrgUserTreeWithDeptAsync(QueryParamsBase request);
    }
    public class SysUserAppService : AppServiceBase, ISysUserAppService
    {
        public SysUserAppService(IHttpClientHelper httpClient, IAuthCenterAppService authCenterApp) : base(httpClient, authCenterApp)
        {
            AppId = ConfigInitHelper.SysConfig.AppAPIHostName?.HisAppBaseAPIHost ?? "HIS.BaseAPI";
            Host = ConfigInitHelper.SysConfig.AppAPIHost?.HisAppBaseAPIHost ?? throw new FailedException(ResponseResultCode.FAILOfConfigInit, "HisAppBaseAPIHost");
        }
        #region MethodRoute
        public string GetSysUserPageApi => $"{Host}api/sysuser/GetSysUserPage";
        public string GetOrgUserTreeWithDeptApi => $"{Host}api/sysuser/GetOrgUserTreeWithDept";
        #endregion

        public async Task<PageResponseRow<List<SysUserVO>>> GetSysUserPageAsync(OLPagination<QueryParamsBase> request)
        {
            return await HttpPostWithToken<PageResponseRow<List<SysUserVO>>, OLPagination<QueryParamsBase>>(request, GetSysUserPageApi);
        }
        public async Task<List<BsTreeSelectExtDataModel>> GetOrgUserTreeWithDeptAsync(QueryParamsBase request)
        {
            return await HttpPostWithToken<List<BsTreeSelectExtDataModel>, QueryParamsBase>(request, GetOrgUserTreeWithDeptApi);
        }
    }
}
