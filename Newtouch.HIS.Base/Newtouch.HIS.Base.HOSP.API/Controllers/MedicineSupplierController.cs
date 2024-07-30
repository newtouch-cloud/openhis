using Autofac;
using Newtouch.HIS.API.Common;
using Newtouch.HIS.Base.HOSP.Request;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace Newtouch.HIS.Base.HOSP.API.Controllers
{
    /// <summary>
    /// 药品提供商
    /// </summary>
    [RoutePrefix("api/MedicineSupplier")]
    public class MedicineSupplierController : ApiControllerBase<MedicineSupplierController>
    {
        private readonly ISysMedicineSupplierRepo _sysMedicineSupplierRepo;

        public MedicineSupplierController(IComponentContext com)
            : base(com)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Query")]
        public HttpResponseMessage Query(MedicineSupplierQueryRequest request)
        {
            Action<MedicineSupplierQueryRequest, DefaultResponse> ac = (req, resp) =>
            {
                var codeUp = (req.keyword ?? "").ToUpper().Trim();
                var list = _sysMedicineSupplierRepo.IQueryable().Where(p => p.zt == "1"
                    && (codeUp == "" || p.gysmc.Contains(req.keyword) || p.gysCode.Contains(codeUp) || p.py.Contains(codeUp))
                    && p.OrganizeId == req.OrganizeId).ToList();

                resp.data = base.FilterObjectProperty(list, req.ResponseColumns
                    , includeProps: new string[] { "gysId", "gysCode", "gysmc", "py" });

                resp.code = ResponseResultCode.SUCCESS;
            };

            var response = base.CommonExecute(ac, request);

            return base.CreateResponse(response);
        }

    }
}
