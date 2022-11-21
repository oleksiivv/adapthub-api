using adapthub_api.Models;
using adapthub_api.Repositories.Interfaces;
using adapthub_api.ViewModels;
using adapthub_api.ViewModels.JobRequest;
using adapthub_api.ViewModels.Organization;
using adapthub_api.ViewModels.Vacancy;
using SendGrid.Helpers.Errors.Model;
using System.Net;

namespace adapthub_api.Repositories
{
    public class VacancyRepository : IVacancyRepository
    {
        private DataContext _data;
        public VacancyRepository(DataContext data)
        {
            _data = data; 
        }

        public VacancyViewModel Find(int id)
        {
            var vacancy = _data.Vacancies.Find(id);

            _data.Entry(vacancy).Reference("Organization").Load();
            _data.Entry(vacancy).Reference("ChosenJobRequest").Load();

            return PrepareResponse(vacancy);
        }

        public IEnumerable<VacancyViewModel> List(FilterVacancyViewModel filter, string sort, string direction, int from, int to)
        {
            if(!Enum.TryParse(filter.Status, out StatusType status)) 
                status = StatusType.Empty;

            var vacancies = _data.Vacancies.Where(x => (x.Status == status || status == StatusType.Empty) && (x.Organization.Id == filter.OrganizationId || filter.OrganizationId == null) && (x.Speciality == filter.Speciality || filter.Speciality == null) && (x.Salary >= filter.Salary || filter.Salary == null) && (x.MinExperience <= filter.MinExperience || filter.MinExperience == null));

            switch (sort.ToLower())
            {
                case "status":
                    vacancies = sort.ToLower().Equals("asc") ? vacancies.OrderBy(x => x.Status) : vacancies.OrderByDescending(x => x.Status);
                    break;
                case "organizationid":
                    vacancies = sort.ToLower().Equals("asc") ? vacancies.OrderBy(x => x.Organization.Id) : vacancies.OrderByDescending(x => x.Organization.Id);
                    break;
                case "speciality":
                    vacancies = sort.ToLower().Equals("asc") ? vacancies.OrderBy(x => x.Speciality) : vacancies.OrderByDescending(x => x.Speciality);
                    break;
                case "salary":
                    vacancies = sort.ToLower().Equals("asc") ? vacancies.OrderBy(x => x.Salary) : vacancies.OrderByDescending(x => x.Salary);
                    break;
                case "experience":
                    vacancies = sort.ToLower().Equals("asc") ? vacancies.OrderBy(x => x.MinExperience) : vacancies.OrderByDescending(x => x.MinExperience);
                    break;
                default:
                    vacancies = sort.ToLower().Equals("asc") ? vacancies.OrderBy(x => x.Id) : vacancies.OrderByDescending(x => x.Id);
                    break;
            }

            vacancies = vacancies.Skip(from).Take(to - from);

            foreach (var vacancy in vacancies)
            {
                if (!_data.Entry(vacancy).Reference("ChosenJobRequest").IsLoaded)
                {
                    _data.Entry(vacancy).Reference("ChosenJobRequest").Load();
                }
                if (!_data.Entry(vacancy).Reference("Organization").IsLoaded)
                {
                    _data.Entry(vacancy).Reference("Organization").Load();
                }
            }

            var vacancyViewModels = new List<VacancyViewModel>();

            foreach (Vacancy vacancy in vacancies)
            {
                vacancyViewModels.Add(PrepareResponse(vacancy));
            }

            return vacancyViewModels;
        }

        public VacancyViewModel Create(CreateVacancyViewModel data)
        {
            var vacancy = new Vacancy
            {
                Organization = _data.Organizations.Find(data.OrganizationId),
                Status = StatusType.InReview,
                Speciality = data.Speciality,
                Salary = data.Salary,
                MinExperience = data.MinExperience,
            };

            _data.Vacancies.Add(vacancy);
            _data.SaveChanges();

            return PrepareResponse(vacancy);
        }

        public VacancyViewModel Update(UpdateVacancyViewModel data)
        {
            var vacancy = _data.Vacancies.Find(data.Id);

            if(vacancy == null)
            {
                throw new NotFoundException();
            }

            StatusType status;

            Enum.TryParse(data.Status, out status);

            //TODO: refactor this logic
            if (data.Status != null)
            {
                vacancy.Status = status;
            }

            if (data.Speciality != null)
            {
                vacancy.Speciality = data.Speciality;
            }

            if (data.Salary != null)
            {
                vacancy.Salary = (int)data.Salary;
            }

            if (data.MinExperience != null)
            {
                vacancy.MinExperience = (int)data.MinExperience;
            }

            _data.Update(vacancy);

            _data.SaveChanges();

            _data.Entry(vacancy).Reference("ChosenJobRequest").Load();
            _data.Entry(vacancy).Reference("Organization").Load();

            return PrepareResponse(vacancy);
        }

        public VacancyViewModel ChooseJobRequest(int id, int jobRequestId)
        {
            var vacancy = _data.Vacancies.Find(id);

            if (vacancy == null)
            {
                throw new NotFoundException();
            }

            if (jobRequestId != null)
            {
                vacancy.ChosenJobRequest = _data.JobRequests.Find(jobRequestId);
                vacancy.Status = StatusType.Past;
            }

            _data.Update(vacancy);

            _data.SaveChanges();

            _data.Entry(vacancy).Reference("ChosenJobRequest").Load();
            _data.Entry(vacancy).Reference("Organization").Load();

            return PrepareResponse(vacancy);
        }

        public VacancyViewModel Delete(int id)
        {
            var vacancy = _data.Vacancies.Find(id);

            if (vacancy == null)
            {
                throw new NotFoundException();
            }

            _data.Vacancies.Remove(vacancy);

            _data.SaveChanges();

            return PrepareResponse(vacancy);
        }

        private VacancyViewModel PrepareResponse(Vacancy vacancy)
        {
            return new VacancyViewModel
            {
                Id = vacancy.Id,
                Speciality = vacancy.Speciality,
                Organization = vacancy.Organization,
                ChosenJobRequest = vacancy.ChosenJobRequest,
                Salary = vacancy.Salary,
                MinExperience = vacancy.MinExperience,
                Status = vacancy.Status.ToString(),
            };
        }
    }
}
