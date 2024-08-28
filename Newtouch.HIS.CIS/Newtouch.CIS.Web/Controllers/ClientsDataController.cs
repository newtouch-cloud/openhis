using Newtouch.Tools;
using System.Web.Mvc;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Common.Operator;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using System.Collections.Generic;
using System;
using Newtouch.Domain.IRepository;

namespace Newtouch.CIS.Web.Controllers
{
    /// <summary>
    /// Home/Index加载时 默认加载的 缓存数据
    /// </summary>
    public class ClientsDataController : FrameworkBase.MultiOrg.Web.Controllers.ClientsDataController
    {
        private readonly IBaseDataDmnService _baseDataDmnService;

        private readonly ISysUserDmnService _sysUserDmnService;
        /// <summary>
        /// 民族
        /// </summary>
        private readonly ISysNationRepo _sysNationRepo;
        /// <summary>
        /// 国籍
        /// </summary>
        private readonly ISysNationalityRepo _sysNationalityRepo;

        private readonly ISysDepartmentRepo _sysDepartmentRepo;

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
            var data = new
            {
                itemDetails = this.GetItemDetailsList(),
                enums = this.GetEnumList(typeof(Infrastructure.Constants).Namespace),
                ypjxyfdy = _baseDataDmnService.GetMediFormlUsageList(),
                ypyf = _baseDataDmnService.GetMediUsageList(),
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAsyncClientsDataJson()
        {
            var opr = OperatorProvider.GetCurrent();
            object sysStaffDutyList = null, doctorInHosBookkeep = null, sysPatientNatureList = null,sysDepartList = null;
            if (!string.IsNullOrWhiteSpace(opr.OrganizeId))
            {
                sysStaffDutyList = _sysUserDmnService.GetStaffDutyListByOrganizeId(opr.OrganizeId);//（医生，健康教练）
                doctorInHosBookkeep = _sysUserDmnService.GetStaffByDutyCode(opr.OrganizeId, "Doctor");//住院记账获取门诊医生
                sysDepartList = _sysDepartmentRepo.GetList(opr.OrganizeId, "1");//科室
                sysPatientNatureList = _sysNationalityRepo.GetBRXZList(opr.OrganizeId);//病人性质,报销政策
            }

            var data = new
            {
                sysDepartList = sysDepartList == null ? null : sysDepartList.ToJson(new[] { "Name", "Code", "yjbz", "py", "zlks" }),    //科室
                sysPatientNatureList = sysPatientNatureList,
                sysStaffDutyList = sysStaffDutyList == null ? null : sysStaffDutyList.ToJson(new[] { "StaffGh", "StaffName", "StaffPY", "DutyCode" }),//健康教练，医生
                sysNationList = _sysNationRepo.GetmzList(),//民族
                SysForCashPayList = _sysNationalityRepo.GetCashPay(), //获取现金支付方式
                sysNationalityList = _sysNationalityRepo.GetgjList()//国籍
        };
            return Content(data.ToJson());
        }

    }
}
