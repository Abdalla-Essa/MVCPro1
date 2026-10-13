using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCPro.DAL.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string FName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string LName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = string.Empty;

        public DateTime JoinedAt { get; set; }

        public ICollection<Grade>? Grades { get; set; }

        public string FullName => $"{FName} {LName}";
    }
}
