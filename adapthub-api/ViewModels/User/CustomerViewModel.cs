using adapthub_api.Models;
using Microsoft.AspNetCore.Identity;

namespace adapthub_api.ViewModels.User
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        public virtual string UserName { get; set; }

        public string NormalizedUserName { get; set; }

        public string Email { get; set; }

        public string NormalizedEmail { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string PassportNumber { get; set; }

        public string IDCode { get; set; }

        public string Gender { get; set; }

        public string HelpOption { get; set; }

        public string CurrentAddress { get; set; }

        public string PhoneNumber { get; set; }

        public CustomerExperience Experience { get; set; }
    }
}
