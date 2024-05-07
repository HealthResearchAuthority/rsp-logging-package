namespace Rsp.Logging.Domain;

/// <summary>
/// Defines various Log Events as a tuples of Code and Description
/// </summary>
public static class LogEvents
{
    /// <summary>
    /// Unhandled exception
    /// </summary>
    public static (string Code, string Description) UnhandledException => (ErrorCodes.UnhandledException, "An unhandled exception occurred in the application.");

    /// <summary>
    /// Unsuccessful Api call that returned a status code > 299
    /// </summary>
    public static (string Code, string Description) ApiUnsuccessful => (ErrorCodes.ApiUnsuccessful, "API call didn't return a successful status code");

    /// <summary>
    /// One or more Api calls returned a status code > 299
    /// </summary>
    public static (string Code, string Description) OneOrMoreApiUnsuccessful => (ErrorCodes.ApiUnsuccessful, "One or more API call(s) didn't return a successful status code");

    /// <summary>
    /// One or more parallel Api calls was faulted or cancelled
    /// </summary>
    public static (string Code, string Description) OneOrMoreApiFaultedOrCancelled => (ErrorCodes.ApiFaultedOrCancelled, "One or more parallel API call(s) was cancelled or faulted");

    /// <summary>
    /// Business rule or a condition failure
    /// </summary>
    public static (string Code, string Description) BusinessRuleFailure => (ErrorCodes.BusinessRuleFaliure, "A condition or a business rule failed");

    /// <summary>
    /// Empty Logs
    /// </summary>
    public static (string Code, string Description) EmptyLogs => (ErrorCodes.EmptyLogs, "No logs found");
}