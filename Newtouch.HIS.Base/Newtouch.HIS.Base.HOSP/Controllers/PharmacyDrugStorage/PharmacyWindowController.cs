using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.HIS.Base.HOSP.Controllers
{
    /// <summary>
    /// 药房窗口
    /// </summary>
    public class PharmacyWindowController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IPharmacyDmnService _PharmacyDmnService;
        private readonly ISysPharmacyWindowRepo _SysPharmacyWindowRepo;
        private readonly ISysOrganizeDmnService _SysOrganizeDmnService;
        private readonly ISysPharmacyDepartmentRepo _sysPharmacyDepartmentRepo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PharmacyDmnService"></param>
        public PharmacyWindowController(IPharmacyDmnService PharmacyDmnService,
            ISysPharmacyWindowRepo SysPharmacyWindowRepo,
            ISysOrganizeDmnService SysOrganizeDmnService
            , ISysPharmacyDepartmentRepo sysPharmacyDepartmentRepo
            , ISysChargeCategoryRepo SysChargeCategoryRepo)
        {
            this._PharmacyDmnService = PharmacyDmnService;
            this._SysPharmacyWindowRepo = SysPharmacyWindowRepo;
            this._SysOrganizeDmnService = SysOrganizeDmnService;
            this._sysPharmacyDepartmentRepo = sysPharmacyDepartmentRepo;
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
                rows = _PharmacyDmnService.GetPagintionList(pagintion, organizeId, keyword),
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
        public ActionResult GetFormJson(int ? keyValue)
        {
            var data = _SysPharmacyWindowRepo.FindEntity(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult submitForm(SysPharmacyWindowEntity entity, int? keyValue)
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
            _SysPharmacyWindowRepo.submitForm(entity, keyValue);
            return Success("操作成功");
        }

        /// <summary>
        /// 根据organizeId获取药房部门
        /// </summary>
        /// <returns></returns>
        public ActionResult PharmacyDepartmentList(string organizeId ,byte? yjbmjb)
        {
            var list = _sysPharmacyDepartmentRepo.GetList(organizeId,yjbmjb);        
            return Content(list.ToJson()); 
        }       
    }
}