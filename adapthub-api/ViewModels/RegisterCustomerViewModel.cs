using adapthub_api.Models;
using adapthub_api.ViewModels.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace adapthub_api.ViewModels
{
    public class RegisterCustomerViewModel
    {

        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Password { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string ConfirmPassword { get; set; }

        [AllowNull]
        public string? PassportNumber { get; set; }

        [AllowNull]
        public string? IDCode { get; set; }

        [AllowNull]
        public string? Gender { get; set; }

        [AllowNull]
        public string? CurrentAddress { get; set; }

        [AllowNull]
        public string? PhoneNumber { get; set; }

        [AllowNull]
        public CustomerExperienceViewModel? Experience { get; set; }
    }
}
