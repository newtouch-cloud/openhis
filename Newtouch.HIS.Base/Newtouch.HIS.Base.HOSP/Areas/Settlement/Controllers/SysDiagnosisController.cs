using Newtouch.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtouch.HIS.Domain.IRepository.Settlement;
using Newtouch.HIS.Domain.Entity.Settlement;

namespace Newtouch.HIS.Base.HOSP.Areas.Settlement.Controllers
{
	/// <summary>
	/// 系统诊断
	/// </summary>
	public class SysDiagnosisController : ControllerBase
	{
		private readonly ISysDiagnosisRepo _sysDiagnosisRepo;
		private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
		private readonly ISysSurgeryRepo _syssurgeryrepo;

		public SysDiagnosisController(ISysDiagnosisRepo sysDiagnosisRepo
			, ISysOrganizeDmnService sysOrganizeDmnService
			, ISysSurgeryRepo syssurgeryrepo
			)
		{
			this._sysDiagnosisRepo = sysDiagnosisRepo;
			this._sysOrganizeDmnService = sysOrganizeDmnService;
			this._syssurgeryrepo = syssurgeryrepo;

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pagination"></param>
		/// <param name="keyword"></param>
		/// <returns></returns>
		[HttpGet]
		[HandlerAjaxOnly]

		public ActionResult GetGridJson(Pagination pagination, string keyword)
		{
			pagination.sidx = "CreateTime desc";
			pagination.sord = "asc";
			var list = _sysDiagnosisRepo.GetCommonPagintionList(pagination, keyword);
			var data = new
			{
				rows = list,
				total = pagination.total,
				page = pagination.page,
				records = pagination.records
			};
			return Content(data.ToJson());
		}

		public ActionResult SurgeryGetGridJson(Pagination pagination, string keyword)
		{
			pagination.sidx = "CreateTime desc";
			pagination.sord = "asc";
			var list = _syssurgeryrepo.SurgeryGetGridJson(pagination, keyword);
			var data = new
			{
				rows = list,
				total = pagination.total,
				page = pagination.page,
				records = pagination.records
			};
			return Content(data.ToJson());
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="keyValue"></param>
		/// <returns></returns>
		[HttpGet]
		[HandlerAjaxOnly]
		public ActionResult GetFormJson(int? keyValue)
		{
			var data = _sysDiagnosisRepo.FindEntity(keyValue);
			return Content(data.ToJson());
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="keyValue"></param>
		/// <returns></returns>
		[HttpPost]
		[HandlerAjaxOnly]
		//[ValidateAntiForgeryToken]
		public ActionResult SubmitForm(SysDiagnosisEntity entity, int? keyValue)
		{
			entity.zt = entity.zt == "true" ? "1" : "0";
            entity.OrganizeId = "*";
			_sysDiagnosisRepo.SubmitForm(entity, keyValue);
			return Success("操作成功！");
		}

	}
}