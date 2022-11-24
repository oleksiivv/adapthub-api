using adapthub_api.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;

namespace adapthub_api.Models
{
    public class Customer : IdentityUser<int>
    {
        public override int Id { get; set; }
        public string PassportNumber { get; set; }

        public string IDCode { get; set; }

        public GenderType Gender { get; set; }

        [AllowNull]
        public HelpOption? HelpOption { get; set; }

        [AllowNull]
        public string? CurrentAddress { get; set; }

        [AllowNull]
        public string? PhoneNumber { get; set; }

        public CustomerExperience Experience { get; set; }
    }
}
