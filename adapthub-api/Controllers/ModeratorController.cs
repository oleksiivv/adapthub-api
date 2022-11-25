using adapthub_api.Models;
using adapthub_api.Repositories.Interfaces;
using adapthub_api.Services;
using adapthub_api.ViewModels.Moderator;
using adapthub_api.ViewModels.User;
using adapthub_api.ViewModels.Vacancy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SendGrid.Helpers.Errors.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
            _moderatorRepository = moderatorRepository;
            _tokenService = tokenService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Moderator), 200)]
        public async Task<IActionResult> Get(int id, [FromHeader] string token)
        {
            try
            {
                _tokenService.CheckAccess(token, "Moderator", id);
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(401);
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }

            return Ok(_moderatorRepository.Find(id));
        }

        [HttpPost]
        public Moderator Post([FromBody] CreateModeratorViewModel data)
        {
            return _moderatorRepository.Create(data);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Moderator), 200)]
        public async Task<IActionResult> Put(int id, [FromHeader] string token,  [FromBody] UpdateModeratorViewModel data)
        {
            try
            {
                _tokenService.CheckAccess(token, "Moderator", id);
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(401);
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }

            data.Id = id;

            var moderator = _moderatorRepository.Update(data);
            moderator.PasswordHash = null;

            return Ok(moderator);
        }

        [HttpPost("/seed")]
        public void SeedDB()
        {
            _moderatorRepository.SeedDB();
        }
    }
}
