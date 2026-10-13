using System.ComponentModel.DataAnnotations;

namespace MVCPro.DAL.Models
{
    public class Grade
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Range(0, 100)]
        public double Score { get; set; }

        [StringLength(250)]
        public string? Remarks { get; set; }

        public Course Course { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
