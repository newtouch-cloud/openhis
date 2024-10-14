using System;
using System.Collections.Generic;
using Newtouch.Core.Common.Utils;
using NLog.Contact.DTO;

namespace Newtouch.Infrastructure.Log
{
    /// <summary>
    /// 日志组件
    /// </summary>
    public static class LogCore
    {
        private static LogProxy Logger = LogProxy.GetLogger(string.IsNullOrWhiteSpace(ConfigurationHelper.GetAppConfigValue("ServiceName")) ? "newtouch.pds" : ConfigurationHelper.GetAppConfigValue("ServiceName"));
        public static void Info<T>(string title, T message, Dictionary<string, string> addInfo = null)
        {
            Logger.Info(title, message, tags: addInfo);
        }

        internal static void Debug(string title, string message = "", Dictionary<string, string> addInfo = null)
        {
            Logger.Debug(title, message, tags: addInfo);
        }

        public static void Error(string title, Exception ex = null, string message = "", Dictionary<string, string> addInfo = null)
        {
            if (ex != null && string.IsNullOrWhiteSpace(message))
            {
                message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            Logger.Error(title, message, ex, addInfo);
        }

        public static void Fatal(string title, Exception ex = null, string message = "", Dictionary<string, string> addInfo = null)
        {
            if (ex != null && string.IsNullOrWhiteSpace(message))
            {
                message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            Logger.Fatal(title, message, ex, addInfo);
        }

        public static void Warn(string title, Exception ex = null, string message = "", Dictionary<string, string> addInfo = null)
        {
            Logger.Warn(title, message, ex, addInfo);
        }

        public static void Moniter(string title, object message, MoniterDTO moniter, Dictionary<string, string> addInfo = null)
        {
            Logger.Moniter(title, message, moniter, addInfo);
        }
    }
}
