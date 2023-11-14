using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using adapthub_api.Models;
using adapthub_api.Repositories.Interfaces;
using adapthub_api.ViewModels;
using adapthub_api.ViewModels.JobRequest;
using SendGrid.Helpers.Errors.Model;

namespace adapthub_api.Repositories
{
    public class JobRequestRepository : IJobRequestRepository
    {
        private readonly DataContext _data;

        public JobRequestRepository(DataContext data)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public JobRequestViewModel Find(int id)
        {
            var jobRequest = _data.JobRequests
                .Include(j => j.Customer)
                    .ThenInclude(c => c.Experience)
                .FirstOrDefault(j => j.Id == id);

            if (jobRequest == null)
            {
                throw new NotFoundException();
            }

            return PrepareResponse(jobRequest);
        }

        public ListJobRequests List(FilterJobRequestViewModel filter, string sort, string direction, int from, int to)
        {
            if (!Enum.TryParse(filter.Status, out StatusType status))
                status = StatusType.Empty;

            var jobRequestsQuery = _data.JobRequests
                .Include(j => j.Customer)
                    .ThenInclude(c => c.Experience)
                .Where(x => (x.Status == status || status == StatusType.Empty) &&
                            (x.Customer.Id == filter.CustomerId || filter.CustomerId == null) &&
                            (string.IsNullOrEmpty(filter.Speciality) || x.Speciality.ToLower().Contains(filter.Speciality.ToLower())) &&
                            (x.ExpectedSalary <= filter.ExpectedSalary || filter.ExpectedSalary == null));

            jobRequestsQuery = sort.ToLower() switch
            {
                "status" => direction.ToLower() == "asc" ? jobRequestsQuery.OrderBy(x => x.Status) : jobRequestsQuery.OrderByDescending(x => x.Status),
                "customerid" => direction.ToLower() == "asc" ? jobRequestsQuery.OrderBy(x => x.Customer.Id) : jobRequestsQuery.OrderByDescending(x => x.Customer.Id),
                "speciality" => direction.ToLower() == "asc" ? jobRequestsQuery.OrderBy(x => x.Speciality) : jobRequestsQuery.OrderByDescending(x => x.Speciality),
                "expectedsalary" => direction.ToLower() == "asc" ? jobRequestsQuery.OrderBy(x => x.ExpectedSalary) : jobRequestsQuery.OrderByDescending(x => x.ExpectedSalary),
                _ => direction.ToLower() == "asc" ? jobRequestsQuery.OrderBy(x => x.Id) : jobRequestsQuery.OrderByDescending(x => x.Id),
            };

            var jobRequests = jobRequestsQuery.Skip(from).Take(to - from).ToList();

            return new ListJobRequests
            {
                Data = jobRequests.Select(PrepareResponse).ToList(),
                TotalCount = _data.JobRequests.Count(),
            };
        }

        public JobRequestViewModel Create(CreateJobRequestViewModel data)
        {
            var customer = _data.Customers.Find(data.CustomerId);
            if (customer == null)
            {
                throw new NotFoundException("Customer not found");
            }

            var jobRequest = new JobRequest
            {
                Customer = customer,
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
            var jobRequest = _data.JobRequests
                .Include(j => j.Customer)
                .FirstOrDefault(j => j.Id == data.Id);

            if (jobRequest == null)
            {
                throw new NotFoundException();
            }

            UpdateJobRequestProperties(data, jobRequest);

            _data.Update(jobRequest);
            _data.SaveChanges();

            return PrepareResponse(jobRequest);
        }

        public JobRequestViewModel Delete(int id)
        {
            var jobRequest = _data.JobRequests
                .Include(j => j.Customer)
                .FirstOrDefault(j => j.Id == id);

            if (jobRequest == null)
            {
                throw new NotFoundException();
            }

            _data.JobRequests.Remove(jobRequest);
            _data.SaveChanges();

            return PrepareResponse(jobRequest);
        }

        private void UpdateJobRequestProperties(UpdateJobRequestViewModel data, JobRequest jobRequest)
        {
            if (data.CustomerId != null)
            {
                var customer = _data.Customers.Find(data.CustomerId);
                if (customer == null)
                {
                    throw new NotFoundException("Customer not found");
                }
                jobRequest.Customer = customer;
            }

            if (data.Speciality != null)
            {
                jobRequest.Speciality = data.Speciality;
            }

            if (data.ExpectedSalary != null)
            {
                jobRequest.ExpectedSalary = (int)(data.ExpectedSalary ?? 6000);
            }

            if (data.Status != null)
            {
                Enum.TryParse(data.Status, out StatusType status);
                jobRequest.Status = status;
            }
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
