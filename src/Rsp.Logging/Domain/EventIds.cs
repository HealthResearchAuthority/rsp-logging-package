namespace Rsp.Logging.Domain;

/// <summary>
/// Contains event IDs for logging purposes.
/// </summary>
internal static class EventIds
{
    /// <summary>
    /// Event ID for trace logs.
    /// </summary>
    public const int Trace = 103;

    /// <summary>
    /// Event ID for informational logs.
    /// </summary>
    public const int Information = 104;

    /// <summary>
    /// Event ID for warning logs.
    /// </summary>
    public const int Warning = 105;

    /// <summary>
    /// Event ID for error logs.
    /// </summary>
    public const int Error = 106;

    /// <summary>
    /// Event ID for error logs with parameters.
    /// </summary>
    public const int ErrorWithParams = 107;

    /// <summary>
    /// Event ID for exception logs.
    /// </summary>
    public const int Exception = 108;

    /// <summary>
    /// Event ID for exception logs with parameters.
    /// </summary>
    public const int ExceptionWithParams = 108;
}