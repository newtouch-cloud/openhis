using FrameworkBase.MultiOrg.Web;
using Newtouch.Core.Common;
using Newtouch.MR.ManageSystem.Domain.Entity;
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
    public class MrFeeController : OrgControllerBase
    {

#pragma warning disable CS0649 // Field 'MrFeeController._MrFeeDmnService' is never assigned to, and will always have its default value null
        private readonly IMrFeeDmnService _MrFeeDmnService;
#pragma warning restore CS0649 // Field 'MrFeeController._MrFeeDmnService' is never assigned to, and will always have its default value null
#pragma warning disable CS0649 // Field 'MrFeeController._MrFeeRepo' is never assigned to, and will always have its default value null
        private readonly IMrFeeRepo _MrFeeRepo;
#pragma warning restore CS0649 // Field 'MrFeeController._MrFeeRepo' is never assigned to, and will always have its default value null

        // GET: RecordManage/MrFee
#pragma warning disable CS0114 // 'MrFeeController.Index()' hides inherited member 'BaseController.Index()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword.
        public ActionResult Index()
#pragma warning restore CS0114 // 'MrFeeController.Index()' hides inherited member 'BaseController.Index()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword.
        {
            return View();
        }

        public ActionResult GetPagintionList(Pagination pagination, string keyword)
        {
            var oplist = _MrFeeDmnService.GetPaginationList(pagination, OrganizeId,keyword);

            var data = new
            {
                rows = oplist,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        public ActionResult GetFormJson(string keyValue,int index)
        {
            var entity = _MrFeeDmnService.GetFormJson(keyValue,index);
            return Content(entity.ToJson());
        }

        //获取一级收费大类
        public ActionResult GetFeeOne(string orgId) {
            var result = _MrFeeDmnService.GetFeeOne(OrganizeId);
            return Content(result.ToJson());
        }
        //获取二级收费大类
        public ActionResult GetFeeTwo(string orgId, string parentCode) {
            var result = _MrFeeDmnService.GetFeeTwo(OrganizeId, parentCode);
            return Content(result.ToJson());
        }

        public ActionResult submitForm(bafeeVO entity, string keyValue)
        {
			//if (string.IsNullOrWhiteSpace(entity.OrganizeId))
			//{
			//    entity.OrganizeId = OrganizeId;
			//}

			//根据主键获取修改前的数据
			var oldEntity=_MrFeeDmnService.GetFormJson(keyValue, entity.Lev);

			_MrFeeRepo.SubmitForm(entity, keyValue,OrganizeId,oldEntity);
            return Success("操作成功。");
        }

        public ActionResult DeleteData(string keyValue)
        {
            _MrFeeRepo.DeleteForm(keyValue);
            return Success("操作成功。");
        }
    }
}