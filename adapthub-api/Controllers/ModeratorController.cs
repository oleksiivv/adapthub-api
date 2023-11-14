using adapthub_api.Models;
using adapthub_api.Repositories.Interfaces;
using adapthub_api.Services;
using adapthub_api.ViewModels.Moderator;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;

namespace adapthub_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModeratorController : ControllerBase
    {
        private readonly IModeratorRepository _moderatorRepository;
        private readonly ITokenService _tokenService;

        public ModeratorController(IModeratorRepository moderatorRepository, ITokenService tokenService)
        {
            _moderatorRepository = moderatorRepository ?? throw new ArgumentNullException(nameof(moderatorRepository));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Moderator), 200)]
        public IActionResult Get(int id, [FromHeader] string token)
        {
            try
            {
                ValidateTokenAndPermissions(token, "Moderator", id);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (ForbiddenException)
            {
                return Forbid();
            }

            var moderator = _moderatorRepository.Find(id);
            return Ok(moderator);
        }

        // ONLY FOR DEBUG
        [HttpPost]
        public IActionResult Post([FromBody] CreateModeratorViewModel data)
        {
            var createdModerator = _moderatorRepository.Create(data);
            return Ok(createdModerator);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Moderator), 200)]
        public IActionResult Put(int id, [FromHeader] string token, [FromBody] UpdateModeratorViewModel data)
        {
            try
            {
                ValidateTokenAndPermissions(token, "Moderator", id);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (ForbiddenException)
            {
                return Forbid();
            }

            data.Id = id;
            var updatedModerator = _moderatorRepository.Update(data);
            updatedModerator.PasswordHash = null;

            return Ok(updatedModerator);
        }

        [HttpPost("/seed")]
        public IActionResult SeedDB()
        {
            _moderatorRepository.SeedDB();
            return Ok(); // Assuming you want to return a success status for seeding the database
        }

        private void ValidateTokenAndPermissions(string token, string role, int id)
        {
            _tokenService.CheckAccess(token, role, id);
        }
    }
}
