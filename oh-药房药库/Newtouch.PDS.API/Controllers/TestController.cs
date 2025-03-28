using System;
using System.Web.Http;
using Autofac;
using Newtouch.Common.Web;
using Newtouch.HIS.API.Common;
using Newtouch.HIS.Application.Implementation;
using Newtouch.PDS.API.App_Start;
using Newtouch.PDS.Requset.ResourcesOperate;
using Newtouch.Tools.Security;

namespace Newtouch.PDS.API.Controllers
{
    /// <summary>
    /// 资源操作
    /// </summary>
    [RoutePrefix("api/test")]
    public class TestController : ApiControllerBase<TestController>
    {

        public TestController(IComponentContext com) : base(com)
        {
        }

        [HttpGet]
        [Route("TestMultithreadTrans")]
        public string TestMultithreadTrans()
        {
            return new TestMultithreadTransApp().test1();
        }


        [HttpGet]
        [Route("TestEFDbTransaction")]
        public string TestEFDbTransaction()
        {
            return new TestMultithreadTransApp().testEFDbTransaction();
        }

        /// <summary>
        /// 测试token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("TestToken")]
        public DefaultResponse TestToken(OutpatientCommitRequestDTO request)
        {
            var response = new DefaultResponse();
           
            response.code = ResponseResultCode.SUCCESS;
            return response;
        }
    }
}