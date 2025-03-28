using FrameworkBase.MultiOrg.Web;
using Newtouch.Core.Common;
using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IDomainServices;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.OR.ManageSystem.Web.Areas.Operation.Controllers
{
    public class OpRoomController : OrgControllerBase
    {
        private readonly ICommonDmnService _CommonDmnService;
        private readonly IORRoomRepo _ORRoomRepo;
        // GET: Operation/OpRoom
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetGridList(Pagination pagination, string keyword)
        {
            var oplist = _ORRoomRepo.GetPagintionList(pagination, keyword, OrganizeId);

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
            var pat = _ORRoomRepo.GetPagintionList(pagination, keyword, OrganizeId);

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
            var entity = _ORRoomRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }
        public ActionResult submitForm(ORRoomEntity entity, string keyValue)
        {
            if (string.IsNullOrWhiteSpace(entity.OrganizeId))
            {
                entity.OrganizeId = OrganizeId;
            }
            _ORRoomRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }
        public ActionResult DeleteData(string keyValue)
        {
            _ORRoomRepo.DeleteForm(keyValue);
            return Success("操作成功。");
        }
    }
}