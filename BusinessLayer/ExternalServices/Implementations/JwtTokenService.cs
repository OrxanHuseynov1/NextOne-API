using BusinessLayer.ExternalServices.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLayer.ExternalServices.Implementations;

public class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<string> GenerateJwtToken(User user)
    {
        List<Claim> claims =
        [
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim("fullName", user.FullName),
            new Claim("companyName", user.Company.Name)

        ];

        if (user.Company != null && user.CompanyId != Guid.Empty)
        {
            claims.Add(new Claim("companyId", user.CompanyId.ToString()));

            if (user.Company.PackageEndDate != default)
            {
                claims.Add(new Claim("packageEndDate", user.Company.PackageEndDate.ToString("o")));
            }
        }

        var secretKey = _configuration["Jwt:SecretKey"];
        if (string.IsNullOrEmpty(secretKey))
        {
            throw new ApplicationException("JWT SecretKey is not configured.");
        }

        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(secretKey));
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

        var expiresInMinutesString = _configuration["Jwt:ExpiresInMinutes"];
        if (!double.TryParse(expiresInMinutesString, out double expiresInMinutes))
        {
            expiresInMinutes = 60;
        }

        JwtSecurityToken securityToken = new(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            signingCredentials: signingCredentials,
            claims: claims,
            expires: DateTime.Now.AddMinutes(expiresInMinutes)
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return Task.FromResult(tokenString);
    }
}