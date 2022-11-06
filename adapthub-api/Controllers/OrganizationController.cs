using Microsoft.AspNetCore.Mvc;
using adapthub_api.Repositories.Interfaces;
using adapthub_api.ViewModels.Organization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace adapthub_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationRepository organizationRepository;

        public OrganizationController(IOrganizationRepository organizationRepository)
        {
            this.organizationRepository = organizationRepository;
        }

        [HttpGet]
        public IEnumerable<string> Get([FromBody] FilterOrganizationViewModel data, int limit = 5, int offset = 0, string sort = "Id")
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] CreateOrganizationViewModel data)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UpdateOrganizationViewModel data)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
