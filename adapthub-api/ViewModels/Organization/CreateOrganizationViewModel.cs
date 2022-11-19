using System.ComponentModel.DataAnnotations;

namespace adapthub_api.ViewModels.Organization
{
    public class CreateOrganizationViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string SiteLink { get; set; }

        [Required]
        public string EDRPOU { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }
    }
}
