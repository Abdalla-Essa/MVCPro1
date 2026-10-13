using MVCPro.BLL.CustomValidate;
using System.ComponentModel.DataAnnotations;
using MVCPro.DAL.Enums;

namespace MVCPro.BLL.ViewModels
{
    public class SessionVM
    {
        public int Id { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        [DateGreaterThan("StartDate")]
        public DateTime EndDate { get; set; }
        public string? Location { get; set; }
    }

    public class CreateSessionVM
    {
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = "";

        [Required(ErrorMessage = "Start time is required")]
        [DataType(DataType.DateTime)]
        public DateTime? StartTime { get; set; }

        [Required(ErrorMessage = "End time is required")]
        [DataType(DataType.DateTime)]
        public DateTime? EndTime { get; set; }

        public string? Location { get; set; }
    }


    public class UpdateSessionVM : CreateSessionVM
    {
        public int Id { get; set; }
    }
}
