using System.Diagnostics;
using MiniAutomationToolkit.Core;
using MiniAutomationToolkit.Core.Configuration;
using MiniAutomationToolkit.Core.Extensions;
using MiniAutomationToolkit.Core.Helpers;
using MiniAutomationToolkit.Core.Models;
using MiniAutomationToolkit.Core.Pages;
using MiniAutomationToolkit.Core.Repositories;
using MiniAutomationToolkit.Core.Services;
using MiniAutomationToolkit.Core.Simulations;

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

Console.WriteLine();

// Задание 4. Неизменяемый пользователь
Console.WriteLine("=== User DTO ===");

UserDto user = new UserDto("Alex Smith", "alex@example.com");
Console.WriteLine("Created: " + user);

// Свойства только для чтения: раскомментированная строка ниже не компилируется.
// user.Name = "Bob";
Console.WriteLine("Name after creation: " + user.Name);

UserDto sameUser = new UserDto("Alex Smith", "alex@example.com");
Console.WriteLine("Objects are equal: " + user.Equals(sameUser));

UserDto otherUser = new UserDto("Bob Jones", "bob@example.com");
Console.WriteLine("Objects are equal: " + user.Equals(otherUser));

Console.WriteLine();

TryCreateUser("", "alex@example.com");
TryCreateUser("Alex Smith", "");
TryCreateUser("Alex Smith", "alex.example.com");
TryCreateUser("Alex Smith", "alex smith@example.com");

Console.WriteLine();

// Задание 5. Базовая страница
Console.WriteLine("=== Pages ===");

List<BasePage> pages = new List<BasePage>();
pages.Add(new LoginPage());
pages.Add(new HomePage());

foreach (BasePage page in pages)
{
    page.Load();
}

CheckUrlsAreUnique(pages);

List<BasePage> pagesWithDuplicate = new List<BasePage>();
pagesWithDuplicate.Add(new LoginPage());
pagesWithDuplicate.Add(new HomePage());
pagesWithDuplicate.Add(new LoginPage());

try
{
    CheckUrlsAreUnique(pagesWithDuplicate);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine("Error: " + ex.Message);
}

Console.WriteLine();

// Задание 6. Умная конфигурация
Console.WriteLine("=== App config ===");

string configPath = Path.Combine(AppContext.BaseDirectory, "data", "appsettings.txt");
AppConfig config = new AppConfig(configPath);

string baseUrl = config.GetSetting<string>("baseUrl");
int timeout = config.GetSetting<int>("timeout");
bool headless = config.GetSetting<bool>("headless");
int retryCount = config.GetSetting<int>("retryCount");

Console.WriteLine("baseUrl: " + baseUrl);
Console.WriteLine("timeout: " + timeout);
Console.WriteLine("headless: " + headless);
Console.WriteLine("retryCount: " + retryCount);

try
{
    config.GetSetting<string>("missingKey");
}
catch (KeyNotFoundException ex)
{
    Console.WriteLine("Error: " + ex.Message);
}

try
{
    config.GetSetting<int>("baseUrl");
}
catch (InvalidDataException ex)
{
    Console.WriteLine("Error: " + ex.Message);
}

Console.WriteLine();

// Задание 7. Расширяем возможности строк
Console.WriteLine("=== String extensions ===");

List<string?> urls = new List<string?>();
urls.Add("https://google.com");
urls.Add("http://example.org");
urls.Add("ftp://files.example.com");
urls.Add(null);
urls.Add("HTTPS://SITE.EXAMPLE.COM");

foreach (string? url in urls)
{
    bool hasHttpScheme = url.HasHttpScheme();

    Console.WriteLine($"{url ?? "<null>"} -> {hasHttpScheme}");
}

Console.WriteLine();

// Задание 8. Имитация длительной операции
Console.WriteLine("=== Long operation ===");

LongOperationSimulator simulator = new LongOperationSimulator();

Stopwatch stopwatch = Stopwatch.StartNew();
string asyncResult = await simulator.LongOperationAsync();
stopwatch.Stop();

Console.WriteLine($"Async result: {asyncResult}, elapsed: {stopwatch.ElapsedMilliseconds} ms");

Console.WriteLine();

// Задание 9. Логгер ошибок
Console.WriteLine("=== Error logger ===");

string dataDirectory = Path.Combine(AppContext.BaseDirectory, "data");
string inputFilePath = Path.Combine(dataDirectory, "input.txt");
string missingFilePath = Path.Combine(dataDirectory, "missing.txt");
string logFilePath = Path.Combine(dataDirectory, "errors.log");

ErrorLogger logger = new ErrorLogger();

string? inputContent = logger.TryReadFile(inputFilePath, logFilePath);

if (inputContent != null)
{
    Console.WriteLine("input.txt content:");
    Console.WriteLine(inputContent.TrimEnd());
}

string? missingContent = logger.TryReadFile(missingFilePath, logFilePath);

if (missingContent == null)
{
    Console.WriteLine("missing.txt was not read, see the log below.");
}

Console.WriteLine("errors.log content:");
Console.WriteLine(File.ReadAllText(logFilePath).TrimEnd());

Console.WriteLine();

// Задание 11. Склад товаров
Console.WriteLine("=== Products ===");

string productsFilePath = Path.Combine(AppContext.BaseDirectory, "data", "products.csv");
List<Product> products = ProductRepository.LoadFromCsv(productsFilePath);

Console.WriteLine("Products loaded: " + products.Count);

PrintAffordableProducts(products, ProductCategory.Food, 10);
PrintAffordableProducts(products, ProductCategory.Food, 1);

static void PrintAffordableProducts(List<Product> products, ProductCategory category, decimal maxPrice)
{
    List<string> names = ProductRepository.GetAffordableProducts(products, category, maxPrice);

    Console.WriteLine($"{category} under {maxPrice}:");

    if (!names.Any())
    {
        Console.WriteLine("No products found.");

        return;
    }

    foreach (string name in names)
    {
        Console.WriteLine("- " + name);
    }
}

static void CheckUrlsAreUnique(List<BasePage> pages)
{
    List<string> duplicateUrls = pages
        .GroupBy(page => page.Url)
        .Where(group => group.Count() > 1)
        .Select(group => group.Key)
        .ToList();

    if (duplicateUrls.Any())
    {
        throw new InvalidOperationException("Duplicate page URLs found: " + string.Join(", ", duplicateUrls));
    }

    Console.WriteLine("All page URLs are unique.");
}

static void TryCreateUser(string name, string email)
{
    try
    {
        UserDto created = new UserDto(name, email);
        Console.WriteLine("Created: " + created);
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

static void PrintDiscount(decimal amount, ClientType clientType)
{
    decimal discount = DiscountCalculator.CalculateDiscount(amount, clientType);

    Console.WriteLine($"Client: {clientType}, amount: {amount:0.##}, discount: {discount:0.##}");
}
