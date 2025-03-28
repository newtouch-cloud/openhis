using Newtouch.Core.Common;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class APILogController : ControllerBase
    {
        private readonly ISyncTreatmentServiceRecordRepo _syncTreatmentServiceRecordRepo;

        public ActionResult GetListJson(Pagination pagination, string blh, string xm, DateTime? kssj, DateTime? jssj, string brlx, bool? wtjlbz, bool? isDeleted, string rygh)
        {
            //var gh = UserIdentity.rygh;
            bool? isAdmini = UserIdentity.IsHospAdministrator;
            var data = _syncTreatmentServiceRecordRepo.GetPagintionList(pagination, OrganizeId, kssj, jssj, rygh, isDeleted, isAdmini, brlx, 1, wtjlbz, "", "", blh, xm);
            var list = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }
    }
}