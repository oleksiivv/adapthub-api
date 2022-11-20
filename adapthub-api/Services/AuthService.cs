using adapthub_api.Models;
using adapthub_api.Repositories.Interfaces;
using adapthub_api.Responses;
using adapthub_api.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace adapthub_api.Services
{
    //TODO: refactor this class
    public class AuthService : IAuthService
    {
        private UserManager<Customer> _userManger;
        private IConfiguration _configuration;
        private IMailService _mailService;
        private ITokenService _tokenService;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IModeratorRepository _moderatorRepository;
        public AuthService(UserManager<Customer> userManager, IConfiguration configuration, IMailService mailService, ITokenService tokenService, IOrganizationRepository organizationRepository, IModeratorRepository moderatorRepository)
        {
            _userManger = userManager;
            _configuration = configuration;
            _mailService = mailService;
            _tokenService = tokenService;
            _organizationRepository = organizationRepository;
            _moderatorRepository = moderatorRepository;
        }

        public async Task<UserManagerResponse> RegisterUserAsync(RegisterCustomerViewModel model)
        {
            if (model == null)
                throw new NullReferenceException("Reigster Model is null");

            if (model.Password != model.ConfirmPassword)
                return new UserManagerResponse
                {
                    Message = "Confirm password doesn't match the password",
                    IsSuccess = false,
                };
            GenderType gender;
            Enum.TryParse(model.Gender, out gender);

            var identityUser = new Customer
            {
                Email = model.Email,
                UserName = model.UserName,
                PassportNumber = model.PassportNumber,
                IDCode = model.IDCode,
                Gender = gender,
                CurrentAddress = model.CurrentAddress,
                PhoneNumber = model.PhoneNumber,
                Experience = new CustomerExperience{
                    Experience = model.Experience.Experience,
                    Education = model.Experience.Education,
                    PastJob = model.Experience.PastJob,
                    Profession = model.Experience.Profession,
                }
            };

            var result = await _userManger.CreateAsync(identityUser, model.Password);

            if (result.Succeeded)
            {
                var confirmEmailToken = await _userManger.GenerateEmailConfirmationTokenAsync(identityUser);

                var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
                var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                string url = $"{_configuration["AppUrl"]}api/auth/confirmemail?userid={identityUser.Id}&token={validEmailToken}";

                await _mailService.SendEmailAsync(identityUser.Email, "Підтверди свій акаунт", $"<h1>AdaptHub</h1>" +
                    $"<p>Підтвердити емейл: <a href='{url}'>Натисни тут</a></p>");


                return new UserManagerResponse
                {
                    Message = "User created successfully!",
                    IsSuccess = true,
                };
            }

            return new UserManagerResponse
            {
                Message = "User did not create",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };

        }

        public async Task<UserManagerResponse> LoginUserAsync(LoginViewModel model)
        {
            Customer user = null;
            Moderator moderator = null;
            Organization organization = null;

            string role = "Customer";

            user = await _userManger.FindByEmailAsync(model.Email);
            
            if (user == null)
            {
                moderator = _moderatorRepository.FindByEmail(model.Email);
                role = "Employee";

                if(moderator == null)
                {
                    organization = _organizationRepository.FindByEmail(model.Email);
                    role = "Organization";

                    if (organization == null)
                    {
                        return new UserManagerResponse
                        {
                            Message = "There is no user with that Email address",
                            IsSuccess = false,
                        };
                    }
                }
            }

            var result = await _userManger.CheckPasswordAsync(user, model.Password);

            if (!result)
            {
                result = _moderatorRepository.CheckPassword(model.Email, model.Password);

                if (!result)
                {
                    result = _organizationRepository.CheckPassword(model.Email, model.Password);

                    if (!result)
                    {
                        return new UserManagerResponse
                        {
                            Message = "Invalid password",
                            IsSuccess = false,
                        };
                    }
                }           
            }

            var currentUserId = user == null
                ? moderator == null ? organization.Id.ToString() : moderator.Id.ToString()
                : user.Id;

            var token = _tokenService.BuildToken(_configuration["AuthSettings:Key"], _configuration["AuthSettings:Issuer"], currentUserId);

            var tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserManagerResponse
            {
                Message = tokenAsString,
                IsSuccess = true,
                ExpireDate = token.ValidTo,
                Role = role,
            };
        }

        public async Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManger.FindByIdAsync(userId);
            if (user == null)
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "User not found"
                };

            /*if (!_tokenService.ValidateToken(_configuration["AuthSettings:Key"], _configuration["AuthSettings:Issuer"], _configuration["AuthSettings:Audience"], token))
            {
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "Invalid token"
                };
            }*/

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManger.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)
                return new UserManagerResponse
                {
                    Message = "Email confirmed successfully!",
                    IsSuccess = true,
                };

            return new UserManagerResponse
            {
                IsSuccess = false,
                Message = "Email did not confirmed",
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task<UserManagerResponse> ForgetPasswordAsync(string email)
        {
            var user = await _userManger.FindByEmailAsync(email);
            if (user == null)
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "No user associated with email",
                };

            var token = await _userManger.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            //TODO: investigate better solution to give user access token
            await _mailService.SendEmailAsync(email, "Змінити пароль", "<h1>Виконай інструкцію шоб змінити пароль</h1>" +
                $"<p>Щоб змінити пароль скопіюй токен: {validToken}");

            return new UserManagerResponse
            {
                IsSuccess = true,
                Message = "Reset password URL has been sent to the email successfully!"
            };
        }

        public async Task<UserManagerResponse> ResetPasswordAsync(ResetCustomerPasswordViewModel model)
        {
            var user = await _userManger.FindByEmailAsync(model.Email);
            if (user == null)
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "No user associated with email",
                };

            if (model.NewPassword != model.ConfirmPassword)
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "Password doesn't match its confirmation",
                };

            var decodedToken = WebEncoders.Base64UrlDecode(model.Token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManger.ResetPasswordAsync(user, normalToken, model.NewPassword);

            if (result.Succeeded)
                return new UserManagerResponse
                {
                    Message = "Password has been reset successfully!",
                    IsSuccess = true,
                };

            return new UserManagerResponse
            {
                Message = "Something went wrong",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description),
            };
        }
    }
}