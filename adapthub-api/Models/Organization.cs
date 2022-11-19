namespace adapthub_api.Models
{
    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string SiteLink { get; set; }

        public string EDRPOU { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }
    }
}
