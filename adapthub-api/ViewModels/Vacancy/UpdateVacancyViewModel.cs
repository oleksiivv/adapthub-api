using System.ComponentModel.DataAnnotations;

namespace adapthub_api.ViewModels.Vacancy
{
    public class UpdateVacancyViewModel
    {
        [Required]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Status { get; set; }

        public int OrganizationId { get; set; }

        public int JobRequestId { get; set; }

        public string Data { get; set; }
    }
}
