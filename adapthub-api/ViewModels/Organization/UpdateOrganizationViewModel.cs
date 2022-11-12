using System.ComponentModel.DataAnnotations;

namespace adapthub_api.ViewModels.Organization
{
    public class UpdateOrganizationViewModel
    {
        [Required]
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? SiteLink { get; set; }

        public string? UserId { get; set; }
    }
}
