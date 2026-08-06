using MiniAutomationToolkit.Core;
using MiniAutomationToolkit.Core.Helpers;
using MiniAutomationToolkit.Core.Models;
using MiniAutomationToolkit.Core.Services;

Console.WriteLine(ToolkitInfo.StartupMessage);
Console.WriteLine();

// Задание 2. Калькулятор скидок
Console.WriteLine("=== Discount calculator ===");

PrintDiscount(500, ClientType.Vip);
PrintDiscount(2000, ClientType.Vip);
PrintDiscount(800, ClientType.Premium);
PrintDiscount(1000, ClientType.Premium);
PrintDiscount(1500, ClientType.Premium);
PrintDiscount(500, ClientType.Regular);
PrintDiscount(1500, ClientType.Regular);
PrintDiscount(1000, ClientType.Regular);

Console.WriteLine();

try
{
    DiscountCalculator.CalculateDiscount(-100, ClientType.Vip);
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine("Negative amount rejected: " + ex.ParamName);
}

Console.WriteLine();

// Задание 3. Поиск в хаосе
Console.WriteLine("=== File searcher ===");

List<string> fileNames = new List<string>();
fileNames.Add("debug.txt");
fileNames.Add("error_2024.log");
fileNames.Add("notes.txt");
fileNames.Add("screen_001.png");
fileNames.Add("trace_2024_01_15.log");
fileNames.Add("readme.txt");
fileNames.Add("screen_002.PNG");
fileNames.Add("app.log");
fileNames.Add("screen_003.png");
fileNames.Add("changelog.txt");
fileNames.Add("crash_dump.log");
fileNames.Add("screen_004.Png");
fileNames.Add("install.log");
fileNames.Add("todo.txt");
fileNames.Add("screen_005.png");
fileNames.Add("warnings.log");
fileNames.Add("config_backup.txt");
fileNames.Add("screen_006.png");
fileNames.Add("session.log");
fileNames.Add("summary.txt");

string firstScreenshot = FileSearcher.FindFirstScreenshot(fileNames);
Console.WriteLine("First screenshot: " + firstScreenshot);

List<string> fileNamesWithoutScreenshots = new List<string>();
fileNamesWithoutScreenshots.Add("debug.txt");
fileNamesWithoutScreenshots.Add("error_2024.log");
fileNamesWithoutScreenshots.Add("notes.txt");
fileNamesWithoutScreenshots.Add("app.log");
fileNamesWithoutScreenshots.Add("summary.txt");

try
{
    FileSearcher.FindFirstScreenshot(fileNamesWithoutScreenshots);
}
catch (FileNotFoundException ex)
{
    Console.WriteLine("Search failed: " + ex.Message);
}

static void PrintDiscount(decimal amount, ClientType clientType)
{
    decimal discount = DiscountCalculator.CalculateDiscount(amount, clientType);

    Console.WriteLine($"Client: {clientType}, amount: {amount:0.##}, discount: {discount:0.##}");
}
