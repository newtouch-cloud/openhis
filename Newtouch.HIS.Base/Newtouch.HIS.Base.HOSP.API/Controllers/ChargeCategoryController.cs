using Newtouch.HIS.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Newtouch.HIS.Base.HOSP.API.Controllers
{
    /// <summary>
    /// 系统收费大类
    /// </summary>
    public class ChargeCategoryController : ApiController
    {
        private readonly ISysChargeCategoryRepo _sysChargeCategoryRepo;

        public ChargeCategoryController(ISysChargeCategoryRepo sysChargeCategoryRepo)
        {
            this._sysChargeCategoryRepo = sysChargeCategoryRepo;
        }

        // GET: api/ChargeCategory
        public IEnumerable<string> Get()
        {
            //var _sysChargeCategoryRepo1111 = Newtouch.Common.DependencyDefaultInstanceResolver.GetInstance<ISysChargeCategoryRepo>();

            //var list = _sysChargeCategoryRepo.GetValidList("2");

            //var list = _sysChargeCategoryRepo1111.GetYPSFDLList("2");

            return new string[] { "value1", "value2" };
        }

        // GET: api/ChargeCategory/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ChargeCategory
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ChargeCategory/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ChargeCategory/5
        public void Delete(int id)
        {
        }
    }
}
