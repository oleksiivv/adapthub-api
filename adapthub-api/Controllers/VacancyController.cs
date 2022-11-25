using adapthub_api.Models;
using adapthub_api.Repositories;
using adapthub_api.Repositories.Interfaces;
using adapthub_api.Services;
using adapthub_api.ViewModels.JobRequest;
using adapthub_api.ViewModels.Organization;
using adapthub_api.ViewModels.Vacancy;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SendGrid.Helpers.Errors.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace adapthub_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacancyController : ControllerBase
    {
        private readonly IVacancyRepository _vacancyRepository;
        private readonly IVacancyProcessService _vacancyProcessService;
        private readonly ITokenService _tokenService;

        public VacancyController(IVacancyRepository vacancyRepository, IVacancyProcessService vacancyProcessService, ITokenService tokenService)
        {
            _vacancyRepository = vacancyRepository;
            _vacancyProcessService = vacancyProcessService;
            _tokenService = tokenService;
        }

        [HttpGet]
        public ListVacancies Get([FromQuery] FilterVacancyViewModel filter, int from = 0, int to = 10, string sort = "Id", string dir = "asc")
        {
            return _vacancyRepository.List(filter, sort, dir, from, to);
        }

        [HttpGet("{id}")]
        public VacancyViewModel Get(int id)
        {
            return _vacancyRepository.Find(id);
        }

        [HttpPost]
        [ProducesResponseType(typeof(VacancyViewModel), 200)]
        public async Task<IActionResult> Post([FromBody] CreateVacancyViewModel data, [FromHeader] string token)
        {
            try
            {
                _tokenService.CheckAccess(token, "Organization");
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(401);
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }

            return Ok(_vacancyRepository.Create(data));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(VacancyViewModel), 200)]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateVacancyViewModel data, [FromHeader] string token)
        {
            try
            {
                _tokenService.CheckAccess(token, "Organization");
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(401);
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }

            data.Id = id;
            data.Status = null;

            return Ok(_vacancyRepository.Update(data));
        }

        [HttpPut("{id}/confirm-job-request/{jobRequestId}/confirm")]
        [ProducesResponseType(typeof(VacancyViewModel), 200)]
        public async Task<IActionResult> ConfirmJobRequest(int id, int jobRequestId, int customerId, [FromHeader] string token)
        {
            try
            {
                _tokenService.CheckAccess(token, "Organization");
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(401);
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }

            _vacancyProcessService.ChooseJobRequestForVacancy(id, jobRequestId);

            return Ok(_vacancyRepository.ChooseJobRequest(id, jobRequestId));
        }

        [HttpPut("{id}/confirm-job-request/{jobRequestId}/cancel")]
        [ProducesResponseType(typeof(VacancyViewModel), 200)]
        public async Task<IActionResult> CancelJobRequest(int id, int jobRequestId, int customerId, [FromHeader] string token)
        {
            try
            {
                _tokenService.CheckAccess(token, "Organization");
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(401);
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }

            _vacancyProcessService.CancelJobRequestForVacancy(id, jobRequestId);

            return Ok(_vacancyRepository.Find(id));
        }

        [HttpPut("{id}/job-request/{jobRequestId}")]
        public async Task<IActionResult> AskForVacancy(int id, int jobRequestId, [FromHeader] string token)
        {
            try
            {
                _tokenService.CheckAccess(token, "Customer");
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(401);
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }

            var result = _vacancyProcessService.AskForVacancy(id, jobRequestId);

            if (result.IsCompletedSuccessfully)
            {
                return Ok();
            }
            else
            {
                return UnprocessableEntity();
            }
        }

        [HttpPut("{id}/status")]
        [ProducesResponseType(typeof(VacancyViewModel), 200)]
        public async Task<IActionResult> UpdateStatus(int id, string status, [FromHeader] string token)
        {
            try
            {
                _tokenService.CheckAccess(token, "Organization");
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(401);
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }

            return Ok(_vacancyRepository.Update(new UpdateVacancyViewModel
            {
                Id = id,
                Status = status,
            }));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(VacancyViewModel), 200)]
        public async Task<IActionResult> Delete(int id, [FromHeader] string token)
        {
            try
            {
                _tokenService.CheckAccess(token, "Organization");
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(401);
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }

            return Ok(_vacancyRepository.Delete(id));
        }
    }
}
