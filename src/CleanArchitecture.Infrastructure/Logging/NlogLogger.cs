using CleanArchitecture.Core.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Logging
{
    public class NLogLogger<T> : ILogger<T>
    {
        private readonly Logger _nLogger;

        public NLogLogger()
        {
            _nLogger =
                LogManager.GetLogger(typeof(T).Name);
        }

        public void Log(Core.Logging.LogLevel logLevel, string message, Exception ex = null)
        {
            switch (logLevel)
            {
                case Core.Logging.LogLevel.Debug:
                    LogDebug(message);
                    break;
                case Core.Logging.LogLevel.Info:
                    LogInfo(message);
                    break;
                case Core.Logging.LogLevel.Warning:
                    LogWarning(message);
                    break;
                case Core.Logging.LogLevel.Error:
                    LogError(ex, message);
                    break;
                case Core.Logging.LogLevel.Trace:
                    LogTrace(message);
                    break;
            }
        }

        public void LogDebug(string message)
        {
            _nLogger.Debug(message);
        }

        public void LogError(Exception ex, string message)
        {
            _nLogger.Error(ex, message);
        }

        public void LogInfo(string message)
        {
            _nLogger.Info(message);
        }

        public void LogWarning(string message)
        {
            _nLogger.Warn(message);
        }

        public void LogTrace(string message)
        {
            _nLogger.Trace(message);
        }
    }
}
