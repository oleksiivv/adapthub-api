using adapthub_api.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace adapthub_api.Models
{
    public class Customer : IdentityUser<int>
    {
        public override int Id { get; set; }
        public string PassportNumber { get; set; }

        public string IDCode { get; set; }

        public GenderType Gender { get; set; }

        public string CurrentAddress { get; set; }

        public string PhoneNumber { get; set; }

        public CustomerExperience Experience { get; set; }
    }
}
