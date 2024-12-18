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

    /// <summary>
    /// Using <see cref="LoggerMessage"/> for High Performance logging,
    /// logs a message as <see cref="LogLevel.Information"/> or <see cref="LogLevel.Trace"/> that a method was called.
    /// </summary>
    /// <param name="logger"><see cref="ILogger" /></param>
    /// <param name="logLevel">
    ///     <see cref="LogLevel"/>Default: <see cref="LogLevel.Information"/>. Only <see cref="LogLevel.Information"/> and <see cref="LogLevel.Trace"/>
    ///     are supported. Anyother level, will be logged as Information.
    /// </param>
    /// <param name="method">Method name</param>
    public static void LogMethodStarted(this ILogger logger, LogLevel logLevel = LogLevel.Information, [CallerMemberName] string method = "")
    {
        switch (logLevel)
        {
            case LogLevel.None:
                break;

            case LogLevel.Trace:
                logger.LogVerbose(method, called);
                break;

            default:
                logger.LogInformation(method, called);
                break;
        }
    }

    /// <summary>
    /// Using <see cref="LoggerMessage"/> for High Performance logging,
    /// logs a message as <see cref="LogLevel.Information"/> or <see cref="LogLevel.Trace"/> that a method was called
    /// with parameters.
    /// </summary>
    /// <param name="logger"><see cref="ILogger" /></param>
    /// <param name="parameters">Comma separated list of parameters</param>
    /// <param name="logLevel">
    ///     <see cref="LogLevel"/>Default: <see cref="LogLevel.Information"/>. Only <see cref="LogLevel.Information"/> and <see cref="LogLevel.Trace"/>
    ///     are supported. Anyother level, will be logged as Information.
    /// </param>
    /// <param name="method">Method name</param>
    public static void LogMethodStarted(this ILogger logger, string parameters, LogLevel logLevel = LogLevel.Information, [CallerMemberName] string method = "")
    {
        switch (logLevel)
        {
            case LogLevel.None:
                break;

            case LogLevel.Trace:
                logger.LogVerbose(method, parameters, called);
                break;

            default:
                logger.LogInformation(method, parameters, called);
                break;
        }
    }

    /// <summary>
    /// Using <see cref="LoggerMessage"/> for High Performance logging,
    /// logs a message as <see cref="LogLevel.Information"/> that a method call was completed.
    /// </summary>
    /// <param name="logger"><see cref="ILogger" /></param>
    /// <param name="logLevel">
    ///     <see cref="LogLevel"/>Default: <see cref="LogLevel.Information"/>. Only <see cref="LogLevel.Information"/> and <see cref="LogLevel.Trace"/>
    ///     are supported. Anyother level, will be logged as Information.
    /// </param>
    /// <param name="method">Method name</param>
    public static void LogMethodCompleted(this ILogger logger, LogLevel logLevel = LogLevel.Information, [CallerMemberName] string method = "")
    {
        switch (logLevel)
        {
            case LogLevel.None:
                break;

            case LogLevel.Trace:
                logger.LogVerbose(method, completed);
                break;

            default:
                logger.LogInformation(method, completed);
                break;
        }
    }

    /// <summary>
    /// Using <see cref="LoggerMessage"/> for High Performance logging,
    /// logs a message as <see cref="LogLevel.Trace"/> that a method call was completed with parameters.
    /// </summary>
    /// <param name="logger"><see cref="ILogger" /></param>
    /// <param name="parameters">Comma separated list of parameters</param>
    /// <param name="logLevel">
    ///     <see cref="LogLevel"/>Default: <see cref="LogLevel.Information"/>. Only <see cref="LogLevel.Information"/> and <see cref="LogLevel.Trace"/>
    ///     are supported. Anyother level, will be logged as Information.
    /// </param>
    /// <param name="method">Method name</param>
    public static void LogMethodCompleted(this ILogger logger, string parameters, LogLevel logLevel = LogLevel.Information, [CallerMemberName] string method = "")
    {
        switch (logLevel)
        {
            case LogLevel.None:
                break;

            case LogLevel.Trace:
                logger.LogVerbose(method, parameters, completed);
                break;

            default:
                logger.LogInformation(method, parameters, completed);
                break;
        }
    }

    /// <summary>
    /// Using <see cref="LoggerMessage"/> for High Performance logging,
    /// logs a message as <see cref="LogLevel.Trace"/>.
    /// </summary>
    /// <param name="logger"><see cref="ILogger"/></param>
    /// <param name="message">Message to log</param>
    /// <param name="method">Method name</param>
    public static void LogAsTrace(this ILogger logger, string message, [CallerMemberName] string method = "")
    {
        logger.LogVerbose(method, message);
    }

    /// <summary>
    /// Using <see cref="LoggerMessage"/> for High Performance logging,
    /// logs a message as <see cref="LogLevel.Trace"/> with parameters.
    /// </summary>
    /// <param name="logger"><see cref="ILogger"/></param>
    /// <param name="parameters">Comma separated list of parameters</param>
    /// <param name="message">Message to log</param>
    /// <param name="method">Method name</param>
    public static void LogAsTrace(this ILogger logger, string parameters, string message, [CallerMemberName] string method = "")
    {
        logger.LogVerbose(method, parameters, message);
    }

    /// <summary>
    /// Using <see cref="LoggerMessage"/> for High Performance logging,
    /// logs a message as <see cref="LogLevel.Information"/>.
    /// </summary>
    /// <param name="logger"><see cref="ILogger"/></param>
    /// <param name="message">Message to log</param>
    /// <param name="method">Method name</param>
    public static void LogAsInformation(this ILogger logger, string message, [CallerMemberName] string method = "")
    {
        logger.LogInformation(method, message);
    }

    /// <summary>
    /// Using <see cref="LoggerMessage"/> for High Performance logging,
    /// logs a message as <see cref="LogLevel.Information"/> with parameters.
    /// </summary>
    /// <param name="logger"><see cref="ILogger"/></param>
    /// <param name="parameters">Comma separated list of parameters</param>
    /// <param name="message">Message to log</param>
    /// <param name="method">Method name</param>
    public static void LogAsInformation(this ILogger logger, string parameters, string message, [CallerMemberName] string method = "")
    {
        logger.LogInformation(method, parameters, message);
    }

    /// <summary>
    /// Using <see cref="LoggerMessage"/> for High Performance logging,
    /// logs a message as <see cref="LogLevel.Warning"/>.
    /// </summary>
    /// <param name="logger"><see cref="ILogger"/></param>
    /// <param name="message">Message to log</param>
    /// <param name="exception">Captured Exception</param>
    /// <param name="method">Method name</param>
    public static void LogAsWarning(this ILogger logger, string message, Exception? exception = null, [CallerMemberName] string method = "")
    {
        logger.LogWarning(method, message, exception);
    }

    /// <summary>
    /// Using <see cref="LoggerMessage"/> for High Performance logging,
    /// logs a message as <see cref="LogLevel.Warning"/> with parameters.
    /// </summary>
    /// <param name="logger"><see cref="ILogger"/></param>
    /// <param name="parameters">Comma separated list of parameters</param>
    /// <param name="message">Message to log</param>
    /// <param name="exception">Captured Exception</param>
    /// <param name="method">Method name</param>
    public static void LogAsWarning(this ILogger logger, string parameters, string message, Exception? exception = null, [CallerMemberName] string method = "")
    {
        logger.LogWarning(method, parameters, message, exception);
    }

    /// <summary>
    /// Using <see cref="LoggerMessage"/> for High Performance logging, logs that method call is failed. The exception will be split into parts
    /// </summary>
    /// <param name="logger"><see cref="ILogger"/></param>
    /// <param name="errorCode">User defined error code</param>
    /// <param name="message">Message to log</param>
    /// <param name="exception">Captured Exception</param>
    /// <param name="method">Method name</param>
    public static void LogAsError(this ILogger logger, string errorCode, string message, Exception? exception = null, [CallerMemberName] string method = "")
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
    public static void LogAsError(this ILogger logger, string parameters, string errorCode, string message, Exception? exception = null, [CallerMemberName] string method = "")
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