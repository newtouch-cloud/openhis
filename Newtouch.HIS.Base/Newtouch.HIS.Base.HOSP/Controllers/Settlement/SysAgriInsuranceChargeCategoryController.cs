using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Web.Mvc;
using Newtouch.Tools;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Core.Common.Exceptions;

namespace Newtouch.HIS.Base.HOSP.Controllers
{
    /// <summary>
    /// 系统农保收费大类
    /// </summary>
    public class SysAgriInsuranceChargeCategoryController : ControllerBase
    {
        private readonly ISysAgriInsuranceChargeCategoryRepo _sysAgriInsuranceChargeCategoryRepo;
        private readonly ISysOrganizeDmnService _SysOrganizeDmnService;

        public SysAgriInsuranceChargeCategoryController(ISysAgriInsuranceChargeCategoryRepo sysAgriInsuranceChargeCategoryRepo
            , ISysOrganizeDmnService sysOrganizeDmnService)
        {
            this._sysAgriInsuranceChargeCategoryRepo = sysAgriInsuranceChargeCategoryRepo;
            this._SysOrganizeDmnService = sysOrganizeDmnService;
        }
        
        /// <summary>
        /// 根据organizeId获取有效大类信息
        /// </summary>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public ActionResult GetListSelectJson(string organizeId)
        {
            var list = _sysAgriInsuranceChargeCategoryRepo.GetValidList(organizeId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetGridJson(string organizeId, string keyword)
        {
            var data = _sysAgriInsuranceChargeCategoryRepo.GetList(organizeId, keyword);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(int keyValue)
        {
            var entity = _sysAgriInsuranceChargeCategoryRepo.GetForm(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(SysAgriInsuranceChargeCategoryEntity entity, int? keyValue)
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
            _sysAgriInsuranceChargeCategoryRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }
    }
}