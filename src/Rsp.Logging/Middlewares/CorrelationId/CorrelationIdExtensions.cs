using System;
using Microsoft.AspNetCore.Builder;

namespace Rsp.Logging.Middlewares.CorrelationId;

/// <summary>
/// Convenience extension methods to allow easy utilization of middleware by using Use[Middleware] convention
/// </summary>
public static class CorrelationIdExtensions
{
    /// <summary>
    /// Adds CorrelationIdMiddleware to the application request's pipeline
    /// </summary>
    /// <param name="app">Application builder to configure an application's request pipeline.</param>
    /// <exception cref="ArgumentNullException" />
    public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder app)
    {
        return app.UseMiddleware<CorrelationIdMiddleware>(new CorrelationIdOptions());
    }

    /// <summary>
    /// Adds CorrelationIdMiddleware to the application request's pipeline
    /// </summary>
    /// <param name="app">Application builder to configure an application's request pipeline.</param>
    /// <param name="options">Middleware options</param>
    /// <exception cref="ArgumentNullException" />
    public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder app, CorrelationIdOptions options)
    {
        return app.UseMiddleware<CorrelationIdMiddleware>(options);
    }

    /// <summary>
    /// Adds CorrelationIdMiddleware to the application request's pipeline
    /// </summary>
    /// <param name="app">Application builder to configure an application's request pipeline.</param>
    /// <param name="configureOptions">Configure middleware's options</param>
    /// <exception cref="ArgumentNullException" />
    public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder app, Action<CorrelationIdOptions> configureOptions)
    {
        var options = new CorrelationIdOptions();

        // configure correlation Id options using the action delegate
        configureOptions(options);

        return app.UseMiddleware<CorrelationIdMiddleware>(options);
    }
}