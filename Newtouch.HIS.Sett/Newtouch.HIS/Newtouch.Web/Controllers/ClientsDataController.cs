using Newtouch.Tools;
using System.Web.Mvc;
using Newtouch.HIS.Application;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Common.Operator;
using Newtouch.HIS.Domain.IRepository.InsuranceManage;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using System.Collections.Generic;
using System;

namespace Newtouch.HIS.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [HandlerLogin]
    public class ClientsDataController : FrameworkBase.MultiOrg.Web.Controllers.ClientsDataController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetModuleDataJson()
        {
            var data = new
            {
                authorizeMenu = this.GetMenuList(),
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetClientsDataJson()
        {
            return null;
        }

        private readonly ISysPatBasicInfoApp _sysPatBasicInfoApp;
        private readonly ISysForCashPayApp _sysForCashPayApp;
        private readonly ISysDepartmentRepo _sysDepartmentRepo;
        private readonly IBookkeepInHosDmnService _BookkeepInHosDmnService;
        private readonly ISysWardRepo _sysWardRepo;
        private readonly ISysChargeCategoryRepo _sysChargeCategoryRepo;
        private readonly ISysNationalityRepo _sysNationalityRepo;
        private readonly ISysNationRepo _sysNationRepo;
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly ICommercialInsuranceRepo _commercialInsuranceRepo;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly ISysFailedCodeMessageMappRepo _sysFailedCodeMessageMappRepo;
        private readonly ICommonDmnService _commonDmnService;

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetAsyncClientsDataJson()
        {
            var opr = OperatorProvider.GetCurrent();
            object sysDepartList = null, sysPatiAreaList = null, sysNationalityList = null;
            object sysNationList = null, sysMajorClassList = null;
            object sysStaffDutyList = null, doctorInHosBookkeep = null, sysPatientNatureList = null;
            object commercialInsuranceList = null;
            object sysWardDeptRelation = null;
            if (!string.IsNullOrWhiteSpace(opr.OrganizeId))
            {
                sysStaffDutyList = _sysUserDmnService.GetStaffDutyListByOrganizeId(opr.OrganizeId);//（医生，健康教练）
                sysDepartList = _sysDepartmentRepo.GetList(opr.OrganizeId, "1");//科室
                sysPatiAreaList = _sysWardRepo.GetbqList(opr.OrganizeId);//病区
                sysMajorClassList = _sysChargeCategoryRepo.GetList(opr.OrganizeId, zt: "1");//大类
                doctorInHosBookkeep = _sysUserDmnService.GetStaffByDutyCode(opr.OrganizeId, "Doctor");//住院记账获取门诊医生
                sysPatientNatureList = _sysPatBasicInfoApp.GetBRXZList(opr.OrganizeId);//病人性质,报销政策
                commercialInsuranceList = _commercialInsuranceRepo.GetList(opr.OrganizeId);//商保list
                sysWardDeptRelation = _commonDmnService.GetWardbyDept(opr.OrganizeId,null,null);
            }
            sysNationalityList = _sysNationalityRepo.GetgjList();//国籍
            sysNationList = _sysNationRepo.GetmzList();//民族

            //
            //年月
            var yearArr = new List<int>();
            var monthArr = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            var yearMin = DateTime.Now.Year - 5;
            var yearMax = DateTime.Now.Year + 1;
            var configVal = _sysConfigRepo.GetValueByCode("Hosp_Year_Start", opr.OrganizeId);
            if (!int.TryParse(configVal, out yearMin))
            {
                yearMin = DateTime.Now.Year - 5;
            }
            for (var i = yearMin; i <= yearMax; i++)
            {
                yearArr.Add(i);
            }
         
               var data = new
                {
                    sysPatientNatureList = sysPatientNatureList,
                    SysForCashPayList = _sysForCashPayApp.GetCashPay(), //获取现金支付方式
                    sysStaffDutyList = sysStaffDutyList == null ? null : sysStaffDutyList.ToJson(new[] { "StaffGh", "StaffName", "StaffPY", "DutyCode" }),//健康教练，医生
                    sysDepartList = sysDepartList == null ? null : sysDepartList.ToJson(new[] { "Name", "Code", "yjbz", "py" }),    //科室
                    sysPatiAreaList = sysPatiAreaList == null ? null : sysPatiAreaList.ToJson(new[] { "bqCode", "bqmc", "py" }),  //病区
                   sysWardDeptRelation = sysWardDeptRelation == null ? null : sysWardDeptRelation.ToJson(),  //病区
                   sysNationalityList = sysNationalityList == null ? null : sysNationalityList.ToJson(new[] { "gjCode", "gjmc", "py" }),   //国籍
                    sysNationList = sysNationList == null ? null : sysNationList.ToJson(new string[] { "mzCode", "mzmc", "py" }),//民族
                    sysMajorClassList = sysMajorClassList == null ? null : sysMajorClassList.ToJson(new string[] { "dlCode", "dlmc", "py" }),//大类
                    doctorInHosBookkeep = doctorInHosBookkeep,
                    commercialInsuranceList = commercialInsuranceList,//?.ToJson(new[] { "bxCode", "Name", "EnglishName" }),//商保
                    itemDetails = this.GetItemDetailsList(),
                    enums = this.GetEnumList(typeof(Infrastructure.Constants).Namespace),
                    yearArr = yearArr.ToArray(),
                    monthArr = monthArr.ToArray(),
                    //
                    SysFailedCodeMessageMapList = _sysFailedCodeMessageMappRepo.GetListByOrgId(opr.OrganizeId),
                    CataloguesComparisonList = this.GetCataloguesComparisonList(),
                };
           


            return Content(data.ToJson());
        }

    }
}
