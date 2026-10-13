using MVCPro.BLL.Helpers;
using MVCPro.BLL.ViewModels;
using MVCPro.DAL.Models;
using MVCPro.DAL.Enums;

using System.Collections.Generic;

namespace MVCPro.BLL.Services.Interfaces
{
    public interface ICourseService
    {
        PaginatedList<CourseVM> GetAllPaged(string? name, Category? category, int pageIndex, int pageSize);
        CourseDetailsVM GetCourseDetailsVM(int id);
        UpdateCourseVM? GetCourseForUpdate(int id);
        void Create(CreateCourseVM vm);
        void Update(UpdateCourseVM vm);
        void Delete(int id);
        List<User> GetAllInstructorsForDropdown(); // <-- للdropdown
        bool IsCourseNameExists(string name, int? excludeId = null);
    }
}
