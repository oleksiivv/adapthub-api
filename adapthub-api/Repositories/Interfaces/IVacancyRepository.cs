using adapthub_api.Models;
using adapthub_api.ViewModels.Organization;
using adapthub_api.ViewModels.Vacancy;
using SendGrid.Helpers.Errors.Model;
using System.Xml.Linq;

namespace adapthub_api.Repositories.Interfaces
{
    public interface IVacancyRepository
    {
        public Vacancy Find(int id);

        public IEnumerable<Vacancy> List(FilterVacancyViewModel filter, string sort, int from, int to);

        public Vacancy Create(CreateVacancyViewModel data);
        public Vacancy Update(UpdateVacancyViewModel data);

        public Vacancy Delete(int id);
    }
}
