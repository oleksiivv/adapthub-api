using adapthub_api.ViewModels.Vacancy;

namespace adapthub_api.ViewModels.JobRequest
{
    public class ListVacancies
    {
        public List<VacancyViewModel> Data { get; set; }
        public int TotalCount { get; set; }
    }
}
