using adapthub_api.Models;
using System.IdentityModel.Tokens.Jwt;

namespace adapthub_api.Services
{
    public interface ITokenService
    {
        JwtSecurityToken BuildToken(string key, string issuer, int userId);
        bool ValidateToken(string key, string issuer, string audience, string token);

        void CheckAccess(string token, string type, int? expectedId = null);
    }
}
