using System.Diagnostics.CodeAnalysis;

namespace adapthub_api.ViewModels.Moderator
{
    public class UpdateModeratorViewModel
    {
        public int Id { get; set; }

        [AllowNull]
        public string? Email { get; set; }

        [AllowNull]
        public string? Password { get; set; }
    }
}
