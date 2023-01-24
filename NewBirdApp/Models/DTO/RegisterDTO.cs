using System;
using System.ComponentModel.DataAnnotations;

namespace NewBirdApp.Models.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 8)]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
    }
}
