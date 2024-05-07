using System.Security.Claims;

namespace Rsp.Logging.Middlewares.RequestTracing;

/// <summary>
/// Represents the request tracing options to initialize the RequestTracingMiddleware
/// </summary>
public class RequestTracingOptions
{
    /// <summary>
    /// Message template to use for logging request e.g.
    /// {AuthId:l} HTTP {RequestMethod:l} {MethodName:l} {RequestPath:l} {Parameters:l} responded {StatusCode} in {Elapsed:0.00} ms
    /// </summary>
    public string MessageTemplate { get; set; } = "{AuthId:l} HTTP {RequestMethod:l} {RequestPath:l} {QueryString:l} responded {StatusCode} in {Elapsed:0.00} ms";

    /// <summary>
    /// Authenticated User Identifier to use for logging. If not specified
    /// <see cref="ClaimTypes.NameIdentifier"/> will be used
    /// </summary>
    public string AuthId { get; set; } = ClaimTypes.NameIdentifier;

    /// <summary>
    /// Spcifies the number of characters used to log AuthId
    /// </summary>
    public short AuthIdLength { get; set; } = 8;
}