using adapthub_api.Models;
using adapthub_api.Repositories.Interfaces;
using adapthub_api.ViewModels.User;
using adapthub_api.ViewModels.Vacancy;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace adapthub_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{id}")]
        public User Get(string id)
        {
            return _userRepository.Find(id);
        }

        [HttpPut("{id}")]
        public User Put([FromBody] UpdateUserViewModel data)
        {
            return _userRepository.Update(data);
        }
    }
}
