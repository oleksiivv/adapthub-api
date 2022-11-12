using Microsoft.AspNetCore.Mvc;
using adapthub_api.Repositories.Interfaces;
using adapthub_api.ViewModels.Organization;
using adapthub_api.Models;
using adapthub_api.Repositories;
using adapthub_api.ViewModels.JobRequest;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace adapthub_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationRepository _organizationRepository;

        public OrganizationController(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        [HttpGet]
        public IEnumerable<Organization> Get([FromBody] FilterOrganizationViewModel filter, int from = 0, int to = 10, string sort = "Id")
        {
            return _organizationRepository.List(filter, sort, from, to);
        }

        [HttpGet("{id}")]
        public Organization Get(int id)
        {
            return _organizationRepository.Find(id);
        }

        [HttpPost]
        public Organization Post([FromBody] CreateOrganizationViewModel data)
        {
            return _organizationRepository.Create(data);
        }

        [HttpPut("{id}")]
        public Organization Put([FromBody] UpdateOrganizationViewModel data)
        {
            return _organizationRepository.Update(data);
        }

        [HttpDelete("{id}")]
        public Organization Delete(int id)
        {
            return _organizationRepository.Delete(id);
        }
    }
}
