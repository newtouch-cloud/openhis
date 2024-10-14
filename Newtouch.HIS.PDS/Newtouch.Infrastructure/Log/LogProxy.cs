using System;
using System.Collections.Generic;
using Newtouch.Common.Operator;
using NLog.Client;
using NLog.Contact.DTO;

namespace Newtouch.Infrastructure.Log
{
    public class LogProxy 
    {
        private static ILogV2 _log;
        #region 单例

        /// <summary>
        /// 服务名称
        /// </summary>
        private static string _serviceName = string.Empty;

        private static LogProxy Instance = new LogProxy();

        private LogProxy() { }

        public static LogProxy GetLogger(string serviceName)
        {
            _serviceName = serviceName;
            var organizeId = "";
            try
            {
                organizeId = OperatorProvider.GetCurrent().OrganizeId;
            }
            catch (Exception e)
            {
                // ignored
            }

            _log = LogManageV2.GetLogger(_serviceName, organizeId);
            Instance = Instance ?? new LogProxy();
            return Instance;
        }

        #endregion

        public void Debug(string title, object message, System.Exception exception = null, Dictionary<string, string> tags = null)
        {
            _log.Debug(title, message, exception, tags);
        }

        public void Error(string title, object message, System.Exception exception = null, Dictionary<string, string> tags = null)
        {
            _log.Error(title, message, exception, tags);
        }

        public void Fatal(string title, object message, System.Exception exception = null, Dictionary<string, string> tags = null)
        {
            _log.Fatal(title, message, exception, tags);
        }

        public void Info(string title, object message, System.Exception exception = null, Dictionary<string, string> tags = null)
        {
            _log.Info(title, message, exception, tags);
        }

        public void Warn(string title, object message, System.Exception exception = null, Dictionary<string, string> tags = null)
        {
            _log.Warn(title, message, exception, tags);
        }

        public void Moniter(string title, object message, MoniterDTO moniter, Dictionary<string, string> tags = null)
        {
            _log.Moniter(title, message, moniter, tags);
        }

        public void ChangeRecord(string title, object message, Exception exception = null, Dictionary<string, string> tags = null)
        {
            throw new NotImplementedException();
        }
    }
}
