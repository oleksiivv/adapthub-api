using adapthub_api.ViewModels.Vacancy;

namespace adapthub_api.ViewModels.JobRequest
{
    public class ListOrganizations
    {
        public List<Models.Organization> Data { get; set; }
        public int TotalCount { get; set; }
    }
}
