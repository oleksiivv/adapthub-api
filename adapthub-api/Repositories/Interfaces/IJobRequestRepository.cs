using adapthub_api.Models;
using adapthub_api.ViewModels.JobRequest;
using SendGrid.Helpers.Errors.Model;
using System.Xml.Linq;

namespace adapthub_api.Repositories.Interfaces
{
    public interface IJobRequestRepository
    {
        public JobRequest Find(int id);
        public IEnumerable<JobRequest> List(FilterJobRequestViewModel filter, string sort, string direction, int from, int to);
        public JobRequest Create(CreateJobRequestViewModel data);
        public JobRequest Update(UpdateJobRequestViewModel data);
        public JobRequest Delete(int id);
    }
}
