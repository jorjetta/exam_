using MiniAutomationToolkit.Core.Models;

namespace MiniAutomationToolkit.Core.Services;


public static class DiscountCalculator
{
    private const decimal LargeOrderThreshold = 1000m;

    public static decimal CalculateDiscount(
        decimal orderAmount,
        ClientType clientType)
    {
        if (orderAmount < 0m)
        {
            throw new ArgumentOutOfRangeException(
                nameof(orderAmount),
                orderAmount,
                "Order amount cannot be negative.");
        }

        var rate = clientType switch
        {
            ClientType.Vip => 0.15m,
            ClientType.Premium when orderAmount > LargeOrderThreshold => 0.10m,
            ClientType.Premium => 0.05m,
            ClientType.Regular when orderAmount > LargeOrderThreshold => 0.05m,
            ClientType.Regular => 0m,
            _ => throw new ArgumentOutOfRangeException(
                nameof(clientType),
                clientType,
                "Unknown client type.")
        };

        return orderAmount * rate;
    }
}
