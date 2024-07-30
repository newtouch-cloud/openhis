using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Autofac;
using System.Net.Http;
using System.Web.Http;
using Newtouch.CIS.APIRequest;
using Newtouch.CIS.APIRequest.Dto;

namespace NewtouchCIS.API.Controllers
{
    [RoutePrefix("api/CQkrlis")]
    public class CQKRLIsController : ApiControllerBase<CQKRLIsController>
    {
        public CQKRLIsController(IComponentContext com) : base(com)
        {
        }
        private readonly string LisConn = @"Provider=SQLOLEDB.1;Password=wqsj2000;Persist Security Info=True;User ID=wqsj;Initial Catalog=LISDATA;Data Source=139.9.249.190";
        [HttpPost]
        [Route("GetLisReport")]
        public void GetLisReport([FromBody]LISResultDTO request)
        {
            //var dbdata = System.Configuration.ConfigurationManager.AppSettings["LISConn"];
            //LisReportInput request = new LisReportInput();
            //request.type = re["type"].ToString();
            //request.brxx_id = re["brxx_id"].ToString();
            request.Ado_lis = LisConn;
            switch (request.type)
            {
                case "mz":

                    LisRequest.GetMzReport(request.Ado_lis, request.brxx_id);
                    break;
                case "zy":
                    //门诊 住院共用
                    //GetMzReport(request.Ado_lis, request.brxx_id);

                    LisRequest.GetZyReport(request.Ado_lis, request.brxx_id);
                    break;
                case "jpgdown":
                    LisRequest.CreateJpg_C(request.Ado_lis, request.brxx_tmh, request.zydj_id, request.brxx_id);
                    break;
                case "pdfdown":
                    LisRequest.CreatePdf_C(request.Ado_lis, request.brxx_tmh, request.zydj_id, request.brxx_id);
                    break;
            }
        }
    }
}
