using Autofac;
using FrameworkBase.MultiOrg.Domain.IRepository;
using System.Web.Http;

namespace Newtouch.CIS.API.Controllers
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

        // DELETE: api/Default1/5
        public void Delete(int id)
        {
        }
    }
}
