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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet("{id}")]
        public Customer Get(string id)
        {
            return _customerRepository.Find(id);
        }

        [HttpPut("{id}")]
        public Customer Put([FromBody] UpdateCustomerViewModel data)
        {
            return _customerRepository.Update(data);
        }
    }
}
