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
        public IEnumerable<Organization> Get([FromBody] FilterOrganizationViewModel filter, int from = 0, int to = 10, string sort = "Id")
        {
            var organizations = _organizationRepository.List(filter, sort, from, to).ToList();

            for(int i=0; i<organizations.Count(); i++)
            {
                var organization = organizations[i];
                organization.PasswordHash = null;
                organizations[i] = organization;
            }

            return organizations;
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

            _tokenService.CheckAccess(token, "Organization", id.ToString());

            return _organizationRepository.Update(data);
        }

        [HttpDelete("{id}")]
        public Organization Delete(int id, [FromHeader] string token)
        {
            _tokenService.CheckAccess(token, "Organization", id.ToString());

            return _organizationRepository.Delete(id);
        }
    }
}
