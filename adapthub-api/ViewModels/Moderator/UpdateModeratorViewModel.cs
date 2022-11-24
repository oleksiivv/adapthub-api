using System.Diagnostics.CodeAnalysis;

namespace adapthub_api.ViewModels.Moderator
{
    public class UpdateModeratorViewModel
    {
        [AllowNull]
        public int? Id { get; set; }

        [AllowNull]
        public string? Email { get; set; }

        [AllowNull]
        public string? Password { get; set; }

        [AllowNull]
        public string? FullName { get; set; }

        [AllowNull]
        public string? PhoneNumber { get; set; }
    }
}
