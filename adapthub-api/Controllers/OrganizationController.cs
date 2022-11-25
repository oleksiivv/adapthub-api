using Microsoft.AspNetCore.Mvc;
using adapthub_api.Repositories.Interfaces;
using adapthub_api.ViewModels.Organization;
using adapthub_api.Models;
using adapthub_api.Repositories;
using adapthub_api.ViewModels.JobRequest;
using adapthub_api.Services;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace adapthub_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly ITokenService _tokenService;

        public OrganizationController(IOrganizationRepository organizationRepository, ITokenService tokenService)
        {
            _organizationRepository = organizationRepository;
            _tokenService = tokenService;
        }

        [HttpGet]
        public ListOrganizations Get([FromQuery] FilterOrganizationViewModel filter, int from = 0, int to = 10, string sort = "Id", string direction = "asc")
        {
            return _organizationRepository.List(filter, sort, direction, from, to); 
        }

        [HttpGet("{id}")]
        public Organization Get(int id)
        {
            var organization = _organizationRepository.Find(id);
            organization.PasswordHash = null;

            return organization;
        }

        [HttpPost]
        public Organization Post([FromBody] CreateOrganizationViewModel data, [FromHeader] string token)
        {
            _tokenService.CheckAccess(token, "Moderator");

            return _organizationRepository.Create(data);
        }

        [HttpPut("{id}")]
        public Organization Put(int id, [FromBody] UpdateOrganizationViewModel data, [FromHeader] string token)
        {
            data.Id = id;

            _tokenService.CheckAccess(token, "Organization", id);

            return _organizationRepository.Update(data);
        }

        [HttpDelete("{id}")]
        public Organization Delete(int id, [FromHeader] string token)
        {
            _tokenService.CheckAccess(token, "Organization", id);

            return _organizationRepository.Delete(id);
        }
    }
}
