using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;

namespace Rsp.Logging.Extensions;

/// <summary>
/// Extensions methods for the <see cref="ILogger"/> interface.
/// </summary>
public static class LoggingExtensions
{
    private const string completed = nameof(completed);

    private const string called = nameof(called);

    private static readonly Action<ILogger, string?, string, string, Exception?> TraceActionWithParams =
        LoggerMessage.Define<string?, string, string>(LogLevel.Trace, 103, "{Method} {Parameters} {Message}");

    private static readonly Action<ILogger, string?, string, Exception?> TraceAction =
        LoggerMessage.Define<string?, string>(LogLevel.Trace, 103, "{Method} {Message}");

    private static readonly Action<ILogger, string?, string, string, Exception?> InfoActionWithParams =
        LoggerMessage.Define<string?, string, string>(LogLevel.Information, 104, "{Method} {Parameters} {Message}");

    private static readonly Action<ILogger, string?, string, Exception?> InfoAction =
        LoggerMessage.Define<string?, string>(LogLevel.Information, 104, "{Method} {Message}");

    private static readonly Action<ILogger, string?, string, string, Exception?> WarnActionWithParams =
        LoggerMessage.Define<string?, string, string>(LogLevel.Warning, 105, "{Method} {Parameters} {Message}");

    private static readonly Action<ILogger, string?, string, Exception?> WarnAction =
        LoggerMessage.Define<string?, string>(LogLevel.Warning, 105, "{Method} {Message}");

    private static readonly Action<ILogger, string?, string, string, string, Exception?> FailedActionWithParams =
        LoggerMessage.Define<string?, string, string, string>(LogLevel.Error, 106, "{Method} {Parameters} {ErrorCode} {Message}");

    private static readonly Action<ILogger, string?, string, string, string, string, string, Exception?> FailedActionWithParamsException =
        LoggerMessage.Define<string?, string, string, string, string, string>(LogLevel.Error, 106, "{Method} {Parameters} {ErrorCode} {ExceptionMessage} {InnerException} {StackTrace}");

    private static readonly Action<ILogger, string?, string, string, Exception?> FailedAction =
        LoggerMessage.Define<string?, string, string>(LogLevel.Error, 106, "{Method} {ErrorCode} {Message}");

    private static readonly Action<ILogger, string?, string, string, string, string, string, Exception?> FailedActionException =
        LoggerMessage.Define<string?, string, string, string, string, string>(LogLevel.Error, 106, "{Method} {ErrorCode} {Message} {ExceptionMessage} {InnerException} {StackTrace}");

    /// <summary>
    /// Logs that a method was called.
    /// </summary>
    /// <param name="logger"><see cref="ILogger" /></param>
    /// <param name="logLevel">Log level <see cref="LogLevel"/></param>
    /// <param name="exception">Captured Exception</param>
    /// <param name="method">Method name</param>
    public static void LogMethodStarted(this ILogger logger, LogLevel logLevel = LogLevel.Trace, Exception? exception = null, [CallerMemberName] string? method = null)
    {
        switch (logLevel)
        {
            case LogLevel.Trace:
                TraceAction(logger, method, called, exception);
                break;

            default:
                InfoAction(logger, method, called, exception);
                break;
        }
    }

    /// <summary>
    /// Logs that a method was called.
    /// </summary>
    /// <param name="logger"><see cref="ILogger" /></param>
    /// <param name="parameters">Comma separated list of parameters</param>
    /// <param name="logLevel">Log level <see cref="LogLevel"/></param>
    /// <param name="exception">Captured Exception</param>
    /// <param name="method">Method name</param>
    public static void LogMethodStarted(this ILogger logger, string parameters, LogLevel logLevel = LogLevel.Trace, Exception? exception = null, [CallerMemberName] string? method = null)
    {
        switch (logLevel)
        {
            case LogLevel.Trace:
                TraceActionWithParams(logger, method, parameters, called, exception);
                break;

            default:
                InfoActionWithParams(logger, method, parameters, called, exception);
                break;
        }
    }

    /// <summary>
    /// Logs that method call is completed.
    /// </summary>
    /// <param name="logger"><see cref="ILogger" /></param>
    /// <param name="logLevel">Log level <see cref="LogLevel"/></param>
    /// <param name="exception">Captured Exception</param>
    /// <param name="method">Method name</param>
    public static void LogMethodCompleted(this ILogger logger, LogLevel logLevel = LogLevel.Trace, Exception? exception = null, [CallerMemberName] string? method = null)
    {
        switch (logLevel)
        {
            case LogLevel.Trace:
                TraceAction(logger, method, completed, exception);
                break;

            default:
                InfoAction(logger, method, completed, exception);
                break;
        }
    }

    /// <summary>
    /// Logs that method call is completed.
    /// </summary>
    /// <param name="logger"><see cref="ILogger" /></param>
    /// <param name="parameters">Comma separated list of parameters</param>
    /// <param name="logLevel">Log level <see cref="LogLevel"/></param>
    /// <param name="exception">Captured Exception</param>
    /// <param name="method">Method name</param>
    public static void LogMethodCompleted(this ILogger logger, string parameters, LogLevel logLevel = LogLevel.Trace, Exception? exception = null, [CallerMemberName] string? method = null)
    {
        switch (logLevel)
        {
            case LogLevel.Trace:
                TraceActionWithParams(logger, method, parameters, completed, exception);
                break;

            default:
                InfoActionWithParams(logger, method, parameters, completed, exception);
                break;
        }
    }

    /// <summary>
    /// Formats and writes a trace log message.
    /// </summary>
    /// <param name="logger"><see cref="ILogger"/></param>
    /// <param name="parameters">Comma separated list of parameters</param>
    /// <param name="message">Message to log</param>
    /// <param name="exception">Captured Exception</param>
    /// <param name="method">Method name</param>
    public static void LogTrace(this ILogger logger, string parameters, string message, Exception? exception = null, [CallerMemberName] string? method = null)
    {
        TraceActionWithParams(logger, method, parameters, message, exception);
    }

    /// <summary>
    /// Formats and writes a trace log message.
    /// </summary>
    /// <param name="logger"><see cref="ILogger"/></param>
    /// <param name="message">Message to log</param>
    /// <param name="exception">Captured Exception</param>
    /// <param name="method">Method name</param>
    public static void LogTrace(this ILogger logger, string message, Exception? exception = null, [CallerMemberName] string? method = null)
    {
        TraceAction(logger, method, message, exception);
    }

    /// <summary>
    /// Using <see cref="LoggerMessage"/> for High Performance logging, formats and writes an informational log message.
    /// </summary>
    /// <param name="logger"><see cref="ILogger"/></param>
    /// <param name="message">Message to log</param>
    /// <param name="exception">Captured Exception</param>
    /// <param name="method">Method name</param>
    public static void LogInformationHp(this ILogger logger, string message, Exception? exception = null, [CallerMemberName] string? method = null)
    {
        InfoAction(logger, method, message, exception);
    }

    /// <summary>
    /// Using <see cref="LoggerMessage"/> for High Performance logging, formats and writes an informational log message.
    /// </summary>
    /// <param name="logger"><see cref="ILogger"/></param>
    /// <param name="parameters">Comma separated list of parameters</param>
    /// <param name="message">Message to log</param>
    /// <param name="exception">Captured Exception</param>
    /// <param name="method">Method name</param>
    public static void LogInformationHp(this ILogger logger, string parameters, string message, Exception? exception = null, [CallerMemberName] string? method = null)
    {
        InfoActionWithParams(logger, method, parameters, message, exception);
    }

    /// <summary>
    /// Using <see cref="LoggerMessage"/> for High Performance logging, formats and writes a warning log message.
    /// </summary>
    /// <param name="logger"><see cref="ILogger"/></param>
    /// <param name="message">Message to log</param>
    /// <param name="exception">Captured Exception</param>
    /// <param name="method">Method name</param>
    public static void LogWarningHp(this ILogger logger, string message, Exception? exception = null, [CallerMemberName] string? method = null)
    {
        WarnAction(logger, method, message, exception);
    }

    /// <summary>
    /// Using <see cref="LoggerMessage"/> for High Performance logging, formats and writes a warning log message.
    /// </summary>
    /// <param name="logger"><see cref="ILogger"/></param>
    /// <param name="parameters">Comma separated list of parameters</param>
    /// <param name="message">Message to log</param>
    /// <param name="exception">Captured Exception</param>
    /// <param name="method">Method name</param>
    public static void LogWarningHp(this ILogger logger, string parameters, string message, Exception? exception = null, [CallerMemberName] string? method = null)
    {
        WarnActionWithParams(logger, method, parameters, message, exception);
    }

    /// <summary>
    /// Using <see cref="LoggerMessage"/> for High Performance logging, logs that method call is failed. The exception will be split into parts
    /// </summary>
    /// <param name="logger"><see cref="ILogger"/></param>
    /// <param name="errorCode">User defined error code</param>
    /// <param name="message">Message to log</param>
    /// <param name="exception">Captured Exception</param>
    /// <param name="method">Method name</param>
    public static void LogErrorHp(this ILogger logger, string errorCode, string message, Exception? exception = null, [CallerMemberName] string? method = null)
    {
        if (exception == null)
        {
            FailedAction(logger, method, errorCode, message, exception);
        }
        else
        {
            string stackTrace = exception.StackTrace == null ? "No Stack Trace" : exception.StackTrace.Replace(Environment.NewLine, @"\r\n");
            string innerException = exception.InnerException == null ? "No Inner Exception" : exception.InnerException.ToString().Replace(Environment.NewLine, @"\r\n");
            FailedActionException(logger, method, errorCode, message, exception.Message.Replace(Environment.NewLine, @"\r\n"), innerException, stackTrace, exception);
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
    public static void LogErrorHp(this ILogger logger, string parameters, string errorCode, string message, Exception? exception = null, [CallerMemberName] string? method = null)
    {
        if (exception == null)
        {
            FailedActionWithParams(logger, method, parameters, errorCode, message, exception);
        }
        else
        {
            string stackTrace = exception.StackTrace == null ? "No Stack Trace" : exception.StackTrace.Replace(Environment.NewLine, @"\r\n");
            string innerException = exception.InnerException == null ? "No Inner Exception" : exception.InnerException.ToString().Replace(Environment.NewLine, @"\r\n");
            FailedActionWithParamsException(logger, method, parameters, errorCode, exception.Message.Replace(Environment.NewLine, @"\r\n"), innerException, stackTrace, exception);
        }
    }
}