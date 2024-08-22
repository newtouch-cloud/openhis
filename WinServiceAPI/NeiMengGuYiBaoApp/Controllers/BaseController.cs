using YiBaoInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using YiBaoInterface.Models;
using System.Web.Http.Results;
using NeiMengGuYiBaoApp.Code;
using Newtonsoft.Json;

namespace NeiMengGuYiBaoApp.Controllers
{
    public class BaseController : ApiController
    {
        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="data">实例或数据集</param>
        /// <param name="errorMsg">错误消息</param>
        /// <returns></returns>
        protected JsonResult<ResponseModel> ToSuccessResponse(object data = null,string errorMsg = null)
        {
            ResponseModel responseYibao = new ResponseModel()
            {
                Code = "1",
                ErrorMsg = errorMsg,
                Data = data
            };
            return Json(responseYibao);
        }

        /// <summary>
        /// 返回错误消息
        /// </summary>
        /// <param name="errorMsg">错误消息</param>
        /// <param name="data">实例或数据集</param>
        /// <returns></returns>
        protected JsonResult<ResponseModel> ToErrorResponse(string errorMsg, object data = null)
        {
            ResponseModel responseYibao = new ResponseModel()
            {
                Code = "-1",
                ErrorMsg = errorMsg,
                Data = data
            };
            return Json(responseYibao);
        }

        /// <summary>
        /// 返回错误消息.string格式
        /// </summary>
        /// <param name="errorMsg">错误消息</param>
        /// <param name="data">实例或数据集</param>
        /// <returns></returns>
        protected string ToErrorResponseString(string errorMsg, object data = null)
        {
            ResponseModel responseYibao = new ResponseModel()
            {
                Code = "-1",
                ErrorMsg = errorMsg,
                Data = data
            };
            return JsonConvert.SerializeObject(responseYibao);
        }

    }
}
