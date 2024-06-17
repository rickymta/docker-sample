using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Draft.Infrastructures.Models.Attribute;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : System.Attribute, IAuthorizationFilter
{
    public string Roles { get; set; } = null!;

    public string Permissions { get; set; } = null!;

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();

        if (allowAnonymous)
        {
            return;
        }

        var user = (Entities.LoginAccount)context.HttpContext.Items["Account"];
        if (user == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (!string.IsNullOrEmpty(Roles))
        {
            var rolesArray = Roles.Split(',');
            if (!rolesArray.Any(role => user.Roles.Contains(role.Trim())))
            {
                context.Result = new ForbidResult();
                return;
            }
        }

        if (!string.IsNullOrEmpty(Permissions))
        {
            var permissionsArray = Permissions.Split(',');
            if (!permissionsArray.Any(permission => user.Permissions.Contains(permission.Trim())))
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}
