using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Rsp.Logging.Middlewares.CorrelationId;

/// <summary>
/// Correlation Id Middleware that adds the x-correlation-Id to request/response
/// </summary>
public class CorrelationIdMiddleware(RequestDelegate next, CorrelationIdOptions correlationIdOptions)
{
    /// <summary>
    /// Intercepts the X-Correlation-ID header if present, and makes it available to the
    /// logging scope. If the header is not present, a new guid is generated and assigned to Context.TraceIdentifier
    /// It also logs the Request duration
    /// </summary>
    /// <param name="context">Http request</param>
    public async Task Invoke(HttpContext context)
    {
        // try getting the X-Correlation-ID (default) or use
        // configured _options.Header value while calling app.CorrelationId in Startup.cs Configure method
        var correlationIdPresent = context.Request.Headers.TryGetValue(correlationIdOptions.Header, out var correlationId);

        // if correlationId is not present generate a new one and assign it to TraceIdentifier
        context.TraceIdentifier = correlationIdPresent ? correlationId.ToString() : Guid.NewGuid().ToString("D");

        // add the correlation id in request
        context.Request.Headers.TryAdd(correlationIdOptions.Header, context.TraceIdentifier);

        if (correlationIdOptions.IncludeInResponse)
        {
            // apply the correlation ID to the response header for client side tracking
            context.Response.OnStarting(() =>
            {
                var correlationIdPresent = context.Response.Headers.TryGetValue(correlationIdOptions.Header, out var correlationId);
                if (!correlationIdPresent)
                {
                    context.Response.Headers.TryAdd(correlationIdOptions.Header, new[] { context.TraceIdentifier });
                }
                else if (correlationId.Count > 0)
                {
                    context.TraceIdentifier = correlationId.ToString();
                }

                return Task.CompletedTask;
            });
        }

        await next(context);
    }
}