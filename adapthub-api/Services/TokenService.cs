using adapthub_api.Models;
using adapthub_api.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;
using SendGrid.Helpers.Errors.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace adapthub_api.Services
{
    //TODO: create middleware with usage of this service
    public class TokenService : ITokenService
    {
        private const double EXPIRY_DURATION_MINUTES = 30;

        private readonly ICustomerRepository _customerRepository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IModeratorRepository _moderatorRepository;

        public TokenService(ICustomerRepository customerRepository, IModeratorRepository moderatorRepository, IOrganizationRepository organizationRepository)
        {
            _customerRepository = customerRepository;
            _organizationRepository = organizationRepository;
            _moderatorRepository = moderatorRepository;
        }

        public JwtSecurityToken BuildToken(string key, string issuer, int userId)
        {
            var claims = new[] {
                new Claim(ClaimTypes.Name, userId.ToString()),
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims,
                expires: DateTime.Now.AddMinutes(EXPIRY_DURATION_MINUTES), signingCredentials: credentials);

            return tokenDescriptor;
        }
        public bool ValidateToken(string key, string issuer, string audience, string token)
        {
            var mySecret = Encoding.UTF8.GetBytes(key);
            var mySecurityKey = new SymmetricSecurityKey(mySecret);
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = mySecurityKey,
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public void CheckAccess(string token, string type, int? expectedId = null)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwt = tokenHandler.ReadJwtToken(token);

            int id = Convert.ToInt32(jwt.Claims.ElementAt(0).Value);

            if(expectedId != null)
            {
                if (id != expectedId)
                {
                    throw new UnauthorizedAccessException("Помилка авторизації.");
                }
            }

            if (type.Equals("Customer"))
            {
                var user = _customerRepository.FindWithoutRelations(id);

                if (user == null)
                {
                    var user1 = _moderatorRepository.Find(id);

                    if (user1 == null) throw new ForbiddenException("Ви не авторизовані для цієї дії.");
                }
            }
            else if (type.Equals("Moderator"))
            {
                var user = _moderatorRepository.Find(id);

                if (user == null)
                {
                    throw new ForbiddenException("Ви не авторизовані для цієї дії.");
                }
            }
            else if (type.Equals("Organization"))
            {
                var user = _organizationRepository.Find(id);

                if (user == null)
                {
                    var user1 = _moderatorRepository.Find(id);

                    if (user1 == null) throw new ForbiddenException("Ви не авторизовані для цієї дії.");
                }
            }
            else
            {
                throw new ForbiddenException("Ви не авторизовані для цієї дії.");
            }
        }
    }
}
