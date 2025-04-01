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
    public class JGUploadController : ControllerBase
    {
        
        private readonly IJGUploadDmnService _JGUploadDmnService;
        // GET: JGManage/JGUpload
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string kssj, string jssj,string scqk,string zyh)
        {
            var data = new
            {
                rows = _JGUploadDmnService.GetList(pagination, this.OrganizeId, kssj, jssj,scqk,zyh),
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Content(data.ToJson());
        }


        //JG上传
        public ActionResult JGUpload(List<JGUploadDto> list)
        {
            var data = _JGUploadDmnService.JGUpload(this.OrganizeId,list);
            return Content(data.ToJson());
        }

    }
}