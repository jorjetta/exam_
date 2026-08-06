namespace MiniAutomationToolkit.Core.Extensions;


public static class StringExtensions
{
    public static bool HasHttpScheme(this string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return false;
        }

        if (input.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        if (input.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        return false;
    }
}
