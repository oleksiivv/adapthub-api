using System.ComponentModel.DataAnnotations;

namespace adapthub_api.ViewModels.Organization
{
    public class CreateOrganizationViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string SiteLink { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
