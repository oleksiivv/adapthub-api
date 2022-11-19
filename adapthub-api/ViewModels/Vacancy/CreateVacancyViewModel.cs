using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace adapthub_api.ViewModels.Vacancy
{
    public class CreateVacancyViewModel
    {
        [Required]
        public string Speciality { get; set; }

        [Required]
        public int OrganizationId { get; set; }

        [AllowNull]
        public int? ChosenJobRequest { get; set; }

        [Required]
        public int Salary { get; set; }

        [Required]
        public int MinExperience { get; set; }
    }
}
