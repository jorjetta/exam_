namespace MiniAutomationToolkit.Core.Configuration;


public class AppConfig
{
    private readonly Dictionary<string, string> _settings = new Dictionary<string, string>();

    public AppConfig(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("Config file path cannot be empty.");
        }

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Config file not found: " + filePath);
        }

        string[] lines = File.ReadAllLines(filePath);

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            int lineNumber = i + 1;

            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            if (line.TrimStart().StartsWith("#"))
            {
                continue;
            }

            string[] parts = line.Split('=', 2);

            if (parts.Length != 2)
            {
                throw new InvalidDataException($"Invalid config line {lineNumber}: missing '=' separator in \"{line}\".");
            }

            string key = parts[0].Trim();
            string value = parts[1].Trim();

            if (string.IsNullOrWhiteSpace(key))
            {
                throw new InvalidDataException($"Invalid config line {lineNumber}: key cannot be empty.");
            }

            if (_settings.ContainsKey(key))
            {
                throw new InvalidDataException($"Invalid config line {lineNumber}: duplicate key \"{key}\".");
            }

            _settings.Add(key, value);
        }
    }

    public T GetSetting<T>(string key)
    {
        if (!_settings.ContainsKey(key))
        {
            throw new KeyNotFoundException($"Setting \"{key}\" was not found in the configuration.");
        }

        string value = _settings[key];

        try
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
        catch (Exception ex) when (ex is FormatException || ex is InvalidCastException || ex is OverflowException)
        {
            throw new InvalidDataException($"Setting \"{key}\" with value \"{value}\" cannot be converted to {typeof(T).Name}.");
        }
    }
}
