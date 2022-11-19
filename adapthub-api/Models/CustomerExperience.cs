using Microsoft.AspNetCore.Identity;

namespace adapthub_api.Models
{
    public class CustomerExperience : IdentityUser
    {
        public string? PastJob { get; set; }

        public string? Education { get; set; }

        public string? Profession { get; set; }

        public string? Experience { get; set; }
    }
}
