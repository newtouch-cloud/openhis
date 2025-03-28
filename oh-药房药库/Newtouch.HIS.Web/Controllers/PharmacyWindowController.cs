using FrameworkBase.MultiOrg.Domain.IDomainServices;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage;
using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Controllers
{
    public class PharmacyWindowController : ControllerBase
    {
        //private readonly IPharmacyDmnService _PharmacyDmnService;
        private readonly ISysPharmacyWindowDmnService _SysPharmacyWindowDmnService;
        private readonly ISysOrganizeDmnService _SysOrganizeDmnService;
        private readonly ISysPharmacyDepartmentBaseDmnService _SysPharmacyDepartmentBaseDmnService;

        public PharmacyWindowController(
            //IPharmacyDmnService PharmacyDmnService,
          ISysPharmacyWindowDmnService SysPharmacyWindowDmnService,
          ISysOrganizeDmnService SysOrganizeDmnService,
          ISysPharmacyDepartmentBaseDmnService _SysPharmacyDepartmentBaseDmnService
          )
        {
            //this._PharmacyDmnService = PharmacyDmnService;
            this._SysPharmacyWindowDmnService = SysPharmacyWindowDmnService;
            this._SysOrganizeDmnService = SysOrganizeDmnService;
            this._SysPharmacyDepartmentBaseDmnService = _SysPharmacyDepartmentBaseDmnService;
        }

        /// <summary>
        /// 查询药房窗口信息
        /// </summary>
        /// <param name="pagintion"></param>
        /// <param name="yfckCode"></param>
        /// <param name="yfckmc"></param>
        /// <returns></returns>
        public ActionResult GetWindowsInformation(Pagination pagintion, string keyword, string organizeId)
        {
            pagintion.sidx = "CreateTime desc";
            pagintion.sord = "asc";
            var data = new
            {
                rows = _SysPharmacyWindowDmnService.GetPagintionList(pagintion, organizeId, keyword),
                total = pagintion.total,
                page = pagintion.page,
                records = pagintion.records,
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 修改窗口信息
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(int? keyValue)
        {
            var data = _SysPharmacyWindowDmnService.GetFormJson(this.OrganizeId, keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult submitForm(SysPharmacyWindowVO entity, int? keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            if (string.IsNullOrWhiteSpace(entity.OrganizeId))
            {
                throw new FailedException("请选择组织机构");
            }
            else if (!_SysOrganizeDmnService.IsMedicalOrganize(entity.OrganizeId))
            {
                throw new FailedException("请选择医疗机构（医院或诊所）");
            }
            _SysPharmacyWindowDmnService.submitForm(entity, keyValue);
            return Success("操作成功");
        }

        /// <summary>
        /// 根据organizeId获取药房部门
        /// </summary>
        /// <returns></returns>
        public ActionResult PharmacyDepartmentList(string organizeId, byte? yjbmjb)
        {
            var list = _SysPharmacyDepartmentBaseDmnService.GetList(organizeId, yjbmjb);
            return Content(list.ToJson());
        }
    }
}