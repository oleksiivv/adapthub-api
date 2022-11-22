using adapthub_api.Models;
using adapthub_api.Repositories;
using adapthub_api.Repositories.Interfaces;
using adapthub_api.Services;
using adapthub_api.ViewModels.JobRequest;
using adapthub_api.ViewModels.Organization;
using adapthub_api.ViewModels.Vacancy;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

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
        public ListVacancies Get([FromBody] FilterVacancyViewModel filter, int from = 0, int to = 10, string sort = "Id", string dir = "asc")
        {
            return _vacancyRepository.List(filter, sort, dir, from, to);
        }

        [HttpGet("{id}")]
        public VacancyViewModel Get(int id)
        {
            return _vacancyRepository.Find(id);
        }

        [HttpPost]
        public VacancyViewModel Post([FromBody] CreateVacancyViewModel data, [FromHeader] string token)
        {
            _tokenService.CheckAccess(token, "Organization");

            return _vacancyRepository.Create(data);
        }

        [HttpPut("{id}")]
        public VacancyViewModel Put(int id, [FromBody] UpdateVacancyViewModel data, [FromHeader] string token)
        {
            _tokenService.CheckAccess(token, "Organization");

            data.Id = id;
            data.Status = null;

            return _vacancyRepository.Update(data);
        }

        [HttpPut("{id}/confirm-job-request/{jobRequestId}/confirm")]
        public VacancyViewModel ConfirmJobRequest(int id, int jobRequestId, int customerId, [FromHeader] string token)
        {
            _tokenService.CheckAccess(token, "Organization", customerId);

            _vacancyProcessService.ChooseJobRequestForVacancy(id, jobRequestId);

            return _vacancyRepository.ChooseJobRequest(id, jobRequestId);
        }

        [HttpPut("{id}/confirm-job-request/{jobRequestId}/cancel")]
        public VacancyViewModel CancelJobRequest(int id, int jobRequestId, int customerId, [FromHeader] string token)
        {
            _tokenService.CheckAccess(token, "Organization", customerId);

            _vacancyProcessService.CancelJobRequestForVacancy(id, jobRequestId);

            return _vacancyRepository.Find(id);
        }

        [HttpPut("{id}/job-request/{jobRequestId}")]
        public async Task<IActionResult> AskForVacancy(int id, int jobRequestId, [FromHeader] string token)
        {
            _tokenService.CheckAccess(token, "Customer");

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
        public VacancyViewModel UpdateStatus(int id, string status, [FromHeader] string token)
        {
            _tokenService.CheckAccess(token, "Organization");

            return _vacancyRepository.Update(new UpdateVacancyViewModel
            {
                Id = id,
                Status = status,
            });
        }

        [HttpDelete("{id}")]
        public VacancyViewModel Delete(int id, [FromHeader] string token)
        {
            _tokenService.CheckAccess(token, "Organization");

            return _vacancyRepository.Delete(id);
        }
    }
}
