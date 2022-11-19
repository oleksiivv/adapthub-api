using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace adapthub_api.ViewModels.Vacancy
{
    public class UpdateVacancyViewModel
    {
        [AllowNull]
        public int? Id { get; set; }

        [AllowNull]
        public string? Speciality { get; set; }

        [AllowNull]
        public int? OrganizationId { get; set; }

        [AllowNull]
        public string? Status;

        [AllowNull]
        public int? ChosenJobRequest { get; set; }

        [AllowNull]
        public int? Salary { get; set; }

        [AllowNull]
        public int? MinExperience { get; set; }
    }
}
