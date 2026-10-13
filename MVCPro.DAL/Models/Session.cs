using System.ComponentModel.DataAnnotations;

namespace MVCPro.DAL.Models
{
    public class Session
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; } = string.Empty; 

        [Required]
        public int CourseId { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime? StartTime { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime? EndTime { get; set; }

        [Required, StringLength(100)]
        public string? Location { get; set; } = string.Empty;

        public Course? Course { get; set; }
    }
}
