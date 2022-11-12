using System.ComponentModel.DataAnnotations;

namespace adapthub_api.ViewModels.Vacancy
{
    public class CreateVacancyViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public int OrganizationId { get; set; }

        [Required]
        public string Data { get; set; }
    }
}
