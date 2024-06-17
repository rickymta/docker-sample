using Draft.Infrastructures.Models.Entities;

namespace Draft.Infrastructures.Models.Cache;

public class AccountCache
{
    public Account AccountData { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;
}
