using FrameworkBase.MultiOrg.Web;
using Newtouch.Core.Common;
using Newtouch.MRQC.Domain.IDomainServices.Apply;
using Newtouch.MRQC.Domain.IRepository.Apply;
using Newtouch.MRQC.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.MRQC.Web.Areas.QualityControlManage.Controllers
{
    public class ApplyController : OrgControllerBase
    {
        private readonly IMrApplyRepo _mrapplyRepo;
        private readonly IMrApplyDmnService _mrapplyDnmservice;
        // GET: QualityControlManage/Apply
        public override ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// emr病历申请列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="applyStutas"></param>
        /// <param name="ksrq"></param>
        /// <param name="jsrq"></param>
        /// <returns></returns>
        public ActionResult GetBlApplyList(Pagination pagination, string applyStutas,DateTime ksrq,DateTime jsrq)
        {
            var data = new
            {
                rows = _mrapplyDnmservice.GetBlApplyList(pagination, this.OrganizeId,applyStutas,ksrq,jsrq),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }
        /// <summary>
        /// 申请审批
        /// </summary>
        /// <param name="shbhlist"></param>
        /// <returns></returns>
        public ActionResult ApprovalApply(string shbhlist)
        {
            var reqObj = new
            {
                Data = new
                {
                    ApplyNos =shbhlist,
                    ApprovePerson =this.UserIdentity.UserCode,
                    ApproveDept =this.UserIdentity.DepartmentCode,
                    ApproveStatus = (int)ApplyStatusEnum.ysp
                },
                AppId = AuthenManageHelper.appId,
                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            var outstr = AuthenManageHelper.HttpPost<bool>(reqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/MedicalApplyHandle/ModifyApplyApprove", this.UserIdentity);
            if (outstr.data==false)
            {
                return Error("审批失败，请刷新待审批列表");
            }
            _mrapplyDnmservice.UpdateApplyStatus(shbhlist, this.OrganizeId, this.UserIdentity.DepartmentCode, this.UserIdentity.UserCode);
            return Success("审批成功");
        }
    }
}