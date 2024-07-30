using Microsoft.Extensions.Logging;

namespace NewtouchHIS.Lib.Base.Logger
{
    public interface ILogTrackHelper
    {
        void InfoMonitor(string? message, params object?[] args);
        void InfoMonitor(Exception exception, string? message, params object?[] args);
        void Info(string? message, params object?[] args);
        void Info(Exception exception, string? message, params object?[] args);
        void Error(Exception? exception, string? message, params object?[] args);
        void Error(string? message, params object?[] args);
        void Warning(Exception? exception, string? message, params object?[] args);
        void Warning(string? message, params object?[] args);
    }
    public class LogTrackHelper<T> : ILogTrackHelper
    {
        private readonly ILogger _logger;
        #region private init
        /// <summary>
        /// 启用日志跟踪
        /// </summary>
        private bool EnableLoggingMonitorAttr { get { return ConfigInitHelper.SysConfig!.EnableLoggingMonitorAttr ?? false; } }

        #endregion
        public LogTrackHelper(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void InfoMonitor(string? message, params object?[] args)
        {
            if (EnableLoggingMonitorAttr)
            {
                _logger.LogInformation(message, args);
            }
        }
        public void InfoMonitor(Exception exception, string? message, params object?[] args)
        {
            if (EnableLoggingMonitorAttr)
            {
                _logger.LogInformation(exception, message, args);
            }

        }
        public void Info(string? message, params object?[] args)
        {
            _logger.LogInformation(message, args);
        }
        public void Info(Exception exception, string? message, params object?[] args)
        {
            _logger.LogInformation(exception, message, args);
        }
        public void Error(Exception? exception, string? message, params object?[] args)
        {
            _logger.LogError(exception, message, args);
        }
        public void Error(string? message, params object?[] args)
        {
            _logger.LogError(message, args);
        }
        public void Warning(Exception? exception, string? message, params object?[] args)
        {
            _logger.LogWarning(exception, message, args);
        }
        public void Warning(string? message, params object?[] args)
        {
            _logger.LogWarning(message, args);
        }
    }
}
