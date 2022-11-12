using adapthub_api.Models;
using adapthub_api.Repositories.Interfaces;
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

            return vacancy;
        }

        public IEnumerable<Vacancy> List(FilterVacancyViewModel filter, string sort, int from, int to)
        {
            var vacancies = _data.Vacancies.Where(x => (x.Status == filter.Status || filter.Status == null) && (x.Organization.Id == filter.OrganizationId || filter.OrganizationId == null)).Skip(from).Take(to - from);

            _data.Entry(vacancies).Reference("Organization").Load();

            switch (sort)
            {
                case "Status":
                    vacancies = vacancies.OrderBy(x => x.Status);
                    break;
                case "OrganizationId":
                    vacancies = vacancies.OrderBy(x => x.Organization.Id);
                    break;
                case "Title":
                    vacancies = vacancies.OrderBy(x => x.Title);
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
                Status = "DRAFT",
                Title = data.Title,
                Data = data.Data,
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

            //TODO: refactor this logic
            if (data.OrganizationId != null)
            {
                vacancy.Organization = _data.Organizations.Find(data.OrganizationId);
            }

            if (data.Status != null)
            {
                vacancy.Status = data.Status;
            }

            if (data.Title != null)
            {
                vacancy.Title = data.Title;
            }

            if (data.Data != null)
            {
                vacancy.Data = data.Data;
            }

            if (data.JobRequestId != null)
            {
                vacancy.JobRequestId = data.JobRequestId;
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
