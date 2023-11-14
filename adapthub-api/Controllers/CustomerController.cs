using adapthub_api.Repositories.Interfaces;
using adapthub_api.Responses;
using adapthub_api.Services;
using adapthub_api.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;

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
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerViewModel), 200)]
        public IActionResult Get(int id, [FromHeader] string token)
        {
            try
            {
                ValidateTokenAndAccess(token, "Customer", id);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (ForbiddenException)
            {
                return Forbid();
            }

            var customer = _customerRepository.Find(id);
            return Ok(customer);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CustomerViewModel), 200)]
        public IActionResult Put(int id, [FromBody] UpdateCustomerViewModel data, [FromHeader] string token)
        {
            try
            {
                ValidateTokenAndAccess(token, "Customer", id);
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

            var updatedCustomer = _customerRepository.Update(data);
            return Ok(updatedCustomer);
        }

        [HttpPut("{id}/help")]
        [ProducesResponseType(typeof(CustomerViewModel), 200)]
        public IActionResult ChooseHelp(int id, string help, [FromHeader] string token)
        {
            try
            {
                ValidateTokenAndAccess(token, "Customer", id);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (ForbiddenException)
            {
                return Forbid();
            }

            var updateCustomerViewModel = new UpdateCustomerViewModel
            {
                Id = id,
                HelpOption = help,
            };

            var updatedCustomer = _customerRepository.Update(updateCustomerViewModel);
            return Ok(updatedCustomer);
        }

        private void ValidateTokenAndAccess(string token, string role, int id)
        {
            _tokenService.CheckAccess(token, role, id);
        }
    }
}
