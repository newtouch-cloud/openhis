using Mapster;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.Entity.SysManage;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Base.Domain.ValueObjects.SysManage;
using NewtouchHIS.Framework.Web.ServiceModels;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Services.HttpService;
using SqlSugar;

namespace NewtouchHIS.Framework.Web.Implementation
{
    /// <summary>
    /// 系统基础功能及字典服务
    /// </summary>
    public interface ISystemAppService : IScopedDependency
    {
        /// <summary>
        /// 当前登录用户菜单
        /// </summary>
        /// <returns></returns>
        Task<List<SysModuleVO>?> GetUserMenuAsync(bool? validLimit = true);
        /// <summary>
        /// 获取系统菜单
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userCacheKey"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<List<SysModuleVO>> GetMenuListAsync(MenuAuthRequest request, string userCacheKey, OperatorModel? user = null);
        /// <summary>
        /// 菜单树
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userCacheKey"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<List<SysModuleWithChildVO>> GetMenuExtendAsync(MenuAuthRequest request, string userCacheKey, OperatorModel? user = null);
        /// <summary>
        /// 菜单详情查询
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        Task<SysModuleEntity> GetMenubyIdAsync(string keyValue);

        /// <summary>
        /// 获取国籍
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<SysNationalityVEntity>> GetsysNationalityListAsync(string request);
        /// <summary>
        /// 获取民族
        /// </summary>
        /// <param name="request"></param>
        Task<List<SysNationVEntity>> GetsysNationListAsync(string request);
        /// <summary>
        /// 获取现金支付方式
        /// </summary>
        /// <param name="request"></param>
        Task<List<SysCashPaymentModelEntity>> GetSysForCashPayAsync(string request);
        Task<List<SysItemExtendVO>> GetItemDetailsListAsync(string request);
        /// <summary>
        /// 获取已注册APP列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<AuthAppVO>> GetRegAppListAsync();
        /// <summary>
        /// 获取药品用法
        /// </summary>
        /// <returns></returns>
        Task<List<DrugUsageEntity>?> GetMediUsageListAsync();
        /// <summary>
        /// 获取剂型用法关系
        /// </summary>
        /// <returns></returns>
        Task<List<DrugFormulationUsageVO>?> GetDrugFormulationUsageListAsync();
    }
    /// <summary>
    /// 系统通用接口
    /// </summary>
    public class SystemAppService : AppServiceBase, ISystemAppService
    {
        public SystemAppService(IHttpClientHelper httpClient, IAuthCenterAppService authCenterApp) : base(httpClient, authCenterApp)
        {
            AppId = ConfigInitHelper.SysConfig.AppAPIHostName?.HisAppBaseAPIHost ?? "HIS.BaseAPI";
            Host = ConfigInitHelper.SysConfig.AppAPIHost?.HisAppBaseAPIHost ?? throw new FailedException(ResponseResultCode.FAILOfConfigInit, "HisAppBaseAPIHost");
        }
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        public string GetMenuListApi => $"{Host}api/System/GetMenuList";
        /// <summary>
        /// 获取菜单列表，可选数据库
        /// </summary>
        public string GetMenuListbyDBApi => $"{Host}api/System/GetAppMenuList";

        /// <summary>
        /// 系统字典表
        /// </summary>
        public string GetItemDetailsListApi => $"{Host}api/System/GetItemDetailsList";
        /// <summary>
        /// 用户数据初始化
        /// </summary>
        public string BuildLoginStatusOprApi => $"{Host}api/System/BuildLoginStatusOpr";
        /// <summary>
        /// 获取国籍信息
        /// </summary>
        public string GetsysNationalityListApi => $"{Host}api/System/GetsysNationalityList";
        /// <summary>
        /// 获取民族信息
        /// </summary>
        public string GetsysNationListApi => $"{Host}api/System/GetsysNationList";
        /// <summary>
        /// 获取现金支付方式
        /// </summary>
        public string GetSysForCashPayListApi => $"{Host}api/System/GetSysForCashPayList";
        /// <summary>
        /// 获取已注册APP
        /// </summary>
        public string GetRegAppListApi => $"{Host}api/System/GetRegAppList";
        /// <summary>
        /// 药品用法
        /// </summary>
        public string DrugUsageApi => $"{Host}api/System/GetMediUsageList";
        /// <summary>
        /// 获取药品剂型用法对照关系
        /// </summary>
        public string DrugFormulationUsageListApi => $"{Host}api/System/GetDrugFormulationUsageList";


        #region Entity操作
        /// <summary>
        /// 菜单信息查询
        /// </summary>
        public string Menu_QuerybyId => $"{Host}api/Systemed/GetMenubyId";

        #endregion


        public async Task<List<SysModuleVO>?> GetUserMenuAsync(bool? validLimit = true)
        {
            var currentUser = OperatorProvider.GetCurrent();
            if (currentUser == null)
            {
                return null;
            }
            var request = new MenuAuthRequest
            {
                UserId = currentUser.UserId,
                IsAdministrator = currentUser.IsAdministrator,
                IsRoot = currentUser.IsRoot,
                RoleIdList = currentUser.RoleIdList,
                ValidLimit = validLimit ?? true
            };
            string userCacheKey = Cache_GetUserTokenKey(currentUser.UserCode, currentUser.OrganizeId, AppId);
            return await HttpPostWithToken<List<SysModuleVO>, MenuAuthRequest>(RequestAsync(request, currentUser?.OrganizeId), GetMenuListbyDBApi, userCacheKey, currentUser);
        }
        public async Task<List<SysModuleVO>> GetMenuListAsync(MenuAuthRequest request, string userCacheKey, OperatorModel? user = null)
        {
            return await HttpPostWithToken<List<SysModuleVO>, MenuAuthRequest>(RequestAsync(request, user?.OrganizeId), GetMenuListbyDBApi, userCacheKey, user);
        }
        public async Task<List<SysModuleWithChildVO>> GetMenuExtendAsync(MenuAuthRequest request, string userCacheKey, OperatorModel? user = null)
        {
            var apiResult = await HttpPostWithToken<List<SysModuleVO>, MenuAuthRequest>(RequestAsync(request, user?.OrganizeId ?? user?.TopOrganizeId), GetMenuListbyDBApi, userCacheKey, user);
            return ToMenuJson(apiResult, null);
        }

        public async Task<SysModuleEntity> GetMenubyIdAsync(string keyValue)
        {
            if (string.IsNullOrWhiteSpace(keyValue))
            {
                return default;
            }
            return await HttpPostWithToken<SysModuleEntity, string>(keyValue, Menu_QuerybyId, AppId);
        }

        public async Task<List<SysItemExtendVO>> GetItemDetailsListAsync(string request)
        {
            return await HttpPostWithToken<List<SysItemExtendVO>, string>(request, GetItemDetailsListApi);
        }
        /// <summary>
        /// 获取国籍
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<SysNationalityVEntity>> GetsysNationalityListAsync(string request)
        {
            List<SysNationalityVEntity> NationalityVEntity = await HttpPostWithToken<List<SysNationalityVEntity>, string>(request, GetsysNationalityListApi);
            return NationalityVEntity;
        }
        /// <summary>
        /// 获取民族
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<SysNationVEntity>> GetsysNationListAsync(string request)
        {
            return await HttpPostWithToken<List<SysNationVEntity>, string>(request, GetsysNationListApi);
        }
        /// <summary>
        /// 获取现金支付方式
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<SysCashPaymentModelEntity>> GetSysForCashPayAsync(string request)
        {
            List<SysCashPaymentModelEntity> sysCashPaymentModelEntities = await HttpPostWithToken<List<SysCashPaymentModelEntity>, string>(request, GetSysForCashPayListApi);
            return sysCashPaymentModelEntities;
        }
        /// <summary>
        /// 获取已注册APP列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<AuthAppVO>> GetRegAppListAsync()
        {
            return await HttpPostWithToken<List<AuthAppVO>, string>(null, GetRegAppListApi);
        }
        /// <summary>
        /// 获取药品用法
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<DrugUsageEntity>?> GetMediUsageListAsync()
        {
            var request = new { code = "", yplx = Convert.ToInt32(null) };
            var resp = await HttpPostAnonymous<List<DrugUsageEntity>>(RequestAsync(request).ToJson(), DrugUsageApi);
            return resp.Data;
        }
        /// <summary>
        /// 获取药品剂型用法
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<DrugFormulationUsageVO>?> GetDrugFormulationUsageListAsync()
        {
            var resp = await HttpPostAnonymous<List<DrugFormulationUsageVO>>(RequestAsync("").ToJson(), DrugFormulationUsageListApi);
            return resp.Data;
        }


        #region private
        private List<SysModuleWithChildVO> ToMenuJson(List<SysModuleVO> data, string? parentId)
        {
            List<SysModuleWithChildVO> result = new List<SysModuleWithChildVO>();
            List<SysModuleVO> list = data?.Where((SysModuleVO t) => t.ParentId == parentId).ToList();

            if (list != null && list.Count > 0)
            {
                foreach (SysModuleVO item in list)
                {
                    SysModuleWithChildVO vo = item.Adapt<SysModuleWithChildVO>();
                    vo.ChildNodes = ToMenuJson(data, item.Id);
                    result.Add(vo);
                }
            }
            return result;
        }
        #endregion
    }
}
