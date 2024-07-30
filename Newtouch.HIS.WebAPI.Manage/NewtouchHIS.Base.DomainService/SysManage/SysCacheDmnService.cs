using Newtonsoft.Json;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;

namespace NewtouchHIS.Base.DomainService.SysManage
{
    public class SysCacheDmnService : ISysCacheDmnService
    {
        /// <summary>
        /// 用户身份信息缓存
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        /// <exception cref="FailedException"></exception>
        public async Task<BusResult<UserIdentity>> GetUserDataCache(string accessToken)
        {
            var logStuMin = ConfigInitHelper.SysConfig.LoginStatusKeepMinute ?? 120;
            string encryptedResult = await RedisHelper.GetStringAsync(accessToken);
            if (encryptedResult == null)
            {
                encryptedResult = await RedisHelper.GetStringAsync(accessToken, false);
                if (!string.IsNullOrWhiteSpace(encryptedResult))
                {
                    await RedisHelper.KeyExpireAsync(accessToken, logStuMin, false);
                }
            }
            else
            {
                await RedisHelper.KeyExpireAsync(accessToken, logStuMin);
            }
            if (!string.IsNullOrWhiteSpace(encryptedResult) && encryptedResult != "SIDELINED")
            {
                var jObj = JsonConvert.DeserializeObject<OperatorModel>(encryptedResult);
                if (jObj != null && string.IsNullOrWhiteSpace(jObj.UserCode) && string.IsNullOrWhiteSpace(jObj.OrganizeId))
                {
                    return new BusResult<UserIdentity>
                    {
                        code = ResponseResultCode.SUCCESS,
                        Data = new UserIdentity { Account = jObj.UserCode, OrganizeId = jObj.OrganizeId }
                    };
                }
            }
            return new BusResult<UserIdentity>
            {
                code = ResponseResultCode.FAILOfAuth,
                msg = "未找到用户信息"
            };

        }
    }
}
