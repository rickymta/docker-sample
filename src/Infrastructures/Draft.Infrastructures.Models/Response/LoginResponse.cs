namespace Draft.Infrastructures.Models.Response;

/// <summary>
/// LoginResponse
/// </summary>
public class LoginResponse
{
    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Fullname
    /// </summary>
    public string Fullname { get; set; } = null!;

    /// <summary>
    /// Avatar
    /// </summary>
    public string Avatar { get; set; } = null!;

    /// <summary>
    /// AccessToken
    /// </summary>
    public string AccessToken { get; set; } = null!;

    /// <summary>
    /// RefreshToken
    /// </summary>
    public string RefreshToken { get; set; } = null!;
}
