namespace adapthub_api.ViewModels.JobRequest
{
    public class ListJobRequests
    {
        public List<JobRequestViewModel> Data { get; set; }
        public int TotalCount { get; set; }
    }
}
