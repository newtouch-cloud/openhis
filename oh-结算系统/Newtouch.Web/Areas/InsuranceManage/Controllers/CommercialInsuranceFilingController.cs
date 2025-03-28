using System.Linq;
using System.Web.Mvc;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using Newtouch.Core.Common.Exceptions;

namespace Newtouch.HIS.Web.Areas.InsuranceManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class CommercialInsuranceFilingController : ControllerBase
    {
        private readonly ISysCommercialInsuranceDmnService _sysCommercialInsuranceDmnService;
        private readonly ISysCommercialInsuranceFilingRepo _sysCommercialInsuranceFilingRepo;

        /// <summary>
        /// 获取商保备案列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult SelectCommercialInsuranceFilingList(string keyword)
        {
            var orgId = OrganizeId;
            if (string.IsNullOrEmpty(orgId))
            {
                throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
            }
            var data = _sysCommercialInsuranceDmnService.SelectCommercialInsuranceFilingList(keyword, orgId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="sbbabId"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(SysCommercialInsuranceFilingEntity entity, string sbbabId)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            entity.OrganizeId = OrganizeId;
            _sysCommercialInsuranceFilingRepo.SubmitForm(entity, sbbabId);
            return Success("操作成功。");
        }

        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name="sbbabId"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(string sbbabId)
        {
            var orgId = this.OrganizeId;
            if (string.IsNullOrEmpty(orgId))
            {
                throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
            }
            var entity = _sysCommercialInsuranceDmnService.SelectCommercialInsuranceFilingList(null, orgId, sbbabId).FirstOrDefault();
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sbbabId"></param>
        /// <returns></returns>
        public ActionResult DeleteForm(string sbbabId)
        {
            var orgId = OrganizeId;
            if (string.IsNullOrEmpty(orgId))
            {
                throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
            }
            _sysCommercialInsuranceFilingRepo.DeleteForm(sbbabId, orgId);
            return Success("操作成功。");
        }
    }
}