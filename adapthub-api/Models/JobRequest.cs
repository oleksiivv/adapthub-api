namespace adapthub_api.Models
{
    public class JobRequest
    {
        public int Id { get; set; }
        public User User { get; set; }

        public string Status { get; set; }

        //TODO: remove this field and provide more specific fields 
        public string Data { get; set; }
    }
}
