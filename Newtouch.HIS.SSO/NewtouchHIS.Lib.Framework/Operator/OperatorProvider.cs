using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;

namespace NewtouchHIS.Lib.Framework.Operator
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
            _appId = ConfigInitHelper.SysConfig.AppId ?? AppSettings.GetConfig("AppId");

            if (!string.IsNullOrWhiteSpace(_appId))
            {
                _loginUserKey = _appId + "_" + _loginUserKey;
            }

            //在线时长
            _LoginStatusKeepMinute = ConfigInitHelper.SysConfig.LoginStatusKeepMinute ?? _LoginStatusKeepMinute;

            //开启登录限制
            _enableMultipleLoginLimit = ConfigInitHelper.SysConfig.EnableMultipleLoginLimit ?? _enableMultipleLoginLimit;

            //
            _multipleLoginLimit_Count = ConfigInitHelper.SysConfig.MultipleLoginLimit_Count ?? _multipleLoginLimit_Count;
        }

        /// <summary>
        /// 获取当前登录身份
        /// </summary>
        /// <returns></returns>
        public static OperatorModel? GetCurrent(bool withInnerCatch = false)
        {
            string? exception = string.Empty;
            return GetCurrent(ref exception, withInnerCatch);
        }

        /// <summary>
        /// 获取当前登录身份（异常类型：SIDELINED-已经被挤掉了）
        /// </summary>
        /// <param name="exception">异常类型：SIDELINED-已经被挤掉了</param>
        /// <param name="withInnerCatch">是否不向外抛异常</param>
        /// <returns></returns>
        public static OperatorModel? GetCurrent(ref string? exception, bool withInnerCatch = false)
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
        private static OperatorModel? GetCurrent(ref string? exception)
        {

            HttpContext httpContext = HttpWebHelper.HttpCurrent;
            if (httpContext == null)
            {
                return null;
            }
            if (httpContext != null && httpContext.Items["OPERATOR_GETCURRENT_EXCEPTION"] != null)
            {
                exception = httpContext.Items["OPERATOR_GETCURRENT_EXCEPTION"].ToString();
                return null;
            }

            string? token = string.Empty;
            if (httpContext != null)
            {
                token = HttpWebHelper.GetCookie(_loginUserKey);
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
            string? encryptedResult = null;
            if (!string.IsNullOrWhiteSpace(token))
            {
                encryptedResult = RedisHelper.GetString(token);
            }

            if (string.IsNullOrWhiteSpace(encryptedResult))
            {
                HttpWebHelper.RemoveCookie(_loginUserKey);
                return null;
            }

            OperatorModel? operatorModel = null;

            if (_enableMultipleLoginLimit)
            {
                //禁用了多处登录
                if (encryptedResult == "SIDELINED")
                {
                    //已经被挤掉了
                    exception = "SIDELINED";
                    if (httpContext != null)
                    {
                        httpContext.Items["OPERATOR_GETCURRENT_EXCEPTION"] = exception;
                    }
                    RedisHelper.Remove(token);  //令牌已无效
                    return null;
                }
            }

            //如果取到了，反序列化成对象
            operatorModel = JsonConvert.DeserializeObject<OperatorModel>(encryptedResult); // encryptedResult.ToObject<OperatorModel>();

            if (operatorModel != null &&
                (!string.IsNullOrWhiteSpace(operatorModel.UserId) || !string.IsNullOrWhiteSpace(operatorModel.UserCode))
                )
            {
                //更新过期时间
                RedisHelper.KeyExpire(token, _LoginStatusKeepMinute);

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
        public static OperatorModel? GetCurrent(HttpContext context)
        {
            string? token;
            if (context != null)
            {
                var v = context.Request.Cookies[_loginUserKey];
                token = v != null ? v : "";
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
            string? encryptedResult = null;
            if (!string.IsNullOrWhiteSpace(token))
            {
                encryptedResult = RedisHelper.GetString(token);
            }

            if (string.IsNullOrWhiteSpace(encryptedResult))
            {
                return null;
            }

            //如果取到了，反序列化成对象
            var operatorModel = JsonConvert.DeserializeObject<OperatorModel>(encryptedResult);// encryptedResult.ToObject<OperatorModel>();

            if (operatorModel == null || string.IsNullOrWhiteSpace(operatorModel.UserId) &&
                string.IsNullOrWhiteSpace(operatorModel.UserCode)) return null; //视为无效 身份
            //更新过期时间
            RedisHelper.KeyExpire(token, _LoginStatusKeepMinute);

            return operatorModel;

        }

        /// <summary>
        /// 重写登录状态
        /// </summary>
        /// <param name="operatorModel"></param>
        public static void AddCurrent(OperatorModel operatorModel)
        {
            HttpContext httpContext = HttpWebHelper.HttpCurrent;
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
                var existTokenList = RedisHelper.GetList<string>(keyUserTokenList)
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
                        var itemEncryptedResult = RedisHelper.GetString(itemToken);
                        if (string.IsNullOrWhiteSpace(itemEncryptedResult))
                        {
                            existTokenList.RemoveAt(i); //已经执行过 退出操作 或 已过期了
                            i--;    //下次进for还得是i
                            continue;
                        }

                        if (itemToken != "SIDELINED")
                        {
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
                                RedisHelper.SetString(itemToken, "SIDELINED", new TimeSpan(0, 60, 0));
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
                if (existTokenList != null && existTokenList.Count > 0)
                {
                    existTokenList.Reverse();   //再反转下，使后登录的 回到list后面
                    existTokenList.Add(token);
                    RedisHelper.SetList<string>(keyUserTokenList, existTokenList);
                }

                if (httpContext != null)
                {
                    HttpWebHelper.WriteCookie(_loginUserKey, token);
                }
                else
                {
                    //非BS
                    throw new NotImplementedException();
                }

                RedisHelper.SetString(token, operatorModel.ToJson(), new TimeSpan(0, _LoginStatusKeepMinute, 0));
            }
            else
            {
                //可以多处登录

                if (httpContext != null)
                {
                    HttpWebHelper.WriteCookie(_loginUserKey, token);
                    RedisHelper.SetString(token, operatorModel.ToJson(), new TimeSpan(0, _LoginStatusKeepMinute, 0));
                }
                else
                {
                    //非BS
                    throw new NotImplementedException();
                }

            }
        }

        /// <summary>
        /// 移除 登录状态
        /// </summary>
        public static void RemoveCurrent()
        {
            HttpContext httpContext = HttpWebHelper.HttpCurrent;
            string? token = null;
            if (httpContext != null)
            {
                token = HttpWebHelper.GetCookie(_loginUserKey);
            }
            else
            {
                //非BS
                throw new NotImplementedException();
            }

            if (!string.IsNullOrWhiteSpace(token))
            {
                HttpWebHelper.RemoveCookie(_loginUserKey);
                RedisHelper.Remove(token);
            }
        }

        /// <summary>
        /// 获取当前身份令牌的Key
        /// </summary>
        /// <returns></returns>
        public static string? GetCurrentToken()
        {
            return HttpWebHelper.GetCookie(_loginUserKey);
        }

        /// <summary>
        /// 补充登录cookie 适用于跨子系统页面跳转
        /// </summary>
        public static void WriteLoginUserCookie()
        {
            HttpContext httpContext = HttpWebHelper.HttpCurrent;
            var former = httpContext.Request.Query.FirstOrDefault(p => p.Key == "appId").Value;
            if (string.IsNullOrWhiteSpace(former)) return;
            former = former + "_" + _loginUserKeyPart1;
            var cookie = HttpWebHelper.GetCookie(former);
            if (string.IsNullOrWhiteSpace(cookie)) return;
            var json = RedisHelper.GetString(cookie);
            if (string.IsNullOrWhiteSpace(json)) return;
            var operatorModel = json.ToObject<OperatorModel>();
            if (operatorModel == null || string.IsNullOrWhiteSpace(operatorModel.UserId) && string.IsNullOrWhiteSpace(operatorModel.UserCode)) return;

            var redisKey = Guid.NewGuid().ToString();
            HttpWebHelper.WriteCookie(_loginUserKey, redisKey);
            RedisHelper.SetString(redisKey, json, new TimeSpan(0, _LoginStatusKeepMinute, 0));

        }
    }

}
