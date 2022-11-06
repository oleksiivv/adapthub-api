using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace adapthub_api.ViewModels.User
{
    public class UpdateUserViewModel
    {
        [AllowNull]
        [StringLength(1000, MinimumLength = 0)]
        public string Data { get; set; }
    }
}
