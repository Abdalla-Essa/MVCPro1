using System.ComponentModel.DataAnnotations;
using MVCPro.DAL.Enums;

namespace MVCPro.BLL.ViewModels
{
    public class GradeVM
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;
        public double Score { get; set; }
        public string? Remarks { get; set; }
    }

    public class CreateGradeVM
    {
        [Required(ErrorMessage = "User is required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Course is required")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Score is required")]
        [Range(0, 100, ErrorMessage = "Score must be between 0 and 100")]
        public double Score { get; set; }

        public string? Remarks { get; set; }
    }

    public class UpdateGradeVM : CreateGradeVM
    {
        public int Id { get; set; }
    }
}
