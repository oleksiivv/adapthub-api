using Microsoft.AspNetCore.Identity;

namespace adapthub_api.Models
{
    public class User : IdentityUser
    {
        //TODO: remove this field and provide more specific fields 
        public string? Data { get; set; }
    }
}
