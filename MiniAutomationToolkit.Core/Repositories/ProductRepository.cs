using System.Globalization;
using MiniAutomationToolkit.Core.Models;

namespace MiniAutomationToolkit.Core.Repositories;


public static class ProductRepository
{
    public static List<Product> LoadFromCsv(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("CSV file not found: " + filePath);
        }

        List<Product> products = new List<Product>();
        string[] lines = File.ReadAllLines(filePath);

        // Заголовок — первая строка файла, товаром не является.
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            int lineNumber = i + 1;

            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            products.Add(ParseProduct(line, lineNumber));
        }

        return products;
    }

    public static List<string> GetAffordableProducts(
        IEnumerable<Product> products,
        ProductCategory category,
        decimal maxPrice)
    {
        return products
            .Where(product => product.Category == category)
            .Where(product => product.Price < maxPrice)
            .OrderBy(product => product.Price)
            .ThenBy(product => product.Name)
            .Select(product => product.Name)
            .ToList();
    }

    private static Product ParseProduct(string line, int lineNumber)
    {
        string[] parts = line.Split(';');

        if (parts.Length != 3)
        {
            throw new InvalidDataException($"Invalid CSV line {lineNumber}: expected 3 fields separated by ';' but got {parts.Length}.");
        }

        string name = parts[0].Trim();
        string priceText = parts[1].Trim();
        string categoryText = parts[2].Trim();

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidDataException($"Invalid CSV line {lineNumber}: product name cannot be empty.");
        }

        if (string.IsNullOrWhiteSpace(priceText))
        {
            throw new InvalidDataException($"Invalid CSV line {lineNumber}: price cannot be empty.");
        }

        if (string.IsNullOrWhiteSpace(categoryText))
        {
            throw new InvalidDataException($"Invalid CSV line {lineNumber}: category cannot be empty.");
        }

        decimal price;

        if (!decimal.TryParse(priceText, NumberStyles.Number, CultureInfo.InvariantCulture, out price))
        {
            throw new InvalidDataException($"Invalid CSV line {lineNumber}: \"{priceText}\" is not a valid price.");
        }

        if (price < 0)
        {
            throw new InvalidDataException($"Invalid CSV line {lineNumber}: price cannot be negative.");
        }

        ProductCategory category;

        if (!Enum.TryParse(categoryText, true, out category))
        {
            throw new InvalidDataException($"Invalid CSV line {lineNumber}: \"{categoryText}\" is not a valid product category.");
        }

        return new Product(name, price, category);
    }
}
