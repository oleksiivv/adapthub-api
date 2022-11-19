using adapthub_api.Models;
using adapthub_api.Repositories.Interfaces;
using adapthub_api.ViewModels;
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

            _data.Entry(jobRequest).Reference("Customer").Load();

            return jobRequest;
        }

        public IEnumerable<JobRequest> List(FilterJobRequestViewModel filter, string sort, string direction, int from, int to)
        {
            //TODO: refactor this fucking mess later
            var jobRequests = _data.JobRequests.Where(x => (x.Status.ToString() == filter.Status.ToString() || filter.Status == null) && (x.Customer.Id == filter.CustomerId || filter.CustomerId == null) && (x.Speciality == filter.Speciality || filter.Speciality == null) && (x.ExpectedSalary <= filter.ExpectedSalary || filter.ExpectedSalary == null)).Skip(from).Take(to - from);

            _data.Entry(jobRequests).Reference("User").Load();

            switch (sort)
            {
                case "Status":
                    jobRequests = jobRequests.OrderBy(x => x.Status);
                    break;
                case "CustomerId":
                    jobRequests = jobRequests.OrderBy(x => x.Customer.Id);
                    break;
                case "Speciality":
                    jobRequests = jobRequests.OrderBy(x => x.Speciality);
                    break;
                case "ExpectedSalary":
                    jobRequests = jobRequests.OrderBy(x => x.ExpectedSalary);
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
                Customer = _data.Customers.Find(data.CustomerId),
                Status = StatusType.InReview,
                Speciality = data.Speciality,
                ExpectedSalary = data.ExpectedSalary,
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
            if (data.CustomerId != null)
            {
                jobRequest.Customer = _data.Customers.Find(data.CustomerId);
            }
            if (data.Speciality != null)
            {
                jobRequest.Speciality = data.Speciality;
            }
            if (data.ExpectedSalary != null)
            {
                jobRequest.ExpectedSalary = ((int)(data.ExpectedSalary != null ? data.ExpectedSalary : 6000));
            }

            StatusType status;
            Enum.TryParse(data.Status, out status);

            if (data.Status != null)
            {
                jobRequest.Status = status;
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
