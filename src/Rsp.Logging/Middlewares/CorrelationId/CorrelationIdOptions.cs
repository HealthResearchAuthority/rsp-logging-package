namespace Rsp.Logging.Middlewares.CorrelationId;

/// <summary>
/// Represents the correlation options to initialize the CorrelationId middleware
/// </summary>
public class CorrelationIdOptions
{
    /// <summary>
    /// The header field name where the correlation ID will be stored
    /// </summary>
    public string Header { get; set; } = "x-correlation-id";

    /// <summary>
    /// Controls whether the correlation ID is returned in the response headers
    /// </summary>
    public bool IncludeInResponse { get; set; } = true;
}