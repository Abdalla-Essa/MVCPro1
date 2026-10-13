using System;
using MVCPro.DAL.Models;
using MVCPro.DAL.Enums;



using System.ComponentModel.DataAnnotations;
using MVCPro.BLL.CustomValidate;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCPro.BLL.ViewModels
{

    
    public class CourseVM 
    {
       
            public int Id { get; set; }

            [Required(ErrorMessage = "Name is required")]
            [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
            public string? Name { get; set; }

            [Required(ErrorMessage = "Description is required")]
            [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
            public string? Description { get; set; }

            [Required(ErrorMessage = "Category is required")]
            public Category? Category { get; set; }

            [Required(ErrorMessage = "Start Date is required")]
            [DataType(DataType.Date)]
            public DateTime? StartDate { get; set; }

            [Required(ErrorMessage = "End Date is required")]
            [DataType(DataType.Date)]
            [DateGreaterThan("StartDate", ErrorMessage = "End Date must be after Start Date")]
            public DateTime? EndDate { get; set; }

            [Required(ErrorMessage = "Instructor is required")]
            public int InstructorId { get; set; }

            public string? InstructorName { get; set; }
        }

    public class CreateCourseVM
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public Category? Category { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required")]
        [DataType(DataType.Date)]
        [DateGreaterThan("StartDate", ErrorMessage = "End Date must be after Start Date")]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Instructor is required")]
        public int InstructorId { get; set; }

        public List<SelectListItem>? InstructorList { get; set; }
    }

    public class UpdateCourseVM : CreateCourseVM
    {
        [Required]
        public int Id { get; set; }

    }

    public class CourseDetailsVM
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Category Category { get; set; }

        public int InstructorId { get; set; }

        public string InstructorName { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }


}
