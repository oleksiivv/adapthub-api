﻿namespace adapthub_api.Models
{
    public class Moderator
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string PhoneNumber { get; set; }
    }
}
