using Newtouch.Core.Common;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Base.HOSP.Areas.Sys.Controllers
{
    public class CombinationController : ControllerBase
	{

		private readonly ISysMedicineDmnService _sysMedicineDmnService;


		public CombinationController(ISysOrganizeDmnService sysOrganizeDmnService, ISysMedicineDmnService sysMedicineDmnService)
		{

			this._sysMedicineDmnService = sysMedicineDmnService;
		}
		/// <summary>
		/// 药品对码视图
		/// </summary>
		/// <returns></returns>
		// GET: Sys/Combination
		public ActionResult Index()
        {
            return View();
        }

        public ActionResult CatalogComparison()
        {
            return View();
        }
        /// <summary>
        /// 耗材对码视图
        /// </summary>
        /// <returns></returns>
        public ActionResult materialIndex()
		{
			return View();
		}
		/// <summary>
		/// 项目对码视图
		/// </summary>
		/// <returns></returns>
		public ActionResult projectIndex()
		{
			return View();
		}
		
		/// <summary>
		/// 查询系统药品信息
		/// </summary>
		/// <param name="pagination"></param>
		/// <param name="keyword"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		[HandlerAjaxOnly]
		public ActionResult Getypxxlist(Pagination pagination, string keyword, string type)
		{
			pagination.sidx = "CreateTime desc";
			pagination.sord = "asc";
			var data = new
			{
				rows = _sysMedicineDmnService.GetPaginationListdm(this.OrganizeId, pagination, type, keyword),
				total = pagination.total,
				page = pagination.page,
				records = pagination.records
			};
			return Content(data.ToJson());
		}
        /// <summary>
		/// 查询系统药品信息
		/// </summary>
		/// <param name="pagination"></param>
		/// <param name="keyword"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		[HandlerAjaxOnly]
        public ActionResult GetMldzxxlist(Pagination pagination, string keyword, string type)
        {
            pagination.sidx = "Id desc";
            pagination.sord = "asc";
            var data = new
            {
                rows = _sysMedicineDmnService.GetPaginationListMldzxx(this.OrganizeId, pagination, type, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        /// <summary>
        /// 根据系统药品信息查询医保对应药品信息
        /// </summary>
        /// <param name="ypmc"></param>
        /// <param name="py"></param>
        /// <param name="gjybdm"></param>
        /// <param name="pzwh"></param>
        /// <returns></returns>
        public ActionResult GetYpypxxlist( string ypmc, string py,string gjybdm,string pzwh)
		{

			var data = _sysMedicineDmnService.GetYpypxxlist(this.OrganizeId, ypmc, py, gjybdm, pzwh);
			
			return Content(data.ToJson());
		}

		/// <summary>
		/// 药品对码操作
		/// </summary>
		/// <param name="ybxx"></param>
		/// <param name="ypid"></param>
		/// <returns></returns>
		public ActionResult SaveYpYb(G_yb_ypxxVO ybxx, int? ypid)
		{
			var data = new
			{
				updatecount = _sysMedicineDmnService.SaveYpYb(ybxx, ypid, OrganizeId),
			};
			return Content(data.ToJson());
		}
		/// <summary>
		/// 系统材料信息
		/// </summary>
		/// <param name="pagination"></param>
		/// <param name="keyword"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public ActionResult Getclxxlist(Pagination pagination, string keyword, string type)
		{
			pagination.sidx = "CreateTime desc";
			pagination.sord = "asc";
			var data = new
			{
				rows = _sysMedicineDmnService.GetclxxList(this.OrganizeId, pagination, type, keyword),
				total = pagination.total,
				page = pagination.page,
				records = pagination.records
			};
			return Content(data.ToJson());
		}

		/// <summary>
		/// 医保材料信息
		/// </summary>
		/// <param name="ypmc"></param>
		/// <param name="py"></param>
		/// <param name="gjybdm"></param>
		/// <param name="pzwh"></param>
		/// <returns></returns>
		public ActionResult GetYbclxxlist(string ypmc, string py, string gjybdm, string pzwh)
		{
			var data = _sysMedicineDmnService.GetYbclxxlist(this.OrganizeId, ypmc, py, gjybdm, pzwh);

			return Content(data.ToJson());
		}

		/// <summary>
		/// 材料对码操作
		/// </summary>
		/// <param name="ybxx"></param>
		/// <param name="ypid"></param>
		/// <returns></returns>
		public ActionResult SaveYpcl(G_yb_clxxVO ybxx, int? ypid)
		{
			var data = new
			{
				updatecount = _sysMedicineDmnService.SaveYpcl(ybxx, ypid, OrganizeId),
			};
			return Content(data.ToJson());
		}

		public ActionResult Getxmxxlist(Pagination pagination, string keyword, string type)
		{
			pagination.sidx = "CreateTime desc";
			pagination.sord = "asc";
			var data = new
			{
				rows = _sysMedicineDmnService.GetxmxxList(this.OrganizeId, pagination, type, keyword),
				total = pagination.total,
				page = pagination.page,
				records = pagination.records
			};
			return Content(data.ToJson());
		}

		/// <summary>
		/// 医保项目信息
		/// </summary>
		/// <param name="ypmc"></param>
		/// <param name="py"></param>
		/// <param name="gjybdm"></param>
		/// <param name="pzwh"></param>
		/// <returns></returns>
		public ActionResult GetYbxmxxlist(string ypmc, string py, string gjybdm, string pzwh)
		{
			var data = _sysMedicineDmnService.GetYbxmxxlist(this.OrganizeId, ypmc, py, gjybdm, pzwh);

			return Content(data.ToJson());
		}

		/// <summary>
		/// 项目对码操作
		/// </summary>
		/// <param name="ybxx"></param>
		/// <param name="ypid"></param>
		/// <returns></returns>
		public ActionResult SaveYpxm(G_yb_xmxxVO ybxx, int? ypid)
		{
			var data = new
			{
				updatecount = _sysMedicineDmnService.SaveYpxm(ybxx, ypid, OrganizeId),
			};
			return Content(data.ToJson());
		}
	}
}