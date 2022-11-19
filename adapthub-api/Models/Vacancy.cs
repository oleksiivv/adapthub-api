using adapthub_api.ViewModels;

namespace adapthub_api.Models
{
    public class Vacancy
    {
        public int Id { get; set; }

        public string Speciality { get; set; }
        public Organization Organization { get; set; }

        public StatusType _status { get; set; }
        public JobRequest? ChosenJobRequest { get; set; }
        public int Salary { get; set; }

        public int MinExperience { get; set; }

        public string Status
        {
            get
            {
                return _status.ToString();
            }
        }
    }
}
