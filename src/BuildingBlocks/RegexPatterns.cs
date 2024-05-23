using System.Text.RegularExpressions;

namespace BuildingBlocks;

public static partial class RegexPatterns
{
    public static readonly Regex PhoneIsValid = PhoneRegexPatternAttr();

    [GeneratedRegex(
        @"^(?:\+971|00971|0)?(?:50|51|52|55|56|2|3|4|6|7|9)\d{7}$",
        RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant)]
    private static partial Regex PhoneRegexPatternAttr();
}

public static partial class RegexPatterns
{
    public static readonly Regex EmailIsValid = EmailRegexPatternAttr();

    [GeneratedRegex(
        @"^([0-9a-zA-Z]([+\-_.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,17})$",
        RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant)]
    private static partial Regex EmailRegexPatternAttr();
}