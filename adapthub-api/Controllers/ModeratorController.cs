using adapthub_api.Models;
using adapthub_api.Repositories.Interfaces;
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

        public ModeratorController(IModeratorRepository moderatorRepository)
        {
            _moderatorRepository = moderatorRepository;
        }

        [HttpGet("{id}")]
        public Moderator Get(int id)
        {
            return _moderatorRepository.Find(id);
        }

        [HttpPost("{id}")]
        public Moderator Post([FromBody] CreateModeratorViewModel data)
        {
            return _moderatorRepository.Create(data);
        }
    }
}
