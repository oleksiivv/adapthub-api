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
        public JobRequestViewModel Find(int id)
        {
            var jobRequest = _data.JobRequests.Find(id);

            _data.Entry(jobRequest).Reference("Customer").Load();

            return PrepareResponse(jobRequest);
        }

        public ListJobRequests List(FilterJobRequestViewModel filter, string sort, string direction, int from, int to)
        {
            if (!Enum.TryParse(filter.Status, out StatusType status))
                status = StatusType.Empty;

            var jobRequests = _data.JobRequests.Where(x => (x.Status == status || status == StatusType.Empty) && (x.Customer.Id == filter.CustomerId || filter.CustomerId == null) && (x.Speciality == filter.Speciality || filter.Speciality == null) && (x.ExpectedSalary <= filter.ExpectedSalary || filter.ExpectedSalary == null));

            switch (sort.ToLower())
            {
                case "status":
                    jobRequests = sort.ToLower().Equals("asc") ? jobRequests.OrderBy(x => x.Status) : jobRequests.OrderByDescending(x => x.Status);
                    break;
                case "customerid":
                    jobRequests = sort.ToLower().Equals("asc") ? jobRequests.OrderBy(x => x.Customer.Id) : jobRequests.OrderByDescending(x => x.Customer.Id);
                    break;
                case "speciality":
                    jobRequests = sort.ToLower().Equals("asc") ? jobRequests.OrderBy(x => x.Speciality) : jobRequests.OrderByDescending(x => x.Speciality);
                    break;
                case "expectedsalary":
                    jobRequests = sort.ToLower().Equals("asc") ? jobRequests.OrderBy(x => x.ExpectedSalary) : jobRequests.OrderByDescending(x => x.ExpectedSalary);
                    break;
                default:
                    jobRequests = sort.ToLower().Equals("asc") ? jobRequests.OrderBy(x => x.Id) : jobRequests.OrderByDescending(x => x.Id);
                    break;
            }

            jobRequests = jobRequests.Skip(from).Take(to - from);

            foreach (var jobRequest in jobRequests)
            {
                if (!_data.Entry(jobRequest).Reference("Customer").IsLoaded)
                {
                    _data.Entry(jobRequest).Reference("Customer").Load();
                }
            }

            var jobRequestViewModels = new List<JobRequestViewModel>();

            foreach (var jobRequest in jobRequests)
            {
                jobRequestViewModels.Add(PrepareResponse(jobRequest));
            }

            return new ListJobRequests {
                Data = jobRequestViewModels,
                TotalCount = _data.JobRequests.Count(),
            };
        }

        public JobRequestViewModel Create(CreateJobRequestViewModel data)
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

            return PrepareResponse(jobRequest);
        }

        public JobRequestViewModel Update(UpdateJobRequestViewModel data)
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

            _data.Entry(jobRequest).Reference("Customer").Load();

            return PrepareResponse(jobRequest);
        }

        public JobRequestViewModel Delete(int id)
        {
            var jobRequest = _data.JobRequests.Find(id);

            if (jobRequest == null)
            {
                throw new NotFoundException();
            }

            _data.JobRequests.Remove(jobRequest);

            _data.SaveChanges();

            return PrepareResponse(jobRequest);
        }

        private JobRequestViewModel PrepareResponse(JobRequest jobRequest)
        {
            return new JobRequestViewModel
            {
                Id = jobRequest.Id,
                Customer = jobRequest.Customer,
                Speciality = jobRequest.Speciality,
                ExpectedSalary = jobRequest.ExpectedSalary,
                Status = jobRequest.Status.ToString(),
            };
        }
    }
}
