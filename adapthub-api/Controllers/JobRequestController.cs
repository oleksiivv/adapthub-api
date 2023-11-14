using System;
using System.Threading.Tasks;
using adapthub_api.Models;
using adapthub_api.Repositories.Interfaces;
using adapthub_api.Services;
using adapthub_api.ViewModels.JobRequest;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;

namespace adapthub_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobRequestController : ControllerBase
    {
        private readonly IJobRequestRepository _jobRequestRepository;
        private readonly ITokenService _tokenService;
        private readonly IVacancyProcessService _vacancyProcessService;

        public JobRequestController(IJobRequestRepository jobRequestRepository, ITokenService tokenService, IVacancyProcessService vacancyProcessService)
        {
            _jobRequestRepository = jobRequestRepository ?? throw new ArgumentNullException(nameof(jobRequestRepository));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _vacancyProcessService = vacancyProcessService ?? throw new ArgumentNullException(nameof(vacancyProcessService));
        }

        [HttpGet]
        public ListJobRequests Get([FromQuery] FilterJobRequestViewModel filter, string sort = "Id", string dir = "asc", int from = 0, int to = 10)
        {
            return _jobRequestRepository.List(filter, sort, dir, from, to);
        }

        [HttpGet("{id}")]
        public JobRequestViewModel Get(int id)
        {
            return _jobRequestRepository.Find(id);
        }

        [HttpPost]
        [ProducesResponseType(typeof(JobRequestViewModel), 200)]
        public async Task<IActionResult> Post([FromBody] CreateJobRequestViewModel data, [FromHeader] string token)
        {
            try
            {
                _tokenService.CheckAccess(token, "Customer");
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (ForbiddenException)
            {
                return Forbid();
            }

            return Ok(_jobRequestRepository.Create(data));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(JobRequestViewModel), 200)]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateJobRequestViewModel data, [FromHeader] string token)
        {
            try
            {
                _tokenService.CheckAccess(token, "Customer");
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
            data.Status = null;

            return Ok(_jobRequestRepository.Update(data));
        }

        [HttpPut("{id}/vacancy/{vacancyId}")]
        public async Task<IActionResult> AskForJobRequest(int id, int vacancyId, [FromHeader] string token)
        {
            try
            {
                _tokenService.CheckAccess(token, "Customer");
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (ForbiddenException)
            {
                return Forbid();
            }

            var result = _vacancyProcessService.AskForJobRequest(vacancyId, id);

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
        [ProducesResponseType(typeof(JobRequestViewModel), 200)]
        public async Task<IActionResult> UpdateStatus(int id, string status, [FromHeader] string token)
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

            return Ok(_jobRequestRepository.Update(new UpdateJobRequestViewModel
            {
                Id = id,
                Status = status,
            }));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(JobRequestViewModel), 200)]
        public async Task<IActionResult> Delete(int id, [FromHeader] string token)
        {
            try
            {
                _tokenService.CheckAccess(token, "Customer");
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (ForbiddenException)
            {
                return Forbid();
            }

            return Ok(_jobRequestRepository.Delete(id));
        }
    }
}
