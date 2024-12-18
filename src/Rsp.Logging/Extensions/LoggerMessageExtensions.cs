using System;
using Microsoft.Extensions.Logging;
using Rsp.Logging.Domain;

namespace Rsp.Logging.Extensions;

/// <summary>
/// Extension methods for the <see cref="ILogger"/> interface.
/// </summary>
internal static partial class LoggerMessageExtensions
{
    /// <summary>
    /// Logs a trace message
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="method">The method name.</param>
    /// <param name="message">The message to log.</param>
    [LoggerMessage(EventId = EventIds.Trace, Level = LogLevel.Trace, Message = "{Method} {Message}")]
    internal static partial void LogVerbose(this ILogger logger, string method, string message);

    /// <summary>
    /// Logs a trace message with parameters
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="method">The method name.</param>
    /// <param name="parameters">Comma-separated list of parameters.</param>
    /// <param name="message">The message to log.</param>
    [LoggerMessage(EventId = EventIds.Trace, Level = LogLevel.Trace, Message = "{Method} {Parameters} {Message}")]
    internal static partial void LogVerbose(this ILogger logger, string method, string parameters, string message);

    /// <summary>
    /// Logs an informational message
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="method">The method name.</param>
    /// <param name="message">The message to log.</param>
    [LoggerMessage(EventId = EventIds.Information, Level = LogLevel.Information, Message = "{Method} {Message}")]
    internal static partial void LogInformation(this ILogger logger, string method, string message);

    /// <summary>
    /// Logs an informational message with parameters.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="method">The method name.</param>
    /// <param name="parameters">Comma-separated list of parameters.</param>
    /// <param name="message">The message to log.</param>
    [LoggerMessage(EventId = EventIds.Information, Level = LogLevel.Information, Message = "{Method} {Parameters} {Message}")]
    internal static partial void LogInformation(this ILogger logger, string method, string parameters, string message);

    /// <summary>
    /// Logs a warning message
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="method">The method name.</param>
    /// <param name="message">The message to log.</param>
    /// <param name="exception">The captured exception.</param>
    [LoggerMessage(EventId = EventIds.Warning, Level = LogLevel.Warning, Message = "{Method} {Message}")]
    internal static partial void LogWarning(this ILogger logger, string method, string message, Exception? exception);

    /// <summary>
    /// Logs a warning message with parameters
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="method">The method name.</param>
    /// <param name="parameters">Comma-separated list of parameters.</param>
    /// <param name="message">The message to log.</param>
    /// <param name="exception">The captured exception.</param>
    [LoggerMessage(EventId = EventIds.Warning, Level = LogLevel.Warning, Message = "{Method} {Parameters} {Message}")]
    internal static partial void LogWarning(this ILogger logger, string method, string parameters, string message, Exception? exception);

    /// <summary>
    /// Logs that a method call failed.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="method">The method name.</param>
    /// <param name="errorCode">The user-defined error code.</param>
    /// <param name="message">The message to log.</param>
    [LoggerMessage(EventId = EventIds.Error, Level = LogLevel.Error, Message = "{Method} {ErrorCode} {Message}")]
    internal static partial void LogError(this ILogger logger, string method, string errorCode, string message);

    /// <summary>
    /// Logs that a method call failed with parameters.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="method">The method name.</param>
    /// <param name="parameters">Comma-separated list of parameters.</param>
    /// <param name="errorCode">The user-defined error code.</param>
    /// <param name="message">The message to log.</param>
    [LoggerMessage(EventId = EventIds.ErrorWithParams, Level = LogLevel.Error, Message = "{Method} {Parameters} {ErrorCode} {Message}")]
    internal static partial void LogError(this ILogger logger, string method, string parameters, string errorCode, string message);

    /// <summary>
    /// Logs that a method call failed with an exception.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="method">The method name.</param>
    /// <param name="errorCode">The user-defined error code.</param>
    /// <param name="message">The message to log.</param>
    /// <param name="exceptionMessage">The message generated by the exception.</param>
    /// <param name="exception">The captured exception.</param>
    /// <param name="stackTrace">The stack trace of the exception.</param>
    [LoggerMessage(EventId = EventIds.Exception, Level = LogLevel.Error, Message = "{Method} {ErrorCode} {Message} {ExceptionMessage} {StackTrace}")]
    internal static partial void LogError(this ILogger logger, string method, string errorCode, string message, string exceptionMessage, Exception exception, string stackTrace = "");

    /// <summary>
    /// Logs that a method call failed with parameters and an exception.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="method">The method name.</param>
    /// <param name="parameters">Comma-separated list of parameters.</param>
    /// <param name="errorCode">The user-defined error code.</param>
    /// <param name="message">The message to log.</param>
    /// <param name="exceptionMessage">The message generated by the exception.</param>
    /// <param name="exception">The captured exception.</param>
    /// <param name="stackTrace">The stack trace of the exception.</param>
    [LoggerMessage(EventId = EventIds.ExceptionWithParams, Level = LogLevel.Error, Message = "{Method} {Parameters} {ErrorCode} {Message} {ExceptionMessage} {StackTrace}")]
    internal static partial void LogError(this ILogger logger, string method, string parameters, string errorCode, string message, string exceptionMessage, Exception exception, string stackTrace = "");
}