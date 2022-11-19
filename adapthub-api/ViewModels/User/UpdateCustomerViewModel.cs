using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace adapthub_api.ViewModels.User
{
    public class UpdateCustomerViewModel
    {
        [AllowNull]
        public string? Id { get; set; }

        [AllowNull]
        public string? PassportNumber { get; set; }

        [Required]
        public string UserName { get; set; }

        [AllowNull]
        public string? IDCode { get; set; }

        [AllowNull]
        public string? Gender { get; set; }

        [AllowNull]
        public string? CurrentAddress { get; set; }

        [AllowNull]
        public string? PhoneNumber { get; set; }

        [AllowNull]
        public CustomerExperienceViewModel? Experience { get; set; }
    }
}
