using System.ComponentModel.DataAnnotations;

namespace adapthub_api.ViewModels.JobRequest
{
    public class CreateJobRequestViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Data { get; set; }
    }
}
