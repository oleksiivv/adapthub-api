using adapthub_api.Models;
using adapthub_api.Repositories.Interfaces;
using adapthub_api.ViewModels;
using adapthub_api.ViewModels.Organization;
using adapthub_api.ViewModels.Vacancy;
using SendGrid.Helpers.Errors.Model;

namespace adapthub_api.Repositories
{
    public class VacancyRepository : IVacancyRepository
    {
        private DataContext _data;
        public VacancyRepository(DataContext data)
        {
            _data = data; 
        }

        public Vacancy Find(int id)
        {
            var vacancy = _data.Vacancies.Find(id);

            _data.Entry(vacancy).Reference("Organization").Load();
            _data.Entry(vacancy).Reference("ChosenJobRequest").Load();

            return vacancy;
        }

        public IEnumerable<Vacancy> List(FilterVacancyViewModel filter, string sort, string direction, int from, int to)
        {
            var vacancies = _data.Vacancies.Where(x => (x.Status.ToString() == filter.Status.ToString() || filter.Status == null) && (x.Organization.Id == filter.OrganizationId || filter.OrganizationId == null) && (x.Speciality == filter.Speciality || filter.Speciality == null) && (x.Salary >= filter.Salary || filter.Salary == null) && (x.MinExperience <= filter.MinExperience || filter.MinExperience == null)).Skip(from).Take(to - from);

            _data.Entry(vacancies).Reference("Organization").Load();

            switch (sort)
            {
                case "Status":
                    vacancies = vacancies.OrderBy(x => x.Status);
                    break;
                case "OrganizationId":
                    vacancies = vacancies.OrderBy(x => x.Organization.Id);
                    break;
                case "Speciality":
                    vacancies = vacancies.OrderBy(x => x.Speciality);
                    break;
                case "Salary":
                    vacancies = vacancies.OrderBy(x => x.Salary);
                    break;
                case "Experience":
                    vacancies = vacancies.OrderBy(x => x.MinExperience);
                    break;
                default:
                    vacancies = vacancies.OrderBy(x => x.Id);
                    break;
            }

            return vacancies;
        }

        public Vacancy Create(CreateVacancyViewModel data)
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

            return vacancy;
        }

        public Vacancy Update(UpdateVacancyViewModel data)
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

            return vacancy;
        }

        public Vacancy ChooseJobRequest(int id, int jobRequestId)
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

            return vacancy;
        }

        public Vacancy Delete(int id)
        {
            var vacancy = _data.Vacancies.Find(id);

            if (vacancy == null)
            {
                throw new NotFoundException();
            }

            _data.Vacancies.Remove(vacancy);

            _data.SaveChanges();

            return vacancy;
        }
    }
}
