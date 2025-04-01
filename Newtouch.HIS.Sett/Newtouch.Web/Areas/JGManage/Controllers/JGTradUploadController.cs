using Newtouch.Core.Common;

using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtouch.HIS.Domain.DTO.OutputDto.JGManage;
using Newtouch.HIS.Domain.IDomainServices.JGManage;


namespace Newtouch.HIS.Web.Areas.JGManage.Controllers
{
    public class JGTradUploadController : ControllerBase
    {
        private readonly IJGTradUploadDmnService _JGTradUploadDmnService;
		// GET: JGManage/JGTradUpload

		public ActionResult From()
		{
			return View();
		}

		[HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(string kssj, string jssj, string scqk, string zyh,string tradiNumber)
        {
            var data = new
            {
                rows = _JGTradUploadDmnService.GetList(this.OrganizeId, kssj, jssj, scqk, zyh, tradiNumber)
            };
            return Content(data.ToJson());
        }

        

        //JG上传
        public ActionResult JGUpload(List<JGUploadDto> list,string tradiNumber)
        {
            var data = _JGTradUploadDmnService.JGUpload(this.OrganizeId, list, tradiNumber);
            return Content(data.ToJson());
        }

        public ActionResult GetGridJsonYp(string kssj, string jssj, string scqk, string tradiNumber)
        {
            var data = new
            {
                rows = _JGTradUploadDmnService.GetListYp(this.OrganizeId, kssj, jssj, scqk, tradiNumber)
            };
            return Content(data.ToJson());
        }
        public ActionResult GetGridJsonsqsz(string kssj, string jssj, string scqk, string zyh, string tradiNumber)
        {
            var data = new
            {
                rows = _JGTradUploadDmnService.GetListsqsz(this.OrganizeId, kssj, jssj, scqk, zyh, tradiNumber)
            };
            return Content(data.ToJson());
        }

		public ActionResult GetGridyzList(string zyh,string types)
		{
			var data = new
			{
				rows = _JGTradUploadDmnService.Getmxlist(this.OrganizeId, zyh, types)
			};
			return Content(data.ToJson());
		}
        
        public ActionResult ReUpload(string zyh,string types)
        {
            var data = new
            {
                rows = _JGTradUploadDmnService.Getmxlist(this.OrganizeId, zyh, types)
            };
            return Content(data.ToJson());
        }
        
        
	}
}