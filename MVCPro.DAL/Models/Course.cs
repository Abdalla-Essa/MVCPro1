using MVCPro.DAL.Enums;
using MVCPro.DAL.Models;
using System.ComponentModel.DataAnnotations;

public class Course
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string? Name { get; set; }

    [Required, StringLength(500)]
    public string? Description { get; set; }

    [Required]
    public Category Category { get; set; }

    [Required, DataType(DataType.Date)]
    public DateTime? StartDate { get; set; }

    [Required, DataType(DataType.Date)]
    public DateTime? EndDate { get; set; }

    [Required]
    public int InstructorId { get; set; }

    // Navigation
    public User? Instructor { get; set; }  // لازم علشان نقدر نجيب الاسم
    public ICollection<Session> Sessions { get; set; } = new List<Session>();
    public ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
