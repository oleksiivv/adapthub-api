using adapthub_api.Models;
using adapthub_api.Repositories.Interfaces;
using adapthub_api.ViewModels.JobRequest;
using SendGrid.Helpers.Errors.Model;
using System.Net;

namespace adapthub_api.Repositories
{
    public class JobRequestRepository : IJobRequestRepository
    {
        private DataContext _data;
        public JobRequestRepository(DataContext data)
        {
            _data = data;
        }
        public JobRequest Find(int id)
        {
            var jobRequest = _data.JobRequests.Find(id);

            _data.Entry(jobRequest).Reference("User").Load();

            return jobRequest;
        }

        public IEnumerable<JobRequest> List(FilterJobRequestViewModel filter, string sort, int from, int to)
        {
            var jobRequests = _data.JobRequests.Where(x => (x.Status == filter.Status || filter.Status == null) && (x.User.Id == filter.UserId || filter.UserId == null)).Skip(from).Take(to - from);

            _data.Entry(jobRequests).Reference("User").Load();

            switch (sort)
            {
                case "Status":
                    jobRequests = jobRequests.OrderBy(x => x.Status);
                    break;
                case "UserId":
                    jobRequests = jobRequests.OrderBy(x => x.User.Id);
                    break;
                default:
                    jobRequests = jobRequests.OrderBy(x => x.Id);
                    break;
            }

            return jobRequests;
        }

        public JobRequest Create(CreateJobRequestViewModel data)
        {
            var jobRequest = new JobRequest
            {
                User = _data.Users.Find(data.UserId),
                Status = "DRAFT",
                Data = data.Data,
            };

            _data.JobRequests.Add(jobRequest);
            _data.SaveChanges();

            return jobRequest;
        }

        public JobRequest Update(UpdateJobRequestViewModel data)
        {
            var jobRequest = _data.JobRequests.Find(data.Id);

            if (jobRequest == null)
            {
                throw new NotFoundException();
            }

            //TODO: refactor this logic
            if (data.UserId != null)
            {
                jobRequest.User = _data.Users.Find(data.UserId);
            }

            if (data.Status != null)
            {
                jobRequest.Status = data.Status;
            }

            if (data.Data != null)
            {
                jobRequest.Data = data.Data;
            }
            _data.Update(jobRequest);

            _data.SaveChanges();

            return jobRequest;
        }

        public JobRequest Delete(int id)
        {
            var jobRequest = _data.JobRequests.Find(id);

            if (jobRequest == null)
            {
                throw new NotFoundException();
            }

            _data.JobRequests.Remove(jobRequest);

            _data.SaveChanges();

            return jobRequest;
        }
    }
}
