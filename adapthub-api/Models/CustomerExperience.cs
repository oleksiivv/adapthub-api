using Microsoft.AspNetCore.Identity;

namespace adapthub_api.Models
{
    public class CustomerExperience
    {
        public int Id { get; set; }
        public string? PastJob { get; set; }

        public string? Education { get; set; }

        public string? Profession { get; set; }

        public string? Experience { get; set; }
    }
}
