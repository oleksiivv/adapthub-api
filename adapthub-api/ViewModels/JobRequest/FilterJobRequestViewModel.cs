using System.Diagnostics.CodeAnalysis;

namespace adapthub_api.ViewModels.JobRequest
{
    public class FilterJobRequestViewModel
    {
        [AllowNull]
        public string? CustomerId { get; set; }

        [AllowNull]
        public string? Status { get; set; }

        [AllowNull]
        public string? Speciality { get; set; }

        [AllowNull]
        public int? ExpectedSalary { get; set; }
    }
}
