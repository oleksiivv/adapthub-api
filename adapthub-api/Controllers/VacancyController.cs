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
        public IEnumerable<Vacancy> Get([FromBody] FilterVacancyViewModel filter, int from = 0, int to = 10, string sort = "Id", string dir = "asc")
        {
            return _vacancyRepository.List(filter, sort, dir, from, to);
        }

        [HttpGet("{id}")]
        public Vacancy Get(int id)
        {
            return _vacancyRepository.Find(id);
        }

        [HttpPost]
        public Vacancy Post([FromBody] CreateVacancyViewModel data, [FromHeader] string token)
        {
            _tokenService.CheckAccess(token, "Organization");

            return _vacancyRepository.Create(data);
        }

        [HttpPut("{id}")]
        public Vacancy Put(int id, [FromBody] UpdateVacancyViewModel data, [FromHeader] string token)
        {
            _tokenService.CheckAccess(token, "Organization");

            data.Id = id;
            data.Status = null;

            return _vacancyRepository.Update(data);
        }

        [HttpPut("{id}/confirm-job-request/{jobRequestId}")]
        public Vacancy ConfirmJobRequest(int id, int jobRequestId, int customerId, [FromHeader] string token)
        {
            _tokenService.CheckAccess(token, "Organization", customerId.ToString());

            _vacancyProcessService.ChooseJobRequestForVacancy(id, jobRequestId);

            return _vacancyRepository.ChooseJobRequest(id, jobRequestId);
        }

        [HttpPut("{id}/job-request/{jobRequestId}")]
        public void AskForVacancy(int id, int jobRequestId, [FromHeader] string token)
        {
            _tokenService.CheckAccess(token, "Customer");

            _vacancyProcessService.AskForVacancy(id, jobRequestId);
        }

        [HttpPut("{id}/status")]
        public Vacancy UpdateStatus(int id, string status, [FromHeader] string token)
        {
            _tokenService.CheckAccess(token, "Organization");

            return _vacancyRepository.Update(new UpdateVacancyViewModel
            {
                Id = id,
                Status = status,
            });
        }

        [HttpDelete("{id}")]
        public Vacancy Delete(int id, [FromHeader] string token)
        {
            _tokenService.CheckAccess(token, "Organization");

            return _vacancyRepository.Delete(id);
        }
    }
}
