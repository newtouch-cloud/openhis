﻿using log4net;
using log4net.Config;
using System.Collections.Concurrent;

namespace NewtouchHIS.Lib.Base.Logger
{
    /// <summary>
    /// 日志帮助实现类
    /// </summary>
    public class LogHelper : ILoggerHelper
    {
        private readonly ConcurrentDictionary<Type, ILog> Loggers = new ConcurrentDictionary<Type, ILog>();
        private static ILog logger;

        public LogHelper()
        {
            if (logger == null)
            {
                var repository = LogManager.CreateRepository("NETCoreRepository");
                XmlConfigurator.Configure(repository, new FileInfo("Configs/Log4Net.config"));
                logger = LogManager.GetLogger(repository.Name, "Logger");
            }
        }

        /// <summary>
        /// 获取记录器
        /// </summary>
        /// <param name="source">soruce</param>
        /// <returns></returns>
        private ILog GetLogger(Type source)
        {
            if (Loggers.ContainsKey(source))
            {
                return Loggers[source];
            }
            else
            {
                Loggers.TryAdd(source, logger);
                return logger;
            }
        }

        /* Log a message object */
        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        public void Debug(object source, string message)
        {
            Debug(source.GetType(), message);
        }
        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        /// <param name="ps">ps</param>
        public void Debug(object source, string message, params object[] ps)
        {
            Debug(source.GetType(), string.Format(message, ps));
        }
        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        public void Debug(Type source, string message)
        {
            logger = GetLogger(source);
            if (logger.IsDebugEnabled)
            {
                logger.Debug(message);
            }
        }
        /// <summary>
        /// 关键信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        public void Info(object source, object message)
        {
            Info(source.GetType(), message);
        }
        /// <summary>
        /// 关键信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        public void Info(Type source, object message)
        {
            logger = GetLogger(source);
            if (logger.IsInfoEnabled)
            {
                logger.Info(message);
            }
        }
        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        public void Warn(object source, object message)
        {
            Warn(source.GetType(), message);
        }
        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        public void Warn(Type source, object message)
        {
            logger = GetLogger(source);
            if (logger.IsWarnEnabled)
            {
                logger.Warn(message);
            }
        }
        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        public void Error(object source, object message)
        {
            Error(source.GetType(), message);
        }
        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        public void Error(Type source, object message)
        {
            logger = GetLogger(source);
            if (logger.IsErrorEnabled)
            {
                logger.Error(message);
            }
        }
        /// <summary>
        /// 失败信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        public void Fatal(object source, object message)
        {
            Fatal(source.GetType(), message);
        }
        /// <summary>
        /// 失败信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        public void Fatal(Type source, object message)
        {
            logger = GetLogger(source);
            if (logger.IsFatalEnabled)
            {
                logger.Fatal(message);
            }
        }
        /* Log a message object and exception */

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        /// <param name="exception">ex</param>
        public void Debug(object source, object message, Exception exception)
        {
            Debug(source.GetType(), message, exception);
        }
        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        /// <param name="exception">ex</param>
        public void Debug(Type source, object message, Exception exception)
        {
            GetLogger(source).Debug(message, exception);
        }
        /// <summary>
        /// 关键信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        /// <param name="exception">ex</param>
        public void Info(object source, object message, Exception exception)
        {
            Info(source.GetType(), message, exception);
        }
        /// <summary>
        /// 关键信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        /// <param name="exception">ex</param>
        public void Info(Type source, object message, Exception exception)
        {
            GetLogger(source).Info(message, exception);
        }
        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        /// <param name="exception">ex</param>
        public void Warn(object source, object message, Exception exception)
        {
            Warn(source.GetType(), message, exception);
        }
        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        /// <param name="exception">ex</param>
        public void Warn(Type source, object message, Exception exception)
        {
            GetLogger(source).Warn(message, exception);
        }
        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        /// <param name="exception">ex</param>
        public void Error(object source, object message, Exception exception)
        {
            Error(source.GetType(), message, exception);
        }
        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        /// <param name="exception">ex</param>
        public void Error(Type source, object message, Exception exception)
        {
            GetLogger(source).Error(message, exception);
        }
        /// <summary>
        /// 失败信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        /// <param name="exception">ex</param>
        public void Fatal(object source, object message, Exception exception)
        {
            Fatal(source.GetType(), message, exception);
        }
        /// <summary>
        /// 失败信息
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="message">message</param>
        /// <param name="exception">ex</param>
        public void Fatal(Type source, object message, Exception exception)
        {
            GetLogger(source).Fatal(message, exception);
        }
    }
}
