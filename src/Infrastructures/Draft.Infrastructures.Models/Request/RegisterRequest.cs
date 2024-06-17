namespace Draft.Infrastructures.Models.Request;

/// <summary>
/// RegisterRequest
/// </summary>
public class RegisterRequest
{
    /// <summary>
    /// Fullname
    /// </summary>
    public string Fullname { get; set; } = null!;

    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Password
    /// </summary>
    public string Password { get; set; } = null!;

    /// <summary>
    /// Address
    /// </summary>
    public string? Address { get; set; }
}
