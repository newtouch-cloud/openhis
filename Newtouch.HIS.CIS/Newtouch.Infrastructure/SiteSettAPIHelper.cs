using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common.Web;
using System;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// Sett API请求帮助类
    /// </summary>
    public class SiteSettAPIHelper : SiteAPIRequestHelperBase<SiteSettAPIHelper>
    {
        /// <summary>
        /// 域Addr配置 的 Key
        /// </summary>
        private readonly static string _domainConfigKey = "SiteSettAPIHost";

        /// <summary>
        /// API Name
        /// </summary>
        private readonly static string _apiName = "Sett";
        private static APIRequestHelper.DefaultResponse apiResp;

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
                bq = dto.bq,
                cw = dto.cw,
                cyrq = dto.cyrq,
                doctor = dto.doctor,
                cyzd = dto.cyzd,
            };
            var apiResp = Request<APIRequestHelper.DefaultResponse>(
                "api/Patient/UpdateInpatientOutInfo", reqObj);
            return apiResp;
        }

        /// <summary>
        /// 已结算处方明细查询
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="fph"></param>
        /// <param name="kh"></param>
        /// <param name="yfCode">用法代码</param>
        /// <returns></returns>
        public static APIRequestHelper.DefaultResponse OutpatientSettledRpQuery(DateTime kssj, DateTime jssj, string fph, string kh, string yfCode,string mzh)
        {
            var reqObj = new
            {
                kssj = kssj,
                jssj = jssj,
                fph = fph,
                kh = kh,
                yfCode = yfCode,
                mzh=mzh
            };
            return Request<APIRequestHelper.DefaultResponse>("api/OutpatientPharmacy/OutpatientSettledRpQuery", reqObj);
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
        public int zybz { get; set; }
        /// <summary>
        /// 病区
        /// </summary>
        public string bq { get; set; }
        /// <summary>
        /// 床位
        /// </summary>
        public string cw { get; set; }
        /// <summary>
        /// 出院日期
        /// </summary>
        public DateTime? cyrq { get; set; }
        /// <summary>
        /// 医生
        /// </summary>
        public string doctor { get; set; }
        /// <summary>
        /// 出院诊断
        /// </summary>
        public string cyzd { get; set; }
    }
}
