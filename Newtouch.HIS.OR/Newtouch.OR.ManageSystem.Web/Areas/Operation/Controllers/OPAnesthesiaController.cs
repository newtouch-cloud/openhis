using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Web;
using Newtouch.Core.Common;
using Newtouch.OR.ManageSystem.Domain.DTO;
using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IDomainServices;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.OR.ManageSystem.Web.Areas.Operation.Controllers
{
    public class OPAnesthesiaController : OrgControllerBase
    {
        private readonly ICommonDmnService _CommonDmnService;
        private readonly IORAnesthesiaRepo _ORAnesthesiaRepo;
        // GET: Operation/OPAnesthesia

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetGridList(Pagination pagination, string keyword)
        {
            var oplist = _ORAnesthesiaRepo.GetPagintionList(pagination, keyword,OrganizeId);

            var data = new
            {
                rows = oplist,
                page = 1,
                records = 1,
                total = 1
            };
            return Content(data.ToJson());
        }
        public ActionResult GetPagintionList(Pagination pagination, string keyword)
        {
            var pat = _ORAnesthesiaRepo.GetPagintionList(pagination, keyword,OrganizeId);

            var data = new
            {
                rows = pat,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = _ORAnesthesiaRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }
        public ActionResult submitForm(ORAnesthesiaEntity entity, string keyValue)
        {
            if (string.IsNullOrWhiteSpace(entity.OrganizeId))
            {
                entity.OrganizeId = OrganizeId;
            }
            _ORAnesthesiaRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }
        public ActionResult DeleteData(string keyValue)
        {
            _ORAnesthesiaRepo.DeleteForm(keyValue);
            return Success("操作成功。");
        }
    }
}