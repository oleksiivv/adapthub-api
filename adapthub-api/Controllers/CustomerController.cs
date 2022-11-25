using adapthub_api.Models;
using adapthub_api.Repositories.Interfaces;
using adapthub_api.Responses;
using adapthub_api.Services;
using adapthub_api.ViewModels.User;
using adapthub_api.ViewModels.Vacancy;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SendGrid.Helpers.Errors.Model;
using System.Net;
using System.Web.Http;

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
        [ProducesResponseType(typeof(CustomerViewModel), 200)]
        public async Task<IActionResult> Get(int id, [FromHeader] string token)
        {
            try
            {
                _tokenService.CheckAccess(token, "Customer", id);
            }
            catch (UnauthorizedAccessException exception)
            {
                return StatusCode(401);
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }

            return Ok(_customerRepository.Find(id));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CustomerViewModel), 200)]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateCustomerViewModel data, [FromHeader] string token)
        {
            try
            {
                _tokenService.CheckAccess(token, "Customer", id);
            }
            catch (UnauthorizedAccessException exception)
            {
                return StatusCode(401);
            } 
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }

            data.Id = id;

            return Ok(_customerRepository.Update(data));
        }

        [HttpPut("{id}/help")]
        [ProducesResponseType(typeof(CustomerViewModel), 200)]
        public async Task<IActionResult> ChooseHelp(int id, string help, [FromHeader] string token)
        {
            try
            {
                _tokenService.CheckAccess(token, "Customer", id);
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(401);
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }


            var updateCussstsomerViewModel = new UpdateCustomerViewModel
            {
                Id = id,
                HelpOption = help,
            };

            return Ok(_customerRepository.Update(updateCussstsomerViewModel));
        }
    }
}
