using Newtonsoft.Json;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.WebAPI.Manage.Models;
using NuGet.Packaging.Signing;
using static Dm.net.buffer.ByteArrayBuffer;

namespace NewtouchHIS.WebAPI.Manage.Controllers
{
    public class InitController
    {
        /// <summary>
        /// 组装his内部API基础Request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="methodcode"></param>
        /// <param name="optype"></param>
        /// <returns></returns>
        public static BookingReqBase BuildBookRequest(BusRequest request, string? methodcode, string? optype)
        {
            return new BookingReqBase
            {
                AppId = request.AppId,
                OrganizeId = request.OrganizeId,
                methodcode = methodcode,
                optype = optype,
                user = string.Empty,
                Timestamp = request.Timestamp
            };
        }
        /// <summary>
        /// 新旧ResponseBase类转化
        /// </summary>
        /// <param name="resp"></param>
        /// <returns></returns>
        public static ResponseBase BuildResponseBase(ResponseBaseOld resp)
        {
            if (resp == null) { return new ResponseBase { code = ResponseResultCode.ERROR, msg = "请求的资源无响应" }; };
            return new ResponseBase { code = resp.code, msg = $"{resp.msg??""}{resp.sub_code}{resp.sub_msg}", data = resp.data };
        }
        public static ResponseBase BuildResponseBase(ResponseBaseOld resp, Pagination pagination)
        {
            if (resp == null)
            {
                return new ResponseBase { code = ResponseResultCode.ERROR, msg = "请求的资源无响应" };
            }
            if (resp.code == ResponseResultCode.SUCCESS && !string.IsNullOrWhiteSpace(resp.sub_msg))
            {
                pagination = JsonConvert.DeserializeObject<Pagination>(resp.sub_msg);
            }

            return new ResponseBase
            {
                code = resp.code,
                msg = resp.code == ResponseResultCode.SUCCESS ? "" : $"{resp.msg}{resp.sub_code}{resp.sub_msg}",
                data = new PaginationResult<object>
                {
                    page = pagination!.page,
                    sidx = pagination!.sidx,
                    records = pagination!.records,
                    sord = pagination!.sord,
                    rows = pagination!.rows,
                    pagedata = resp.data
                }
            };
        }
    }
}
