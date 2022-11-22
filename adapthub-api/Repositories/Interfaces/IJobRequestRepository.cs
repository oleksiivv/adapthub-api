using adapthub_api.Models;
using adapthub_api.ViewModels.JobRequest;
using SendGrid.Helpers.Errors.Model;
using System.Xml.Linq;

namespace adapthub_api.Repositories.Interfaces
{
    public interface IJobRequestRepository
    {
        public JobRequestViewModel Find(int id);
        public ListJobRequests List(FilterJobRequestViewModel filter, string sort, string direction, int from, int to);
        public JobRequestViewModel Create(CreateJobRequestViewModel data);
        public JobRequestViewModel Update(UpdateJobRequestViewModel data);
        public JobRequestViewModel Delete(int id);
    }
}
