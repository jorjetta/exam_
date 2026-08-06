using MiniAutomationToolkit.Core.Models;

namespace MiniAutomationToolkit.Core.Services;


public static class DiscountCalculator
{
    public static decimal CalculateDiscount(decimal orderAmount, ClientType clientType)
    {
        if (orderAmount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(orderAmount), "Order amount cannot be negative.");
        }

        decimal rate = clientType switch
        {
            ClientType.Vip => 0.15m,
            ClientType.Premium when orderAmount > 1000 => 0.10m,
            ClientType.Premium => 0.05m,
            ClientType.Regular when orderAmount > 1000 => 0.05m,
            ClientType.Regular => 0m,
            _ => throw new ArgumentOutOfRangeException(nameof(clientType), "Unknown client type.")
        };

        return orderAmount * rate;
    }
}
