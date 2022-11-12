using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace adapthub_api.ViewModels.User
{
    public class UpdateUserViewModel
    {
        [Required]
        public string Id { get; set; }

        public string? Data { get; set; }
    }
}
