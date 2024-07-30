using Newtouch.Common;
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
    /// 
    /// </summary>
    public class PharmacyDepartmentController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ISysPharmacyDepartmentRepo _SysPharmacyDepartmentRepo;
        private readonly ISysOrganizeDmnService _SysOrganizeDmnService;
        private readonly ISysPharmacyDepartmentOpenMedicineRepo _SysPharmacyDepartmentOpenMedicineRepo;
        private readonly IPharmacyDmnService _PharmacyDmnService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SysPharmacyDepartmentRepo"></param>
        public PharmacyDepartmentController(ISysPharmacyDepartmentRepo SysPharmacyDepartmentRepo
            , ISysPharmacyDepartmentOpenMedicineRepo SysPharmacyDepartmentOpenMedicineRepo
            , ISysOrganizeDmnService SysOrganizeDmnService
            , IPharmacyDmnService PharmacyDmnService
            )
        {
            this._SysPharmacyDepartmentOpenMedicineRepo = SysPharmacyDepartmentOpenMedicineRepo;
            this._SysPharmacyDepartmentRepo = SysPharmacyDepartmentRepo;
            this._SysOrganizeDmnService = SysOrganizeDmnService;
            this._PharmacyDmnService = PharmacyDmnService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public ActionResult GetGridJson(Pagination pagination, string keyword, string organizeId)
        {
            pagination.sidx = "CreateTime desc";
            pagination.sord = "asc";
            var data = new
            {
                rows = _SysPharmacyDepartmentRepo.GetPagintionList(pagination, organizeId, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(int? keyValue)
        {
            var data = _SysPharmacyDepartmentRepo.FindEntity(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult submitForm(SysPharmacyDepartmentEntity entity, int? keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            entity.fybz = entity.fybz == "true" ? "1" : "0";
            entity.ynwbz = entity.ynwbz == "true" ? "1" : "0";
            if (string.IsNullOrWhiteSpace(entity.OrganizeId))
            {
                throw new FailedException("请选择组织机构");
            }
            else if (!_SysOrganizeDmnService.IsMedicalOrganize(entity.OrganizeId))
            {
                throw new FailedException("请选择医疗机构（医院或诊所）");
            }
            _SysPharmacyDepartmentRepo.submitForm(entity, keyValue);
            return Success("操作成功");
        }

        #region xt_yfbm_yp维护

        /// <summary>
        /// 主页面 
        /// </summary>
        /// <returns></returns>
        public ActionResult OpenMedicineIndex()
        {
            return View();

        }

        /// <summary>
        /// 更新添加页面
        /// </summary>
        /// <returns></returns>
        public ActionResult OpenMadicineForm()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public ActionResult GetOpenMedicineGridJson(string keyword, string organizeId)
        {
            var data = _PharmacyDmnService.OpenMedicineIndex(organizeId, keyword);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetOpenMedicineFormJson(string keyValue)
        {
            var data = _SysPharmacyDepartmentOpenMedicineRepo.FindEntity(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult OpenMedicineSubmitForm(SysPharmacyDepartmentOpenMedicineEntity entity ,string  keyValue)
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
            _SysPharmacyDepartmentOpenMedicineRepo.submitForm(entity, keyValue);
            return Success("操作成功");
        }

        #endregion

    }
}