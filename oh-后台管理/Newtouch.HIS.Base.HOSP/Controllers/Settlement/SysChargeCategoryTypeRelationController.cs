using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.HIS.Base.HOSP.Controllers
{
    /// <summary>
    /// 收费大类 大类类型
    /// </summary>
    public class SysChargeCategoryTypeRelationController : ControllerBase
    {
        private readonly ISysChargeCategoryTypeRelationRepo _sysChargeCategoryTypeRelationRepo;
        private readonly ISysOrganizeDmnService _SysOrganizeDmnService;

        public SysChargeCategoryTypeRelationController(ISysChargeCategoryTypeRelationRepo sysChargeCategoryTypeRelationRepo, ISysOrganizeDmnService SysOrganizeDmnService)
        {
            this._sysChargeCategoryTypeRelationRepo = sysChargeCategoryTypeRelationRepo;
            this._SysOrganizeDmnService = SysOrganizeDmnService;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetGridJson(string organizeId, string keyword)
        {
            var data = _sysChargeCategoryTypeRelationRepo.GetList(organizeId, keyword);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            var data = _sysChargeCategoryTypeRelationRepo.FindEntity(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(SysChargeCategoryTypeRelationEntity entity, string keyValue)
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
           _sysChargeCategoryTypeRelationRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }

    }
}