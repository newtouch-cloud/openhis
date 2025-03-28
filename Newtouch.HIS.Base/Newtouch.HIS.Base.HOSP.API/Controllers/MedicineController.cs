using Newtouch.HIS.Domain.IRepository;
using System.Collections.Generic;
using System.Web.Http;

namespace Newtouch.HIS.Base.HOSP.API.Controllers
{
    /// <summary>
    /// 药品
    /// </summary>
    public class MedicineController : ApiController
    {
        private readonly ISysMedicineRepo _sysMedicineRepo;

        public MedicineController(ISysMedicineRepo sysMedicineRepo)
        {
            this._sysMedicineRepo = sysMedicineRepo;
        }

        // GET: api/Medicine
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Medicine/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Medicine
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Medicine/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Medicine/5
        public void Delete(int id)
        {
        }
    }
}
