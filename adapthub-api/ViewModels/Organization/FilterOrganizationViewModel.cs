using System.Diagnostics.CodeAnalysis;

namespace adapthub_api.ViewModels.Organization
{
    public class FilterOrganizationViewModel
    {
        [AllowNull]
        public string? Name { get; set; }

        [AllowNull]
        public string? Description { get; set; }

        [AllowNull]
        public string? SiteLink { get; set; }

        [AllowNull]
        public string? EDRPOU { get; set; }

        [AllowNull]
        public string? Email { get; set; }

        [AllowNull]
        public string? PasswordHash { get; set; }
    }
}
