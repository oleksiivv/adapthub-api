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
        public IEnumerable<JobRequest> Get([FromBody] FilterJobRequestViewModel filter, string sort = "Id", string dir = "asc", int from = 0, int to = 10)
        {
            return _jobRequestRepository.List(filter, sort, dir, from, to);
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
        public JobRequest Put(int id, [FromBody] UpdateJobRequestViewModel data)
        {
            data.Id = id;
            data.Status = null;

            return _jobRequestRepository.Update(data);
        }

        [HttpPut("{id}/status")]
        public JobRequest UpdateStatus(int id, string status)
        {
            return _jobRequestRepository.Update(new UpdateJobRequestViewModel
            {
                Id = id,
                Status = status,
            });
        }

        [HttpDelete("{id}")]
        public JobRequest Delete(int id)
        {
            return _jobRequestRepository.Delete(id);
        }
    }
}
