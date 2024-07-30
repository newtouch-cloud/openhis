using Newtouch.HIS.API.Common;
using Newtouch.HIS.Base.HOSP.Request;
using Newtouch.HIS.Domain.IRepository;
using System;
using System.Net.Http;
using System.Web.Http;
using System.Linq;
using Autofac;

namespace Newtouch.HIS.Base.HOSP.API.Controllers
{
    /// <summary>
    /// 药品 出入库方式
    /// </summary>
    [RoutePrefix("api/MedicineStorageIOMode")]
    public class MedicineStorageIOModeController : ApiControllerBase<MedicineStorageIOModeController>
    {
        private readonly ISysMedicineStorageIOModeRepo _sysMedicineStorageIOModeRepo;

        public MedicineStorageIOModeController(IComponentContext com)
            : base(com)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("Query")]
        [HttpPost]
        public HttpResponseMessage Query(MedicineStorageIOModeQueryRequest request)
        {
            Action<MedicineStorageIOModeQueryRequest, DefaultResponse> ac = (req, resp) =>
            {
                var list = _sysMedicineStorageIOModeRepo.IQueryable().Where(p => p.zt == "1" && p.crkbz == request.crkbz).ToList();

                resp.data = base.FilterObjectProperty(list, req.ResponseColumns
                    , excludeProps: new string[] { "crkbz", "CreatorCode", "CreateTime", "LastModifyTime", "LastModifierCode", "zt", "px" });

                resp.code = ResponseResultCode.SUCCESS;
            };

            var response = base.CommonExecute(ac, request);

            return base.CreateResponse(response);
        }


    }
}
