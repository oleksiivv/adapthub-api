using System.Diagnostics.CodeAnalysis;

namespace adapthub_api.ViewModels.Organization
{
    public class FilterVacancyViewModel
    {
        [AllowNull]
        public string? Speciality { get; set; }

        [AllowNull]
        public int? OrganizationId { get; set; }

        [AllowNull]
        public StatusType? Status;

        [AllowNull]
        public int? ChosenJobRequest { get; set; }

        [AllowNull]
        public int? Salary { get; set; }

        [AllowNull]
        public int? MinExperience { get; set; }
    }
}
