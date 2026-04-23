using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BlogApp_API_.Dtos;
using BlogApp_API_.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BlogApp_API_.Controllers;

[ApiController]
[Route("api/admin")]
public sealed class AdminAuthController : ControllerBase
{
    private readonly AdminOptions _admin;
    private readonly JwtOptions _jwt;

    public AdminAuthController(IOptions<AdminOptions> adminOptions, IOptions<JwtOptions> jwtOptions)
    {
        _admin = adminOptions.Value;
        _jwt = jwtOptions.Value;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public ActionResult<AdminLoginResponse> Login([FromBody] AdminLoginRequest request)
    {
        var username = (request.Username ?? string.Empty).Trim();
        var password = request.Password ?? string.Empty;

        if (!string.Equals(username, _admin.Username, StringComparison.Ordinal) ||
            !string.Equals(password, _admin.Password, StringComparison.Ordinal))
        {
            return Unauthorized(new { message = "Invalid username or password." });
        }

        if (string.IsNullOrWhiteSpace(_jwt.Secret))
        {
            return Problem("JWT secret is not configured.");
        }

        var expiresAtUtc = DateTime.UtcNow.AddMinutes(Math.Max(10, _jwt.ExpiryMinutes));

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, username),
            new(JwtRegisteredClaimNames.UniqueName, username),
            new(ClaimTypes.Name, username),
            new(ClaimTypes.Role, "admin"),
        };

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Secret));
        var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: expiresAtUtc,
            signingCredentials: creds
        );

        return Ok(new AdminLoginResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Username = username,
            ExpiresAtUtc = expiresAtUtc,
        });
    }
}
