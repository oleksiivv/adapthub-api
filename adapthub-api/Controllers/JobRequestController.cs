using adapthub_api.Models;
using adapthub_api.Repositories.Interfaces;
using adapthub_api.ViewModels.JobRequest;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace adapthub_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobRequestController : ControllerBase
    {
        private readonly IJobRequestRepository _jobRequestRepository;

        public JobRequestController(IJobRequestRepository jobRequestRepository)
        {
            _jobRequestRepository = jobRequestRepository;
        }

        [HttpGet]
        public IEnumerable<JobRequest> Get([FromBody] FilterJobRequestViewModel filter, int from = 0, int to = 10, string sort = "Id")
        {
            return _jobRequestRepository.List(filter, sort, from, to);
        }

        [HttpGet("{id}")]
        public JobRequest Get(int id)
        {
            return _jobRequestRepository.Find(id);
        }

        [HttpPost]
        public JobRequest Post([FromBody] CreateJobRequestViewModel data)
        {
            return _jobRequestRepository.Create(data);
        }

        [HttpPut("{id}")]
        public JobRequest Put([FromBody] UpdateJobRequestViewModel data)
        {
            return _jobRequestRepository.Update(data);
        }

        [HttpDelete("{id}")]
        public JobRequest Delete(int id)
        {
            return _jobRequestRepository.Delete(id);
        }
    }
}
