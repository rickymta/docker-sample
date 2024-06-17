namespace Draft.Infrastructures.Models.Entities;

public class LoginAccount : Account
{
    public List<string> Roles { get; set; } = [];

    public List<string> Permissions { get; set; } = [];
}
