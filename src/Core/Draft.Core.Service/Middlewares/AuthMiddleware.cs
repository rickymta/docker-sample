using Draft.Core.Infrastructure.Abstractions;
using Draft.Infrastructures.Models.Jwt.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Draft.Core.Service.Middlewares;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;

    public AuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IJwtProvider jwtProvider)
    {
        var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
        if (token!.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            token = token["Bearer ".Length..].Trim();
        }

        if (jwtProvider.ValidateJwt(token, JwtType.AccessToken))
        {
            var payload = jwtProvider.DecodeJwtToPayload(token, JwtType.AccessToken);
            context.Items["Account"] = payload!.AccountId;
            await _next(context);
        }
        else
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }
    }
}

public static class AuthMiddlewareExtensions
{
    public static void UseAuthMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<AuthMiddleware>();
    }
}
