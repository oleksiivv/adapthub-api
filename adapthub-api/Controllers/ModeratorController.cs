using adapthub_api.Models;
using adapthub_api.Repositories.Interfaces;
using adapthub_api.Services;
using adapthub_api.ViewModels.Moderator;
using adapthub_api.ViewModels.User;
using adapthub_api.ViewModels.Vacancy;
using Microsoft.AspNetCore.Mvc;

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
        public Moderator Get(int id, [FromHeader] string token)
        {
            _tokenService.CheckAccess(token, "Moderator", id);

            return _moderatorRepository.Find(id);
        }

        [HttpPost]
        public Moderator Post([FromBody] CreateModeratorViewModel data)
        {
            return _moderatorRepository.Create(data);
        }

        [HttpPost("/seed")]
        public void SeedDB()
        {
            _moderatorRepository.SeedDB();
        }
    }
}
