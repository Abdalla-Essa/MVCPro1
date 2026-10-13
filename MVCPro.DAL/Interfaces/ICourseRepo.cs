using MVCPro.DAL.Models;
using System.Collections.Generic;

namespace MVCPro.DAL.Interfaces
{
    public interface ICourseRepo : IGenericRepo<Course>
    {
        bool IsCourseNameUnique(string? name, int? excludeId = null);
        Course? GetCourseWithInstructorById(int id);
        IEnumerable<Course> GetAllWithInstructor();
    }
}
