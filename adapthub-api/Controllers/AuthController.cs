using adapthub_api.Responses;
using adapthub_api.Services;
using adapthub_api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace adapthub_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _userService;
        private readonly IMailService _mailService;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthService userService, IMailService mailService, IConfiguration configuration)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost("Register")]
        [ProducesResponseType(typeof(UserManagerResponse), 200)]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterCustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(model);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest("Деякі поля невірні.");
        }

        [HttpPost("Login")]
        [ProducesResponseType(typeof(UserManagerResponse), 200)]
        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUserAsync(model);

                if (result.IsSuccess)
                {
                    await _mailService.SendEmailAsync(model.Email, "Новий вхід", $"<h1>Привіт! Ми помітили новий вхід на ваш акаунт!</h1><p>Сталося це {DateTime.Now}</p>");
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Деякі поля невірні.");
        }

        [HttpGet("ConfirmEmail")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
                return NotFound();

            var result = await _userService.ConfirmEmailAsync(userId, token);

            if (result.IsSuccess)
                return NoContent();

            return BadRequest(result);
        }

        [HttpPost("ForgetPassword")]
        [ProducesResponseType(typeof(UserManagerResponse), 200)]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
                return NotFound();

            var result = await _userService.ForgetPasswordAsync(email);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("ResetPassword")]
        [ProducesResponseType(typeof(UserManagerResponse), 200)]
        public async Task<IActionResult> ResetPassword([FromForm] ResetCustomerPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ResetPasswordAsync(model);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest("Деякі поля невірні.");
        }
    }
}
