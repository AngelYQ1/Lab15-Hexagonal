namespace Lab11_AngelYucra.Application.Common;

internal static class TicketStatusRules
{
    private static readonly HashSet<string> AllowedStatuses =
    [
        "abierto",
        "en_proceso",
        "cerrado"
    ];

    public static bool IsValid(string status) => AllowedStatuses.Contains(status);
}
