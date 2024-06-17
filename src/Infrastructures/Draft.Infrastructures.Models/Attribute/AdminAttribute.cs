namespace Draft.Infrastructures.Models.Attribute;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class AdminAttribute : System.Attribute
{
    public string Permissions { get; }

    public AdminAttribute(string permissions)
    {
        Permissions = permissions;
    }
}
