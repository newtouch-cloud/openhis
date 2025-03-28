using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Services.HttpService;

namespace NewtouchHIS.WebAPI.Manage.Controllers
{
    /// <summary>
    /// API基类
    /// </summary>
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        protected readonly IHttpClientHelper _httpClient;
        /// <summary>
        /// 结算系统Api Host
        /// </summary>
        protected readonly string SettHttpUrl = ConfigInitHelper.SysConfig!.AppAPIHost!.SiteSettAPIHost ?? "";
        /// <summary>
        /// 医护协同Api Host
        /// </summary>
        protected readonly string CisHttpUrl = ConfigInitHelper.SysConfig!.AppAPIHost!.SiteCisAPIHost ?? "";
        /// <summary>
        /// 患者信息
        /// （匿名身份验证）
        /// </summary>
        protected readonly string PatientAnonRoute = "api/PatientAnon";
        /// <summary>
        /// 订单信息
        /// （匿名身份验证）
        /// </summary>
        protected readonly string OrderMethodRoute = "api/OrderCenter";

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="httpClient"></param>
        public ApiBaseController(IHttpClientHelper httpClient)
        {
            _httpClient = httpClient;
        }

        
    }
}
