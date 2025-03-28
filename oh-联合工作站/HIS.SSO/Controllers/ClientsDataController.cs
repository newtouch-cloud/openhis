using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;

namespace HIS.SSO.Controllers
{
    [HandlerLogin]
    public class ClientsDataController : ClientsDataBaseController
    {
        private readonly ISystemAppService _systemApp;
        private readonly IOrganizeAppService _organizeApp;
        public ClientsDataController(ISystemAppService systemApp, ISysConfigDmnService sysConfigDmn, IOrganizeAppService organizeAppService) : base(systemApp, sysConfigDmn)
        {
            _systemApp = systemApp;
            _organizeApp = organizeAppService;

        }
        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<ActionResult> GetModuleDataJson()
        {
            var data = new
            {
                authorizeMenu = await this.GetMenuList(),
            };
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<ActionResult> GetClientsDataJson()
        {
            var yearArr = new List<int>();
            var monthArr = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            var yearMin = DateTime.Now.Year - 5;
            var yearMax = DateTime.Now.Year + 1;
            for (var i = yearMin; i <= yearMax; i++)
            {
                yearArr.Add(i);
            }
            var data = new
            {
                sysMajorClassList = await _organizeApp.GetsysMajorClassListAsync(OrganizeId),//大类
                doctorInHosBookkeep = await _organizeApp.GetStaffByDutyCodeAsync(OrganizeId),
                commercialInsuranceList = "",//商保（暂无）
                yearArr = yearArr.ToArray(),
                monthArr = monthArr.ToArray(),
                SysFailedCodeMessageMapList = await _organizeApp.GetSysFailedCodeMessageMapListAsync(OrganizeId),
            };
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<ActionResult> GetAsyncClientsDataJson()
        {
            object sysDepartList = null;
            sysDepartList = await _organizeApp.GetsysDepartListAsync(OrganizeId);
            var data = new
            {
                itemDetails = await _systemApp.GetItemDetailsListAsync(""),
                sysNationalityList = await _systemApp.GetsysNationalityListAsync(""),   //国籍
                sysNationList = await _systemApp.GetsysNationListAsync(""),//民族
                sysPatientNatureList = await _organizeApp.GetsysatientNatureListAsync(OrganizeId),//病人性质,报销政策
                SysForCashPayList = await _systemApp.GetSysForCashPayAsync(""),//获取现金支付方式
                sysStaffDutyList = await _organizeApp.GetsysStaffDutyListAsync(OrganizeId),//健康教练，医生
                sysDepartList = sysDepartList.ToJson(new[] { "Name", "Code", "yjbz", "py", "zlks", "mzzybz" }),    //科室
                sysPatiAreaList = await _organizeApp.GetsysPatiAreaListAsync(OrganizeId),  //病区
                sysWardDeptRelation = await _organizeApp.GetsysWardDeptRelationAsync(OrganizeId),  //病区
                ypjxyfdy = await _systemApp.GetDrugFormulationUsageListAsync(),
                ypyf = await _systemApp.GetMediUsageListAsync(),
                enums = await GetEnumList("NewtouchHIS.Base.Domain", "HIS.SSO.Domain"),

            };
            return Content(data.ToJson());
        }
    }
}
