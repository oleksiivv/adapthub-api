using adapthub_api.Models;
using adapthub_api.ViewModels.Organization;
using adapthub_api.ViewModels.Vacancy;
using SendGrid.Helpers.Errors.Model;
using System.Xml.Linq;

namespace adapthub_api.Repositories.Interfaces
{
    public interface IVacancyRepository
    {
        public VacancyViewModel Find(int id);

        public IEnumerable<VacancyViewModel> List(FilterVacancyViewModel filter, string sort, string direction, int from, int to);

        public VacancyViewModel Create(CreateVacancyViewModel data);
        public VacancyViewModel Update(UpdateVacancyViewModel data);

        public VacancyViewModel Delete(int id);

        public VacancyViewModel ChooseJobRequest(int id, int jobRequestId);
    }
}
