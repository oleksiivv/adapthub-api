using adapthub_api.Models;
using adapthub_api.Repositories.Interfaces;
using adapthub_api.Services;
using adapthub_api.ViewModels.JobRequest;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace adapthub_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobRequestController : ControllerBase
    {
        private readonly IJobRequestRepository _jobRequestRepository;
        private readonly ITokenService _tokenService;

        public JobRequestController(IJobRequestRepository jobRequestRepository, ITokenService tokenService)
        {
            _jobRequestRepository = jobRequestRepository;
            _tokenService = tokenService;
        }

        [HttpGet]
        public IEnumerable<JobRequestViewModel> Get([FromBody] FilterJobRequestViewModel filter, string sort = "Id", string dir = "asc", int from = 0, int to = 10)
        {
            return _jobRequestRepository.List(filter, sort, dir, from, to);
        }

        [HttpGet("{id}")]
        public JobRequestViewModel Get(int id)
        {
            return _jobRequestRepository.Find(id);
        }

        [HttpPost]
        public JobRequestViewModel Post([FromBody] CreateJobRequestViewModel data, [FromHeader] string token)
        {
            _tokenService.CheckAccess(token, "Customer");

            return _jobRequestRepository.Create(data);
        }

        [HttpPut("{id}")]
        public JobRequestViewModel Put(int id, [FromBody] UpdateJobRequestViewModel data, [FromHeader] string token)
        {
            _tokenService.CheckAccess(token, "Customer");

            data.Id = id;
            data.Status = null;

            return _jobRequestRepository.Update(data);
        }

        [HttpPut("{id}/status")]
        public JobRequestViewModel UpdateStatus(int id, string status, [FromHeader] string token)
        {
            _tokenService.CheckAccess(token, "Moderator");

            return _jobRequestRepository.Update(new UpdateJobRequestViewModel
            {
                Id = id,
                Status = status,
            });
        }

        [HttpDelete("{id}")]
        public JobRequestViewModel Delete(int id, [FromHeader] string token)
        {
            _tokenService.CheckAccess(token, "Customer");

            return _jobRequestRepository.Delete(id);
        }
    }
}
