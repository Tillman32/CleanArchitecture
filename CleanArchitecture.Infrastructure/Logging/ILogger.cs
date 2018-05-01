using System;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Logging
{
    public interface ILogger
    {
        Task LogAsync(LogLevel logLevel, string message);
        Task LogDebugAsync(string message);
        Task LogInfoAsync(string message);
        Task LogWarningAsync(string message);
        Task LogErrorAsync(string message, Exception exception = null);
    }
}