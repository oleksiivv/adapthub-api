using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;

namespace adapthub_api.ViewModels.User
{
    public class CustomerExperienceViewModel : IdentityUser
    {
        [AllowNull]
        public string? PastJob { get; set; }

        [AllowNull]
        public string? Education { get; set; }

        [AllowNull]
        public string? Profession { get; set; }

        [AllowNull]
        public string? Experience { get; set; }
    }
}
