using adapthub_api.ViewModels;

namespace adapthub_api.Models
{
    public class JobRequest
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public StatusType Status { get; set; }
        public string Speciality { get; set; }
        public int ExpectedSalary { get; set; }
    }
}
