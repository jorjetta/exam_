namespace MiniAutomationToolkit.Core.Helpers;


public static class FileSearcher
{
    public static string FindFirstScreenshot(List<string> fileNames)
    {
        List<string> screenshots = fileNames
            .Where(name => name.ToLower().EndsWith(".png"))
            .ToList();

        if (!screenshots.Any())
        {
            throw new FileNotFoundException("No screenshots found in the provided list.");
        }

        string? firstScreenshot = screenshots.FirstOrDefault();

        return firstScreenshot!;
    }
}
