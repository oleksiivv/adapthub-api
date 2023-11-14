using Microsoft.AspNetCore.Mvc;
using adapthub_api.Repositories.Interfaces;
using adapthub_api.ViewModels.Organization;
using adapthub_api.Models;
using adapthub_api.Services;
using adapthub_api.ViewModels.JobRequest;
using SendGrid.Helpers.Errors.Model;

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
            _organizationRepository = organizationRepository ?? throw new ArgumentNullException(nameof(organizationRepository));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
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
        [ProducesResponseType(typeof(Organization), 200)]
        public async Task<IActionResult> Post([FromBody] CreateOrganizationViewModel data, [FromHeader] string token)
        {
            try
            {
                _tokenService.CheckAccess(token, "Moderator");
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (ForbiddenException)
            {
                return Forbid();
            }

            return Ok(_organizationRepository.Create(data));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Organization), 200)]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateOrganizationViewModel data, [FromHeader] string token)
        {
            data.Id = id;

            try
            {
                _tokenService.CheckAccess(token, "Organization", id);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (ForbiddenException)
            {
                return Forbid();
            }

            return Ok(_organizationRepository.Update(data));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Organization), 200)]
        public async Task<IActionResult> Delete(int id, [FromHeader] string token)
        {
            try
            {
                _tokenService.CheckAccess(token, "Organization", id);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (ForbiddenException)
            {
                return Forbid();
            }

            return Ok(_organizationRepository.Delete(id));
        }
    }
}
