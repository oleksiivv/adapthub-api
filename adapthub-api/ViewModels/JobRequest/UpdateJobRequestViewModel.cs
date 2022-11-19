using adapthub_api.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace adapthub_api.ViewModels.JobRequest
{
    public class UpdateJobRequestViewModel
    {
        [AllowNull]
        public int? Id { get; set; }

        [AllowNull]
        public int? CustomerId { get; set; }

        [AllowNull]
        public string? Status { get; set; }

        [AllowNull]
        public string? Speciality { get; set; }

        [AllowNull]
        public int? ExpectedSalary { get; set; }
    }
}
