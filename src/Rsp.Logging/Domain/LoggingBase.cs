using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Rsp.Logging.Domain;

/// <summary>
/// Base class for logging interceptors.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="LoggingBase"/> class.
/// </remarks>
/// <param name="loggerFactory">The logger factory to create loggers.</param>
/// <param name="configuration">The configuration to retrieve settings.</param>
public class LoggingBase(ILoggerFactory loggerFactory, IConfiguration configuration)
{
    private static readonly ConcurrentDictionary<Type, Lazy<ILogger>> LoggerCache = new();

    /// <summary>
    /// Gets the log level for method start logging from configuration.
    /// </summary>
    protected internal LogLevel StartLogLevel => GetLogLevel(AppSettings.StartLogLevel);

    /// <summary>
    /// Gets the log level for method finish logging from configuration.
    /// </summary>
    protected internal LogLevel FinishLogLevel => GetLogLevel(AppSettings.FinishLogLevel);

    /// <summary>
    /// Retrieves or creates a logger for the specified target type.
    /// </summary>
    /// <param name="targetType">The type of the target class.</param>
    /// <returns>An <see cref="ILogger"/> instance for the specified type.</returns>
    protected ILogger GetLogger(Type targetType)
    {
        return LoggerCache.GetOrAdd(targetType, type => new Lazy<ILogger>(() => loggerFactory.CreateLogger(type))).Value;
    }

    /// <summary>
    /// Helper method to parse log level from configuration.
    /// </summary>
    /// <param name="key">The configuration key for the log level.</param>
    /// <returns>The parsed <see cref="LogLevel"/> or <see cref="LogLevel.None"/> if parsing fails.</returns>
    private LogLevel GetLogLevel(string key)
    {
        return Enum.TryParse(configuration[key], out LogLevel logLevel) ? logLevel : LogLevel.None;
    }
}