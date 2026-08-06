namespace MiniAutomationToolkit.Core.Models;


public record UserDto
{
    public string Name { get; }

    public string Email { get; }

    public UserDto(string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Invalid name: name cannot be empty.");
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException($"Invalid email: {email}");
        }

        if (!email.Contains("@"))
        {
            throw new ArgumentException($"Invalid email: {email}");
        }

        if (email.Contains(" "))
        {
            throw new ArgumentException($"Invalid email: {email}");
        }

        Name = name;
        Email = email;
    }
}
