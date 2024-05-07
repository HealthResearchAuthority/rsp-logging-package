using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Serilog;

namespace Rsp.Logging.Middlewares.RequestTracing;

/// <summary>
/// Convenience extension methods to allow easy utilization of middleware by using Use[Middleware] convention
/// </summary>
public static class RequestTracingExtensions
{
    /// <summary>
    /// Adds RequestTracingMiddleware to the application request's pipeline
    /// </summary>
    /// <param name="app">Application builder to configure an application's request pipeline.</param>
    /// <exception cref="ArgumentNullException" />
    public static IApplicationBuilder UseRequestTracing(this IApplicationBuilder app)
    {
        var options = new RequestTracingOptions();

        return app.UseRequestTracing(o => o = options);
    }

    /// <summary>
    /// Adds RequestTracingMiddleware to the application request's pipeline
    /// </summary>
    /// <param name="app">Application builder to configure an application's request pipeline.</param>
    /// <param name="options">Middleware options</param>
    /// <exception cref="ArgumentNullException" />
    public static IApplicationBuilder UseRequestTracing(this IApplicationBuilder app, RequestTracingOptions options)
    {
        return app.UseRequestTracing(o => o = options);
    }

    /// <summary>
    /// Adds RequestTracingMiddleware to the application request's pipeline
    /// </summary>
    /// <param name="app">Application builder to configure an application's request pipeline.</param>
    /// <param name="configureOptions">Configure middleware's options</param>
    /// <exception cref="ArgumentNullException" />
    public static IApplicationBuilder UseRequestTracing(this IApplicationBuilder app, Action<RequestTracingOptions> configureOptions)
    {
        var options = new RequestTracingOptions();

        // configure request tracing options using the action delegate
        configureOptions(options);

        return app.UseSerilogRequestLogging
        (
            requestLoggingOptions =>
            {
                // Customize the message template
                requestLoggingOptions.MessageTemplate = options.MessageTemplate;

                // Attach additional properties to the request completion event
                requestLoggingOptions.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    var queryParams = httpContext.Request.QueryString;

                    Claim? claim = null;

                    if (httpContext.User?.Identity?.IsAuthenticated == true)
                    {
                        // Get NameIdentifier (AuthId)
                        claim = httpContext.User.Claims.SingleOrDefault(x => x.Type == options.AuthId);
                    }

                    var authId = claim != null ? claim.Value[..options.AuthIdLength] : "";
                    var parameters = queryParams.HasValue ? queryParams.Value : "";

                    diagnosticContext.Set("AuthId", authId);
                    diagnosticContext.Set("QueryString", parameters);
                };
            }

        );
    }
}