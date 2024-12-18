using System.Threading.Tasks;
using Castle.DynamicProxy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Rsp.Logging.Domain;
using Rsp.Logging.Extensions;

namespace Rsp.Logging.Interceptors;

/// <summary>
/// Interceptor for logging method calls.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="LoggingInterceptor"/> class.
/// </remarks>
/// <param name="loggerFactory">The logger factory instance.</param>
/// <param name="configuration">The configuration instance.</param>
public class LoggingInterceptor(ILoggerFactory loggerFactory, IConfiguration configuration) : LoggingBase(loggerFactory, configuration), IAsyncInterceptor
{
    /// <summary>
    /// Intercepts synchronous method calls.
    /// </summary>
    /// <param name="invocation">The invocation information.</param>
    public void InterceptSynchronous(IInvocation invocation)
    {
        var logger = GetLogger(invocation.TargetType);

        // Log the start of the method
        logger.LogMethodStarted(StartLogLevel, invocation.Method.Name);

        invocation.Proceed();

        // Log the completion of the method
        logger.LogMethodCompleted(FinishLogLevel, invocation.Method.Name);
    }

    /// <summary>
    /// Intercepts asynchronous method calls.
    /// </summary>
    /// <param name="invocation">The invocation information.</param>
    public void InterceptAsynchronous(IInvocation invocation)
    {
        invocation.ReturnValue = InternalInterceptAsynchronous(invocation);
    }

    /// <summary>
    /// Intercepts asynchronous method calls with a return value.
    /// </summary>
    /// <typeparam name="TResult">The type of the return value.</typeparam>
    /// <param name="invocation">The invocation information.</param>
    public void InterceptAsynchronous<TResult>(IInvocation invocation)
    {
        invocation.ReturnValue = InternalInterceptAsynchronous<TResult>(invocation);
    }

    private async Task InternalInterceptAsynchronous(IInvocation invocation)
    {
        var logger = GetLogger(invocation.TargetType);

        // Log the start of the method
        logger.LogMethodStarted(StartLogLevel, invocation.Method.Name);

        invocation.Proceed();

        var task = (Task)invocation.ReturnValue;
        await task;

        // Log the completion of the method
        logger.LogMethodCompleted(FinishLogLevel, invocation.Method.Name);
    }

    private async Task<TResult> InternalInterceptAsynchronous<TResult>(IInvocation invocation)
    {
        var logger = GetLogger(invocation.TargetType);

        // Log the start of the method
        logger.LogMethodStarted(StartLogLevel, invocation.Method.Name);

        invocation.Proceed();

        var task = (Task<TResult>)invocation.ReturnValue;
        var result = await task;

        // Log the completion of the method
        logger.LogMethodCompleted(FinishLogLevel, invocation.Method.Name);

        return result;
    }
}