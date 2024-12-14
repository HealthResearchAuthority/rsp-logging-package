using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using Rsp.Logging.Domain;

namespace Rsp.Logging.Extensions;

/// <summary>
/// Extensions methods for the <see cref="ILogger"/> interface.
/// </summary>
public static class LoggingExtensions
{
    private const string completed = nameof(completed);

    private const string called = nameof(called);

    /// <summary>
    /// Logs that a method was called.
    /// </summary>
    /// <param name="logger"><see cref="ILogger" /></param>
    /// <param name="logLevel">Log level <see cref="LogLevel"/></param>
    /// <param name="method">Method name</param>
    public static void LogMethodStarted(this ILogger logger, LogLevel logLevel = LogLevel.Trace, [CallerMemberName] string method = "")
    {
        var eventId = logLevel switch
        {
            LogLevel.Trace => EventIds.Trace,
            LogLevel.Information => EventIds.Information,
            _ => 0
        };

        logger.LogMessage(logLevel, eventId, method, called);
    }

    /// <summary>
    /// Logs that a method was called.
    /// </summary>
    /// <param name="logger"><see cref="ILogger" /></param>
    /// <param name="parameters">Comma separated list of parameters</param>
    /// <param name="logLevel">Log level <see cref="LogLevel"/></param>
    /// <param name="method">Method name</param>
    public static void LogMethodStarted(this ILogger logger, string parameters, LogLevel logLevel = LogLevel.Trace, [CallerMemberName] string method = "")
    {
        var eventId = logLevel switch
        {
            LogLevel.Trace => EventIds.Trace,
            LogLevel.Information => EventIds.Information,
            _ => 0
        };

        logger.LogMessage(logLevel, eventId, method, parameters, called);
    }

    /// <summary>
    /// Logs that method call is completed.
    /// </summary>
    /// <param name="logger"><see cref="ILogger" /></param>
    /// <param name="logLevel">Log level <see cref="LogLevel"/></param>
    /// <param name="exception">Captured Exception</param>
    /// <param name="method">Method name</param>
    public static void LogMethodCompleted(this ILogger logger, LogLevel logLevel = LogLevel.Trace, Exception? exception = null, [CallerMemberName] string method = "")
    {
        var eventId = logLevel switch
        {
            LogLevel.Trace => EventIds.Trace,
            LogLevel.Information => EventIds.Information,
            _ => 0
        };

        logger.LogMessage(logLevel, eventId, method, completed);
    }

    /// <summary>
    /// Logs that method call is completed.
    /// </summary>
    /// <param name="logger"><see cref="ILogger" /></param>
    /// <param name="parameters">Comma separated list of parameters</param>
    /// <param name="logLevel">Log level <see cref="LogLevel"/></param>
    /// <param name="method">Method name</param>
    public static void LogMethodCompleted(this ILogger logger, string parameters, LogLevel logLevel = LogLevel.Trace, [CallerMemberName] string method = "")
    {
        var eventId = logLevel switch
        {
            LogLevel.Trace => EventIds.Trace,
            LogLevel.Information => EventIds.Information,
            _ => 0
        };

        logger.LogMessage(logLevel, eventId, method, parameters, completed);
    }

    /// <summary>
    /// Formats and writes a trace log message.
    /// </summary>
    /// <param name="logger"><see cref="ILogger"/></param>
    /// <param name="message">Message to log</param>
    /// <param name="method">Method name</param>
    public static void LogTrace(this ILogger logger, string message, [CallerMemberName] string method = "")
    {
        logger.LogMessage(LogLevel.Trace, EventIds.Trace, method, message);
    }

    /// <summary>
    /// Formats and writes a trace log message.
    /// </summary>
    /// <param name="logger"><see cref="ILogger"/></param>
    /// <param name="parameters">Comma separated list of parameters</param>
    /// <param name="message">Message to log</param>
    /// <param name="method">Method name</param>
    public static void LogTrace(this ILogger logger, string parameters, string message, [CallerMemberName] string method = "")
    {
        logger.LogMessage(LogLevel.Trace, EventIds.Trace, method, parameters, message);
    }

    /// <summary>
    /// Using <see cref="LoggerMessage"/> for High Performance logging, formats and writes an informational log message.
    /// </summary>
    /// <param name="logger"><see cref="ILogger"/></param>
    /// <param name="message">Message to log</param>
    /// <param name="method">Method name</param>
    public static void LogInformationHp(this ILogger logger, string message, [CallerMemberName] string method = "")
    {
        logger.LogMessage(LogLevel.Information, EventIds.Information, method, message);
    }

    /// <summary>
    /// Using <see cref="LoggerMessage"/> for High Performance logging, formats and writes an informational log message.
    /// </summary>
    /// <param name="logger"><see cref="ILogger"/></param>
    /// <param name="parameters">Comma separated list of parameters</param>
    /// <param name="message">Message to log</param>
    /// <param name="method">Method name</param>
    public static void LogInformationHp(this ILogger logger, string parameters, string message, [CallerMemberName] string method = "")
    {
        logger.LogMessage(LogLevel.Information, EventIds.Information, method, parameters, message);
    }

    /// <summary>
    /// Using <see cref="LoggerMessage"/> for High Performance logging, formats and writes a warning log message.
    /// </summary>
    /// <param name="logger"><see cref="ILogger"/></param>
    /// <param name="message">Message to log</param>
    /// <param name="exception">Captured Exception</param>
    /// <param name="method">Method name</param>
    public static void LogWarningHp(this ILogger logger, string message, Exception? exception = null, [CallerMemberName] string method = "")
    {
        logger.LogMessage(LogLevel.Warning, EventIds.Warning, method, message);
    }

    /// <summary>
    /// Using <see cref="LoggerMessage"/> for High Performance logging, formats and writes a warning log message.
    /// </summary>
    /// <param name="logger"><see cref="ILogger"/></param>
    /// <param name="parameters">Comma separated list of parameters</param>
    /// <param name="message">Message to log</param>
    /// <param name="exception">Captured Exception</param>
    /// <param name="method">Method name</param>
    public static void LogWarningHp(this ILogger logger, string parameters, string message, Exception? exception = null, [CallerMemberName] string method = "")
    {
        logger.LogMessage(LogLevel.Warning, EventIds.Warning, method, parameters, message);
    }

    /// <summary>
    /// Using <see cref="LoggerMessage"/> for High Performance logging, logs that method call is failed. The exception will be split into parts
    /// </summary>
    /// <param name="logger"><see cref="ILogger"/></param>
    /// <param name="errorCode">User defined error code</param>
    /// <param name="message">Message to log</param>
    /// <param name="exception">Captured Exception</param>
    /// <param name="method">Method name</param>
    public static void LogErrorHp(this ILogger logger, string errorCode, string message, Exception? exception = null, [CallerMemberName] string method = "")
    {
        if (exception == null)
        {
            logger.LogError(method, errorCode, message);
        }
        else
        {
            var stackTrace = exception.StackTrace == null ? "No Stack Trace" : exception.StackTrace.Replace(Environment.NewLine, @"\r\n").AsSpan();
            var exceptionMessage = exception.Message.Replace(Environment.NewLine, @"\r\n").AsSpan();
            logger.LogError(method, errorCode, message, exceptionMessage.ToString(), exception, stackTrace.ToString());
        }
    }

    /// <summary>
    /// Using <see cref="LoggerMessage"/> for High Performance logging, logs that method call is failed.
    /// </summary>
    /// <param name="logger"><see cref="ILogger"/></param>
    /// <param name="parameters">Comma separated list of parameters</param>
    /// <param name="errorCode">User defined error code</param>
    /// <param name="message">Message to log</param>
    /// <param name="exception">Captured Exception</param>
    /// <param name="method">Method name</param>
    public static void LogErrorHp(this ILogger logger, string parameters, string errorCode, string message, Exception? exception = null, [CallerMemberName] string method = "")
    {
        if (exception == null)
        {
            logger.LogError(method, parameters, errorCode, message);
        }
        else
        {
            var stackTrace = exception.StackTrace == null ? "No Stack Trace" : exception.StackTrace.Replace(Environment.NewLine, @"\r\n").AsSpan();
            var exceptionMessage = exception.Message.Replace(Environment.NewLine, @"\r\n").AsSpan();
            logger.LogError(method, parameters, errorCode, message, exceptionMessage.ToString(), exception, stackTrace.ToString());
        }
    }
}