namespace MiniAutomationToolkit.Core.Services;


public class ErrorLogger
{
    public string? TryReadFile(string sourceFilePath, string logFilePath)
    {
        try
        {
            return File.ReadAllText(sourceFilePath);
        }
        catch (FileNotFoundException ex)
        {
            WriteToLog(logFilePath, ex);

            return null;
        }
        catch (UnauthorizedAccessException ex)
        {
            WriteToLog(logFilePath, ex);

            return null;
        }
    }

    private void WriteToLog(string logFilePath, Exception ex)
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string logLine = $"{timestamp} | {ex.GetType().Name} | {ex.Message}" + Environment.NewLine;

        File.AppendAllText(logFilePath, logLine);
    }
}
