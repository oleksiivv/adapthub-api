namespace adapthub_api.Models
{
    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string SiteLink { get; set; }

        public User User { get; set; }
    }
}
