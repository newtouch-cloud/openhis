using System.Collections.Generic;
using System.Web.Http;

namespace Newtouch.HIS.Sett.API.Controllers
{
    public class DefaultController : ApiController
    {
        // GET: api/Default1
        public IEnumerable<string> Get()
        {
            //var repo = DependencyDefaultInstanceResolver.GetInstance<IOutpatientSettlementRepo>();
            //var result = repo.GetFyhjInfo("", DateTime.Now, 0);
            return new string[] { "value1", "value2" };
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
