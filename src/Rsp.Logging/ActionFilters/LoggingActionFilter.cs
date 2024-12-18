using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Rsp.Logging.Domain;
using Rsp.Logging.Extensions;

namespace Rsp.Logging.ActionFilters;

/// <summary>
/// ActionFilter and EndpointFilter to log the start and end of controller actions and Minimal Api endpoints respectively.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="LogActionFilter"/> class.
/// </remarks>
/// <param name="loggerFactory">The logger factory instance.</param>
/// <param name="configuration">The configuration instance.</param>
public class LogActionFilter(ILoggerFactory loggerFactory, IConfiguration configuration) : LoggingBase(loggerFactory, configuration), IAsyncActionFilter, IEndpointFilter
{
    /// <inheritdoc/>
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        // get the endpoint from HttpContext
        var endpoint = context.HttpContext.GetEndpoint();

        if (endpoint == null)
        {
            return await next(context);
        }

        // this metadata was added during the Mapping of the minimal api endpoint

        // we need to get the type so that a logger of that
        // type can be created, to log the correct SourceContext in logs i.e. endpoint type and endpoint name.
        var typeMetaData = endpoint.Metadata.GetMetadata<Type>();
        var endpointNameMetaData = endpoint.Metadata.GetMetadata<IEndpointNameMetadata>();

        // check if any of the above are null, then don't log anything
        if (typeMetaData == null || endpointNameMetaData == null)
        {
            return await next(context);
        }

        // get the logger
        var logger = GetLogger(typeMetaData);

        // Log the start of the method
        logger.LogMethodStarted(StartLogLevel, endpointNameMetaData.EndpointName);

        // Execute the action
        var result = await next(context);

        // Log the completion of the method
        logger.LogMethodCompleted(FinishLogLevel, endpointNameMetaData.EndpointName);

        return result;
    }

    /// <summary>
    /// Called asynchronously before the action is executed.
    /// </summary>
    /// <param name="context">The action executing context.</param>
    /// <param name="next">The delegate to execute the next action filter or the action itself.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // get the ControllerActionDescriptor so that a logger of that
        // type can be created, to log the correct SourceContext in logs i.e. ControllerName and ActionName.
        if (context.ActionDescriptor is not ControllerActionDescriptor controller)
        {
            await Task.CompletedTask;
            return;
        }

        var actionName = controller.ActionName ?? string.Empty;

        var logger = GetLogger(controller.ControllerTypeInfo.AsType());

        // Log the start of the method
        logger.LogMethodStarted(StartLogLevel, actionName);

        // Execute the action
        await next();

        // Log the completion of the method
        logger.LogMethodCompleted(FinishLogLevel, actionName);
    }
}