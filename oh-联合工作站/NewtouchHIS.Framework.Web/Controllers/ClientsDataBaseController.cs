using Mapster;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.Entity.SysManage;
using NewtouchHIS.Base.Domain.EnumExtend;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Framework.Web.ServiceModels;
using NewtouchHIS.Lib.Base.EnumExtend;
using NewtouchHIS.Lib.Base.Extension;
using SqlSugar;
using System.Reflection;
using static NewtouchHIS.Lib.Base.Extension.EnumExtensions;

namespace NewtouchHIS.Framework.Web.Controllers
{
    public abstract class ClientsDataBaseController : OrgControllerBase
    {
        private readonly ISysConfigDmnService _sysConfigDmn;
        private readonly ISystemAppService _systemApp;

        protected ClientsDataBaseController(ISystemAppService systemApp, ISysConfigDmnService sysConfigDmn)
        {
            _systemApp = systemApp;
            _sysConfigDmn = sysConfigDmn;
        }


        /// <summary>
        /// 字典 分类仅有效的，字典项包括无效的（可用于显示成select）
        /// {Type:"OrganizeType",Items:[{Code:"001",Name:"集团",zt:"1"},{Code:"002",Name:"区域",zt:"1"}]}
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public virtual object GetItemDetailsList()
        {
            //var result = _itemDmnService.GetValidItemTypeList().Select(k => new
            //{
            //    Type = k.Code,
            //    Items = itemdata.Where(t => t.ItemId.Equals(k.Id)).OrderByDescending(p => p.zt == "1")
            //        .ThenBy(p => p.px)
            //        .Select(p => new { Name = p.Name, Code = p.Code, zt = p.zt })
            //        .ToList(),
            //}).ToList();

            return null;
        }

        /// <summary>
        /// 获取三方目录对照信息
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public virtual object GetCataloguesComparisonList()
        {
            //if (string.IsNullOrWhiteSpace(this.OrganizeId))
            //{
            //    return null;
            //}
            //var _ttCataloguesComparisonDmnService = DependencyDefaultInstanceResolver.GetInstance<ITTCataloguesComparisonDmnService>();
            //var itemdata = _ttCataloguesComparisonDmnService.GetDetailListByOrgId(this.OrganizeId).ToList();

            //var result = _ttCataloguesComparisonDmnService.GetValidMainList(this.OrganizeId).Select(k => new
            //{
            //    Code = k.Code,
            //    TTCode = k.TTCode,
            //    TTMark = k.TTMark,
            //    Items = itemdata.Where(t => t.MainId.Equals(k.Id)).OrderByDescending(p => p.zt == "1")
            //        .ThenBy(p => p.px)
            //        .Select(p => new { Code = p.Code, Name = p.Name, TTCode = p.TTCode, TTName = p.TTName, TTExplain = p.TTExplain })
            //        .ToList(),
            //}).ToList();

            //return result;
            return null;
        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public async virtual Task<List<SysModuleWithChildVO>> GetMenuList()
        {
            var menuList = await _sysConfigDmn.GetMenuList(OrganizeId, UserIdentity?.RoleIdList?.ToList(), UserIdentity.IsRoot, UserIdentity.IsAdministrator);
            return ToMenuJson(menuList.Adapt<List<SysModuleVO>>(), null);
            //return await _systemApp.GetMenuExtendAsync(new MenuAuthRequest
            //{
            //    RoleIdList = UserIdentity?.RoleIdList?.ToList(),
            //    IsAdministrator = UserIdentity.IsAdministrator,
            //    IsRoot = UserIdentity.IsRoot,
            //    UserId = UserIdentity.UserId,
            //}, GetUserCacheKey, UserIdentity);
        }

        /// <summary>
        /// 获取系统所有定义的枚举
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public async virtual Task<object> GetEnumList(params string[] namespaceList)
        {
            return await Task.Run(() =>
            {
                List<Assembly> asmList = new List<Assembly>();
                if (namespaceList != null)
                {
                    foreach (var namespaceItem in namespaceList)
                    {
                        asmList.Add(Assembly.Load(namespaceItem));
                    }
                }
                var result = new Dictionary<string, List<EnumItemInfo>>();
                foreach (var asm in asmList)
                {
                    var enmList = asm.GetTypes().Where(p => p.BaseType != null && p.FullName.Contains("EnumExtend") && p.FullName.Contains("ClientEnum")).ToList();
                    foreach (var enm in enmList)
                    {
                        if (enm.IsEnum)
                        {
                            //DbLogType DbLogType_Ex视为同一个枚举
                            var typeName = enm.Name.EndsWith("_Ex") ? enm.Name.Substring(0, enm.Name.Length - 3) : enm.Name;
                            var items = enm.getEnumNameValueDescInfo().ToList();
                            if (!result.ContainsKey(typeName))
                            {
                                result.Add(typeName, items);
                            }
                            else
                            {
                                result[typeName].AddRange(items);
                            }
                        }
                    }
                }
                return result.Select(p => new { Type = p.Key, Items = p.Value });
            });
        }

        /// <summary>
        /// 获取国籍
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public async virtual Task<List<SysNationalityVEntity>> GetsysNationalityList()
        {
            return await _systemApp.GetsysNationalityListAsync("");
        }
        /// <summary>
        /// 获取民族
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public async virtual Task<List<SysNationVEntity>> GetsysNationList()
        {
            return await _systemApp.GetsysNationListAsync("");
        }
        ///// <summary>
        ///// Sett获取字典缓存
        ///// </summary>
        ///// <returns></returns>
        //[NonAction]
        //public virtual object GetSettDictionaryList(string orgId)
        //{
        //    var yearArr = new List<int>();
        //    var monthArr = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        //    var yearMin = DateTime.Now.Year - 5;
        //    var yearMax = DateTime.Now.Year + 1;
        //    for (var i = yearMin; i <= yearMax; i++)
        //    {
        //        yearArr.Add(i);
        //    }
        //    var data = new
        //    {
        //        sysPatientNatureList = _organizeApp.GetsysatientNatureListAsync(orgId),//病人性质,报销政策
        //        SysForCashPayList = _systemApp.GetSysForCashPayAsync(""),//获取现金支付方式
        //        sysStaffDutyList = _organizeApp.GetsysStaffDutyListAsync(orgId) == null ? null : _organizeApp.GetsysStaffDutyListAsync(orgId).ToJson(new[] { "StaffGh", "StaffName", "StaffPY", "DutyCode" }),//健康教练，医生
        //        sysDepartList = _organizeApp.GetsysDepartListAsync(orgId) == null ? null : _organizeApp.GetsysDepartListAsync(orgId).ToJson(new[] { "Name", "Code", "yjbz", "py", "zlks" }),    //科室
        //        sysPatiAreaList = _organizeApp.GetsysPatiAreaListAsync(orgId) == null ? null : _organizeApp.GetsysPatiAreaListAsync(orgId).ToJson(new[] { "bqCode", "bqmc", "py" }),  //病区
        //        sysWardDeptRelation = _organizeApp.GetsysWardDeptRelationAsync(orgId) == null ? null : _organizeApp.GetsysWardDeptRelationAsync(orgId).ToJson(),  //病区
        //        sysNationalityList = _systemApp.GetsysNationalityListAsync("") == null ? null : _systemApp.GetsysNationalityListAsync("").ToJson(new[] { "gjCode", "gjmc", "py" }),   //国籍
        //        sysNationList = _systemApp.GetsysNationListAsync("") == null ? null : _systemApp.GetsysNationListAsync("").ToJson(new string[] { "mzCode", "mzmc", "py" }),//民族
        //        sysMajorClassList = _organizeApp.GetsysMajorClassListAsync(orgId) == null ? null : _organizeApp.GetsysMajorClassListAsync(orgId).ToJson(new string[] { "dlCode", "dlmc", "py" }),//大类
        //        doctorInHosBookkeep = _organizeApp.GetStaffByDutyCodeAsync(orgId),
        //        commercialInsuranceList = "",//商保（暂无）
        //        yearArr = yearArr.ToArray(),
        //        monthArr = monthArr.ToArray(),
        //        SysFailedCodeMessageMapList = _organizeApp.GetSysFailedCodeMessageMapListAsync(orgId),
        //        itemDetails = _systemApp.GetItemDetailsListAsync(""),
        //    };
        //    return data;
        //}
        ///// <summary>
        ///// CIS获取字典缓存
        ///// </summary>
        ///// <returns></returns>
        //[NonAction]
        //public virtual object GetCISDictionaryList(string orgId)
        //{
        //    var data = new
        //    {
        //        sysPatientNatureList = _organizeApp.GetsysatientNatureListAsync(orgId),//病人性质,报销政策                sysStaffDutyList = _organizeApp.GetsysStaffDutyListAsync(orgId) == null ? null : _organizeApp.GetsysStaffDutyListAsync(orgId).ToJson(new[] { "StaffGh", "StaffName", "StaffPY", "DutyCode" }),//健康教练，医生
        //        sysNationalityList = _systemApp.GetsysNationalityListAsync("") == null ? null : _systemApp.GetsysNationalityListAsync("").ToJson(new[] { "gjCode", "gjmc", "py" }),   //国籍
        //        sysNationList = _systemApp.GetsysNationListAsync("") == null ? null : _systemApp.GetsysNationListAsync("").ToJson(new string[] { "mzCode", "mzmc", "py" }),//民族
        //        doctorInHosBookkeep = _organizeApp.GetStaffByDutyCodeAsync(orgId),
        //    };
        //    return data;
        //}
        ///// <summary>
        ///// PDS获取字典缓存
        ///// </summary>
        ///// <returns></returns>
        //[NonAction]
        //public virtual object GetPDSDictionaryList(string orgId)
        //{

        //    var data = new
        //    {
        //        sysDepartList = _organizeApp.GetsysDepartListAsync(orgId) == null ? null : _organizeApp.GetsysDepartListAsync(orgId).ToJson(new[] { "Name", "Code", "yjbz", "py", "zlks" }),    //科室
        //        SysFailedCodeMessageMapList = _organizeApp.GetSysFailedCodeMessageMapListAsync(orgId),
        //        dataItems = _systemApp.GetItemDetailsListAsync(""),
        //    };
        //    return data;
        //}

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
