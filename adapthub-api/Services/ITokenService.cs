using adapthub_api.Models;
using System.IdentityModel.Tokens.Jwt;

namespace adapthub_api.Services
{
    public interface ITokenService
    {
        JwtSecurityToken BuildToken(string key, string issuer, Customer user);
        bool ValidateToken(string key, string issuer, string audience, string token);
    }
}
