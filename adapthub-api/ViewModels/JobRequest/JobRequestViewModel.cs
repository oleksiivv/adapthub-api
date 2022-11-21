using adapthub_api.Models;
using System.ComponentModel.DataAnnotations;

namespace adapthub_api.ViewModels.JobRequest
{
    public class JobRequestViewModel
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public string Speciality { get; set; }
        public int ExpectedSalary { get; set; }
        public string Status { get; set; }
    }
}
