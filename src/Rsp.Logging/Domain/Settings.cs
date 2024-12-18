namespace Rsp.Logging.Domain;

/// <summary>
/// Defines the constants for LogLevels
/// </summary>
internal static class AppSettings
{
    /// <summary>
    /// Only Information and Trace levels will be supported. If set to None, the logging will be disabled
    /// </summary>
    public const string StartLogLevel = "AppSettings:LoggingInterceptor:StartLogLevel";

    /// <summary>
    /// Only Information and Trace levels will be supported. If set to None, the logging will be disabled
    /// </summary>
    public const string FinishLogLevel = "AppSettings:LoggingInterceptor:FinishLogLevel";
}