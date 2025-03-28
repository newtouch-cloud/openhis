using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Services.ClinicService;
using static NewtouchHIS.Lib.Common.HisEnum;

namespace NewtouchHIS.Services
{
    /// <summary>
    /// 诊所接口帮助类
    /// </summary>
    public class ClinicApiHelper
    {
        /// <summary>
        /// 云诊所服务地址
        /// </summary>
        public static string OuterClinicSystemAPIHost
        {
            get
            {
                string hostconfig = AppSettings.GetConfig("BusinessConfig:AppAPIHost:OuterClinicSystemAPIHost");
                if (hostconfig.EndsWith("/"))
                {
                    return hostconfig;
                }
                return $"{hostconfig}/";
            }
        }

        /// <summary>
        /// 组装请求头 Header
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> HeaderItems(string? token)
        {
            string cacheTokenKey = RedisKey.TokenOfOhClinic;
            //传入token 不为空 则重置token 缓存
            if (!string.IsNullOrWhiteSpace(token))
            {
                RedisHelper.SetString(cacheTokenKey, token);
            }
            else
            {
                token = RedisHelper.GetString(cacheTokenKey);
            }
            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                { "X-Token", token }
            };
            return dic;
        }


        #region ApiRoute
        /// <summary>
        /// 获取诊所Token方法
        /// </summary>
        public readonly static string GetTokenRoute = $"{OuterClinicSystemAPIHost}lease-backend-test/auth/getToken";
        /// <summary>
        /// 诊疗申请结果通知
        /// </summary>
        public readonly static string TreatedApplyResultNotice = $"{OuterClinicSystemAPIHost}lease-backend-test/outpatient/remoteDiagnosisTreatment/modifiedState";
        /// <summary>
        /// 获取诊所患者病历
        /// </summary>
        public readonly static string GetPatMedicalRecord = $"{OuterClinicSystemAPIHost}lease-backend-test/outpatient/medicalRecord/medical/";
        /// <summary>
        /// 同步HIS患者病历
        /// </summary>
        public readonly static string SendHisPatMedicalRecord = $"{OuterClinicSystemAPIHost}lease-backend-test/outpatient/medicalRecord/saveAdd";
        /// <summary>
        /// 同步HIS处方数据
        /// </summary>
        public readonly static string SendHisRecipelData = $"{OuterClinicSystemAPIHost}lease-backend-test/outpatient/medicalRecord/addRecipel";
        #endregion

        #region Method
        public static int ApplyStuExChange(int sqzt)
        {
            switch(sqzt)
            {
                case (int)EmunRemoteTreatedStu.dqr:
                    return (int)EmunClinicRemoteTreatedStu.dqr;
                case (int)EmunRemoteTreatedStu.jzz:
                    return (int)EmunClinicRemoteTreatedStu.jzz;
                case (int)EmunRemoteTreatedStu.yjs:
                    return (int)EmunClinicRemoteTreatedStu.yjs;
                case (int)EmunRemoteTreatedStu.yfy:
                    return (int)EmunClinicRemoteTreatedStu.yfy;
                case (int)EmunRemoteTreatedStu.yth:
                    return (int)EmunClinicRemoteTreatedStu.yth;
                case (int)EmunRemoteTreatedStu.ycx:
                    return (int)EmunClinicRemoteTreatedStu.wtj;
            }
            return -1;
        }
        #endregion

    }
}
