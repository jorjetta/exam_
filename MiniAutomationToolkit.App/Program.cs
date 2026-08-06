using System.Globalization;
using MiniAutomationToolkit.Core;
using MiniAutomationToolkit.Core.Models;
using MiniAutomationToolkit.Core.Services;

Console.WriteLine(ToolkitInfo.StartupMessage);
Console.WriteLine();

(ClientType ClientType, decimal Amount)[] orders =
[
    (ClientType.Vip, 500m),
    (ClientType.Vip, 2000m),
    (ClientType.Premium, 800m),
    (ClientType.Premium, 1000m),
    (ClientType.Premium, 1500m),
    (ClientType.Regular, 500m),
    (ClientType.Regular, 1500m),
    (ClientType.Regular, 1000m)
];

foreach (var (clientType, amount) in orders)
{
    var discount = DiscountCalculator.CalculateDiscount(amount, clientType);

    Console.WriteLine(
        $"Client: {clientType}, amount: {Format(amount)}, discount: {Format(discount)}");
}

Console.WriteLine();

try
{
    DiscountCalculator.CalculateDiscount(-100m, ClientType.Vip);
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine($"Negative amount rejected: {ex.ParamName}");
}

static string Format(decimal value) =>
    value.ToString("0.##", CultureInfo.InvariantCulture);
