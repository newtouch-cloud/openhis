using FrameworkBase.MultiOrg.Web;
using Newtouch.Core.Common;
using Newtouch.MR.ManageSystem.Domain.IDomainServices;
using Newtouch.MR.ManageSystem.Domain.IRepository;
using Newtouch.MR.ManageSystem.Domain.ValueObjects;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.MR.ManageSystem.Web.Areas.RecordManage.Controllers
{
    public class MrFeeRelController : OrgControllerBase
    {

        private readonly IMrFeeRelRepo _MrFeeRelRepo;
        private readonly IMrFeeRelDmnService _MrFeeRelDmnService;

        // GET: RecordManage/MrFeeRel
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPagintionList(Pagination pagination, string keyword,string code)
        {
            var pat = _MrFeeRelDmnService.GetPagintionList(pagination, keyword, OrganizeId,code);

            var data = new
            {
                rows = pat,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        //获取二级和三级收费大类
        public ActionResult GetFeeSel(string orgId)
        {
            var result = _MrFeeRelDmnService.GetFeeSel(OrganizeId);
            return Content(result.ToJson());
        }

        public ActionResult GetFormJson(string keyValue)
        {
            var entity = _MrFeeRelDmnService.GetFormJson(keyValue, OrganizeId);
            return Content(entity.ToJson());
        }

        public int SubmitForm(bafeeRelVO entity)
        {
            if (string.IsNullOrWhiteSpace(entity.OrganizeId))
            {
                entity.OrganizeId = OrganizeId;
            }
            entity.CreateTime = Convert.ToDateTime(DateTime.Now);
            entity.CreatorCode = UserIdentity.UserName;
            _MrFeeRelDmnService.SubmitForm(entity);
            return 1;
        }

        public ActionResult Save(IList<bafeeRelVO> list) {
            foreach (var obj in list) {
                SubmitForm(obj);
            }
            return Success("操作成功。");
        }

    }
}