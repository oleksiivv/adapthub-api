﻿using System.ComponentModel.DataAnnotations;

namespace adapthub_api.ViewModels.JobRequest
{
    public class CreateJobRequestViewModel
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public string Speciality { get; set; }

        [Required]
        public int ExpectedSalary { get; set; }
    }
}
