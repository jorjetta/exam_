namespace MiniAutomationToolkit.Core;

/// <summary>
/// Общая информация о наборе инструментов, переиспользуемая клиентскими приложениями.
/// </summary>
public static class ToolkitInfo
{
    /// <summary>Отображаемое имя набора инструментов.</summary>
    public const string Name = "MiniAutomationToolkit";

    /// <summary>Сообщение, которое приложение выводит при запуске.</summary>
    public static string StartupMessage => $"{Name} started";
}
