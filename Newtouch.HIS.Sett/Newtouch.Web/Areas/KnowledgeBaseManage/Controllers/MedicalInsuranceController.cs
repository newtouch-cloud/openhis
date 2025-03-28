using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices.KnowledgeBaseManage;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.KnowledgeBaseManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class MedicalInsuranceController : ControllerBase
    {
        private readonly IMedicalInsuranceDmnService _medicalInsuranceDmnService;
        private readonly ISysMedicalInsuranceFilingRepo _sysMedicalInsuranceFilingRepo;

        // GET: KnowledgeBaseManage/MedicalInsurance
        public override ActionResult Index()
        {
            return View();
        }

        public override ActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// 获取医保备案列表
        /// </summary>
        /// <param name="blh"></param>
        /// <param name="ksrq"></param>
        /// <param name="jsrq"></param>
        /// <returns></returns>
        public ActionResult SelectMedicalInsuranceFilingList(Pagination pagination, string keyword)
        {
            var list = new
            {
                rows = _medicalInsuranceDmnService.SelectMedicalInsuranceFilingList(pagination, keyword, this.OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ybbabId"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(SysMedicalInsuranceFilingEntity entity, string ybbabId)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            entity.OrganizeId = this.OrganizeId;
            _sysMedicalInsuranceFilingRepo.SubmitForm(entity, ybbabId);
            return Success("操作成功。");
        }

        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name="ybbabId"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(string ybbabId)
        {
            Pagination pagination = new Pagination();
            pagination.sidx = "CreateTime desc";
            pagination.rows = 1;
            pagination.page = 1;
            var entity = _medicalInsuranceDmnService.SelectMedicalInsuranceFilingList(pagination, null, this.OrganizeId, ybbabId).FirstOrDefault();
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ybbabId"></param>
        /// <returns></returns>
        public ActionResult DeleteForm(string ybbabId)
        {
            var orgId = this.OrganizeId;
            if (string.IsNullOrEmpty(orgId))
            {
                throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
            }
            _sysMedicalInsuranceFilingRepo.DeleteForm(ybbabId, orgId);
            return Success("操作成功。");
        }
    }
}