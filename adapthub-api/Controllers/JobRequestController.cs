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
        private readonly IUserRepository jobRequestRepossitory;

        public JobRequestController(IJobRequestRepository jobRequestRepository)
        {
            this.jobRequestRepossitory = jobRequestRepossitory;
        }

        [HttpGet]
        public IEnumerable<string> Get([FromBody] FilterJobRequestViewModel filter, int limit = 5, int offset = 0, string sort = "Id")
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] CreateJobRequestViewModel data)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UpdateJobRequestViewModel data)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
