using adapthub_api.ViewModels;
using System.Net.NetworkInformation;

namespace adapthub_api.Models
{
    public class JobRequest
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public StatusType _status { get; set; }
        public string Speciality { get; set; }
        public int ExpectedSalary { get; set; }

        public string Status
        {
            get
            {
                return _status.ToString();
            }
        }
    }
}
