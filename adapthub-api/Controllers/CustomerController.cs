﻿using adapthub_api.Models;
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
        public Customer Get(string id, [FromHeader] string token)
        {
            _tokenService.CheckAccess(token, "Customer", id);

            return _customerRepository.Find(id);
        }

        [HttpPut("{id}")]
        public Customer Put(string id, [FromBody] UpdateCustomerViewModel data, [FromHeader] string token)
        {
            _tokenService.CheckAccess(token, "Customer", id);

            data.Id = id;

            return _customerRepository.Update(data);
        }
    }
}