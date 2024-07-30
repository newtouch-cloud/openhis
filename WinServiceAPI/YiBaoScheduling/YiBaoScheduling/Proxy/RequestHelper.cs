using Newtonsoft.Json;
using Newtouch.Core.Common.Utils;
using Newtouch.Tools;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiBaoScheduling.Common;
using YiBaoScheduling.Model;

namespace YiBaoScheduling.Proxy
{
    public class RequestHelper
    {
        public static readonly RequestHelper _reqobj = new RequestHelper();
        public static string apiUrl=ConfigurationManager.AppSettings["yibaoUrl"];
        private RequestHelper()
        {

        }

        public static RequestHelper GetInstance()
        {
            return _reqobj;
        }

        public ResponseDTO InpatientCost(UploadData request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(apiUrl) || request == null) return null;
                var uri = apiUrl + "api/YiBao/HospitaFeedetail_2301V2";
                var responseResult= HttpClientHelper.HttpPostString(uri, request.ToJson(), contentType: HttpClientHelper.EnumContentType.json);
                return responseResult.Trim('"').Replace("\\","").ToObject<ResponseDTO>();
            }
            catch (Exception er)
            {
                AppLogger.Info("住院号："+request.hisId+"【2301】费用明细上传异常："+ er.Message);
                var ermsgobj = new ResponseDTO
                {
                    infcode = "1",
                    err_msg = er.Message
                };
                return ermsgobj;
            }
        }
        public ResponseDTO BedModification(BedDifference request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(apiUrl) || request == null) return null;
                var uri = apiUrl + "api/YiBao/HospitaMdtrtinfo_2403";
                var responseResult = HttpClientHelper.HttpPostString(uri, request.ToJson(), contentType: HttpClientHelper.EnumContentType.json);
                return responseResult.Trim('"').Replace("\\", "").ToObject<ResponseDTO>();
            }
            catch (Exception er)
            {
                AppLogger.Info("住院号：" + request.hisId + "【2403】修改入院登记信息异常：" + er.Message);
                var ermsgobj = new ResponseDTO
                {
                    infcode = "1",
                    err_msg = er.Message
                };
                return ermsgobj;
            }
        }
        public ResponseDTO InventoryUpload3501(Inventory3501 request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(apiUrl) || request == null) return null;
                var uri = apiUrl + "api/YiBao/InventoryUpload_3501";
                var responseResult = HttpClientHelper.HttpPostString(uri, request.ToJson(), contentType: HttpClientHelper.EnumContentType.json);
                return responseResult.Trim('"').Replace("\\", "").ToObject<ResponseDTO>();
            }
            catch (Exception er)
            {
                AppLogger.Info("盘点ID：" + request.pdId + "盘点信息上传异常：" + er.Message);
                var ermsgobj = new ResponseDTO
                {
                    infcode = "1",
                    err_msg = er.Message
                };
                return ermsgobj;
            }
        }
        public ResponseDTO InventoryUpload3502(Inventory3502 request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(apiUrl) || request == null) return null;
                var uri = apiUrl + "api/YiBao/InventoryUpload_3502";
                var responseResult = HttpClientHelper.HttpPostString(uri, request.ToJson(), contentType: HttpClientHelper.EnumContentType.json);
                return responseResult.Trim('"').Replace("\\", "").ToObject<ResponseDTO>();
            }
            catch (Exception er)
            {
                AppLogger.Info("出入库ID：" + request.crkId + "库存变更信息上传异常：" + er.Message);
                var ermsgobj = new ResponseDTO
                {
                    infcode = "1",
                    err_msg = er.Message
                };
                return ermsgobj;
            }
        }
        public ResponseDTO InventoryUpload3503(Inventory3502 request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(apiUrl) || request == null) return null;
                var uri = apiUrl + "api/YiBao/InventoryUpload_3503";
                var responseResult = HttpClientHelper.HttpPostString(uri, request.ToJson(), contentType: HttpClientHelper.EnumContentType.json);
                return responseResult.Trim('"').Replace("\\", "").ToObject<ResponseDTO>();
            }
            catch (Exception er)
            {
                AppLogger.Info("出入库ID：" + request.crkId + "商品采购信息上传异常：" + er.Message);
                var ermsgobj = new ResponseDTO
                {
                    infcode = "1",
                    err_msg = er.Message
                };
                return ermsgobj;
            }
        }
        public ResponseDTO InventoryUpload3504(Inventory3502 request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(apiUrl) || request == null) return null;
                var uri = apiUrl + "api/YiBao/InventoryUpload_3504";
                var responseResult = HttpClientHelper.HttpPostString(uri, request.ToJson(), contentType: HttpClientHelper.EnumContentType.json);
                return responseResult.Trim('"').Replace("\\", "").ToObject<ResponseDTO>();
            }
            catch (Exception er)
            {
                AppLogger.Info("出入库ID：" + request.crkId + "商品采购退货信息上传异常：" + er.Message);
                var ermsgobj = new ResponseDTO
                {
                    infcode = "1",
                    err_msg = er.Message
                };
                return ermsgobj;
            }
        }
        public ResponseDTO InpatientPreSett(UploadData request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(apiUrl) || request == null) return null;
                var uri = apiUrl + "api/YiBao/HospitaSettlement_2303";
                var responseResult= HttpClientHelper.HttpPostString(uri, request.ToJson(), contentType: HttpClientHelper.EnumContentType.json);
                var fymx = responseResult.Trim('"').Replace("\\", "").ToObject<ResponseDTO>();
                if (fymx.output!=null)
                {
                    var resulut = JsonConvert.DeserializeObject<Output_2304>(Convert.ToString(fymx.output));
                    fymx.setlinfo = resulut.setlinfo;
                }
                return fymx;
            }
            catch (Exception er)
            {
                AppLogger.Info("住院号：" + request.hisId + "【2303】预结算异常：" + er.Message);
                var ermsgobj = new ResponseDTO
                {
                    infcode = "1",
                    err_msg = er.Message
                };
                return ermsgobj;
            }
        }
    }
}
