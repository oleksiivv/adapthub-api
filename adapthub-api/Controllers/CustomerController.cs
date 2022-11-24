using adapthub_api.Models;
using adapthub_api.Repositories.Interfaces;
using adapthub_api.Services;
using adapthub_api.ViewModels.User;
using adapthub_api.ViewModels.Vacancy;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace adapthub_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ITokenService _tokenService;

        public CustomerController(ICustomerRepository customerRepository, ITokenService tokenService)
        {
            _customerRepository = customerRepository;
            _tokenService = tokenService;
        }

        [HttpGet("{id}")]
        public CustomerViewModel Get(int id, [FromHeader] string token)
        {
            _tokenService.CheckAccess(token, "Customer", id);

            return _customerRepository.Find(id);
        }

        [HttpPut("{id}")]
        public CustomerViewModel Put(int id, [FromBody] UpdateCustomerViewModel data, [FromHeader] string token)
        {
            _tokenService.CheckAccess(token, "Customer", id);

            data.Id = id;

            return _customerRepository.Update(data);
        }

        [HttpPut("{id}/help")]
        public CustomerViewModel ChooseHelp(int id, string help, [FromHeader] string token)
        {
            _tokenService.CheckAccess(token, "Customer", id);

            var updateCussstsomerViewModel = new UpdateCustomerViewModel
            {
                Id = id,
                HelpOption = help,
            };

            return _customerRepository.Update(updateCussstsomerViewModel);
        }
    }
}
