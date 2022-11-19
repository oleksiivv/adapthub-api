using adapthub_api.Models;
using System.IdentityModel.Tokens.Jwt;

namespace adapthub_api.Services
{
    public interface ITokenService
    {
        JwtSecurityToken BuildToken(string key, string issuer, string userId);
        bool ValidateToken(string key, string issuer, string audience, string token);

        void CheckAccess(string token, string type, string? expectedId = null);
    }
}
