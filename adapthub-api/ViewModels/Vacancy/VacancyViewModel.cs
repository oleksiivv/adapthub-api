using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using adapthub_api.Models;

namespace adapthub_api.ViewModels.Vacancy
{
    public class VacancyViewModel
    {
        public int Id { get; set; }

        public string Speciality { get; set; }
        public Models.Organization Organization { get; set; }

        public Models.JobRequest? ChosenJobRequest { get; set; }
        public int Salary { get; set; }

        public int MinExperience { get; set; }

        public string Status { get; set; }
    }
}
