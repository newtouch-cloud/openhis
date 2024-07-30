using Microsoft.AspNetCore.Http;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Lib.Services.Identity
{
    /// <summary>
    /// 登录用户 身份信息 操作类
    /// </summary>
    public class OperatorProvider
    {
        /// <summary>
        /// 应用Id（在config配置 AppId）
        /// </summary>
        public static string _appId { get; private set; }


        private static string _loginUserKeyPart1 = "newtouch_loginuserkey_2016";

        /// <summary>
        /// 关联key，key能换群Model
        /// </summary>
        private static string _loginUserKey = _loginUserKeyPart1;

        /// <summary>
        /// 组装本地Cookie的
        /// </summary>
        public static string LocalhostCookieKey
        {
            get
            {
                return _loginUserKey;
            }
        }

        /// <summary>
        /// 登录状态 的 维持 时间
        /// </summary>
        private static int _LoginStatusKeepMinute = 30;

        /// <summary>
        /// 启用‘禁用多处登录’的限制
        /// </summary>
        private static bool _enableMultipleLoginLimit = false;

        /// <summary>
        /// 启用‘禁用多处登录’的限制 - 数量
        /// </summary>
        private static int _multipleLoginLimit_Count = 1;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static OperatorProvider()
        {
            _appId = ConfigurationHelper.GetAppConfigValue("AppId");

            if (!string.IsNullOrWhiteSpace(_appId))
            {
                _loginUserKey = _appId + "_" + _loginUserKey;
            }

            //
            _LoginStatusKeepMinute = ConfigurationHelper.GetAppConfigIntValue("LoginStatusKeepMinute", defaultValue: _LoginStatusKeepMinute);

            //
            _enableMultipleLoginLimit = ConfigurationHelper.GetAppConfigBoolValue("EnableMultipleLoginLimit", defaultValue: _enableMultipleLoginLimit) ?? false;

            //
            _multipleLoginLimit_Count = ConfigurationHelper.GetAppConfigIntValue("MultipleLoginLimit_Count", defaultValue: _multipleLoginLimit_Count);
        }

        /// <summary>
        /// 获取当前登录身份
        /// </summary>
        /// <returns></returns>
        public static OperatorModel GetCurrent(bool withInnerCatch = false)
        {
            string exception = null;
            return GetCurrent(ref exception, withInnerCatch);
        }

        /// <summary>
        /// 获取当前登录身份（异常类型：SIDELINED-已经被挤掉了）
        /// </summary>
        /// <param name="exception">异常类型：SIDELINED-已经被挤掉了</param>
        /// <param name="withInnerCatch">是否不向外抛异常</param>
        /// <returns></returns>
        public static OperatorModel GetCurrent(ref string exception, bool withInnerCatch = false)
        {
            if (withInnerCatch)
            {
                try
                {
                    return GetCurrent(ref exception);
                }
                catch
                {
                    exception = "INNER_EXCEPTION";
                    return null;
                }
            }
            else
            {
                return GetCurrent(ref exception);
            }
        }

        /// <summary>
        /// 获取当前登录身份
        /// </summary>
        /// <param name="exception">异常类型：SIDELINED-已经被挤掉了</param>
        /// <returns></returns>
        private static OperatorModel GetCurrent(ref string exception)
        {
            if (HttpContext.Current != null && HttpContext.Current.Items["OPERATOR_GETCURRENT_EXCEPTION"] != null)
            {
                exception = HttpContext.Current.Items["OPERATOR_GETCURRENT_EXCEPTION"].ToString();
                return null;
            }

            string token = null;
            if (HttpContext.Current != null)
            {
                token = WebHelper.GetCookie(_loginUserKey);
            }
            else
            {
                //非BS
                throw new NotImplementedException();
            }

            if (string.IsNullOrWhiteSpace(token))
            {
                return null;
            }

            //去取加密后的结果
            string encryptedResult = null;
            if (!string.IsNullOrWhiteSpace(token))
            {
                encryptedResult = RedisHelper.StringGet(token);
            }

            if (string.IsNullOrWhiteSpace(encryptedResult))
            {
                return null;
            }

            OperatorModel operatorModel = null;

            if (_enableMultipleLoginLimit)
            {
                //禁用了多处登录
                if (encryptedResult == "SIDELINED")
                {
                    //已经被挤掉了
                    exception = "SIDELINED";
                    if (HttpContext.Current != null)
                    {
                        HttpContext.Current.Items["OPERATOR_GETCURRENT_EXCEPTION"] = exception;
                    }
                    RedisHelper.Remove(token);  //令牌已无效
                    return null;
                }
            }

            //如果取到了，反序列化成对象
            operatorModel = encryptedResult.ToObject<OperatorModel>();

            if (operatorModel != null &&
                (!string.IsNullOrWhiteSpace(operatorModel.UserId) || !string.IsNullOrWhiteSpace(operatorModel.UserCode))
                )
            {
                //更新过期时间
                RedisHelper.KeyExpire(token, new TimeSpan(0, _LoginStatusKeepMinute, 0));

                return operatorModel;
            }
            else
            {
                return null;    //视为无效 身份
            }
        }

        /// <summary>
        /// 获取当前登录身份
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static OperatorModel GetCurrent(HttpContext context)
        {
            string token;
            if (context != null)
            {
                var v = context.Request.Cookies[_loginUserKey];
                token = v != null ? v.Value : "";
            }
            else
            {
                //非BS
                throw new NotImplementedException();
            }

            if (string.IsNullOrWhiteSpace(token))
            {
                return null;
            }

            //去取加密后的结果
            string encryptedResult = null;
            if (!string.IsNullOrWhiteSpace(token))
            {
                encryptedResult = RedisHelper.StringGet(token);
            }

            if (string.IsNullOrWhiteSpace(encryptedResult))
            {
                return null;
            }

            //如果取到了，反序列化成对象
            var operatorModel = encryptedResult.ToObject<OperatorModel>();

            if (operatorModel == null || string.IsNullOrWhiteSpace(operatorModel.UserId) &&
                string.IsNullOrWhiteSpace(operatorModel.UserCode)) return null; //视为无效 身份
            //更新过期时间
            RedisHelper.KeyExpire(token, new TimeSpan(0, _LoginStatusKeepMinute, 0));

            return operatorModel;

        }

        /// <summary>
        /// 重写登录状态
        /// </summary>
        /// <param name="operatorModel"></param>
        public static void AddCurrent(OperatorModel operatorModel)
        {
            if (operatorModel == null ||
                (string.IsNullOrWhiteSpace(operatorModel.UserId) && string.IsNullOrWhiteSpace(operatorModel.UserCode))
                )
            {
                return;     //视为无效 身份
            }

            //token直接对应UserInfo Model
            var token = Guid.NewGuid().ToString();

            if (string.IsNullOrWhiteSpace(operatorModel.token))
            {
                operatorModel.token = token;
            }

            if (_enableMultipleLoginLimit)
            {
                //禁用了多处登录

                //应用+UserId+登录账号 唯一 系统该用户，已登录的TokenList
                var keyUserTokenList = string.Format("list:Token_WithUser_{0}_{1}_{2}"
                    , _appId, operatorModel.UserId, operatorModel.UserCode);
                var existTokenList = RedisHelper.Get<List<string>>(keyUserTokenList)
                    ?? new List<string>();
                var limitCount = _multipleLoginLimit_Count;  //同一用户 最大同时有效在线客户端数
                if (existTokenList != null && existTokenList.Count >= limitCount) //可以做到控制同时登录状态的数量
                {
                    var availablyOnlineCount = 0;    //同一用户 已经统计到的有效在线的登录客户端
                    existTokenList.Reverse();   //先反转下，使后登录的在前面
                    //挤掉多余的
                    for (var i = 0; i < existTokenList.Count; i++)
                    {
                        var itemToken = existTokenList[i];
                        var itemEncryptedResult = RedisHelper.StringGet(itemToken);
                        if (string.IsNullOrWhiteSpace(itemEncryptedResult))
                        {
                            existTokenList.RemoveAt(i); //已经执行过 退出操作 或 已过期了
                            i--;    //下次进for还得是i
                            continue;
                        }

                        if (itemToken != "SIDELINED")
                        {
                            //availablyOnlineCount++;
                            if ((availablyOnlineCount + 1) < limitCount)    //+1是因为最后一个名额当前token
                            {
                                //这是一有效在线客户端
                                availablyOnlineCount++;
                                continue;   //尚未达到同时在线客户端数 的限制
                            }
                            else
                            {
                                //已达到最大有效数
                                //做挤掉处理

                                //更新为已经被挤掉了
                                RedisHelper.StringSet(itemToken, "SIDELINED", new TimeSpan(0, 60, 0));
                                continue;
                            }
                        }
                    }
                    if (existTokenList.Count > limitCount + 20)
                    {
                        //被挤掉之后 没有GetCurrent、退出操作。 导致existTokenList不能RemoveAt
                        //existTokenList.RemoveRange(5, limitCount + 20 - 5);  //清掉前面的（最新登录的被reverse到前面了）
                        existTokenList.RemoveRange(limitCount, 20 - 1);  //清掉前面的（最新登录的被reverse到前面了）
                    }
                }

                existTokenList.Reverse();   //再反转下，使后登录的 回到list后面
                existTokenList.Add(token);
                RedisHelper.Set<List<string>>(keyUserTokenList, existTokenList);

                if (HttpContext.Current != null)
                {
                    WebHelper.WriteCookie(_loginUserKey, token);
                }
                else
                {
                    //非BS
                    throw new NotImplementedException();
                }

                RedisHelper.StringSet(token, operatorModel.ToJson(), new TimeSpan(0, 60, 0));
            }
            else
            {
                //可以多处登录

                if (HttpContext.Current != null)
                {
                    WebHelper.WriteCookie(_loginUserKey, token);
                }
                else
                {
                    //非BS
                    throw new NotImplementedException();
                }

                RedisHelper.StringSet(token, operatorModel.ToJson(), new TimeSpan(0, 60, 0));
            }
        }

        /// <summary>
        /// 移除 登录状态
        /// </summary>
        public static void RemoveCurrent()
        {
            string token = null;
            if (HttpContext.Current != null)
            {
                token = WebHelper.GetCookie(_loginUserKey);
            }
            else
            {
                //非BS
                throw new NotImplementedException();
            }

            if (!string.IsNullOrWhiteSpace(token))
            {
                RedisHelper.Remove(token);
            }
        }

        /// <summary>
        /// 获取当前身份令牌的Key
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentToken()
        {
            return WebHelper.GetCookie(_loginUserKey);
        }

        /// <summary>
        /// 补充登录cookie 适用于跨子系统页面跳转
        /// </summary>
        public static void WriteLoginUserCookie()
        {
            try
            {
                var former = HttpContext.Current.Request.QueryString["appId"];
                if (string.IsNullOrWhiteSpace(former)) return;
                former = former + "_" + _loginUserKeyPart1;
                var cookie = WebHelper.GetCookie(former);
                if (string.IsNullOrWhiteSpace(cookie)) return;
                var json = RedisHelper.StringGet(cookie);
                if (string.IsNullOrWhiteSpace(json)) return;
                var operatorModel = json.ToObject<OperatorModel>();
                if (operatorModel == null || string.IsNullOrWhiteSpace(operatorModel.UserId) && string.IsNullOrWhiteSpace(operatorModel.UserCode)) return;

                var redisKey = Guid.NewGuid().ToString();
                WebHelper.WriteCookie(_loginUserKey, redisKey);
                RedisHelper.StringSet(redisKey, json, new TimeSpan(0, 60, 0));
            }
            catch (Exception e)
            {
                AppLogger.Instance.Error("WriteLoginUserCookie error", e);
            }
        }
    }
}
