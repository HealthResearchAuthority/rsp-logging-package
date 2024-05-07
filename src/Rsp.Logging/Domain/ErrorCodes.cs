namespace Rsp.Logging.Domain;

internal static class ErrorCodes
{
    public const string UnhandledException = "ERR_UNHANDLED_EXCEPTION";
    public const string BusinessRuleFaliure = "ERR_BUSINESS_RULE_FAILED";
    public const string ApiUnsuccessful = "ERR_API_FAILED";
    public const string ApiFaultedOrCancelled = "ERR_API_FAULTED_OR_CANCELLED";
    public const string EmptyLogs = "ERR_EMPTY_LOG_EVENT_REQUEST";
}