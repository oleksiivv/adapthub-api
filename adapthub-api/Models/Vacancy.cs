namespace adapthub_api.Models
{
    public class Vacancy
    {
        public int Id { get; set; }

        public String Title { get; set; }
        public Organization Organization { get; set; }
        public string Status { get; set; }
        public int? JobRequestId { get; set; }

        //TODO: remove this field and provide more specific fields 
        public string Data { get; set; }
    }
}
