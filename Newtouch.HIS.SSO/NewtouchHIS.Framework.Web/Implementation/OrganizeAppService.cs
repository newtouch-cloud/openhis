using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Services.HttpService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.Entity.SysManage;

namespace NewtouchHIS.Framework.Web.Implementation
{
    /// <summary>
    /// 组织机构基础数据
    /// </summary>
    public interface IOrganizeAppService : ISingletonDependency
    {
        /// <summary>
        /// 机构列表 （TopOrganize）
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="userCacheKey"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<List<SysOrgVo>> OrgListAsync(string keyword);
        /// <summary>
        /// 获取机构名称
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="userCacheKey"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<string?> GetNameByOrgId(string orgId);
        /// <summary>
        /// 人员岗位关联关系
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<List<SysStaffDutyComplexVEntity>> GetsysStaffDutyListAsync(string orgId);
        /// <summary>
        /// 获取病人性质
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<List<SysPatientNatureEntity>> GetsysatientNatureListAsync(string orgId);
        /// <summary>
        /// 获取科室
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<List<SysDepartmentEntity>> GetsysDepartListAsync(string orgId);
        /// <summary>
        /// 获取病区
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<List<SysWardVEntity>> GetsysPatiAreaListAsync(string orgId);
        /// <summary>
        /// 根据科室获取病区
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<List<SysDepartmentWardRelationVO>> GetsysWardDeptRelationAsync(string orgId);
        /// <summary>
        /// 获取大类
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<List<SysChargeCategoryVEntity>> GetsysMajorClassListAsync(string orgId);
        /// <summary>
        /// 住院记账获取医生
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<List<SysDutyStaffVO>> GetStaffByDutyCodeAsync(string orgId);
        /// <summary>
        /// 获取错误配置
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<List<SysFailedCodeMessageMappEntity>> GetSysFailedCodeMessageMapListAsync(string orgId);

    }
    public class OrganizeAppService : AppServiceBase, IOrganizeAppService
    {

        public OrganizeAppService(IHttpClientHelper httpClient, IAuthCenterAppService authCenterApp) : base(httpClient, authCenterApp)
        {
            AppId = ConfigInitHelper.SysConfig.AppAPIHostName?.HisAppBaseAPIHost ?? "HIS.BaseAPI";
            Host = ConfigInitHelper.SysConfig.AppAPIHost?.HisAppBaseAPIHost ?? throw new FailedException(ResponseResultCode.FAILOfConfigInit, "HisAppBaseAPIHost");
        }

        #region Api
        public string OrgListApi => $"{Host}api/organize/OrgList";
        /// <summary>
        /// 获取用户关系表
        /// </summary>
        public string sysStaffDutyListApi => $"{Host}api/organize/GetsysStaffDutyList";
        /// <summary>
        /// 获取病人性质
        /// </summary>
        public string sysPatientNatureListApi => $"{Host}api/organize/GetsysPatientNatureList";
        /// <summary>
        /// 获取科室
        /// </summary>
        public string sysDepartListApi => $"{Host}api/organize/GetsysDepartList";
        /// <summary>
        /// 获取病区
        /// </summary>
        public string sysPatiAreaListApi => $"{Host}api/organize/GetsysPatiAreaList";
        /// <summary>
        /// 根据科室获取病区
        /// </summary>
        public string sysWardDeptRelationApi => $"{Host}api/organize/GetsysWardDeptRelation";

        /// <summary>
        /// 获取大类
        /// </summary>
        public string sysMajorClassApi => $"{Host}api/organize/GetsysMajorClassList";
       /// <summary>
       /// 获取门诊医生
       /// </summary>
        public string StaffByDutyCodeApi => $"{Host}api/organize/GetStaffByDutyCode";
        /// <summary>
        /// 获取错误配置
        /// </summary>
        public string FailedCodeMessageMapApi => $"{Host}api/organize/GetSysFailedCodeMessageMapList";

        #endregion

        public async Task<List<SysOrgVo>> OrgListAsync(string keyword)
        {
            var cacheKey = SystemKey.AssemblyOrgListKey(TopOrganizeId);
            var keyValue = RedisHelper.GetList<SysOrgVo>(cacheKey);
            if (keyValue != null && keyValue.Count > 0)
            {
                return keyValue;
            }
            var orgList = await HttpPostWithToken<List<SysOrgVo>, string>(keyword, OrgListApi,AppId);
            if (orgList.Count > 0)
            {
                RedisHelper.SetList(cacheKey, orgList);
                return orgList;
            }
            return default;
        }
        public async Task<string?> GetNameByOrgId(string orgId)
        {
            var orgList = await HttpPostWithToken<List<SysOrgVo>, string>(orgId, OrgListApi, AppId);
            if (orgList.Count() > 0)
            {
                return orgList.Where(p => p.OrganizeId == orgId).FirstOrDefault()?.Name;
            }
            return null;
        }
        /// <summary>
        /// 获取用户关系表
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public async Task<List<SysStaffDutyComplexVEntity>> GetsysStaffDutyListAsync(string orgId)
        {
            return await HttpPostWithToken<List<SysStaffDutyComplexVEntity>, string>(orgId, sysStaffDutyListApi, AppId);
        }
        /// <summary>
        /// 病人性质表
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public async Task<List<SysPatientNatureEntity>> GetsysatientNatureListAsync(string orgId)
        {
            return await HttpPostWithToken<List<SysPatientNatureEntity>, string>(orgId, sysPatientNatureListApi, AppId);
        }
        /// <summary>
        /// 获取科室
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public async Task<List<SysDepartmentEntity>> GetsysDepartListAsync(string orgId)
        {
            return await HttpPostWithToken<List<SysDepartmentEntity>, string>(orgId, sysDepartListApi, AppId);
        }
        /// <summary>
        /// 获取科室
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public async Task<List<SysWardVEntity>> GetsysPatiAreaListAsync(string orgId)
        {
            return await HttpPostWithToken<List<SysWardVEntity>, string>(orgId, sysPatiAreaListApi, AppId);
        }
        /// <summary>
        ///根据科室获取病区
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public async Task<List<SysDepartmentWardRelationVO>> GetsysWardDeptRelationAsync(string orgId)
        {
            return await HttpPostWithToken<List<SysDepartmentWardRelationVO>, string>(orgId, sysWardDeptRelationApi, AppId);
        }
        /// <summary>
        /// 获取大类
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public async Task<List<SysChargeCategoryVEntity>> GetsysMajorClassListAsync(string orgId)
        {
            return await HttpPostWithToken<List<SysChargeCategoryVEntity>, string>(orgId, sysMajorClassApi, AppId);
        }

        /// <summary>
        /// 获取门诊医生
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public async Task<List<SysDutyStaffVO>> GetStaffByDutyCodeAsync(string orgId)
        {
            return await HttpPostWithToken<List<SysDutyStaffVO>, string>(orgId, StaffByDutyCodeApi, AppId);
        }
        /// <summary>
        /// 获取错误配置
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public async Task<List<SysFailedCodeMessageMappEntity>> GetSysFailedCodeMessageMapListAsync(string orgId)
        {
            return await HttpPostWithToken<List<SysFailedCodeMessageMappEntity>, string>(orgId, FailedCodeMessageMapApi, AppId);
        }
        
    }
}
