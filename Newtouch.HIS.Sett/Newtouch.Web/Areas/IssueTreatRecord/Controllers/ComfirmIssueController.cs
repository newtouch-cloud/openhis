using Newtouch.Core.Common;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.IssueTreatRecord.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class ComfirmIssueController : ControllerBase
    {
        private readonly ISyncTreatmentServiceRecordRepo _syncTreatmentServiceRecordRepo;

        public ActionResult GetListJson(Pagination pagination, string blh, string xm, DateTime? kssj, DateTime? jssj, string brlx)
        {
            var gh = UserIdentity.rygh;
            var data = _syncTreatmentServiceRecordRepo.GetPagintionList(pagination, OrganizeId, kssj, jssj, gh, false, false, brlx, 1, false, "", "", blh, xm);
            var list = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        public ActionResult Involid(List<string> list)
        {
            _syncTreatmentServiceRecordRepo.SaveCancel(OrganizeId, list);
            return Success("操作成功");
        }
    }
}