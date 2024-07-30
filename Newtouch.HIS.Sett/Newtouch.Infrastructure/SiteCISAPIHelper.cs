using System;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common.Web;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// CIS API请求帮助类
    /// </summary>
    public class SiteCISAPIHelper : SiteAPIRequestHelperBase<SiteCISAPIHelper>
    {
        /// <summary>
        /// 域Addr配置 的 Key
        /// </summary>
        private readonly static string _domainConfigKey = "SiteCISAPIHost";

        /// <summary>
        /// API Name
        /// </summary>
        private readonly static string _apiName = "CIS";

        /// <summary>
        /// 同步病人信息给CIS
        /// </summary>
        /// <param name="dto"></param>
        public static APIRequestHelper.DefaultResponse UpdateInpatientBasicInfo(InpatientPatientInfoDTO dto)
        {
            var reqObj = new
            {
                zyh = dto.zyh,
                zybz = dto.zybz,
                ryzd = dto.ryzd,
                ryzdmc = dto.ryzdmc,
                brxz = dto.brxz,
                brxzmc = dto.brxzmc,
                lxr = dto.lxr,
                lxrgx = dto.lxrgx,
                lxrdh = dto.lxrdh,
            };
            var apiResp = Request<APIRequestHelper.DefaultResponse>(
                "api/PatInfo/UpdateInpatientBasicInfo", reqObj);
            return apiResp;
        }
        /// <summary>
        /// 同步病人性质给CIS
        /// </summary>
        /// <param name="dto"></param>
        public static APIRequestHelper.DefaultResponse UpdatebrxzInfo(UpdatebrxzRequest dto)
        {
            var apiResp = Request<APIRequestHelper.DefaultResponse>(
                "api/PatInfo/UpdatebrxzInfo", dto);
            return apiResp;
        }
        /// <summary>
        /// 门诊预约查询 不分预约日期、科室、专家
        /// </summary>
        /// <param name="zjlx"></param>
        /// <param name="zjh"></param>
        /// <param name="blh"></param>
        /// <param name="kh"></param>
        /// <returns></returns>
        public static APIRequestHelper.DefaultResponse MzBespeakRegisterQuery(int? zjlx, string zjh, string blh, string kh)
        {
            var reqObj = new
            {
                zjlx = zjlx,
                zjh = zjh,
                blh = blh,
                kh = kh
            };
            var apiResp = Request<APIRequestHelper.DefaultResponse>(
                "api/MzBespeakRegister/MzBespeakRegisterQuery", reqObj);
            return apiResp;
        }

        /// <summary>
        /// 门诊预约排班查询
        /// </summary>
        /// <param name="ksCode"></param>
        /// <param name="ysgh"></param>
        /// <param name="regDate"></param>
        /// <param name="regTime"></param>
        /// <returns></returns>
        public static APIRequestHelper.DefaultResponse MzBespeakRegisterSchedulingQuery(string ksCode, string ysgh, DateTime regDate, string regTime)
        {
            var reqObj = new
            {
                ksCode = ksCode,
                ysgh = ysgh,
                regDate = regDate,
                regTime = regTime
            };
            var apiResp = Request<APIRequestHelper.DefaultResponse>(
                "api/MzBespeakRegister/MzBespeakRegisterSchedulingQuery", reqObj);
            return apiResp;
        }

        /// <summary>
        /// 已预约门诊挂号信息查询
        /// </summary>
        /// <param name="ksCode"></param>
        /// <param name="ysgh"></param>
        /// <param name="regDate"></param>
        /// <param name="regTime"></param>
        /// <returns></returns>
        public static APIRequestHelper.DefaultResponse MzAlreadyBespeakRegisterCountQuery(string ksCode, string ysgh, DateTime regDate, string regTime)
        {
            var reqObj = new
            {
                ksCode = ksCode,
                ysgh = ysgh,
                regDate = regDate,
                regTime = regTime
            };
            var apiResp = Request<APIRequestHelper.DefaultResponse>("api/MzBespeakRegister/MzAlreadyBespeakRegisterCountQuery", reqObj);
            return apiResp;
        }

        /// <summary>
        /// 赴约
        /// </summary>
        /// <param name="mzyyghId">门诊预约挂号ID</param>
        /// <param name="arrivalDate">赴约时间</param>
        /// <param name="arrivalOpereater">门诊挂号操作员</param>
        /// <returns></returns>
        public static APIRequestHelper.DefaultResponse KeepAnAppointment(string mzyyghId, DateTime arrivalDate, string arrivalOpereater)
        {
            var reqObj = new
            {
                mzyyghId = mzyyghId,
                arrivalDate = arrivalDate,
                arrivalOpereater = arrivalOpereater
            };
            var apiResp = Request<APIRequestHelper.DefaultResponse>("api/MzBespeakRegister/KeepAnAppointment", reqObj);
            return apiResp;
        }

        /// <summary>
        /// 获取就诊信息
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public static APIRequestHelper.DefaultResponse PatientTreatmentInfoQuery(string mzh, string organizeId)
        {
            var reqObj = new
            {
                mzh = mzh,
                OrganizeId = organizeId
            };
            return Request<APIRequestHelper.DefaultResponse>("api/PatientSeekingInfo/PatientTreatmentInfoQuery", reqObj);
        }

    }

    /// <summary>
    /// 同步住院病人信息
    /// </summary>
    public class InpatientPatientInfoDTO
    {
        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// 在院标志
        /// </summary>
        public string zybz { get; set; }
        /// <summary>
        /// 入院诊断
        /// </summary>
        public string ryzd { get; set; }
        /// <summary>
        /// 入院诊断名称
        /// </summary>
        public string ryzdmc { get; set; }
        /// <summary>
        /// 病人性质
        /// </summary>
        public string brxz { get; set; }
        /// <summary>
        /// 病人性质名称
        /// </summary>
        public string brxzmc { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string lxr { get; set; }
        /// <summary>
        /// 联系人关系
        /// </summary>
        public string lxrgx { get; set; }
        /// <summary>
        /// 联系人电话
        /// </summary>
        public string lxrdh { get; set; }
    }
    public class UpdatebrxzRequest
    {
        public string zyh { get; set; }
        public string mzh { get; set; }
        public string brxzCode { get; set; }
        public string brxzmc { get; set; }
    }
}
