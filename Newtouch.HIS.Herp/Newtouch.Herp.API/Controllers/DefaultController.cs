using Autofac;
using Newtouch.Herp.APIRequest;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.HIS.API.Common;
using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace Newtouch.Herp.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("api/Default")]
    public class DefaultController : ApiControllerBase<DefaultController>
    {
        private readonly ISysConfigRepo _sysConfigRepo;

        public DefaultController(IComponentContext com)
            : base(com)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Query")]
        public HttpResponseMessage Query(DefaultQueryRequest par)
        {
            Action<DefaultQueryRequest, DefaultResponse> ac = (req, resp) =>
            {
                var data = _sysConfigRepo.IQueryable().ToList();
                resp.data = data;

                resp.code = ResponseResultCode.SUCCESS;
            };

            var response = base.CommonExecute(ac, par);
            
            return base.CreateResponse(response);
        }

        // GET: api/Default1/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Default1
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Default1/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Default1/5
        public void Delete(int id)
        {
        }
    }
}