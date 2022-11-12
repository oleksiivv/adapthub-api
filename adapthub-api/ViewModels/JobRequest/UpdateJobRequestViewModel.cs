using System.ComponentModel.DataAnnotations;

namespace adapthub_api.ViewModels.JobRequest
{
    public class UpdateJobRequestViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string Data { get; set; }
    }
}
