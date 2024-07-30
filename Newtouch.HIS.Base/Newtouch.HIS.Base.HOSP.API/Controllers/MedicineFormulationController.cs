using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Newtouch.HIS.Base.HOSP.API.Controllers
{
    /// <summary>
    /// 药品剂型
    /// </summary>
    public class MedicineFormulationController : ApiController
    {
        // GET: api/MedicineFormulation
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/MedicineFormulation/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/MedicineFormulation
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/MedicineFormulation/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MedicineFormulation/5
        public void Delete(int id)
        {
        }
    }
}
