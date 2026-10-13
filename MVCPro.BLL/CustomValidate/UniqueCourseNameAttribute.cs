using System.ComponentModel.DataAnnotations;
using MVCPro.DAL.Interfaces;
using MVCPro.BLL.ViewModels;

namespace MVCPro.Validation
{
    

    public class UniqueCourseNameAttribute : ValidationAttribute
    {
      
        public bool AllowCurrent { get; set; } = false;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var courseRepo = (ICourseRepo)validationContext.GetService(typeof(ICourseRepo))!;
            var name = value as string;

            if (string.IsNullOrWhiteSpace(name))
                return ValidationResult.Success;

            int? excludeId = null;

            if (AllowCurrent && validationContext.ObjectInstance is UpdateCourseVM updateVM)
            {
                excludeId = updateVM.Id;
            }

            var isUnique = courseRepo.IsCourseNameUnique(name, excludeId);

            if (!isUnique)
            {
                return new ValidationResult("Course name must be unique");
            }

            return ValidationResult.Success;
        }
    }

}
