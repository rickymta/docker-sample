using Draft.Infrastructures.Models.Enums;

namespace Draft.Infrastructures.Models.Entities;

/// <summary>
/// Account Entity
/// </summary>
public class Account : BaseEntity
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
    
    /// <summary>
    /// Avatar
    /// </summary>
    public string? Avatar { get; set; }

	/// <summary>
	/// AccountType
	/// </summary>
	public AccountType Type { get; set; }
}
