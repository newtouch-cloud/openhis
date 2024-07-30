using Autofac;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.HIS.API.Common.Attributes;
using NewtouchCIS.Infrastructure;
using System;
using System.Data;
using System.Web.Http;

namespace NewtouchCIS.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("api/Default")]
    public class DefaultController : ApiControllerBase<DefaultController>
    {
        private readonly ISysUserRepo _sysUserRepo;
        public DefaultController(IComponentContext com)
            : base(com)
        {

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
        [HttpPost]
        [Route("Delete")]
        // DELETE: api/Default1/5
        public void Delete(int id)
        {
        }
    }
}
