using Draft.Infrastructures.Models.Enums;

namespace Draft.Infrastructures.Models.Request;

/// <summary>
/// LoginRequest
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// IpAddress
    /// </summary>
    public string IpAddress { get; set; } = null!;

    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Password
    /// </summary>
    public string Password { get; set; } = null!;

    /// <summary>
    /// AccountType
    /// </summary>
    public AccountType Type { get; set; } = AccountType.Customer;
}
